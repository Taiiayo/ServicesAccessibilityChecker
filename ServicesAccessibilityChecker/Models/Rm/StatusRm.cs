namespace ServicesAccessibilityChecker.Models.Rm
{
    public class StatusRm
    {
        public string ServiceName { get; set; }
        public bool IsAvailable { get; set; }
        public double ResponseDuration { get; set; }
        public int LastHourErrors { get; set; }
        public int LastDayErrors { get; set; }
        public double LastHourResponseDeviationTime { get; set; }
        public double LastDayResponseDeviationTime { get; set; }
        public double LastHourAvgResponseDuration { get; set; }
        public double LastDayAvgResponseDuration { get; set; }
        public double LastDayMaxResponseDuration { get; set; }
        public double LastHourMaxResponseDuration { get; set; }
        public double AvgResponseDuration { get; set; }
        public double BestResponseTime { get; set; }
    }
}
