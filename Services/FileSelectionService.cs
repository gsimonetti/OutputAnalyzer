using Avalonia.Controls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OutputAnalyzer.Services
{
    public static class FileSelectionService
    {
        public static async Task<List<string>> OpenDataFileSelectionAsync(Window window)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Select .dat file",
                AllowMultiple = true,
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter()
                    {
                        Name = "Data file",
                        Extensions = new List<string>() { "dat" }
                    }
                }
            };

            string[] result = await dialog.ShowAsync(window);

            return result?.ToList();
        }

        public static async Task<string> OpenFolderSelectionAsync(Window window)
        {
            OpenFolderDialog dialog = new OpenFolderDialog();
            return await dialog.ShowAsync(window);
        }

        public static async Task<bool> IsDataFileValid(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            using FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            if (!fs.CanRead)
            {
                return false;
            }

            using BufferedStream bs = new BufferedStream(fs);
            using StreamReader sr = new StreamReader(bs);

            string line;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                if (!double.TryParse(line, out double number))
                {
                    sr.Close();
                    return false;
                }
            }

            sr.Close();
            return true;
        }
    }
}
