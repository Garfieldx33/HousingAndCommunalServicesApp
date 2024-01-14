﻿using CommonLib.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CommonLib.Entities
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public AppStatusEnum Status { get; set; }
        public AppStatusEnum ApplicationTypeId { get; set; }
        public int DepartamentId { get; set; }
        public int ApplicantId { get; set; }
        public int? ExecutorId { get; set; }
        public DateTimeOffset DateCreate { get; set; }
        public DateTimeOffset? DateConfirm { get; set; }
        public DateTimeOffset? DateClose { get; set; }
    }
}
