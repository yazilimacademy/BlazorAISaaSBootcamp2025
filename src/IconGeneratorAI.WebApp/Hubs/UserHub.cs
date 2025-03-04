using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace IconGeneratorAI.WebApp.Hubs;

[Authorize]
public sealed class UserHub : Hub
{

}
