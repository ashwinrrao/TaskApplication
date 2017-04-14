using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Empty_TaskApplication.Models
{
    public class DateInfo
    {
        public int Id { get; set; }
        [DisplayName("Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yy}")]
        public DateTime date { get; set; }
        [DisplayName("Holiday")]
        public bool isHoliday { get; set; }
    }
}