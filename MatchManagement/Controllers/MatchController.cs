using MatchManagement.Classes;
using MatchManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManagement.Controllers
{       
    [Route("api/[controller]")]
    [ApiController]
    
    public class MatchController : Controller
    {
        private readonly RaceDBContext _context;
        public MatchController(RaceDBContext context)
        {
            _context = context;

        }
        [HttpGet]
        public ContentResult GetMatchOdds(string TeamA,string TeamB,DateTime matchDate,Sport sport)
        {
            StringBuilder jsonString = new StringBuilder();
            var sportString = EnumExtension.GetString(sport);
            if (sportString== "No Sport")
            {
                return new ContentResult { StatusCode = StatusCodes.Status404NotFound, Content = "{" + "\"Sport\":" + '"' + "Not Found" + '"' + "}", ContentType = "application/json" };

            }
            var match=_context.Match.Where(m => m.TeamA == TeamA && m.TeamB == TeamB && m.MatchDate == matchDate.Date && m.Sport==sportString).FirstOrDefault();
            if(match ==null)
            {
                return new ContentResult { StatusCode = StatusCodes.Status404NotFound, Content = "{"+"\"Match\":"+'"'+"Not Found"+'"'+"}", ContentType = "application/json" };
            }
             var date  = match.MatchDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            List<MatchOdds> matchedOdds = _context.MatchOdds.Where(m => m.MatchId == match.MatchId).ToList();
            jsonString.Append("{");
            jsonString.Append("\"Match\": { ");
            jsonString.Append("\"Description\":");
            jsonString.Append('"');
            jsonString.Append(match.MatchDescription);
            jsonString.Append('"');
            jsonString.Append(" ,");
            jsonString.Append("\"Match Date\":");
            jsonString.Append('"');
            jsonString.Append(date);
            jsonString.Append('"');
            jsonString.Append(" ,");
            jsonString.Append("\"Match Time\":");
            jsonString.Append('"');
            jsonString.Append(match.MatchTime);
            jsonString.Append('"');
            jsonString.Append(" ,");
            jsonString.Append("\"Sport\":");
            jsonString.Append('"');
            jsonString.Append(match.Sport);
            jsonString.Append('"');
            jsonString.Append(" ,");
            jsonString.Append("\"Odds\": ");
            jsonString.Append("[");
            foreach (var odd in matchedOdds)
            {
                jsonString.Append("{");
                jsonString.Append("\"Specifier\":");
                jsonString.Append('"');
                jsonString.Append(odd.Specifier);
                jsonString.Append('"');
                jsonString.Append(" ,");
                jsonString.Append("\"Odd\":");
                jsonString.Append('"');
                jsonString.Append(odd.Odd);
                jsonString.Append('"');
                jsonString.Append("}");
                jsonString.Append(" ,");
            }
            jsonString=jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            jsonString.Append("}");
            jsonString.Append("}");
            return new ContentResult { StatusCode = StatusCodes.Status200OK, Content = jsonString.ToString(), ContentType = "application/json" };
        }
    }
}
