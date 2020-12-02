namespace OutputAnalyzer.Analysis.Reports
{
    public class NumericResult
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public NumericResult(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
