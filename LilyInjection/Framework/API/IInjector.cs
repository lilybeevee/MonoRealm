using System;

namespace LilyInjection.Framework.API{
    public interface IInjector{
        IInjectionMapper Map<T>();
        IInjectionMapper Map(Type type);

        bool HasMapping<T>();
        bool HasMapping(Type type);
        IInjectionMapper GetMapping<T>();
        IInjectionMapper GetMapping(Type type);

        T GetInstance<T>();
        object GetInstance(Type type);

        object InjectInto(object obj);

        T Instantiate<T>();
        object Instantiate(Type type);
    }
}