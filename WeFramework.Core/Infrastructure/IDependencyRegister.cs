using Unity.Lifetime;
using Unity;

namespace WeFramework.Core.Infrastructure
{
    public interface IDependencyRegister
    {
        void RegisterTypes(IUnityContainer container);
    }
}
