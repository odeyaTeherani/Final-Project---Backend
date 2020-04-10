using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        // https://localhost:44341/report
        [HttpGet]
        public List<Report> Get()
        {
            return ApplicationDbContext.Reports;
        }

        // https://localhost:44341/report/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = ApplicationDbContext.Reports.Find(e => e.Id == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // https://localhost:44341/report
        [HttpPost]
        public IActionResult AddNewReport([FromBody] Report newReport)
        {
            if (newReport == null) return BadRequest();
            var indexId = ApplicationDbContext.Reports.Max(e => e.Id);
            if (indexId == 0)
                newReport.Id = 1;
            else
                newReport.Id = ApplicationDbContext.Reports.Max(e => e.Id) + 1;
            ApplicationDbContext.Reports.Add(newReport);
            return CreatedAtAction("GetById", new { id = newReport.Id }, newReport);

        }

        // Don't work
        // One of the parameters are empty
        // https://localhost:44341/report/{id}
        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] Report updateReport)
        {
            if (updateReport == null) return BadRequest();

            var result = ApplicationDbContext.Reports.Find(e => e.Id == id);
            if (result == null) return NotFound();

            result.CarNumber = updateReport.CarNumber;
            result.Date = updateReport.Date;
            result.EventType = updateReport.EventType;
            result.Location = updateReport.Location;
            result.Name = updateReport.Name;
            result.Note = updateReport.Note;
            result.NumOfEv = updateReport.NumOfEv;
            result.SeverityLevel = updateReport.SeverityLevel;

            return NoContent();
        }

        // https://localhost:44341/report/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = ApplicationDbContext.Reports.Find(e => e.Id == id);
            if (result == null) return NotFound();

            ApplicationDbContext.Reports.Remove(result);
            return Ok();
        }
    }
}