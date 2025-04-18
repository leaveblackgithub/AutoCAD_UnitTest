using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnitLite;

namespace TestRunnerACAD
{
    public class TestRunnerBase
    {
        public void RunTestsBase(Assembly testAssembly)
        {
            
            // 创建PathManager实例，传入程序集路径
            var assemblyLocation = testAssembly.Location;
            var reportGenerator = new ReportGenerator(assemblyLocation);

            string xmlReportPath = reportGenerator.NunitXmlPath;

            reportGenerator.CleanNunitXml();

            // 设置NUnit参数，包括输出XML结果
            var nunitArgs = new List<string>
            {
                "--trace=verbose", "--result=" + xmlReportPath
            }.ToArray();

            // 运行测试
            new AutoRun(testAssembly).Execute(nunitArgs);
            
            // 生成HTML测试报告
            //The extentreports-dotnet-cli deprecates ReportUnit. Can only define output folder and export to default index.html
            reportGenerator.CreateHtmlReport();
            reportGenerator.OpenHtmlReport();
        }
    }
}