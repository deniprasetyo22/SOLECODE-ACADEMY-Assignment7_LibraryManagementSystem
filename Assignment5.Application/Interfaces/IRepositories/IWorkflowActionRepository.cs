using Assignment7.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7.Application.Interfaces.IRepositories
{
    public interface IWorkflowActionRepository
    {
        Task<IEnumerable<Workflowaction>> GetAllWorkflowActionsAsync();
        Task<Workflowaction> GetWorkflowActionByIdAsync(int actionId);
        Task AddWorkflowActionAsync(Workflowaction workflowAction);
        Task UpdateWorkflowActionAsync(Workflowaction workflowAction);
        Task DeleteWorkflowActionAsync(int actionId);
    }
}
