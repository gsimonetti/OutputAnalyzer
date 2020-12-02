using CenterSpace.NMath.Core;
using System.IO;
using System.Threading.Tasks;

namespace OutputAnalyzer.Services
{
    public static class FileReaderService
    {
        public static async Task<DataFrame> LoadDataFrameFromFile(string filePath)
        {
            DataFrame dataFrame = new DataFrame();
            dataFrame.AddColumn(new DFNumericColumn("Values"));

            using FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BufferedStream bs = new BufferedStream(fs);
            using StreamReader sr = new StreamReader(bs);

            string line;
            int lineNumber = 1;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                if (double.TryParse(line, out double number))
                {
                    dataFrame.AddRow(lineNumber, number);
                    lineNumber++;
                }
            }

            return dataFrame;
        }
    }
}
