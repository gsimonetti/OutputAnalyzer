using System.Collections.Generic;

namespace OutputAnalyzer.Analysis.Reports
{
    public static class TableFormatter
    {
        public static string BuildTableRow(List<int> columnPositions, List<string> columnData)
        {
            string response = string.Empty;

            if (columnData.Count == columnPositions.Count)
            {
                for (int i = 0; i < columnPositions.Count; i++)
                {
                    response += new string(' ', columnPositions[i] - response.Length);
                    response += columnData[i];
                }
            }

            return response;
        }
    }
}
