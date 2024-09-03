﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Domain.Models.Mail
{
    public class MailData
    {
        public List<string> EmailToIds { get; set; } = new List<string>();
        public List<string> EmailCCIds { get; set; } = new List<string>();
        public string? EmailToName { get; set; }
        public string? EmailSubject { get; set; }
        public string? EmailBody { get; set; }
        public string? Password { get; set; }
        public List<string>? Attachments { get; set; }
    }
}
