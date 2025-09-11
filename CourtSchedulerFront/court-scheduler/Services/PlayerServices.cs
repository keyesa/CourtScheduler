namespace CourtScheduler.Services
{
    public class PlayerServices
    {
        public static async Task<IList<Player>> GetPlayers() {
            return await API.Get<IList<Player>>(new Uri(""))
        }
    }
}
