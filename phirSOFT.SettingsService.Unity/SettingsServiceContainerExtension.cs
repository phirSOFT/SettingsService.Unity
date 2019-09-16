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

            foreach (MemberProcessor memberProcessor in strategies)
            {
                switch (memberProcessor)
                {
                    case ConstructorProcessor constructorProcessor:
                        constructorProcessor.Add(typeof(SettingValueAttribute), _ => null, SettingResolutionFactory);
                        break;
                    case PropertyProcessor propertyProcessor:
                        propertyProcessor.Add(typeof(SettingValueAttribute), _ => null, SettingResolutionFactory);
                        break;
                }
            }
        }

        private ResolveDelegate<BuilderContext> SettingResolutionFactory(Attribute attribute, object info, object value)
        {
            Type type = null;
            if (info is ParameterInfo parameterInfo)
            {
                type = parameterInfo.ParameterType;
            }
            else if (info is PropertyInfo propertyInfo)
            {
                type = propertyInfo.PropertyType;
            }

            return (ref BuilderContext context) =>
            {
                if (!(attribute is SettingValueAttribute settingValueAttribute) || type == null)
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
