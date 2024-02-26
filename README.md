# MyWebForms Sample Application: React

## Introduction

MyWebForms App sample app is to provide ISV developers with a practical example of how to sign documents using web forms with focused view and embedded signing. The application is Single Page Application leveraging the C# DocuSign SDK. The SPA is created using React.js and .Net Web API. You can find a live instance at [https://MyWebForms.azurewebsites.net/](https://MyWebForms.azurewebsites.net/).

MyWebForms demonstrates the following:

1. **Authentication** with DocuSign via [JWT Grant](https://developers.docusign.com/platform/auth/jwt/).
2. **Personal Loan:**
   
   DocuSign Features:
   - Web Forms
   - Single signer
   - Templates
   - Focused view
3. **Auto Loan:**
   
   DocuSign Features:
   - Web Forms
   - Multiple signers
   - Embedded signing
   - Templates
4. **Sailboat Loan:**
   
   DocuSign Features:
   - Web Forms
   - Templates
   - Auto-place (anchor) positioning
   - Embedded signing

## Prerequisites

- Create a DocuSign [Developer Account](https://go.docusign.com/o/sandbox/).
- Create an application on the [Apps and Keys](https://admindemo.docusign.com/authenticate?goTo=appsAndKeys) page.
- Press "GENERATE RSA" and save the generated key pairs (it will be used later in "Settings configuration" section to configure "private.key")
- Installed and configured [Node.js](https://nodejs.org/en/download)
- Installed and configured [Docker](https://www.docker.com/)
- Installed and configured [.Net 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Add all templates from folder 'Templates' to your account
- Add all web form configurations from folder 'WebForms' to your account

### Settings configuration

Create a copy of the file appsettings-example.json, save the copy as appsettings.json, and fill in the data:

- {IntegrationKey} - integration key of the application created in section "Create an application on the Apps and Keys page" above (string)
- Save generate private RSA key (section "Prerequisites") to the file \sample-app-MyWebForms-csharp\DocuSign.MyWebForms\DocuSign.MyWebForms\private.key
- {UserId} - ID of the test user
- {AccountId} - ID of the account that is connected to the test user
- {RedirectUri} - Internal redirection URL that is used during the embedded signing process. To run the app locally this should be `"http://localhost:5000/sign/completed"`
- {PersonalLoanTemplateName} - The name for personal loan template which should be used when adding template to your account
- {AutoLoanTemplateName} - The name for auto loan template which should be used when adding template to your account
- {SailboatLoanTemplateName} - The name for sailboat loan template which should be used when adding template to your account

## Local installation instructions (without Docker)

1. Clone the git repository to your local machine
1. Make the Settings configuration described above
1. Open a terminal and navigate to \sample-app-MyWebForms-csharp\DocuSign.MyWebForms\DocuSign.MyWebForms\ClientApp folder
1. Install required client application packages running the following command in the terminal:
   ```
   npm install
   ```
1. Start the client application running the following command in the terminal:
   ```
   npm start
   ```
1. Open a new terminal and navigate to \sample-app-MyWebForms-csharp\DocuSign.MyWebForms
1. Build the .Net solution:
   ```
   dotnet build --configuration Debug
   ```
1. Start the .NET application:
   ```
   dotnet run --project .\DocuSign.MyWebForms\DocuSign.MyWebForms.csproj --configuration Debug
   ```
1. Open a browser to [localhost:5000](http://localhost:5000) (if the page is not opened automatically).

## Local installation instructions (using Docker)

1. Clone the git repository to your local machine
1. Make the Settings configuration described above
1. Open a terminal in the \sample-app-MyWebForms-csharp directory.
1. Build the docker image running the following command in the terminal:
   ```
   docker build -f DocuSign.MyWebForms/DocuSign.MyWebForms/Dockerfile -t docusign-mywebforms .
   ```
1. Start the application (run the docker container) with the following command in the terminal:

   ```
   docker run -p 80:80 -d docusign-mywebforms
   ```

1. Open a browser to [localhost](http://localhost)

## License information

This repository uses the MIT License. See the [LICENSE](./LICENSE) file for more information.
