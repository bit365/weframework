using Microsoft.Practices.Unity;

namespace WeFramework.Core.Infrastructure
{
    public interface IDependencyRegister
    {
        void RegisterTypes(IUnityContainer container);
    }
}
