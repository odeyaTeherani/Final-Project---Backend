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
        List<ReportDto> GetAll();
        ReportDto GetById(int id);
        ReportDto AddNewReport([FromBody] ReportDto newReport);
        ReportDto UpdateReport(int id, [FromBody] ReportDto updateReport);
        void Delete(int id);

    }
}
