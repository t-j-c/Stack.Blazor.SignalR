# Stack.Blazor.SignalR

This is a sample project as a complement to [this StackOverflow answer](https://stackoverflow.com/a/64990693/5803406).

The project uses the following components to create an app that displays the 5 most popular tags on Stack Overflow in real time:
- A [SignalR Hub](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-5.0) that handles publishing new tags from the server to all connected clients
  - *See [StackOverflowTagsHub.cs](StackOverflowTagsHub.cs) for implementation*
- A background task implemented as a [.NET Core Hosted Service](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-5.0&tabs=visual-studio)
  - This polls the Stack Exchange API and publishes the results to clients using the `StackOverflowTagsHub`
  - *See [StackOverflowTagService.cs](StackOverflowTagService.cs) for implementation*
- A Blazor client that connects to the Hub to display the tags
  - *See [Index.razor](Pages/Index.razor) for implementation*
