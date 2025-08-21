using CourtSchedulerAPI.Types;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CourtSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly string _db = "";

        public PlayersController(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("COURT_SCHEDULER");
            if (connString != null) _db = connString;
        }

        [HttpGet]
        [Route("{playerID}")]
        public Player Get(int playerID)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    SELECT * FROM Players WHERE PlayerId = COALESCE(@playerID, PlayerId)
                    """;
                var player = conn.QueryFirst<Player>(sql, new { playerID }, commandType: CommandType.StoredProcedure);
                return player;
            }
        }

        [HttpGet]
        [Route("")]
        public IList<Player> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = "SELECT * FROM Players";
                var players = conn.Query<Player>(sql).ToList();
                return players;
            }
        }

        [HttpPost]
        [Route("")]
        public int Add(Player req)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    INSERT INTO Players (FirstName, LastName, Email, Rating)
                    VALUES (@FirstName, @LastName, @Email, @Rating)
                    """;
                var res = conn.Execute(sql, req);
                return res;
            }
        }

        [HttpPut]
        [Route("")]
        public int Update(Player player)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    UPDATE Players SET
                    	FirstName = COALESCE(@FirstName, FirstName),
                    	LastName = COALESCE(@LastName, LastName),
                    	Email = COALESCE(@Email, Email),
                    	Rating = COALESCE(@Rating, Rating)
                    WHERE PlayerId = @PlayerId; 
                    """;
                var res = conn.Execute(sql, player);
                return res;
            }
        }

        [HttpDelete]
        [Route("{playerId}")]
        public int Delete(int playerId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    DELETE FROM Players WHERE PlayerId = @playerId; 
                    """;
                var res = conn.Execute(sql, new { playerId });
                return res;
            }
        }
    }
}
