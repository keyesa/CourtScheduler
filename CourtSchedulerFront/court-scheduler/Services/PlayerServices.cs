namespace CourtScheduler.Services
{
    public class PlayerServices
    {
        public static async Task<IList<Player>> GetPlayers() {
            //TODO dns stuff
            return await API.Get<IList<Player>>(new Uri("localhost:3030/players"))
        }

        public static async Task<IList<Player>> GetPlayers(int playerId) {
            return await API.Get<IList<Player>>(new Uri($"localhost:3030/players/{playerId}"))
        }
    }
}
