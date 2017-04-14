using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Empty_TaskApplication.Models
{
    public class Task
    {
        public int Id { get; set; }
        [DisplayName("Task Name")]
        public string name { get; set; }
        [DisplayName("From Date")]
        public DateTime fromDate { get; set; }
        [DisplayName("To Date")]
        public DateTime toDate { get; set; }
        [DisplayName("Duration")]
        public int? duration { get; set; }
    }
}