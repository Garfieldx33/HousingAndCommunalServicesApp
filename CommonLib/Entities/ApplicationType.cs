﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    [Table("applicationtype")]
    public class ApplicationType
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return @$"ApplicationType: Id = {Id}; Имя = {Name}";
        }
    }
}
