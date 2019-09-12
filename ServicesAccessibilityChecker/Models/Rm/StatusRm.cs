namespace ServicesAccessibilityChecker.Models.Rm
{
    public class StatusRm
    {
        public string ServiceName { get; set; }
        public bool IsAvailable { get; set; }
        public double ResponseDuration { get; set; }
        public int LastHourErrors { get; set; }
        public int LastDayErrors { get; set; }
        public double MaxResponseDuration { get; set; }
    }
}
