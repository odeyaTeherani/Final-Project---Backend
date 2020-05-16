using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using backend.Business.Dto.ReportDtoModels;
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

        public async Task<List<GetReportDto>> GetAllAsync(bool isAdmin,string userName)
        {
            List<Report> all;
            if(isAdmin)
                 all = await _context.Reports
                     .Include(x=>x.EventType)
                     .Include(x=>x.Location)
                     .ToListAsync();
            else
            {
                all = await _context.Reports
                    .Include(x=>x.EventType)
                    .Include(x=>x.Location)
                    .Where(report => report.Name.Equals(userName))
                    .ToListAsync();
            }
            return _mapper.Map<List<GetReportDto>>(all.OrderByDescending(report => report.Date));
        }

        public async Task<GetReportDto> GetByIdAsync(int id)
        {
            var result = await 
                _context.Reports
                    .Include(x=>x.Images)
                    .Include(x => x.EventType)
                    .Include(x=> x.Location)
                .SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Report whit id {id} not found",HttpStatusCode.NotFound );
            return _mapper.Map<GetReportDto>(result);
        }

        public async Task<GetReportDto> AddNewReportAsync(AddReportDto newReport,string userName)
        {
            var mapperReport = _mapper.Map<Report>(newReport);
            mapperReport.Date = DateTime.Now;
            mapperReport.Name = userName;
            await _context.Reports.AddAsync(mapperReport);
            await _context.SaveChangesAsync();
            return _mapper.Map<GetReportDto>(mapperReport);
        }

        public async Task UpdateReportAsync(int id, ReportDto updateReport)
        {
            var result = await _context.Reports.SingleOrDefaultAsync(e => e.Id == id); // Make sure it is single and if you didn't find return null
            if (result == null) throw new CustomException($"Report whit id {id} is not found", HttpStatusCode.NotFound);

            result.CarNumber = updateReport.CarNumber;
          //  result.Date = updateReport.Date;
         //   result.EventType = _mapper.Map<EventType>(updateReport.EventType);
          //  result.Location = updateReport.Location;
          //  result.Name = updateReport.Name;
            result.Note = updateReport.Note;
         //   result.Casualties = updateReport.Casualties;
       //     result.SeverityLevelType = updateReport.SeverityLevelType;
            await _context.SaveChangesAsync();

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
