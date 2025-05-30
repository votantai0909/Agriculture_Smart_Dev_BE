using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<(bool Success, string Message, Users User)> RegisterUserAsync(string username, string email, string password, string address, string phoneNumber);
        Task<(bool Success, string Message, Users User, string Token, DateTime Expiration)> LoginAsync(string username, string password);
    }
}
