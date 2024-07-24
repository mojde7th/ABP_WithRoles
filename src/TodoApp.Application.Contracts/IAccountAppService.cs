using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Contracts;

namespace TodoApp
{
    public interface IAccountAppService
    {
        Task<string> LoginAsync(LoginDto input);
       Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AssignRoleAsync(string username, string roleName);
    }
}
