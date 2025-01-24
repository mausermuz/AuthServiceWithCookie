using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceBulgakov.Application.Services
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool VerifyPassword(string password, string hashPassword);
    }
}
