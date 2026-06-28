namespace Clicker.Helpers;

public class FileHelper(string mainPath)
{
    private const string _directoryName = "Sequences";

    private readonly string _directoryPath = Path.Combine(mainPath, _directoryName);

    public void CreateDirectory()
    {
        _ = Directory.CreateDirectory(_directoryPath);
    }

    public List<string> ReadProfilesNames()
    {
        List<string> result = [];
        DirectoryInfo dictionaryInfo = new(_directoryPath);
        foreach (FileInfo fileInfo in dictionaryInfo.GetFiles("*.json"))
        {
            result.Add(fileInfo.Name);
        }
        return result;
    }

    public void SaveProfile(string fileName, string content)
    {
        string path = Path.Combine(_directoryPath, $"{fileName}.json");
        File.WriteAllText(path, content);
    }

    public bool TryDeleteProfile(string fileName)
    {
        string path = Path.Combine(_directoryPath, $"{fileName}.json");
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }
}
