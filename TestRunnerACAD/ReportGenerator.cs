using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TestRunnerACAD
{
    /// <summary>
    /// Utility class for managing file paths and report operations
    /// </summary>
    public static class PathManager
    {
        /// <summary>
        /// Gets the path to the plugin directory
        /// </summary>
        /// <returns>The full path to the plugin directory</returns>
        public static string GetPluginDirectory()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(assemblyLocation);
        }

        /// <summary>
        /// Gets the path to the report directory
        /// </summary>
        /// <param name="createIfNotExists">If true, creates the directory if it doesn't exist</param>
        /// <returns>The full path to the report directory</returns>
        public static string GetReportDirectory(bool createIfNotExists = true)
        {
            string pluginDir = GetPluginDirectory();
            if (pluginDir == null)
                return null;

            string reportDir = Path.Combine(pluginDir, TestRunnerConsts.ReportToolFolderName);
            
            if (createIfNotExists && !Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);
                
            return reportDir;
        }

        /// <summary>
        /// Gets the path to the NUnit XML report file
        /// </summary>
        /// <returns>The full path to the NUnit XML report file</returns>
        public static string GetNUnitXmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (reportDir == null)
                return null;
                
            return Path.Combine(reportDir, TestRunnerConsts.ReportNunitXml);
        }

        /// <summary>
        /// Gets the path to the HTML output report file
        /// </summary>
        /// <returns>The full path to the HTML output report file</returns>
        public static string GetHtmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (reportDir == null)
                return null;
                
            return Path.Combine(reportDir, TestRunnerConsts.ReportOutputHtml);
        }

        /// <summary>
        /// Gets the path to the report generator tool
        /// </summary>
        /// <returns>The full path to the report generator executable</returns>
        public static string GetReportGeneratorPath()
        {
            string pluginDir = GetPluginDirectory();
            if (pluginDir == null)
                return null;
                
            return Path.Combine(pluginDir, TestRunnerConsts.ReportToolFolderName, TestRunnerConsts.ReportToolFileName);
        }

        /// <summary>
        /// Generate and open report based on NUnit test results
        /// </summary>
        public static void GenerateReport()
        {
            string nunitXmlPath = GetNUnitXmlReportPath();
            if (!File.Exists(nunitXmlPath))
                return;
                
            string htmlReportPath = GetHtmlReportPath();
            if (File.Exists(htmlReportPath))
                File.Delete(htmlReportPath);
                
            string reportDir = GetReportDirectory();
            string generatorPath = GetReportGeneratorPath();
            
            //The extentreports-dotnet-cli deprecates ReportUnit. Can only define output folder and export to default index.html
            CreateHtmlReport(nunitXmlPath, reportDir, generatorPath);
            OpenHtmlReport(htmlReportPath);
        }

        /// <summary>
        /// Opens a HTML report with the default viewer.
        /// </summary>
        /// <param name="fileName">Path to the HTML report file</param>
        public static void OpenHtmlReport(string fileName)
        {
            if (!File.Exists(fileName))
                return;
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.FileName = fileName;
                process.Start();
            }
        }

        /// <summary>
        /// Creates a HTML report based on the NUnit XML report.
        /// </summary>
        /// <param name="inputFile">The NUnit XML file.</param>
        /// <param name="outputFolder">The output HTML report file.</param>
        /// <param name="reportUnitPath">Path to the ReportUnit executable.</param>
        public static void CreateHtmlReport(string inputFile, string outputFolder, string reportUnitPath)
        {
            if (!File.Exists(inputFile))
                return;

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = reportUnitPath;
                //extent -i results/nunit.xml -o results/ -r v3html
                process.StartInfo.Arguments = $" \"-i\" \"{inputFile}\" \"-o\" \"{outputFolder}\" \"-r\" \"v3html\"";

                process.Start();
                process.WaitForExit();
            }
        }
    }
} 