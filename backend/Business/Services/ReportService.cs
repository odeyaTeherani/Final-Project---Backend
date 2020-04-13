using System;
using System.Collections.Generic;
using System.Linq;
using backend.Business.Interfaces;
using backend.Data;
using backend.Data.Models;
using Microsoft.AspNetCore.Mvc;


namespace backend.Business.Services
{
    public class ReportService: IReportService  
    {
        private readonly ApplicationDbContext _context; //variable that represents the Database

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Report> GetAll()
        {
            return _context.Reports.ToList();
        }

        public Report GetById(int id)
        {
             return _context.Reports.SingleOrDefault(e => e.Id == id); // Make sure it is single and if you didn't find return null
        }

        public Report AddNewReport(Report newReport)
        {
            _context.Reports.Add(newReport);
            _context.SaveChanges();
            return newReport;
        }

        public Report UpdateReport(int id, Report updateReport)
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
            return result;
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
