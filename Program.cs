// Authentication based on code by Khalid Abuhakmeh:
// https://khalidabuhakmeh.com/github-openid-auth-aspnet-core-apps

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(o =>
        {
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(o =>
        {
            o.LoginPath = "/signin";
            o.LogoutPath = "/signout";
        })
        .AddGitHub(opts =>
        {
            opts.ClientId = builder.Configuration["github:clientId"]!;
            opts.ClientSecret = builder.Configuration["github:clientSecret"]!;
            opts.CallbackPath = "/signin-github";

            opts.Events.OnCreatingTicket += context =>
            {
                if (context.AccessToken is { })
                {
                    context.Identity?.AddClaim(
                        new Claim("access_token", context.AccessToken));
                }
                return Task.CompletedTask;
            };
        });

var app = builder.Build();

app.UseAuthentication();
app.MapGet("/signout", async ctx =>
    {
        await ctx.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = "/"
            });
    });


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
