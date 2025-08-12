using System.Text.RegularExpressions;
using Tomlyn;

namespace DivaSongIndexer;

internal class PVDataExtractor
{
    public static async Task<PVData[][]> GetAllPVDataFromModsDirectory(string modsRootDirectoryPath)
    {
        string[] modDirectoryPaths = Directory.GetDirectories(modsRootDirectoryPath);
        List<Task<PVData[]?>> getPVDataCollectionTasks = new();
        foreach (string path in modDirectoryPaths)
        {
            getPVDataCollectionTasks.Add(GetPVDataCollectionFromIndividualModDirectory(path));
        }

        await Task.WhenAll(getPVDataCollectionTasks);

        List<PVData[]> allPVDataCollections = new();
        foreach (var task in getPVDataCollectionTasks)
        {
            var individualPVDataCollection = await task;
            if (individualPVDataCollection != null)
            {
                allPVDataCollections.Add(individualPVDataCollection);
            }
        }

        if (allPVDataCollections.Count < 1)
        {
            throw new Exception($"Unable to find mod PV data in directory \"{modsRootDirectoryPath}\"");
        }

        return allPVDataCollections.ToArray();
    }

    private static async Task<PVData[]?> GetPVDataCollectionFromIndividualModDirectory(string modDirectoryPath)
    {
        PVData[]? pvDataCollection;
        try
        {
            string? modName = await ParseModNameFromConfigTOML(modDirectoryPath);
            pvDataCollection = await ParsePVDataCollectionFromModPVDB(modDirectoryPath, modName);
        }
        catch (Exception ex)
        {
            pvDataCollection = null; // some mods don't have mod_pv_dbs 
        }

        return pvDataCollection;
    }

    private static async Task<string?> ParseModNameFromConfigTOML(string modDirectoryPath)
    {
        string modTomlFilePath = Path.Join(modDirectoryPath, "config.toml");
        string rawConfigText = await File.ReadAllTextAsync(modTomlFilePath);
        var model = Toml.ToModel(rawConfigText);
        string? modName = model["name"].ToString();

        return modName;
    }

    private static async Task<PVData[]?> ParsePVDataCollectionFromModPVDB(string modDirectoryPath, string modName="")
    {
        string modPVDBFilePath = Path.Join(modDirectoryPath, "rom", "mod_pv_db.txt");
        string[] modPvDbLines;
        try
        {
            modPvDbLines = await File.ReadAllLinesAsync(modPVDBFilePath);
        }
        catch (Exception ex)
        {
            // Unable to find a mod_pv_db file in this folder, likely does not contain song mods
            return null;
        }

        List<PVData> pvCollection = new();
        PVData currentPV = new PVData { ID = 0 }; // initialize ID to 0
        foreach (string line in modPvDbLines)
        {
            int linePVID = ParsePVIDFromModPVDBLine(line);
            if (linePVID == 0)
                continue; // ID of 0 means no data, go to next line

            if (currentPV.ID != linePVID)
            {
                if (currentPV.ID != 0)
                {
                    // Add PV to collection
                    pvCollection.Add(currentPV);
                }

                // Create new PV
                currentPV = new();
                currentPV.ID = linePVID;
                currentPV.Source = modName;
            }

            // Parse property key. Collect value depending on key
            string propertyKey = ParsePVPropertyKeyFromModPVDBLine(line);
            string pvDifficultyCode;
            switch (propertyKey)
            {
                case "difficulty.easy.0.level":
                    pvDifficultyCode = ParsePVPropertyValueFromModPVDBLine(line);
                    currentPV.DifficultyEasy = ParseDifficultyFromCode(pvDifficultyCode);
                    break;
                case "difficulty.normal.0.level":
                    pvDifficultyCode = ParsePVPropertyValueFromModPVDBLine(line);
                    currentPV.DifficultyNormal = ParseDifficultyFromCode(pvDifficultyCode);
                    break;
                case "difficulty.hard.0.level":
                    pvDifficultyCode = ParsePVPropertyValueFromModPVDBLine(line);
                    currentPV.DifficultyHard = ParseDifficultyFromCode(pvDifficultyCode);
                    break;
                case "difficulty.extreme.0.level":
                    pvDifficultyCode = ParsePVPropertyValueFromModPVDBLine(line);
                    currentPV.DifficultyEX = ParseDifficultyFromCode(pvDifficultyCode);
                    break;
                case "difficulty.extreme.1.level":
                    pvDifficultyCode = ParsePVPropertyValueFromModPVDBLine(line);
                    currentPV.DifficultEXEX = ParseDifficultyFromCode(pvDifficultyCode);
                    break;
                case "song_name":
                    currentPV.Name = ParsePVPropertyValueFromModPVDBLine(line);
                    break;
                case "song_name_en":
                    currentPV.NameEN = ParsePVPropertyValueFromModPVDBLine(line);
                    break;
                case "songinfo.music":
                    currentPV.Artist = ParsePVPropertyValueFromModPVDBLine(line);
                    break;
                case "songinfo_en.music":
                    currentPV.ArtistEN = ParsePVPropertyValueFromModPVDBLine(line);
                    break;
            }
        }

        // Add "last" PV to collection
        if (currentPV.ID > 0)
            pvCollection.Add(currentPV);

        return pvCollection.ToArray();
    }


    private static int ParsePVIDFromModPVDBLine(string line)
    {
        int modPVID;
        try
        {
            Match match = Regex.Match(line, @"^pv_(\d+)");
            modPVID = Int32.Parse(match.Groups[1].Value);
        }
        catch (Exception ex)
        {
            // Unable to parse ID
            modPVID = 0;
        }

        return modPVID;
    }

    private static string ParsePVPropertyKeyFromModPVDBLine(string line)
    {
        var firstPeriodIndex = line.IndexOf('.');
        var equalSignIndex = line.IndexOf('=');
        string propertyKey = String.Empty;

        if (firstPeriodIndex > 0 && equalSignIndex > 0)
        {
            var substringLength = equalSignIndex - firstPeriodIndex - 1; // -1 to exclude equal sign
            propertyKey = line.Substring(firstPeriodIndex + 1, substringLength); // +1 to exclude first period
        }

        return propertyKey;
    }

    private static string ParsePVPropertyValueFromModPVDBLine(string line)
    {
        var equalSignIndex = line.IndexOf('=');
        string propertyValue = String.Empty;
        if (equalSignIndex > 0)
        {
            var substringLength = line.Length - equalSignIndex - 1; // -1 to prevent length being greater than end of str
            propertyValue = line.Substring(equalSignIndex + 1, substringLength); // +1 to exclude equal sign
        }

        return propertyValue;
    }

    private static decimal ParseDifficultyFromCode(string code)
    {
        decimal difficulty;
        try
        {
            Match match = Regex.Match(code, @"(\d+)_(\d+)$");
            var whole = Decimal.Parse(match.Groups[1].Value);
            difficulty = whole;

            var fraction = Decimal.Parse(match.Groups[2].Value);
            if (fraction == 5m)
            {
                difficulty += 0.5m;
            }
        }
        catch (Exception ex)
        {
            // Unable to parse difficulty
            difficulty = 0m;
        }

        return difficulty;
    }
}
