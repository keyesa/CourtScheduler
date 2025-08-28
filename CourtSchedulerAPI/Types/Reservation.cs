namespace CourtSchedulerAPI.Types
{
    public class Reservation : FlatReservation
    {
        public IList<Player> Players { get; set; } = new List<Player>();
        public Court Court { get; set; } = new Court();
    }

    //just for database access
    public class FlatReservation
    {
        public int ReservationId { get;set; }
        public int PlayerId { get; set; }
        public int CourtId { get; set; } 
        public DateTime? ScheduledTime { get; set; }
    }
}
