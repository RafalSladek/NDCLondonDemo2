using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Thinktecture.IdentityModel.Tokens.Http;

namespace NDCLondonDemo2
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Routes.MapHttpRoute(
                name: "NDCApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var authnConfig = new AuthenticationConfiguration();
            authnConfig.SendWwwAuthenticateResponseHeaders = false;
            
            // TODO: Call into your identity store ...
            authnConfig.AddBasicAuthentication((un, pw) => un == pw);

            config.MessageHandlers.Add(new AuthenticationHandler(authnConfig));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}