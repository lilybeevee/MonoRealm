using System;
using LilyInjection.Framework.API;

namespace LilyInjection.Framework.Impl{
    public class InjectionMapper : IInjectionMapper{
        private readonly Type _type;
        private readonly IInjector _injector;
        private Mapping _mapping;
        private object _value;

        public InjectionMapper(Type type, IInjector injector){
            _type = type;
            _injector = injector;
            _mapping = Mapping.TYPE;
        }

        public void AsSingleton(bool initializeImmediately = false) => ToSingleton(_type, initializeImmediately);
        public void ToSingleton<T>(bool initializeImmediately = false) => ToSingleton(typeof(T), initializeImmediately);

        public void ToSingleton(Type type, bool initializeImmediately = false){
            _mapping = Mapping.SINGLETON;
            _value = initializeImmediately
                ? _injector.Instantiate(type)
                : type;
        }

        public void ToType<T>() => ToType(typeof(T));

        public void ToType(Type type){
            _mapping = Mapping.TYPE;
            _value = type;
        }

        public void ToValue(object value){
            _mapping = Mapping.VALUE;
            _value = value;
        }

        public object GetInstance(){
            switch(_mapping){
                case Mapping.SINGLETON:
                    return (_value is Type type) ? _value = _injector.Instantiate(type) : _value;
                case Mapping.TYPE:
                    return _injector.Instantiate((Type)_value ?? _type);
                case Mapping.VALUE:
                    return _value;
                default:
                    return null;
            }
        }

        private enum Mapping{
            SINGLETON,
            TYPE,
            VALUE
        }
    }
}