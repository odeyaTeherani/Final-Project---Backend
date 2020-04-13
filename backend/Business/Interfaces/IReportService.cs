using backend.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Business.Interfaces
{
    public interface IReportService
    {
        List<Report> GetAll();
        Report GetById(int id);
        Report AddNewReport([FromBody] Report newReport);
        Report UpdateReport(int id, [FromBody] Report updateReport);
        void Delete(int id);

    }
}
