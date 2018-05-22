using System;

namespace LilyInjection.Framework.API{
    public interface IContext{
        IContext Extend<T>() where T : IExtension;
        IContext Extend(Type type);

        IContext Config<T>() where T : IConfig;
        IContext Config(Type type);

        IInjector GetInjector();
    }
}