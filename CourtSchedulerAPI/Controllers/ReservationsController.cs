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
                var sql = $"""
                    SELECT p.*, r.ReservationId, c.*  FROM Reservations r
                    LEFT JOIN Players p ON p.PlayerId = r.PlayerId
                    LEFT JOIN Courts c ON r.CourtId = c.CourtId
                    WHERE ReservationId = COALESCE(@resId, ReservationId)
                    """;

                var reservations = conn.Query<Player, FlatReservation, Court, Reservation>(sql, new { resId }, mapped: (player , flatRes, court) => {
                    Reservation res = new Reservation();
                    res.ReservationId = flatRes.ReservationId;
                    res.Players.Add(player);
                    res.Court = court;
                    return res;
                }, splitOn: "p.ReservationId,  c.CourtId");
                
                var result = reservations.GroupBy(p => p.ReservationId).Select(r => {
                    var groupedRes = r.First();
                    groupedRes.Players = r.Select(p => p.Players.Single()).ToList();
                    return groupedRes;
                }).First(); // should just be the one reservation anyway because of Id

                return result
            }
        }

        [HttpGet]
        [Route("")]
        public IList<Reservation> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = $"""
                    SELECT p.*, r.ReservationId, c.*  FROM Reservations r
                    LEFT JOIN Players p ON p.PlayerId = r.PlayerId
                    LEFT JOIN Courts c ON r.CourtId = c.CourtId
                    """;

                var reservations = conn.Query<Player, FlatReservation, Court, Reservation>(sql, mapped: (player , flatRes, court) => {
                    Reservation res = new Reservation();
                    res.ReservationId = flatRes.ReservationId;
                    res.Players.Add(player);
                    res.Court = court;
                    return res;
                }, splitOn: "p.ReservationId,  c.CourtId");
                
                var result = reservations.GroupBy(p => p.ReservationId).Select(r => {
                    var groupedRes = r.First();
                    groupedRes.Players = r.Select(p => p.Players.Single()).ToList();
                    return groupedRes;
                });
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
