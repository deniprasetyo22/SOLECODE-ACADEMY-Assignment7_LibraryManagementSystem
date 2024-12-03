using Assignment5.Persistence.Context;
using Assignment7.Application.Interfaces.IRepositories;
using Assignment7.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Persistence.Repositories
{
    public class ProcessRepository:IProcessRepository
    {
        private readonly LibraryContext _context;
        public ProcessRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Process> GetProcessByIdAsync(int processId)
        {
            return await _context.Processes
                .Include(b=>b.Requester)
                .FirstOrDefaultAsync(b=>b.Processid == processId);

        }

        public async Task<IEnumerable<Process>> GetAllProcessAsync()
        {
            return await _context.Processes
                .Include(b=>b.Requester)
                .Include(b=>b.Workflow)
                .ToListAsync();
        }

        public async Task AddProcessAsync(Process process)
        {
            await _context.Processes.AddAsync(process);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcessAsync(Process process)
        {
            _context.Processes.Update(process);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcessAsync(int processId)
        {
            var existingProcess = await _context.Processes.FindAsync(processId);
            if (existingProcess != null)
            {
                _context.Processes.Remove(existingProcess);
                await _context.SaveChangesAsync();
            }
        }
    }
}
