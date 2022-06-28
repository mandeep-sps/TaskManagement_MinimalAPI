using BusinessLogic.Common;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly string _key;

        public UserService(IRepository repository, IConfiguration configuration)
        {

            _repository = repository;
            _key = configuration["JWTKey"];

        }
        /// <summary>
        /// UserRegister Service
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        public async Task<ServiceResult<bool>> RegisterUser(RegisterUser registerUser)
        {
            try
            {
                var checkSql = "SELECT COUNT(0) FROM dbo.AppUser WHERE Email=@Email";
                var count = await _repository.InvokeExecuteQuery(checkSql, new { registerUser.Email});
                if (count > 0)
                {
                    return new ServiceResult<bool>(false, "Email already exist", true);
                }

                var sql = "INSERT INTO dbo.AppUser (NAME,EMAIL,PASSWORD,UserRole,CreatedOn,UpdatedOn,isActive) VALUES(@Name,@Email,@Password,'User',GETDATE(),GETDATE(),1 )";
                await _repository.InvokeExecute(sql, new { registerUser.Name, registerUser.Email, registerUser.Password});

                return new ServiceResult<bool>(true, "User Registered",false);

            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>(false, ex.Message,true);

            }
        }
        /// <summary>
        /// LogIn Service
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<ServiceResult<LoginResponse>> LogIn(LoginRequest loginRequest)
        {
            try
            {
                var checkSql = "SELECT * FROM dbo.AppUser WHERE Email=@Email and Password=@Password";
                var data =  _repository.InvokeSingleQuery<LoginResponse>(checkSql, new { loginRequest.Email, loginRequest.Password });
                if (data!=null)
                {
                    LoginResponse response = new()
                    {
                        Id = data.Id,
                        Email = data.Email,
                        UserRole=data.UserRole, 
                        Name=data.Name, 
                        Token= JwtManager.JwtGenerator(loginRequest.Email, key: _key)
                    };

                    return new ServiceResult<LoginResponse>(response, "Logged in successfully",false);
                }
                else
                {
                    return new ServiceResult<LoginResponse>(null, "Invalid credentials", true);

                }

            }

            catch (Exception ex)
            {
                return new ServiceResult<LoginResponse>(ex, ex.Message);
            }
        }

        /// <summary>
        /// GetEmployee Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<GetUsers>>> GetUsers()
        {
            try
            {
                var Sql = "SELECT Id,Name FROM dbo.AppUser WHERE UserRole = 'User'";
                var userDetails = await _repository.InvokeQuery<GetUsers>(Sql);
                if (userDetails.Count() > 0)
                {
                    return new ServiceResult<IEnumerable<GetUsers>>(userDetails, $"{userDetails.Count()} record(s) found");
                }
                else
                {
                    return new ServiceResult<IEnumerable<GetUsers>>(null, "No record found", true);

                }

            }

            catch (Exception ex)
            {
                return new ServiceResult<IEnumerable<GetUsers>>(ex, ex.Message);
            }
        }

    }
}
