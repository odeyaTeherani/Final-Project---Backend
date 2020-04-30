using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Controllers;
using backend.Data;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Business.Services
{
    public class ReportService: IReportService  
    {
        private readonly ApplicationDbContext _context; //variable that represents the Database
        private readonly IMapper _mapper;

        public ReportService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReportDto>> GetAllAsync()
        {
            var all = await _context.Reports.ToListAsync();
            return _mapper.Map<List<ReportDto>>(all);
        }

        public async Task<ReportDto> GetByIdAsync(int id)
        {
            var result = await _context.Reports.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Report whit id {id} not found",HttpStatusCode.NotFound );
            return _mapper.Map<ReportDto>(result);
        }

        public async Task<ReportDto> AddNewReportAsync(ReportDto newReport)
        {
            var mapperReport = _mapper.Map<Report>(newReport);
            await _context.Reports.AddAsync(mapperReport);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReportDto>(mapperReport);
        }

        public async Task<ReportDto> UpdateReportAsync(int id, ReportDto updateReport)
        {
            var result = await _context.Reports.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Report whit id {id} is not found", HttpStatusCode.NotFound);

            result.CarNumber = updateReport.CarNumber;
            result.Date = updateReport.Date;
            result.EventType = updateReport.EventType;
            result.Location = updateReport.Location;
            result.Name = updateReport.Name;
            result.Note = updateReport.Note;
            result.Casualties = updateReport.Casualties;
            result.SeverityLevelType = updateReport.SeverityLevelType;

            await _context.SaveChangesAsync();
            return _mapper.Map<ReportDto>(result); ;
        }

        public async void DeleteAsync(int id)
        {
            var result = await _context.Reports.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if(result == null) throw new CustomException($"Report whit id {id} is not found", HttpStatusCode.NotFound);
            _context.Reports.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
