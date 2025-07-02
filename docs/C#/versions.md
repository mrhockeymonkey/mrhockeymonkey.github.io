# Versions

When buildin you can pass `/p:Version=${version}"` to have it burnt into the binary and then access it at runtime with 

```cs
public static string GetSemVer(this Assembly? assembly) =>
        assembly?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "0.0.0";
```
