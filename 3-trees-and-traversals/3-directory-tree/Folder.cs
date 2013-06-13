using System.IO;
using System.Linq;
using System;

class Folder
{
    public string Name { get; set; }

    public File[] Files { get; set; }

    public Folder[] Folders { get; set; }

    public int GetSize()
    {
        int size = 0;
        foreach (var file in this.Files)
            size += file.Size;
        foreach (var folder in this.Folders)
            size += folder.GetSize();
        return size;
    }

    static bool UseDirectory(string directory)
    {
        if (directory.ToLower().Contains("windows\\assembly"))
            // ignore the GAC pseudo-directory
            return false;
        if (directory.ToLower().Contains("windows\\winsxs"))
            // ignore the side-by-side executable cache
            return false;
        if (directory.ToLower().Contains("windows\\temp"))
            // ignore the temp directory
            return false;
        if (directory.ToLower().Contains("windows\\installer"))
            // ignore the installer cache
            return false;
        if (directory.ToLower().Contains("windows\\syswow"))
            // ignore the windows-on-windows binary directory
            return false;
        try
        {
            // check if accessible
            var dir = Directory.GetDirectories(directory);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }

    }
    public static Folder Create(string path)
    {
        var folder = new Folder();

        // GetDirectoryName returns the segment *before* the last directory separator
        // we want the segment *after* it

        folder.Name = Path.GetFileName(path);

        folder.Folders = Directory.GetDirectories(path).Where(UseDirectory).Select(dir => Create(dir)).ToArray();
        folder.Files = Directory.GetFiles(path).Select(file => File.Create(file)).ToArray();

        return folder;
    }

    public void Print(TextWriter tw, string indent = "")
    {
        tw.WriteLine(indent + this.Name + "\\" + "    " + this.GetSize() + " B");
        foreach (var file in this.Files)
            tw.WriteLine(indent + "    " + file.Name + "    " + file.Size + " B");
        foreach (var folder in this.Folders)
            folder.Print(tw, indent + "    ");
    }
}