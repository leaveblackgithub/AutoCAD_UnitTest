using System.IO;

namespace TestRunnerACAD
{
    /// <summary>
    /// Utility class for managing file paths
    /// </summary>
    public class PathManager
    {
        // Constants for file paths
        public const string ReportToolFolderName = @"ExtentReports";
        public const string ReportNunitXml = @"Report-NUnit.xml";
        public const string ReportOutputHtml = @"index.html";
        public const string ReportToolFileName = @"ExtentReports.exe";

        private readonly string _testAssemblyPath;

        /// <summary>
        /// Initializes a new instance of the PathManager class
        /// </summary>
        /// <param name="testAssemblyPath">Path to the test assembly</param>
        public PathManager(string testAssemblyPath)
        {
            _testAssemblyPath = testAssemblyPath;
        }

        /// <summary>
        /// Gets the path to the plugin directory
        /// </summary>
        /// <returns>The full path to the plugin directory</returns>
        public string GetAssemblyDirectory()
        {
            return Path.GetDirectoryName(_testAssemblyPath);
        }

        /// <summary>
        /// Gets the path to the report directory
        /// </summary>
        /// <param name="createIfNotExists">If true, creates the directory if it doesn't exist</param>
        /// <returns>The full path to the report directory</returns>
        public string GetReportDirectory(bool createIfNotExists = true)
        {
            string pluginDir = GetAssemblyDirectory();
            if (pluginDir == null)
                return null;

            string reportDir = Path.Combine(pluginDir, ReportToolFolderName);
            
            if (createIfNotExists && !Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);
                
            return reportDir;
        }

        /// <summary>
        /// Gets the path to the NUnit XML report file
        /// </summary>
        /// <returns>The full path to the NUnit XML report file</returns>
        public string GetNUnitXmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (reportDir == null)
                return null;
                
            return Path.Combine(reportDir, ReportNunitXml);
        }

        /// <summary>
        /// Gets the path to the HTML output report file
        /// </summary>
        /// <returns>The full path to the HTML output report file</returns>
        public string GetHtmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (reportDir == null)
                return null;
                
            return Path.Combine(reportDir, ReportOutputHtml);
        }

        /// <summary>
        /// Gets the path to the report generator tool
        /// </summary>
        /// <returns>The full path to the report generator executable</returns>
        public string GetReportGeneratorPath()
        {
            string pluginDir = GetAssemblyDirectory();
            if (pluginDir == null)
                return null;
                
            return Path.Combine(pluginDir, ReportToolFolderName, ReportToolFileName);
        }
    }
} 