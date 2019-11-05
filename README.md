# TestSqlClient for netstandard2.0

[SqlClient](https://github.com/fsprojects/FSharp.Data.SqlClient/issues?page=1&q=is%3Aissue+is%3Aopen) cannot build as a netstandard2.0 project with the dotnet SDK build, but does build in Visual Studio 16.3.8. I have tried Visual Studio 16.4 previews, and they do not build.

The errors and warinings vary by SDK version.

Also possibly significant, ```paket install``` produces this warning:

```
Could not detect any platforms from 'typeproviders' in E:\GitRepos\TestSqlClient\packages\FSharp.Data.SqlClient\lib\typeproviders\fsharp41\net461\FSharp.Data.SqlClient.DesignTime.dll', please tell the package authors
```

## 2.1.301

```
Warning: E:\GitRepos\TestSqlClient\.paket\Paket.Restore.targets(154,5): warning : This version of MSBuild (we assume '15.7.179' or older) doesn't support GetFileHash, so paket fast restore is disabled.
Fsc: FSC(0,0): warning FS3005: Referenced assembly 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' has assembly level attribute 'Microsoft.FSharp.Core.CompilerServices.TypeProviderAssemblyAttribute' but no public type provider classes were found
Fsc: FSC(0,0): error FS3031: The type provider 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' reported an error: Assembly attribute 'TypeProviderAssemblyAttribute' refers to a designer assembly 'FSharp.Data.SqlClient.DesignTime.dll' which cannot be loaded or doesn't exist. Could not load file or assembly 'FSharp.Core, Version=4.7.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified.
```

## 2.1.800

```
FSC : error FS3031: The type provider 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' reported an error: Assembly attribute 'TypeProviderAssemblyAttribute' refers to a designer assembly 'FSharp.Data.SqlClient.DesignTime.dll' which cannot be loaded or doesn't exist. Could not load file or assembly 'FSharp.Core, Version=4.7.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified. [E:\GitRepos\TestSqlClient\src\NovaData\NovaData.fsproj]
FSC : warning FS3005: Referenced assembly 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' has assembly level attribute 'Microsoft.FSharp.Core.CompilerServices.TypeProviderAssemblyAttribute' but no public type provider classes were found [E:\GitRepos\TestSqlClient\src\NovaData\NovaData.fsproj]
```

## 2.2.402

```
Fsc: FSC(0,0): warning FS3005: Referenced assembly 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' has assembly level attribute 'Microsoft.FSharp.Core.CompilerServices.TypeProviderAssemblyAttribute' but no public type provider classes were found
Fsc: FSC(0,0): error FS3031: The type provider 'C:\Users\Jack\.nuget\packages\fsharp.data.sqlclient\2.0.6\lib\netstandard2.0\FSharp.Data.SqlClient.dll' reported an error: Assembly attribute 'TypeProviderAssemblyAttribute' refers to a designer assembly 'FSharp.Data.SqlClient.DesignTime.dll' which cannot be loaded or doesn't exist. Could not load file or assembly 'FSharp.Core, Version=4.7.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified.
```

## 3.0.100

```
E:\GitRepos\TestSqlClient\src\NovaData\Proxy.fs(9,24): error FS3033: The type provider 'FSharp.Data.SqlCommandProvider' reported an error: Could not load file or assembly 'System.Data.SqlClient, Version=4.4.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. Reference assemblies should not be loaded for execution.  They can only be loaded in the Reflection-only loader context. (0x80131058) [E:\GitRepos\TestSqlClient\src\NovaData\NovaData.fsproj]
```
