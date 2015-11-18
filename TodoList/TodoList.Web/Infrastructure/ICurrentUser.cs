using System.Security.Cryptography.X509Certificates;

namespace TodoList.Web.Infrastructure
{
    public interface ICurrentUser
    {
        string UserId { get; }
    }
}