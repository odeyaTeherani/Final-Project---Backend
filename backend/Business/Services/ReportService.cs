using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using backend.Business.Dto;
using backend.Business.Interfaces;
using backend.Data;
using backend.Data.Models;


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

        public List<ReportDto> GetAll()
        {
            var all = _context.Reports.ToList();
            return _mapper.Map<List<ReportDto>>(all);
        }

        public ReportDto GetById(int id)
        {
            var result = _context.Reports.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didn't find return null
            return _mapper.Map<ReportDto>(result);
        }

        public ReportDto AddNewReport(ReportDto newReport)
        {
            var mapperReport = _mapper.Map<Report>(newReport);
            _context.Reports.Add(mapperReport);
            _context.SaveChanges();
            return _mapper.Map<ReportDto>(mapperReport);
        }

        public ReportDto UpdateReport(int id, ReportDto updateReport)
        {
            var result = _context.Reports.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if (result == null) return null;
            result.CarNumber = updateReport.CarNumber;
            result.Date = updateReport.Date;
            result.EventType = updateReport.EventType;
            result.Location = updateReport.Location;
            result.Name = updateReport.Name;
            result.Note = updateReport.Note;
            result.NumOfEv = updateReport.NumOfEv;
            result.SeverityLevel = updateReport.SeverityLevel;

            _context.SaveChanges();
            return _mapper.Map<ReportDto>(result); ;
        }

        public void Delete(int id)
        {
            var result = _context.Reports.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didnt find return null
            if(result == null) throw new Exception($"not found id {id}");
            _context.Reports.Remove(result);
            _context.SaveChanges();
        }
    }
}
