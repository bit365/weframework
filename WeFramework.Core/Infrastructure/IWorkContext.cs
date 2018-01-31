using WeFramework.Core.Domain.Users;

namespace WeFramework.Core.Infrastructure
{
    public interface IWorkContext
    {
        User CurrentUser { get; }
    }
}
