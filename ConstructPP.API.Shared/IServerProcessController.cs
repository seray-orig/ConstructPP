using System.ServiceModel;

namespace ConstructPP.API.Shared
{
    [ServiceContract(Name = "ServerProcessController")]
    public interface IServerProcessController
    {
        [OperationContract]
        Task<Report> StartServer(Guid id);

        [OperationContract]
        Task<Report> StopServer(Guid id);

        [OperationContract]
        Task<Report> SendCommand(Guid id, string command);

        [OperationContract]
        Task<Report> SetLaunchArguments(Guid id, string[] arguments);

        [OperationContract]
        Task<Report> CheckUpdate(Guid id);

        [OperationContract]
        Task<Report> Update(Guid id);

        [OperationContract]
        IAsyncEnumerable<string> StreamLogsAsync(Guid id);
    }
}
