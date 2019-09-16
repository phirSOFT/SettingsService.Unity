# Release Notes

In this release we moved to from Unity [`Unity`] to . Since unity has undergone a lot of internal changes, we had to reimplement several features.
Since the `DependencyAttribute` has been dropped in its old form we had to use a [workaround] to preseve the settings injection.
However you now have to add the `SettingsServiceContaienrExtension` to your unity container to retain the attributes working. You can do it by using

```c-sharp
 var container = new UnityContainer();
 container.AddNewExtension<SettingsServiceContainerExtension>();
```

As the [workaround] points out, this approach relies heavely on internals of unity, so we expect it to break.

[workaround]: https://github.com/unitycontainer/abstractions#109