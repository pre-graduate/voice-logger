
using System;
using System.IO.Compression;
using System.Windows.Forms;
using System.IO;
using static CodeLog.Classes.Alerts;

namespace CodeLog.Classes
{
    public class ZipFolder
    {
        public string ExportedFilename { get; set; }
        public string FolderDirectory { get; set; }
        public string InputDirectory { get; set; }

        public int NumberOfLogs => Directory.GetFiles(InputDirectory).Length;
        public int Size { get; set; }

        public bool FilesToCopy => NumberOfLogs != 0;

        public ZipFolder(string input)
        {
            InputDirectory = Directory.GetCurrentDirectory() + input;

            if (!Directory.Exists(InputDirectory))
            {
                Directory.CreateDirectory(InputDirectory);
            }
        }

        public bool ChooseDirectory()
        {
            var fileBrowser = new FolderBrowserDialog();
            var result = fileBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                FolderDirectory = Path.Combine(fileBrowser.SelectedPath, @"Logs.zip");
                return true;
            }

            return false;
        }

        public bool Create(string folder)
        {
            try
            {
                ZipFile.CreateFromDirectory(InputDirectory, folder);
                ExportedFilename = Path.GetFileName(folder);
            }
            catch(Exception)
            {
                ShowError("We encountered an error when writing the file", "Error!");
                return false;
            }

            return true;
        }

        public void Cleanup()
        {
            foreach (var filePath in Directory.GetFiles(InputDirectory))
            {
                Size += (int)new FileInfo(filePath).Length;
                File.Delete(filePath);
            }
        }

        public bool Create()
        {
            return InputDirectory == null || Create(UniqueFilename(FolderDirectory));
        }

        public long GetDirectorySize()
        {
           return Size / 1024 /1024;
        }

        private static string UniqueFilename(string filename)
        {
            var outputFilename = Path.ChangeExtension(filename, string.Empty).TrimEnd('.');

            if (File.Exists(filename))
            {
                outputFilename += " - ";
                outputFilename += Guid.NewGuid().ToString();
            }

            return Path.ChangeExtension(outputFilename, ".zip");
        }
    }
}
