# SettingsService.Unity

This repository contains the source code for the [`phirSOFT.SettingsService.Unity`](https://www.nuget.org/packages/phirSOFT.SettingsService.Unity) package.
This package allows you to inject settings of a settings service via unity. The source has been forked out from the [SettingsService](https://github.com/phirSOFT/SettingsService) repository to decouple the version numbers of the packages.

## Example

You can inject settings as properties or constructor arguments using the `SettingsValueAttribute`. The attribute takes the key of the setting as mandatory parameter. You can optional specify the named instance to use and the type of the setting. If no type is specified the settings type is inferred by the parameter resp. proptery type.

    class SampleClass
    {
        // inject the user.names property
        public SampleClass([SettingValue("user.names")] IEnumerable<string> usernames)
        {
            // do something with usernames
        }
    }
