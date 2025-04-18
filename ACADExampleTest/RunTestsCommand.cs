using System.IO;
using System.Reflection;
using ACADExampleTest;
using Autodesk.AutoCAD.Runtime;
using TestRunnerACAD;

[assembly: CommandClass(typeof(RunTestsCommand))]

namespace ACADExampleTest
{
    public class RunTestsCommand 
    {
        [CommandMethod("RunTests", CommandFlags.Session)]
        public void RunTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            TestUtils.Run(assembly);
        }
    }
}