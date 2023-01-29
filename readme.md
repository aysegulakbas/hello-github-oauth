# Hello Github OAuth

This is a 'Hello World' app to demonstrate a minimal working connection with GitHub OAuth for an ASP.NET Core 7.0 application (specifically v7.0.102).

This web app is based on the article [Add GitHub OpenID Auth For ASP.NET Core Apps][article] by Khalid Abuhakmeh which uses ASP.NET Core 5.0.

This app only claims access to public information for an account so I believe I can include the ClientSecret below without putting anybody's personal data at risk. However, if you do use the Client details below to authorize this app with your GitHub account I would still recommend you revoke access afterwards by going to [GitHub Settings](https://github.com/settings/applications) and revoking access for "Hello GitHub OAuth".

## Expected Behaviour

* Click on "my secret" on the homepage.  
* You should be presented with Authentication page with a "Sign in using GitHub" button.
* After clicking on the button you will be asked if you want to "Authorise Hello GitHub OAuth" with a green "Authorize codybartfast" button.
* If you authorize the app you should be redirected to the 'secret' page.

## To Run This App Using My GitHub OAuth Registration

To authenticate using my OAuth registration run the following commands in the application root:

```lang-powershell
dotnet user-secrets set github:clientId "75dfc8f5ff877ffce7f2" --project HelloGithubOAuth.csproj
dotnet user-secrets set github:clientSecret "92a33c17e7c8c87ab69d0f63a66059b2daef8f84" --project HelloGithubOAuth.csproj
```

## To Run This App Using Your Own GitHub OAuth Registration

Visit [GitHub Developer Settings](https://github.com/settings/applications/new) to register a new OAuth application. Use `http://localhost:5291/` as the 'Homepage URL' and `http://localhost:5291/signin-github` as the 'Authorized callback URL'.  After registering your application you can view the 'Client ID'  of your registration and can create a Client Secret.  Configure the App to use your details using:

```lang-powershell
dotnet user-secrets set github:clientId "<your client id>" --project HelloGithubOAuth.csproj
dotnet user-secrets set github:clientSecret "<your client secret>" --project HelloGithubOAuth.csproj
```

## To Add GitHub OAuth To an Existing App

The base of this application was created using these commands:

```lang-powershell
dotnet new webapp -n HelloGithubOAuth
mv HelloGithubOAuth hello-github-oauth
cd hello-github-oauth

dotnet new page --name Signin --output Pages --namespace HelloGithubOAuth.Pages
dotnet new page --name Secret --output Pages --namespace HelloGithubOAuth.Pages
dotnet new gitignore
```

[This commit](https://github.com/codybartfast/hello-github-oauth/commit/2e0a2f05d5591e102ebf29fb8f09ad10aad33b1b) shows all the changes that were made to that base project to enable GitHub OAuth and add a secured ('secret') page.

Do not use the same `UserSecretId` in your own project file. Instead use this command to add a new User Secrets Id _before_ setting the ClientId and ClientSecret user secrets.

```lang-powershell
dotnet user-secrets init --project <Your Project Name>.csproj
```

For details see:

* [Khalid Abuhakmeh's article][article]
* [Stack Overflow](https://stackoverflow.com/questions/71740380/authentication-with-github-in-asp-net-core-6)

[article]: https://khalidabuhakmeh.com/github-openid-auth-aspnet-core-apps
