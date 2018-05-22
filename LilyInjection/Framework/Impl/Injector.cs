using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LilyInjection.Framework.API;

namespace LilyInjection.Framework.Impl{
    public class Injector : IInjector{
        private readonly Dictionary<Type, IInjectionMapper> _mappers;

        public Injector(){
            _mappers = new Dictionary<Type, IInjectionMapper>();
        }

        #region Type Parameter Alternatives
        public IInjectionMapper Map<T>() => Map(typeof(T));
        public bool HasMapping<T>() => HasMapping(typeof(T));
        public IInjectionMapper GetMapping<T>() => GetMapping(typeof(T));
        public T GetInstance<T>() => (T)GetInstance(typeof(T));
        public T Instantiate<T>() => (T)Instantiate(typeof(T));
        #endregion

        public IInjectionMapper Map(Type type){
            var mapper = GetMapping(type);
            if(mapper != null)
                return mapper;
            _mappers.Add(type, (mapper = new InjectionMapper(type, this)));
            return mapper;
        }

        public bool HasMapping(Type type) => _mappers.ContainsKey(type);

        public IInjectionMapper GetMapping(Type type){
            if(!HasMapping(type))
                return null;
            _mappers.TryGetValue(type, out var mapper);
            return mapper;
        }

        public object GetInstance(Type type) => GetMapping(type)?.GetInstance();

        public object InjectInto(object obj){
            foreach(var i in Inspect(obj.GetType()))
                i.SetValue(obj, GetInstance(i.FieldType));
            return obj;
        }

        public object Instantiate(Type type){
            var constructors = type.GetConstructors();
            foreach(var constructor in constructors){
                var parameters = constructor.GetParameters();
                if(!parameters.All(parameter => HasMapping(parameter.ParameterType)))
                    continue;
                var injectParams = new object[parameters.Length];
                for(var i = 0; i < parameters.Length; i++)
                    injectParams[i] = GetInstance(parameters[i].ParameterType);
                return InjectInto(constructor.Invoke(injectParams));
            }
            return null;
        }

        private static IEnumerable<FieldInfo> Inspect(Type type) =>
            type.GetFields().Where(_ => _.IsDefined(typeof(Inject)));
    }
}