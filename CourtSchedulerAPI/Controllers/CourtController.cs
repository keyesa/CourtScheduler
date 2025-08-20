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
    public class CourtController : ControllerBase
    {
        private readonly string _db = "";

        public CourtController(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("COURT_SCHEDULER");
            if (connString != null) _db = connString;
        }

        [HttpGet]
        [Route("{playerID}")]
        public Court Get([FromQuery] int courtId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    SELECT * FROM Court
                    WHERE CourtId = @courtId
                    """;

                var court = conn.QueryFirst<Court>(sql, new { courtId });
                return court;
            }
        }

        [HttpGet]
        [Route("")]
        public IList<Court> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = "SELECT * FROM Court";

                var court = conn.Query<Court>(sql).ToList();
                return court;
            }
        }

        [HttpPost]
        [Route("")]
        public int Add(Court req)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    INSERT INTO Court (Name)
                    VALUES (@Name)
                    """;
                var res = conn.Execute(sql, req);
                return res;
            }
        }

        [HttpPut]
        [Route("")]
        public int Update(Court court)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    UPDATE Court SET
                    	Name = COALESCE(@Name, Name),
                    WHERE CourtId = @CourtId; 
                    """;
                var res = conn.Execute(sql, court);
                return res;
            }
        }
    }
}
