namespace CourtSchedulerAPI.Types
{
    public class ReservationInfo
    {
        public int ReservationInfoId { get;set; }
        public int ReservationId { get;set; }
        public DateTime? ScheduledTime { get; set; }
    }
}