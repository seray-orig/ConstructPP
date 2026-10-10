using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ConstructPP.API.Settings
{
    [Authorize]
    public class LogHub : Hub { }
}
