using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllAsync();
        Task<ReportDto> GetByIdAsync(int id);
        Task<ReportDto> AddNewReportAsync([FromBody] ReportDto newReport);
        Task<ReportDto> UpdateReportAsync(int id, [FromBody] ReportDto updateReport);
        void DeleteAsync(int id);

    }
}
