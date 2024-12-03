using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Interfaces.IRepositories
{
    public interface IWorkflowRepository
    {
        Task<IEnumerable<Workflow>> GetAllWorkflowsAsync();
        Task<Workflow> GetWorkflowByIdAsync(int workflowId);
        Task AddWorkflowAsync(Workflow workflow);
        Task UpdateWorkflowAsync(Workflow workflow);
        Task DeleteWorkflowAsync(int workflowId);
    }
}
