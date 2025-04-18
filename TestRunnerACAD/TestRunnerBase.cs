using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnitLite;

namespace TestRunnerACAD
{
    public class TestRunnerBase
    {
        public void RunTestsBase(Assembly assembly, string directoryPlugin)
        {
            if (directoryPlugin == null)
                return;

            string reportDir = PathManager.GetReportDirectory();
            string xmlReportPath = PathManager.GetNUnitXmlReportPath();
            
            // 删除现有的测试报告文件
            if (File.Exists(xmlReportPath))
                File.Delete(xmlReportPath);
                
            // 设置NUnit参数，包括输出XML结果
            var nunitArgs = new List<string>
            {
                "--trace=verbose", "--result=" + xmlReportPath
            }.ToArray();

            // 运行测试
            new AutoRun(assembly).Execute(nunitArgs);
            
            // 生成HTML测试报告
            PathManager.GenerateReport();
        }
    }
}