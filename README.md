# Stack.Blazor.SignalR

This is a sample project as a complement to [this StackOverflow answer](https://stackoverflow.com/a/64990693/5803406).

The project uses the following components to create an app that displays the 5 most popular tags on Stack Overflow:
- A [SignalR Hub](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-5.0) that handles publishing new tags from the server to all connected clients
- A background task implemented as a [.NET Core Hosted Service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio)
  - This polls the Stack Exchange API every 5 seconds, publishing the results to clients using the `StackOverflowTagsHub`
- A Blazor client that connects to the Hub to display the tags
