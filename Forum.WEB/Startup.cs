using System;
using System.Threading.Tasks;
using Forum.WEB.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Forum.WEB.Startup))]

namespace Forum.WEB
{
    /// <summary>
    /// Owin start up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration application owin
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions
            {
                //URL in which client take token
                TokenEndpointPath = new PathString("/token"),
                //Custom provider authorization
                Provider = new ApplicationOAuthProvider(),
                //The expiration time of the access token 
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
                AllowInsecureHttp = true
            };
            //Register OAuthAuthorizationServerOptions 
            app.UseOAuthAuthorizationServer(option);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
