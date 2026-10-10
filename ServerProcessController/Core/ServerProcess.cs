using ConstructPP.API.Shared;
using Porta.Pty;

namespace ConstructPP.API.ServerProcessController.Core
{
    internal class ServerProcess
    {
        public required IPtyConnection Terminal {  get; set; }

        public string[] LaunchArgumets { get; set; } = [];

        public CancellationTokenSource Token { get; init; } = new();
    }
}
