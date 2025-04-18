Forked from puppetsw/AutoCAD_UnitTest

# Note:
Referencing TestRunnerACAD dll directly will cause exceptions, ACADExampleTest can only work with referencing TestRunnerACAD project.

# Updated Sep 14, 2023
1. Add Supports to AutoCAD 2019
2. The extentreports-dotnet-cli deprecates ReportUnit. Use extent-framework / extentreports-dotnet-cli to replace ReportUnit. https://github.com/extent-framework/extentreports-dotnet-cli
3. Add Trustedpaths in scr file.
4. Move example tests to separate project to isolate changes.

# AutoCAD Test Runner

An AutoCAD test runner using the NUnitLite framework. 

Inspired by the work done by CADBloke. https://github.com/CADbloke/CADtest

## Running

Update the TestLoader.scr file with your project output directory.

```
netload "C:\Users\scott\RiderProjects\AutoCAD_UnitTest\TestRunnerACAD\bin\Debug\TestRunnerACAD.dll"
RunTests
```

Make sure to add your project output directory to AutoCAD's trusted folders. Also add references to acdbmgd.dll etc.

Set your run configuration as follows...

![image](https://user-images.githubusercontent.com/79826944/144696304-57bf5f8e-4461-47e7-8d63-6b443cc81363.png)

Tested on AutoCAD 2017.

## Reference Settings and Debug Configurations

### AutoCAD References Configuration

When setting up references to AutoCAD libraries, follow these guidelines:

1. **Core References** - Required for all configurations:
   - `AcCoreMgd.dll`: Core AutoCAD managed API
   - `AcDbMgd.dll`: Database management API

2. **UI References** - Only required for the `DebugInApp` configuration:
   - `acmgd.dll`: AutoCAD application UI API
   - `AdWindows.dll`: AutoCAD interface components

3. **Reference Settings**:
   - Always set `Private=False` for all AutoCAD references to prevent unnecessary copying of large DLLs
   - Use `SpecificVersion=False` to avoid version conflicts
   - Ensure correct casing in file paths (e.g., `acmgd.dll` not `AcMgd.dll`)

```xml
<!-- Example reference configuration -->
<Reference Include="AcCoreMgd">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>C:\Program Files\Autodesk\AutoCAD 2019\AcCoreMgd.dll</HintPath>
  <Private>False</Private>
</Reference>
```

### Debug Configurations

The solution supports two debugging modes:

1. **DebugInApp**: Runs tests within the full AutoCAD application
   - Provides complete UI support and interactive editing capabilities
   - Start program: `C:\Program Files\Autodesk\AutoCAD 2019\acad.exe`
   - Access to all AutoCAD features via `acmgd.dll`
   - Recommended for tests that require UI interaction or visual verification

2. **DebugInAcCoreConsle**: Runs tests in the headless AcCoreConsole environment
   - Faster execution, suitable for automated testing
   - Start program: `C:\Program Files\Autodesk\AutoCAD 2019\accoreconsole.exe`
   - Requires a script file (TestLoader.scr) to automate loading and execution
   - Limited to non-UI functionality
   - Ideal for continuous integration and database-only operations

### Conditional Compilation

The solution uses conditional compilation symbols to differentiate between environments:

- `IN_APP`: Defined in the DebugInApp configuration
- `IN_ACCORE`: Defined in the DebugInAcCoreConsle configuration

Use these symbols to include/exclude code based on the running environment:

```csharp
#if IN_ACCORE
// AcCoreConsole-specific code
#else
// Full AutoCAD application code
#endif
```

### Best Practices

1. **Namespace Organization**:
   - Place full AutoCAD application-specific tests in a separate namespace (e.g., `TestInApp`)
   - Use `TestUtils.Run(assembly, "ExcludedNamespace")` to exclude these tests when running in AcCoreConsole

2. **Unified Test Methods**:
   - Use `TestUtils.ExecuteInAny()` for tests that should work in both environments
   - It automatically selects the appropriate execution method based on the current environment

3. **Drawing Files**:
   - Always provide explicit drawing file paths in AcCoreConsole tests
   - Use default drawing files for simple tests

4. **Reports and Output**:
   - Test reports are automatically generated after test execution
   - Check the output directory for HTML reports with detailed results
