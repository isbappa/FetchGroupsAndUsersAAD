# Project Description

This project demonstrates how to efficiently retrieve and process groups and users from Azure Active Directory (AAD) using the Microsoft Graph SDK in C#. It utilizes PageIterator for paginated data fetching and Parallel.ForEach for concurrent processing, ensuring efficient handling of large datasets.


## Prerequisites

- Azure Active Directory Tenant
- Azure AD Application with Client ID and Client Secret
- API permissions in Azure AD for Microsoft Graph (Group.Read.All and User.Read.All)
- .NET development environment


## Getting Started

Follow the steps below to get started with this project:

1. Clone the project repository.
2. Open the project in your preferred code editor.
3. Replace the following placeholders with your own values:
   - `YOUR TENANT ID HERE`: Replace with your Azure Active Directory tenant ID.
   - `YOUR CLIENT ID HERE`: Replace with your client ID.
   - `YOUR CLIENT SECRET HERE`: Replace with your client secret.


## Running the Code

1. Build the project to ensure all dependencies are resolved.
2. Run the `Main` method in the `Program` class.
3. Navigate to the project directory.
4. Execute `dotnet run` to start the application.


## Usage

- The application will authenticate using the provided Azure AD credentials.
- It will fetch and display all groups and users from your Azure AD tenant in the console.


## Contributing

Feel free to submit a pull request if you find any bugs or have any suggestions for improvement.