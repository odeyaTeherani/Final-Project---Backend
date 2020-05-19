using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Dto.ReportDtoModels;
using backend.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService; //variable that represents the Database

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        

        // https://localhost:44341/report 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var username = User.FindFirst("Sub")?.Value;
            return Ok(await _reportService.GetAllAsync(User.IsInRole("admin"),username));
        }

        /// <summary>
        ///  TODO: Delete -------> jest for Roles example 
        /// </summary>
        /// <returns></returns>
        // [Authorize(Roles = "admin")]
        // [HttpGet ("admin")]
        // public async Task<List<ReportDto>> GetAsyncAdmin()
        // {
        //     return await _reportService.GetAllAsync();
        // }


        // https://localhost:44341/report/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _reportService.GetByIdAsync(id);
            return Ok(result);
        }


        // https://localhost:44341/report
        [HttpPost]
        public async Task<IActionResult> AddNewReport([FromBody] AddReportDto newReport)
        {
            var username = User.FindFirst("Sub")?.Value;
            if (newReport == null) throw new CustomException($"The new report is empty");
            var result = await _reportService.AddNewReportAsync(newReport,username);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }


        // One of the parameters are empty
        // https://localhost:44341/report/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportDto updateReport)
        {
            if (updateReport == null) throw new CustomException($"The report is empty");
            await _reportService.UpdateReportAsync(id, updateReport);
            return NoContent();
        }


        // https://localhost:44341/report/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _reportService.DeleteAsync(id);
            return Ok();
        }
    }
}
