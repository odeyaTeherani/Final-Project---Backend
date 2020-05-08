using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Dto.ReportDtoModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportDto>> GetAllAsync();
        Task<ReportDto> GetByIdAsync(int id);
        Task<GetReportDto> AddNewReportAsync(AddReportDto newReport);
        Task UpdateReportAsync(int id, ReportDto updateReport);
        void DeleteAsync(int id);

    }
}
