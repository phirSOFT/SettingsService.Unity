using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.SettingsService.Abstractions;
using Unity;
using Unity.Injection;
using Unity.Registration;

namespace phirSOFT.SettingsService.Unity.Test
{
    [TestFixture]
    public class TestClass
    {
      
        [Test]
        public void TestProperty()
        {
            UnityContainer container = CreateContainer();

            var instance = container.Resolve<TestSampleClass>();

            Assert.AreEqual("Test1", instance.Test1);
        }

        [Test]
        public void TestConstructor()
        {
            UnityContainer container = CreateContainer();

            var instance = container.Resolve<TestSampleClass>();

            Assert.AreEqual("test", instance.Test);
        }

        private static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<SettingsServiceContainerExtension>();
            container.RegisterType<IReadOnlySettingsService, ISettingsService>();
            container.RegisterType<ISettingsService, CallResponseService>();
            return container;
        }
    }

    internal class TestSampleClass
    {
        [SettingValue("Test1")]
        public string Test1 { get; set; }

        public string Test { get; }

        public TestSampleClass([SettingValue("test")]string test)
        {
            Test = test;
        }
    }
}
