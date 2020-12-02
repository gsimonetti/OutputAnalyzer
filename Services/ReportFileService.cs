using OutputAnalyzer.Analysis.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OutputAnalyzer.Services
{
    public static class ReportFileService
    {
        public static async Task CreateReportsFileAsync(string path, List<WritableAnalysisReport> reports)
        {
            using FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            using BufferedStream bs = new BufferedStream(fs);
            using StreamWriter sw = new StreamWriter(bs);

            await WriteFileHeader(sw);

            foreach (WritableAnalysisReport report in reports)
            {
                await WriteReport(sw, report);
            }

            await WriteFileFooter(sw);

            sw.Close();
        }

        private static async Task WriteFileHeader(StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync("ANALYSIS REPORT");
        }

        private static async Task WriteReport(StreamWriter streamWriter, WritableAnalysisReport report)
        {
            await WriteReportHeader(streamWriter, report.Header);
            await WriteReportResults(streamWriter, report.Results);
            await WriteReportFooter(streamWriter, report.Footer);
        }

        private static async Task WriteReportHeader(StreamWriter streamWriter, List<string> header)
        {
            if (header != null)
            {
                await streamWriter.WriteLineAsync(string.Empty);

                foreach (string line in header)
                {
                    await streamWriter.WriteLineAsync(line);
                }

                await streamWriter.WriteLineAsync(string.Empty);
            }
        }

        private static async Task WriteReportResults(StreamWriter streamWriter, List<NumericResult> results)
        {
            const int DefaultNumberOfDots = 64;

            if (results != null)
            {
                int numberOfDots;
                foreach (NumericResult item in results)
                {
                    numberOfDots = DefaultNumberOfDots - (item.Name.Length + Math.Round(item.Value, 0).ToString().Length);
                    await streamWriter.WriteLineAsync($"{item.Name}{new string('.', numberOfDots)}{item.Value}");
                }
            }
        }

        private static async Task WriteReportFooter(StreamWriter streamWriter, List<string> footer)
        {
            if (footer != null)
            {
                await streamWriter.WriteLineAsync(string.Empty);

                foreach (string line in footer)
                {
                    await streamWriter.WriteLineAsync(line);
                }

                await streamWriter.WriteLineAsync(string.Empty);
            }
        }

        private static async Task WriteFileFooter(StreamWriter streamWriter)
        {
            await streamWriter.WriteLineAsync("END OF ANALYSIS REPORT");
        }
    }
}
