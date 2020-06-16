using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Business.Dto;
using backend.Business.Dto.ReportDtoModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IReportService
    {
        Task<List<GetReportDto>> GetAllAsync(bool isAdmin,string userName);
        Task<GetReportDto> GetByIdAsync(int id);
        Task<GetReportDto> AddNewReportAsync(ReportDto newReport,string userName);
        Task UpdateReportAsync(int id, ReportDto updateReport);
        void DeleteAsync(int id);

    }
}
