using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using phirSOFT.SettingsService;
using phirSOFT.SettingsService.Abstractions;

namespace phirSOFT.SettingsService.Unity.Test
{
internal class CallResponseService : ISettingsService
{
    public Task<object> GetSettingAsync(string key, Type type)
    {
        if (type != typeof(string))
            return Task.FromResult(type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null);
        return Task.FromResult((object) key);
    }

    public Task<bool> IsRegisterdAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task SetSettingAsync(string key, object value, Type type)
    {
        throw new NotImplementedException();
    }

    public Task RegisterSettingAsync(string key, object defaultValue, object initialValue, Type type)
    {
        throw new NotImplementedException();
    }

    public Task UnregisterSettingAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task StoreAsync()
    {
        throw new NotImplementedException();
    }

    public Task DiscardAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsRegisteredAsync(string key)
    {
        throw new NotImplementedException();
    }
}
}
