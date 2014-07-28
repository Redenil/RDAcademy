namespace RDAcademy.ActionFilter
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using RDAcademy.DAL;

    public class BasicAuthorizedAttribute : AuthorizationFilterAttribute
    {
        private IndividualContext db = new IndividualContext();

        /// <summary>
        /// Calls when a process requests authorization.
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using <see cref="T:System.Web.Http.Filters.AuthorizationFilterAttribute" />.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Case that user is authenticated using forms authentication
            // so no need to check header for basic authentication.
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credArray = GetCredentials(authHeader);
                    var firstname = credArray[0];
                    var lastname = credArray[1];

                    // You can use Websecurity or asp.net memebrship provider to login, for
                    // for he sake of keeping example simple, we used out own login functionality
                    var isIndividuExist = db.Individuals.Any(
                        x => x.FirstName == firstname && x.LastName == lastname);
                    if (isIndividuExist)
                    {
                        var currentPrincipal = new GenericPrincipal(new GenericIdentity(firstname), null);
                        Thread.CurrentPrincipal = currentPrincipal;
                        return;
                    }

                }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        /// <summary>
        /// The get credentials.
        /// </summary>
        /// <param name="authHeader">The auth header.</param>
        /// <returns>
        /// The <see cref="string[]" />.
        /// </returns>
        private string[] GetCredentials(AuthenticationHeaderValue authHeader)
        {
            // Base 64 encoded string
            var rawCred = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var cred = encoding.GetString(Convert.FromBase64String(rawCred));

            var credArray = cred.Split(':');

            return credArray;
        }

        /// <summary>
        /// The is resource owner.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsResourceOwner(string userName, HttpActionContext actionContext)
        {
            var routeData = actionContext.Request.GetRouteData();
            var resourceUserName = routeData.Values["userName"] as string;

            if (resourceUserName == userName)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Handles the unauthorized request.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        private void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add(
                "WWW-Authenticate", "Basic Scheme='eLearning' location='http://localhost:8323/account/login'");
        }
    }
}
