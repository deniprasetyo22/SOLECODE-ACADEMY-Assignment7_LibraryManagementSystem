﻿using Assignment7.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Domain.Models
{
    public class AppUser:IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        [InverseProperty("Requester")]
        public virtual ICollection<Process> Processes { get; set; } = new List<Process>();

        [InverseProperty("AppUser")]
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        [InverseProperty("Actor")]
        public virtual ICollection<Workflowaction> Workflowactions { get; set; } = new List<Workflowaction>();

        [InverseProperty("RequiredroleNavigation")]
        public virtual ICollection<Workflowsequence> Workflowsequences { get; set; } = new List<Workflowsequence>();
        [InverseProperty("User")]
        public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();
    }
}
