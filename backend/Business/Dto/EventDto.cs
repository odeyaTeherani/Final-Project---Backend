﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Data.Enums;
using backend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.Business.Dto
{
    public class EventDto
    {
        public int Id { get; set; } //primary key
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public List<Report> Reports { get; set; }
        public SeverityLevel SeverityLevelType { get; set; }
        public Location Location { get; set; }
        public DateTime Date { get; set; }
        public List<Image> Images { get; set; }
    }
}
