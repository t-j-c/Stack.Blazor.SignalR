using Microsoft.AspNetCore.SignalR;
using Stack.Blazor.SignalR.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stack.Blazor.SignalR
{
    public interface IStackOverflowTagsClient
    {
        Task NewTags(List<Tag> tags);
    }

    public class StackOverflowTagsHub : Hub<IStackOverflowTagsClient>
    {
        public const string HubUrl = "/stackOverflowTags";

        public Task NewTags(List<Tag> tags) => Clients.All.NewTags(tags);
    }
}
