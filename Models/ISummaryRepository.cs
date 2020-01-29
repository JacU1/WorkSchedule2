﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public interface ISummaryRepository
    {
        IEnumerable<Summary> AllSummary { get; set; }
        Summary GetSummaryById(int id);
    }
}