using System.IO;

class File
{
    public static File Create(string path)
    {
        var file = new File();
        file.Name = Path.GetFileName(path);
        file.Size = (int)new System.IO.FileInfo(path).Length;
        return file;
    }

    public string Name { get; set; }

    public int Size { get; set; }
}