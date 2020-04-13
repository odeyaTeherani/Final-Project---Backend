﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.Data.Models
{
    [Table("Reports")]

    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EventType { get; set; }
        public string SeverityLevel { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public string CarNumber { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int NumOfEv { get; set; }
        // image
    }
}
