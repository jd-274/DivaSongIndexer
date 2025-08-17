namespace DivaSongIndexer;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void modDirectoryBrowseButton_Click(object sender, EventArgs e)
    {
        string? selectedDirectoryPath = PromptUserForDirectoryPath();
        if (selectedDirectoryPath != null)
        {
            modsDirectoryTextBox.Text = selectedDirectoryPath;
        }
    }

    private void outputDirectoryBrowseButton_Click(object sender, EventArgs e)
    {
        string? selectedDirectoryPath = PromptUserForDirectoryPath();
        if (selectedDirectoryPath != null)
        {
            outputDirectoryTextBox.Text = selectedDirectoryPath;
        }
    }

    private string? PromptUserForDirectoryPath()
    {
        string? selectedPath = null;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
            selectedPath = folderBrowserDialog.SelectedPath;
        }

        return selectedPath;
    }

    private async void createSongIndexButton_Click(object sender, EventArgs e)
    {
        // show loading spinner
        loadingSpinnerPictureBox.Visible = true;

        string messageText, messageCaption;
        MessageBoxButtons buttons = MessageBoxButtons.OK;
        MessageBoxIcon icon;
        try
        {
            // create index
            // Note: use Run to move work off of main thread (allows loading animation)
            string outputFilePath = await Task.Run(CreateSongIndex);

            messageText = $"Song index created in: {outputFilePath}";
            messageCaption = "Index Created";
            icon = MessageBoxIcon.Information;
        }
        catch (DirectoryNotFoundException ex)
        {
            messageText = ex.Message;
            messageCaption = "Invalid Directory";
            icon = MessageBoxIcon.Warning;
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

    private async Task<string> CreateSongIndex()
    {
        string modsDirectoryPath = modsDirectoryTextBox.Text;
        ValidateDirectoryPath(modsDirectoryPath, "Mods");
        string outputDirectoryPath = outputDirectoryTextBox.Text;
        ValidateDirectoryPath(outputDirectoryPath, "Output");

        var pvData = await PVDataExtractor.GetAllPVDataFromModsDirectory(modsDirectoryPath);
        string outputFilePath = Path.Join(outputDirectoryPath, GenerateOutputFileName());
        await CsvPVDataExporter.OutputPVDataToFile(outputFilePath, pvData);

        return outputFilePath;
    }

    private void ValidateDirectoryPath(string directoryPath, string directoryPathFriendlyName)
    {
        if (Directory.Exists(directoryPath) == false)
        {
            throw new DirectoryNotFoundException($"Path to the {directoryPathFriendlyName} directory is invalid. " +
                "Please enter a valid directory.");
        }
    }

    private string GenerateOutputFileName()
    {
        return $"DivaSongIndexer_export_{DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss")}";
    }
}
