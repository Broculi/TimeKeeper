using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Models
{
    public class Workday
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan WorkTime => (StartTime > EndTime) ? TimeSpan.Zero : EndTime.Subtract(StartTime);
    }
}
