using Ecars.Model;
using Ecars.Model.Dto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecars.Database.Repository
{
    public interface IUserRepository
    {
        //bool IsUniqueUser(string username);
        Task<ApiResponse> Login(LoginRequestDTO loginRequestDTO);
        Task<ApiResponse> Register(RegisterationRequestDTO registerationRequestDTO);

    }
}
