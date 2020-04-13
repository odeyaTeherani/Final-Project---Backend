using System;
using System.Collections.Generic;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService; //variable that represents the Database

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        

        // https://localhost:44341/report
        [HttpGet]
        public List<ReportDto> Get()
        {
            return _reportService.GetAll();
        }


        // https://localhost:44341/report/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _reportService.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }


        // https://localhost:44341/report
        [HttpPost]
        public IActionResult AddNewReport([FromBody] ReportDto newReport)
        {
            if (newReport == null) return BadRequest();
            var result = _reportService.AddNewReport(newReport);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }


        // One of the parameters are empty
        // https://localhost:44341/report/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateReport(int id, [FromBody] ReportDto updateReport)
        {
            if (updateReport == null) return BadRequest();

            var result = _reportService.UpdateReport(id, updateReport);
            if (result == null) return NotFound();
            return NoContent();
        }


        // https://localhost:44341/report/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _reportService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
