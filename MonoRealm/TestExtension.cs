using LilyInjection.Framework.API;

namespace MonoRealm{
    public class TestExtension : IExtension{
        public void Extend(IContext context){
            context.GetInjector().Map<string>().ToValue("mama mia");
            context.GetInjector().Map<int>().ToValue(22);
        }
    }
}