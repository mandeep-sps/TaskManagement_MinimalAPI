using BusinessLogic.Common;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<bool>> RegisterUser(RegisterUser registerUser);

        Task<ServiceResult<LoginResponse>> LogIn(LoginRequest loginRequest);
        Task<ServiceResult<IEnumerable<GetUsers>>> GetUsers();

    }
}
