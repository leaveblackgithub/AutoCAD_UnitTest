using System;
using System.IO;
using System.Configuration;

namespace TestRunnerACAD
{
    /// <summary>
    /// Utility class for managing file paths
    /// </summary>
    public class PathManager
    {
        // Constants for file paths
        public const string ReportToolFolderName = "ExtentReports";
        public const string ReportNunitXml = "Report-NUnit.xml";
        public const string ReportOutputHtml = "index.html";
        public const string ReportToolFileName = "ExtentReports.exe";
        
        // 配置文件名
        private const string CONFIG_FILE_NAME = "paths.config";
        
        // 用于回退的默认路径
        private const string DEFAULT_PATH = @"D:\leaveblackgithub\AutoCAD_UnitTest\bin\Debug";
        
        // 从配置文件中读取输出路径
        private string GetConfiguredOutputPath()
        {
            try
            {
                // 配置文件路径 - 使用相对于程序运行目录的路径
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CONFIG_FILE_NAME);
                
                // 如果配置文件存在，直接读取
                if (File.Exists(configPath))
                {
                    try
                    {
                        // 创建配置映射
                        var configFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
                        var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                        
                        if (config.AppSettings.Settings["OutputPath"] != null)
                        {
                            return config.AppSettings.Settings["OutputPath"].Value;
                        }
                    }
                    catch { /* 忽略配置文件读取错误 */ }
                }
                
                // 尝试加载应用程序配置
                try
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    if (appSettings["OutputPath"] != null)
                    {
                        return appSettings["OutputPath"];
                    }
                }
                catch { /* 忽略错误 */ }
                
                // 如果无法读取配置，返回默认路径
                return DEFAULT_PATH;
            }
            catch (Exception)
            {
                // 出现异常时返回默认路径
                return DEFAULT_PATH;
            }
        }
        
        public PathManager() { }
        
        public string GetAssemblyDirectory()
        {
            return GetConfiguredOutputPath();
        }
        
        public string GetReportDirectory(bool createIfNotExists = true)
        {
            string pluginDir = GetAssemblyDirectory();
            if (string.IsNullOrEmpty(pluginDir))
                return null;
                
            string reportDir = Path.Combine(pluginDir, ReportToolFolderName);
            if (createIfNotExists && !Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);
                
            return reportDir;
        }
        
        public string GetNUnitXmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (string.IsNullOrEmpty(reportDir))
                return null;
                
            return Path.Combine(reportDir, ReportNunitXml);
        }
        
        public string GetHtmlReportPath()
        {
            string reportDir = GetReportDirectory();
            if (string.IsNullOrEmpty(reportDir))
                return null;
                
            return Path.Combine(reportDir, ReportOutputHtml);
        }
        
        public string GetReportGeneratorPath()
        {
            string pluginDir = GetAssemblyDirectory();
            if (string.IsNullOrEmpty(pluginDir))
                return null;
                
            return Path.Combine(pluginDir, ReportToolFolderName, ReportToolFileName);
        }
    }
} 