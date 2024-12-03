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
    public class WorkflowActionRepository : IWorkflowActionRepository
    {
        private readonly LibraryContext _context;

        public WorkflowActionRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workflowaction>> GetAllWorkflowActionsAsync()
        {
            return await _context.Workflowactions.ToListAsync();
        }

        public async Task<Workflowaction> GetWorkflowActionByIdAsync(int actionId)
        {
            return await _context.Workflowactions.FirstOrDefaultAsync(a => a.Actionid == actionId);
        }

        public async Task AddWorkflowActionAsync(Workflowaction workflowAction)
        {
            await _context.Workflowactions.AddAsync(workflowAction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkflowActionAsync(Workflowaction workflowAction)
        {
            _context.Workflowactions.Update(workflowAction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkflowActionAsync(int actionId)
        {
            var existingAction = await _context.Workflowactions.FindAsync(actionId);
            if (existingAction != null)
            {
                _context.Workflowactions.Remove(existingAction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
