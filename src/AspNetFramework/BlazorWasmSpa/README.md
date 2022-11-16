# Blazor WASM SPA

Based on [Blaze Orbital](https://github.com/SteveSandersonMS/BlazeOrbital)

**Currently Broken WIP**
I plan to come back to this when some more official docs are released

### Uses Native Sqlite 
By referencing `SQLitePCLRaw.bundle_e_sqlite3` and setting `<WasmBuildNative>true</WasmBuildNative>`
(which requires the wasm-tools workload) the app is able to run a native sqlite
instance in the browser. 

This is configured with persistant storage via `DbContextInitializer.cs`

### Single Threaded
Multi threading may be coming in .NET 8. 
I intend to update this project when that becomes available. 

Single threaded means contention between UI responsiveness and db writes.

