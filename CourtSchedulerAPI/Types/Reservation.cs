namespace CourtSchedulerAPI.Types
{
    public class Reservation
    {
        public int ReservationId { get;set; }
        public int PlayerId { get; set; }
        public int CourtId { get; set; }
        public DateTime? ScheduledTime { get; set; }
    }
}
