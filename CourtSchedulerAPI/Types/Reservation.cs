namespace CourtSchedulerAPI.Types
{
    public class Reservation
    {
        public int ReservationId { get;set; }
        public IList<Player> Players { get; set; } = new List<Player>();
        public Court Court { get; set; } = new Court();
        public DateTime? ScheduledTime { get; set; }
    }

    //just for database access
    public class FlatReservation : Reservation
    {
        public int PlayerId { get; set; } 
        public int CourtId { get; set; } 
    }
}
