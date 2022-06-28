﻿using BusinessLogic.Common;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository _repository;
        public ProjectService(IRepository repository)
        {

            _repository = repository;
        }

        public async Task<ServiceResult<bool>> AddProject(ProjectRequest projectRequest)
        {
            try
            {
                var checkSql = "SELECT COUNT(0) FROM dbo.Project WHERE ProjectName=@ProjectName";
                var count = await _repository.InvokeExecuteQuery(checkSql, new { projectRequest.ProjectName });
                if (count > 0)
                {
                    return new ServiceResult<bool>(false, "Project with this name already exist", true);
                }

                var sql = "INSERT INTO dbo.Project (PROJECTNAME,DESCRIPTION,CREATEDON,UPDATEDON,ISACTIVE,MANAGERID) VALUES(@ProjectName,@Description,GETDATE(),GETDATE(),1,@ManagerId )";
                await _repository.InvokeExecute(sql, new { projectRequest.ProjectName, projectRequest.Description, projectRequest.ManagerId });
                var Sql = "SELECT ID FROM DBO.PROJECT WHERE PROJECTNAME=@ProjectName";
                int projectId = await _repository.InvokeExecuteQuery(Sql, new { projectRequest.ProjectName });

                foreach (var UserId in projectRequest.UserId)
                {
                    var query = "INSERT INTO PROJECTUSER (USERID,PROJECTID,ISACTIVE) VALUES(@UserId,@projectId,1)";
                    await _repository.InvokeExecute(query, new { UserId, projectId });

                }

                return new ServiceResult<bool>(true, "Project Registered", false);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message, true);

            }
        }


        public async Task<ServiceResult<bool>> UpdateProject(ProjectUpdateRequest projectUpdateRequest)
        {
            try
            {
                var checkSql = "SELECT COUNT(0) FROM dbo.Project WHERE ProjectName=@ProjectName";
                var count = await _repository.InvokeExecuteQuery(checkSql, new { projectUpdateRequest.ProjectName });
                if (count > 0)
                {
                    return new ServiceResult<bool>(false, "Project with this name already exist", true);
                }

                var sql = "UPDATE dbo.Project SET PROJECTNAME=@ProjectName,DESCRIPTION=@Description,UPDATEDON=GETDATE(),MANAGERID=@ManagerId WHERE Id=@Id";
                await _repository.InvokeExecute(sql, new { projectUpdateRequest.ProjectName, projectUpdateRequest.Description, projectUpdateRequest.ManagerId, projectUpdateRequest.Id});
                var query2 = "UPDATE PROJECTUSER SET ISACTIVE = 0 WHERE PROJECTID = @Id";
                await _repository.InvokeExecute(query2, new { projectUpdateRequest.Id });
                foreach (var UserId in projectUpdateRequest.UserId)
                {
                    var query = "INSERT INTO PROJECTUSER (USERID,PROJECTID,ISACTIVE) VALUES(@UserId,@Id,1)";
                    await _repository.InvokeExecute(query, new { UserId, projectUpdateRequest.Id });

                }

                return new ServiceResult<bool>(true, "Project Updated", false);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message, true);

            }
        }


        public async Task<ServiceResult<bool>> DeleteProject(int Id)
        {
            try
            {
                var sql = "UPDATE dbo.Project SET IsActive=0 WHERE Id=@Id";
                await _repository.InvokeExecute(sql, new { Id });
                var query2 = "UPDATE PROJECTUSER SET ISACTIVE = 0 WHERE PROJECTID = @Id";
                await _repository.InvokeExecute(query2, new { Id });
                return new ServiceResult<bool>(true, "Project Deleted Successfully", false);
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message, true);

            }
        }


    }
}
