using BusinessLogic.Common;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IProjectService
    {
        Task<ServiceResult<bool>> AddProject(ProjectRequest projectRequest);
        Task<ServiceResult<bool>> UpdateProject(ProjectUpdateRequest projectUpdateRequest );
        Task<ServiceResult<bool>> DeleteProject(int Id);
        Task<ServiceResult<IEnumerable<ProjectResponse>>> GetAllProject();
        Task<ServiceResult<IEnumerable<ProjectResponse>>> GetProjectsByUser(int UserId);
        Task<ServiceResult<IEnumerable<ProjectResponse>>> GetManagedProjects(int Id);


    }
}
