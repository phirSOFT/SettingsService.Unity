using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nito.AsyncEx;
using phirSOFT.SettingsService.Abstractions;
using Unity.Builder;
using Unity.Extension;
using Unity.Processors;
using Unity.Resolution;

namespace phirSOFT.SettingsService.Unity
{
    public class SettingsServiceContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var strategies = (IEnumerable<MemberProcessor>)Context.BuildPlanStrategies;
            var processor = (ConstructorProcessor)strategies.First(s => s is ConstructorProcessor);
            processor.Add(typeof(SettingValueAttribute), _ => null, SettingResolutionFactory);
        }

        private ResolveDelegate<BuilderContext> SettingResolutionFactory(Attribute attribute, object info, object value)
        {
            var type = ((ParameterInfo)info).ParameterType;
            return (ref BuilderContext context) =>
            {
                if (!(attribute is SettingValueAttribute settingValueAttribute))
                    return value;

                IReadOnlySettingsService readOnlySettingsService = (IReadOnlySettingsService)
                    context.Resolve(
                        typeof(IReadOnlySettingsService),
                        ((SettingValueAttribute)attribute).ServiceInstance);
                return AsyncContext.Run(
                    () => readOnlySettingsService.GetSettingAsync(settingValueAttribute.SettingKey, settingValueAttribute.SettingType ?? type));

            };
        }
    }
}
