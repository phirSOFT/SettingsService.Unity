namespace phirSOFT.SettingsService.Unity.Test
{
internal class TestSampleClass
{
    [SettingValue("Test1")]
    public string Test1 {
        get;
        set;
    }

    public string Test {
        get;
    }

    public TestSampleClass([SettingValue("test")]string test)
    {
        Test = test;
    }
}
}
