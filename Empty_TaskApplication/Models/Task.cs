﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public DateTime fromDate { get; set; }
        [DisplayName("To Date")]
        [Required]
        public DateTime toDate { get; set; }

        public Task()
        {

        }

        public Task(TaskView taskView)
        {
            Id = taskView.Id;
            name = taskView.name;
            fromDate = taskView.fromDate;
            toDate = taskView.toDate;
        }
    }
}