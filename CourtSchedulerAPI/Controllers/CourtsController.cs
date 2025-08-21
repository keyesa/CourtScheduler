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
    public class CourtsController : ControllerBase
    {
        private readonly string _db = "";

        public CourtsController(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("COURT_SCHEDULER");
            if (connString != null) _db = connString;
        }

        [HttpGet]
        [Route("{courtId}")]
        public Court Get(int courtId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    SELECT * FROM Courts
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
                var sql = "SELECT * FROM Courts";

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
                    INSERT INTO Courts (Name)
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
                    UPDATE Courts SET
                    	Name = COALESCE(@Name, Name),
                    WHERE CourtId = @CourtId; 
                    """;
                var res = conn.Execute(sql, court);
                return res;
            }
        }

        [HttpDelete]
        [Route("{courtId}")]
        public int Delete(int courtId)
        {
            using (SqlConnection conn = new SqlConnection(_db))
            {
                var sql = """
                    DELETE FROM Courts WHERE CourtId = @courtId; 
                    """;
                var res = conn.Execute(sql, new { courtId });
                return res;
            }
        }
    }
}
