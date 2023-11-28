using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System.Collections.Concurrent;


// Define your tenant ID, client ID, and client secret
string tenantId = "YOUR TENANT ID HERE";
string clientId = "YOUR CLIENT ID HERE";
string clientSecret = "YOUR CLIENT SECRET HERE";

// Create a GraphServiceClient instance with the defined TokenCredential
var scopes = new[] { "https://graph.microsoft.com/.default" };
var options = new TokenCredentialOptions
{
    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
};
// https://learn.microsoft.com/dotnet/api/azure.identity.clientsecretcredential  
var clientSecretCredential = new ClientSecretCredential(
    tenantId, clientId, clientSecret, options);

// get accessToken to check if we have a token, it's optional          
var accessToken = await clientSecretCredential.GetTokenAsync(new TokenRequestContext(scopes) { });
Console.WriteLine(accessToken.Token);

var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

var groupsList = await GetGroupsAsync(graphClient);


Parallel.ForEach(groupsList!, group =>
{
    Console.WriteLine(group.DisplayName);
});

var usersList = await GetUsersAsync(graphClient);

Parallel.ForEach(usersList!, user =>
{
    Console.WriteLine(user.DisplayName);
});


// Get all groups from Azure AD
async Task<ConcurrentBag<Group>?> GetGroupsAsync(GraphServiceClient graphClient)
{
    var groupsList = new ConcurrentBag<Group>();

    var requestGroup = graphClient.Groups
        .GetAsync()
        .GetAwaiter()
        .GetResult();

    if (requestGroup is null)
    {
        Console.WriteLine("No group found in Azure AD");
        return null;
    }

    var pageIterator = PageIterator<Group, GroupCollectionResponse>
        .CreatePageIterator(graphClient, requestGroup,
        group =>
        {
            groupsList.Add(group);
            return true;
        });
    await pageIterator.IterateAsync();

    return groupsList;
}

// Get all users from Azure AD
async Task<ConcurrentBag<User>?> GetUsersAsync(GraphServiceClient graphClient)
{
    var usersList = new ConcurrentBag<User>();

    var requestUser = graphClient.Users
        .GetAsync()
        .GetAwaiter()
        .GetResult();

    if (requestUser is null)
    {
        Console.WriteLine("No user found in Azure AD");
        return null;
    }

    var pageIterator = PageIterator<User, UserCollectionResponse>
        .CreatePageIterator(graphClient, requestUser,
        user =>
        {
            usersList.Add(user);
            return true;
        });
    await pageIterator.IterateAsync();

    return usersList;
}