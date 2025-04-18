/*
 * CommonAssemblyInfo.cs - 解决方案共享程序集信息
 * 
 * 功能：
 * 1. 为所有项目提供统一的版本号和程序集信息
 * 2. 使用通配符版本号(1.0.*)自动增加内部版本号
 * 3. 集中管理版权和公司信息
 * 
 * 用法：
 * 在各项目的.csproj文件中通过Link引用此文件：
 * <Compile Include="$(SolutionDir)CommonAssemblyInfo.cs">
 *   <Link>Properties\CommonAssemblyInfo.cs</Link>
 * </Compile>
 */

using System.Reflection;
using System.Runtime.InteropServices;

// General Information about the solution
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AutoCAD_UnitTest")]
[assembly: AssemblyCopyright("Copyright © 2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.
[assembly: ComVisible(false)]

// Version information
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")] 