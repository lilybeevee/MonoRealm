using LilyInjection.Framework.API;

namespace MonoRealm{
    public class TestExtension : IExtension{
        public void Extend(IContext context){
            context.GetInjector().Map<string>().ToValue("test value");
        }
    }
}