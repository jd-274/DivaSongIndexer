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
        loadingIconPictureBox.Visible = true;
        await CreateSongIndex();
        loadingIconPictureBox.Visible = false;
    }

    private async Task CreateSongIndex()
    {
        string modsDirectoryPath = modsDirectoryTextBox.Text;
        string outputDirectoryPath = outputDirectoryTextBox.Text;
        string outputFilePath = Path.Join(outputDirectoryPath, GenerateOutputFileName());

        var pvData = await PVDataExtractor.GetAllPVDataFromModsDirectory(modsDirectoryPath);
        await CsvPVDataExporter.OutputPVDataToFile(outputFilePath, pvData);
    }

    private string GenerateOutputFileName()
    {
        return $"DivaSongIndexer_export_{DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss")}";
    }
}
