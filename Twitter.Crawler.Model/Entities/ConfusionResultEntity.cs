namespace Twitter.Crawler.Model.Entities
{
    public class ConfusionResultEntity
    {
        public bool ExpectedValue { get; set; }
        public bool? ActualResult { get; set; }
        public int DistanceValue { get; set; }

        public ConfusionResultEntity(bool expectedValue)
        {
            ExpectedValue = expectedValue;
        }
        public bool? IsFalsePositive()
        {
            if (ActualResult == null) return null;
            return ActualResult == true && !ExpectedValue;
        }

        public bool? IsFalseNegative()
        {
            if (ActualResult == null) return null;
            return ActualResult == false && ExpectedValue;
        }

        public bool? IsTruePositive()
        {
            if (ActualResult == null) return null;
            return ActualResult == true && ExpectedValue;
        }

        public bool? IsTrueNegative()
        {
            if (ActualResult == null) return null;
            return ActualResult == false && !ExpectedValue;

        }
    }
}