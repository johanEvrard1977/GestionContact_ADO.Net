using GestionContact.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactApi.Helpers
{
    public interface ITokenService
    {
        string GenerateToken(LoginSuccessDto user);
        LoginSuccessDto ValidateToken(string token);
    }
}
