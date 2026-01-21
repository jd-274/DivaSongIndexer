using Microsoft.Win32;

namespace DivaSongIndexer;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void modsDirectoryBrowseButton_Click(object sender, EventArgs e)
    {
        string? selectedDirectoryPath = PromptUserForModsDirectoryPath();
        if (selectedDirectoryPath != null)
        {
            modsDirectoryTextBox.Text = selectedDirectoryPath;
        }
    }

    private async void createSongIndexButton_Click(object sender, EventArgs e)
    {
        // validate user-supplied mods directory path
        string modsDirectoryPath = modsDirectoryTextBox.Text;
        if (Directory.Exists(modsDirectoryPath) == false)
        {
            // if directory does not exist, prompt for path again
            string? selectedModsDirectoryPath = PromptUserForModsDirectoryPath();
            if (selectedModsDirectoryPath == null) 
            {
                return; // stop; do not create index
            }
            else
            {
                modsDirectoryPath = selectedModsDirectoryPath;
                modsDirectoryTextBox.Text = selectedModsDirectoryPath;
            }
        }

        // prompt and validate user-supplied output directory path
        string outputDirectoryPath = PromptUserForOutputDirectoryPath();
        if (outputDirectoryPath == null)
        {
            return; // stop; do not create index;
        }

        // show loading spinner
        loadingSpinnerPictureBox.Visible = true;

        string messageText, messageCaption;
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        MessageBoxIcon icon;
        try
        {
            // create index
            // note: use Task.Run to move work off of main thread (allows loading animation)
            string outputFilePath = await Task.Run(() => CreateSongIndex(modsDirectoryPath, outputDirectoryPath));

            messageText = $"Song index created in: {outputFilePath}";
            messageCaption = "Index Created";
            icon = MessageBoxIcon.Information;
        }
        catch (Exception ex)
        {
            messageText = ex.Message;
            messageCaption = "Error";
            icon = MessageBoxIcon.Error;
        }

        // hide loading spinner
        loadingSpinnerPictureBox.Visible = false;

        // show ending message
        MessageBox.Show(messageText, messageCaption, buttons, icon);
    }

    private string? PromptUserForModsDirectoryPath()
    {
        string? dialogInitialDirectory = InferGameInstallDirectory();
        string dialogTitle = "Select Mods Folder";

        // show dialog and return result
        return PromptUserForDirectoryPath(dialogInitialDirectory, dialogTitle);
    }

    private string? InferGameInstallDirectory()
    {
        string? inferredDirectory = null;

        // attempt to use game uninstall registry entry
        using (RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 1761390"))
        {
            var divaInstallLocation = key?.GetValue("InstallLocation") as string;
            if (divaInstallLocation != null)
            {
                inferredDirectory = divaInstallLocation;
            }
        }

        // if unable to infer from above
        if (inferredDirectory == null)
        {
            // attempt to use steam install path registry entry + default subdirectory names
            using (RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"Software\WOW6432Node\Valve\Steam"))
            {
                var steamInstallLocation = key?.GetValue("InstallPath") as string;
                if (steamInstallLocation != null)
                {
                    inferredDirectory = Path.Join(steamInstallLocation, "steamapps", "common",
                        "Hatsune Miku Project DIVA Mega Mix Plus");
                }
            }

        }

        // if inferredDirectory is not valid, return null
        if (inferredDirectory == null || Directory.Exists(inferredDirectory) == false)
        {
            inferredDirectory = null;
        }

        return inferredDirectory;
    }

    private string? PromptUserForOutputDirectoryPath()
    {
        string dialogInitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string dialogTitle = "Select Output Folder";

        // show dialog and return result
        return PromptUserForDirectoryPath(dialogInitialDirectory, dialogTitle);
    }

    private string? PromptUserForDirectoryPath(string? dialogInitialDirectory, string? dialogTitle)
    {
        // configure dialog

        // set initial directory
        folderBrowserDialog.InitialDirectory = dialogInitialDirectory ?? "";

        // set description/title
        if (dialogTitle == null)
        {
            folderBrowserDialog.Description = "";
            folderBrowserDialog.UseDescriptionForTitle = false;
        }
        else
        {
            folderBrowserDialog.Description = dialogTitle;
            folderBrowserDialog.UseDescriptionForTitle = true;
        }

        // reset selected path (regardless if necessary)
        folderBrowserDialog.SelectedPath = "";

        // show dialog
        string? selectedPath = null;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            selectedPath = folderBrowserDialog.SelectedPath;
        }

        return selectedPath;
    }

    private async Task<string> CreateSongIndex(string modsDirectoryPath, string outputDirectoryPath)
    {
        var pvData = await PVDataExtractor.GetAllPVDataFromModsDirectory(modsDirectoryPath);
        string outputFilePath = Path.Join(outputDirectoryPath, GenerateOutputFileName());
        await CsvPVDataExporter.OutputPVDataToFile(outputFilePath, pvData);

        return outputFilePath;
    }

    private string GenerateOutputFileName()
    {
        return $"DivaSongIndexer_export_{DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss")}";
    }
}
