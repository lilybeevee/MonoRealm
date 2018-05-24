using System;
using LilyInjection.Framework.API;
using LilyInjection.Framework.Impl;

namespace MonoRealm{
    public class TestConfig : IConfig{
        [Inject] public string testString;

        public TestConfig(int testInt){
            Console.WriteLine(testInt);
        }

        public void Configure(){
            Console.WriteLine(testString);
        }
    }
}