﻿using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Domain.Models
{
    [Table("nextsteprules")]
    public partial class Nextsteprule
    {
        [Key]
        [Column("ruleid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ruleid { get; set; }

        [Column("currentstepid")]
        public int? Currentstepid { get; set; }

        [Column("nextstepid")]
        public int? Nextstepid { get; set; }

        [Column("conditiontype")]
        [StringLength(255)]
        public string? Conditiontype { get; set; }

        [Column("conditionvalue")]
        [StringLength(255)]
        public string? Conditionvalue { get; set; }

        [ForeignKey("Currentstepid")]
        [InverseProperty("NextstepruleCurrentsteps")]
        public virtual Workflowsequence? Currentstep { get; set; }

        [ForeignKey("Nextstepid")]
        [InverseProperty("NextstepruleNextsteps")]
        public virtual Workflowsequence? Nextstep { get; set; }
    }
}
