using System;

namespace LilyInjection.Framework.API{
    public interface IInjectionMapper{
        void AsSingleton(bool initializeImmediately = false);
        void ToSingleton<T>(bool initializeImmediately = false);
        void ToSingleton(Type type, bool initializeImmediately = false);
        void ToType<T>();
        void ToType(Type type);
        void ToValue(object value);

        object GetInstance();
    }
}