namespace CourtSchedulerAPI.Types
{
    public class Reservation
    {
        public int ID { get;set; }
        public int PlayerId { get; set; }
        public int CourtId { get; set; }
        public DateTime? ScheduledTime { get; set; }
    }
}
