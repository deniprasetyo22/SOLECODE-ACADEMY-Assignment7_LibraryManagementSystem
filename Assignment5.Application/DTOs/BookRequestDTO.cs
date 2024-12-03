using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.DTOs
{
    public class BookRequestDTO
    {
        public int Id { get; set; }
        public string? RequestName { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public string? Author {  get; set; }
        public string? Publisher { get; set; }
        public int ProcessId { get; set; }
        public string? Requester {  get; set; }
        public string? Status { get; set; }
        public DateTime RequestDate { get; set; }

    }
}
