using CenterSpace.NMath.Core;

namespace OutputAnalyzer.Models
{
    public class FileDataFrame
    {
        public string FileName { get; set; }
        public DataFrame DataFrame { get; set; }

        public FileDataFrame(string fileName, DataFrame dataFrame)
        {
            FileName = fileName;
            DataFrame = dataFrame;
        }
    }
}
