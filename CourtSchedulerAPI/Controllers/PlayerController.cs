using CourtSchedulerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CourtSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly string _db = "";

        public PlayerController(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("COURT_SCHEDULER");
            if (connString != null) _db = connString;
        }

        [HttpGet]
        [Route("{playerID}")]
        public Player Get([FromQuery] int playerID)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var player = conn.QueryFirstOrDefault<Player>("dbo.Player_Select", new { ID = playerID }, commandType: CommandType.StoredProcedure);
                return player;
            }
        }

        [HttpGet]
        [Route("")]
        public IList<Player> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var players = conn.Query<Player>("dbo.Player_Select", commandType: CommandType.StoredProcedure).ToList();
                return players;
            }
        }

        [HttpPost]
        [Route("")]
        public int Add(Player req)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var res = conn.Execute("dbo.Player_Insert", req, commandType: CommandType.StoredProcedure);
                return res;
            }
        }

        [HttpPut]
        [Route("")]
        public int Update()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var res = conn.Execute("dbo.Player_Update", commandType: CommandType.StoredProcedure);
                return res;
            }
        }
    }
}
