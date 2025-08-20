namespace CourtSchedulerAPI.Types
{
    public class Reservation
    {
        public int ID { get;set; }
        public int PlayerID { get; set; }
        public int CourtID { get; set; }
        public DateTime? ScheduledTime { get; set; }
    }
}
