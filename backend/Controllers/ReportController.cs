using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<List<ReportDto>> GetAsync()
        {
            return await _reportService.GetAllAsync();
        }


        // https://localhost:44341/report/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reportService.GetByIdAsync(id);
            //if (result == null) return NotFound();
            return Ok(result);
        }


        // https://localhost:44341/report
        [HttpPost]
        public IActionResult AddNewReportAsync([FromBody] ReportDto newReport)
        {
            //if (newReport == null) return BadRequest();
            var result = _reportService.AddNewReportAsync(newReport);
            return CreatedAtAction("GetByIdAsync", new { id = result.Id }, result);
        }


        // One of the parameters are empty
        // https://localhost:44341/report/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateReportAsync(int id, [FromBody] ReportDto updateReport)
        {
            //if (updateReport == null) return BadRequest();

            var result = _reportService.UpdateReportAsync(id, updateReport);
            if (result == null) return NotFound();
            return NoContent();
        }


        // https://localhost:44341/report/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAsync(int id)
        {
            _reportService.DeleteAsync(id);
            return Ok();
        }
    }
}
