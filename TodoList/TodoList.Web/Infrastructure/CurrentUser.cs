using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace TodoList.Web.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IIdentity _identity;

        public CurrentUser(IIdentity identity)
        {
            _identity = identity;
        }

        public string UserId
        {
            get { return _identity.GetUserId(); }
        }
    }
}