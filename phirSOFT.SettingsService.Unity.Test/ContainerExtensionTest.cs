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
using Unity.Lifetime;
using Unity.Registration;

namespace phirSOFT.SettingsService.Unity.Test
{
    [TestFixture]
    public class ContainerExtensionTest
    {

        [Test]
        public void TestProperty()
        {
            using (UnityContainer container = CreateContainer())
            {
                var instance = container.Resolve<TestSampleClass>();
                Assert.AreEqual("Test1", instance.Test1);
            }
        }

        [Test]
        public void TestConstructor()
        {
            using (UnityContainer container = CreateContainer())
            {
                var instance = container.Resolve<TestSampleClass>();
                Assert.AreEqual("test", instance.Test);
            }
        }

        [Test]
        public static void TestContainerCreation()
        {
            using (var container = CreateContainer())
            {
                Assert.DoesNotThrow(() => container.Resolve<CallResponseService>());
                Assert.DoesNotThrow(() => container.Resolve<ISettingsService>());
                Assert.DoesNotThrow(() => container.Resolve<IReadOnlySettingsService>());
            }
        }

        private static UnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            container.AddNewExtension<SettingsServiceContainerExtension>();
            container.RegisterType<ISettingsService, CallResponseService>(new SingletonLifetimeManager());
            container.RegisterType<IReadOnlySettingsService, ISettingsService>(new SingletonLifetimeManager());
            return container;
        }
    }
}
