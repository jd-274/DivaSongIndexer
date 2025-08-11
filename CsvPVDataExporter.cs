using Csv;

namespace DivaSongIndexer;

internal class CsvPVDataExporter
{
    private static string[] _columnNames = { "ID", "Name (EN)", "Name (JP)", "Source", "Artist (EN)", "Artist (JP)",
        "Easy", "Normal", "Hard", "EX", "EX EX" };
    private static string fileExtension = ".csv";

    public static async Task OutputPVDataToFile(string fullOutputFilePath, PVData[][] data)
    {
        string[][] rows = BuildRowsFromPVData(data);
        string csvData = CsvWriter.WriteToText(_columnNames, rows, ',');
        await File.WriteAllTextAsync(fullOutputFilePath + fileExtension, csvData);
    }

    private static string[][] BuildRowsFromPVData(PVData[][] data)
    {
        List<string[]> rows = new();
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                List<string> row = new();
                PVData pvd = data[i][j];

                // Should match order in _columnNames
                row.Add(pvd.ID.ToString());
                row.Add(pvd.NameEN);
                row.Add(pvd.Name);
                row.Add(pvd.Source);
                row.Add(pvd.ArtistEN);
                row.Add(pvd.Artist);
                row.Add(pvd.DifficultyEasy.ToString());
                row.Add(pvd.DifficultyNormal.ToString());
                row.Add(pvd.DifficultyHard.ToString());
                row.Add(pvd.DifficultyEX.ToString());
                row.Add(pvd.DifficultEXEX.ToString());

                // Append row
                rows.Add(row.ToArray());
            }
        }

        return rows.ToArray();
    }
}
