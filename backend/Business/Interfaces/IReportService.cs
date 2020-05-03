using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllAsync();
        Task<ReportDto> GetByIdAsync(int id);
        Task<ReportDto> AddNewReportAsync(ReportDto newReport);
        Task UpdateReportAsync(int id, ReportDto updateReport);
        void DeleteAsync(int id);

    }
}
