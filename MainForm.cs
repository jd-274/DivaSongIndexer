namespace DivaSongIndexer;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void testButton_Click(object sender, EventArgs e)
    {
        Test();
    }

    private async Task Test() // DELETEME
    {
        const string testModsRootDirectoryPath = "";
        const string testOutputDirectory = "";

        var pvData = await PVDataExtractor.GetAllPVDataFromModsDirectory(testModsRootDirectoryPath);
        await CsvPVDataExporter.OutputPVDataToFile(testOutputDirectory, pvData);
        MessageBox.Show("Job's done");
        this.Close();
    }
}
