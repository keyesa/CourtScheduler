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
    public class ReservationsController : ControllerBase
    {
        private readonly string _db = "";

        public ReservationsController(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("COURT_SCHEDULER");
            if (connString != null) _db = connString;
        }

        [HttpGet]
        [Route("{resId}")]
        public Reservation Get(int resId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    SELECT * FROM Reservations WHERE ReservationId = COALESCE(@resId, ReservationId)
                    """;
                var player = conn.QueryFirst<Reservation>(sql, new { resId }, commandType: CommandType.StoredProcedure);
                return player;
            }
        }

        [HttpGet]
        [Route("")]
        public IList<Reservation> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = "SELECT * FROM Reservations";
                var reservations = conn.Query<Reservation>(sql).ToList();
                return reservations;
            }
        }

        [HttpPost]
        [Route("")]
        public int Add(Reservation req)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    INSERT INTO Reservations (PlayerId, CourtId, ScheduledTime)
                    VALUES (@PlayerId, @CourtId, @ScheduledTime)
                    """;
                var res = conn.Execute(sql, req);
                return res;
            }
        }

        [HttpDelete]
        [Route("{resId}")]
        public int Delete(int resId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    DELETE FROM Reservations WHERE ReservationId = @resId; 
                    """;
                var res = conn.Execute(sql, new { resId });
                return res;
            }
        }
    }
}
