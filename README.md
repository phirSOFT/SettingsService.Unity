# SettingsService.Unity

This repository contains the source code for the
[`phirSOFT.SettingsService.Unity`](https://www.nuget.org/packages/phirSOFT.SettingsService.Unity)
package. This package allows you to inject settings of a settings service via
unity. The source has been forked out from the
[SettingsService](https://github.com/phirSOFT/SettingsService) repository to
decouple the version numbers of the packages.

## Example

You can use your Unity container to resolve settings as an injectable
dependency. To do this you first have to add the
`SettingsServiceContainerExtension` to your unity container. Then you have
register the `IReadOnlySettingsService` interface with your actual settings
service.

```c-sharp
public IUnityContaier CreateUnityContainer()
{
	 var container = new UnityContainer();
     container.AddNewExtension<SettingsServiceContainerExtension>();
	 container.RegisterSingleton<IReadOnlySettingsService, MySettingsService>();
}
```

Then you can reveice your settings via the `SettingsValueAttribute`. This workls
for constructors and properties. The attribute takes the key of the setting as
mandatory parameter. You can optional specify the named instance to use and the
type of the setting. If no type is specified the settings type is inferred by
the parameter resp. proptery type.

```c-sharp
class SampleClass
{
    // inject the user.names property
    public SampleClass([SettingValue("user.names")] IEnumerable<string> usernames)
    {
        // do something with usernames
    }
}
```
