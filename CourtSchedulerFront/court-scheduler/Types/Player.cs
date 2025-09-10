namespace CourtSchedulerAPI.Types
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Rating { get; set; }

        public bool Equals(Player other)
        {
            return PlayerId == other.PlayerId;
        }

        public override int GetHashCode() => PlayerId.GetHashCode();
        public override string ToString() => $"{FirstName} {LastName}";
    }
}
