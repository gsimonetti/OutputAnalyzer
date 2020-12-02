using CenterSpace.NMath.Core;

namespace OutputAnalyzer.Analysis.Inputs
{
    public static class HypothesisTypeCreator
    {
        private const string TwoSided = "TwoSided";
        private const string Left = "Left";
        private const string Right = "Right";

        public static HypothesisType FromString(string input)
        {
            return input switch
            {
                TwoSided => HypothesisType.TwoSided,
                Left => HypothesisType.Left,
                Right => HypothesisType.Right,
                _ => HypothesisType.TwoSided,
            };
        }
    }
}
