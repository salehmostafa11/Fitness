using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitData.Repositories
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var data =  connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return data ?? string.Empty;
        }
    }
}
