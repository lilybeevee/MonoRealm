using System;
using LilyInjection.Framework.API;

namespace LilyInjection.Framework.Impl{
    public class Context : IContext{
        protected IInjector Injector;

        public Context(IInjector injector){
            Injector = injector;
            Injector.Map<IContext>().ToValue(this);
            Injector.Map<IInjector>().ToValue(Injector);
        }

        public IContext Extend<T>() where T : IExtension => Extend(typeof(T));

        public IContext Extend(Type type){
            ((IExtension)Injector.Instantiate(type)).Extend(this);
            return this;
        }

        public IContext Config<T>() where T : IConfig => Config(typeof(T));

        public IContext Config(Type type){
            ((IConfig)Injector.Instantiate(type)).Configure();
            return this;
        }

        public IInjector GetInjector() => Injector;
    }
}