﻿using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Messaging;
using Bam.Net.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Automation.MSBuild;
using Bam.Net.Presentation.Handlebars;
using System.Yaml;
using System.Threading;
using System.Reflection;
using Bam.Net.Data.Dynamic;

namespace Bam.Net.Application
{
    [Serializable]
    public class WebActions : CommandLineTestInterface
    {
        public const string AppDataFolderName = "AppData";
        public const string GenerationOutputFolderName = "_gen";

        static DirectoryInfo _appData;
        static object _appDataLock = new object();
        static DirectoryInfo AppData
        {
            get
            {
                return _appDataLock.DoubleCheckLock(ref _appData, () => new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, AppDataFolderName)));
            }
        }

        static DirectoryInfo _generationOutput;
        static object _generationOutputLock = new object();
        static DirectoryInfo GenerationOutput
        {
            get
            {
                return _generationOutputLock.DoubleCheckLock(ref _generationOutput, () => new DirectoryInfo(Path.Combine(AppData.FullName, GenerationOutputFolderName)));
            }
        }

        [ConsoleAction("init", "Add BamFramework to the current csproj")]
        public void Init()
        {
            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root
            // - clone bam.js into wwwroot/bam.js
            // - add Pages/Api/V1.cshtml
            // - add Pages/Api/V1.cshtml.cs
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if(csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                
                Thread.Sleep(3000);
                Exit(1);
            }
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(projectParent.FullName, "wwwroot"));
            if (!wwwroot.Exists)
            {
                Warn("{0} doesn't exist, creating...", wwwroot.FullName);
                wwwroot.Create();
            }
            string bamJsPath = Path.Combine(wwwroot.FullName, "bam.js");
            if (!Directory.Exists(bamJsPath))
            {
                OutLineFormat("Cloning bam.js to {0}", ConsoleColor.Yellow, bamJsPath);
                string cloneCommand = "git clone https://github.com/BryanApellanes/bam.js.git wwwroot/bam.js";
                cloneCommand.Run((output) => OutLine(output, ConsoleColor.Cyan));
            }

            WriteStartupCs(csprojFile);
            WriteBaseAppModules(csprojFile);          
        }

        [ConsoleAction("addPage", "Add a page to the current BamFramework project")]
        public void AddPage()
        {
            string pageName = GetArgument("addPage", "Please enter the name of the page to create");
            if (string.IsNullOrEmpty(pageName))
            {
                OutLine("Page name not specified", ConsoleColor.Magenta);
                Exit(1);
            }

            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root, add the files
            // - Pages/[pagePath].cshtml
            // - Pages/[pagePath].cshtml.cs
            // - wwwroot/bam.js/pages/[pagePath].js
            // - wwwroot/bam.js/configs/[pagePath]/webpack.config.js
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if (csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                Exit(1);
            }
            AddPage(csprojFile, pageName);
        }

        [ConsoleAction("gen", "Generate a dynamic type assembly for json and yaml data")]
        public void GenerateDataModels()
        {
            DynamicTypeManager dynamicTypeManager = new DynamicTypeManager();
            OutLineFormat("Generating dynamic types from json ({0}) and yaml ({1}).", Path.Combine(AppData.FullName, "json"), Path.Combine(AppData.FullName, "yaml"));

            // TOOD: change this to only write files without compiling
            Assembly assembly = dynamicTypeManager.Generate(AppData);

            Expect.IsNotNull(assembly);
            Expect.IsGreaterThan(assembly.GetTypes().Length, 0);

            // TODO: add a call to troo, example: troo.exe /gsr /ta:.\bam.net.dll /sn:DynamicTypeData /fns:Bam.Net.Data.Dynamic.Data /cfi:yes /uis:no /ws:C:\bam\src\_gen\Bam.Net.Data.Dynamic\Data\Generated_Dao
            // TODO: add a call to laotze to process any db.js files

            foreach (Type type in assembly.GetTypes())
            {
                OutLineFormat("{0}.{1}", ConsoleColor.Cyan, type.Namespace, type.Name);
            }
        }

        [ConsoleAction("webpack", "WebPack each bam.js page found in wwwroot/bam.js/pages using corresponding configs found in wwwroot/bam.js/configs")]
        public void WebPack()
        {
            // find the first csproj file by looking first in the current directory then going up
            // using the parent of the csproj as the root
            // change directories into wwwroot/bam.js
            // for every webpack.config.js file in ./configs/ call
            // npx  webpack --config [configPath]

            string startDir = Environment.CurrentDirectory;
            DirectoryInfo projectParent = FindProjectParent(out FileInfo csprojFile);
            if (csprojFile == null)
            {
                OutLine("Can't find csproj file", ConsoleColor.Magenta);
                Exit(1);
            }
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(projectParent.FullName, "wwwroot"));
            if (!wwwroot.Exists)
            {
                OutLineFormat("{0} doesn't exist", ConsoleColor.Magenta, wwwroot.FullName);
                Exit(1);
            }
            DirectoryInfo bamJs = new DirectoryInfo(Path.Combine(wwwroot.FullName, "bam.js"));
            if (!bamJs.Exists)
            {
                OutLineFormat("{0} doesn't exist", ConsoleColor.Magenta, bamJs.FullName);
            }
            Environment.CurrentDirectory = bamJs.FullName;
            DirectoryInfo configs = new DirectoryInfo(Path.Combine(bamJs.FullName, "configs"));
            foreach (FileInfo config in configs.GetFiles("webpack.config.js", SearchOption.AllDirectories))
            {
                string configPath = config.FullName.Replace(configs.FullName, "");
                if (configPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    configPath = configPath.TruncateFront(1);
                }
                OutLineFormat("Packing {0}", ConsoleColor.Green, configPath);
                $"npx webpack --config {configPath}".Run(output => OutLine(output, ConsoleColor.Cyan));
            }
            Environment.CurrentDirectory = startDir;
        }
        
        private void AddPage(FileInfo csprojFile, string pageName)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);
            DirectoryInfo pagesDirectory = new DirectoryInfo(Path.Combine(projectParent.FullName, "Pages"));
            PageRenderModel pageRenderModel = new PageRenderModel { BaseNamespace = $"{appName}", PageName = pageName };

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();

            string csHtmlFilePath = Path.Combine(pagesDirectory.FullName, $"{pageName}.cshtml");            
            if (!File.Exists(csHtmlFilePath))
            {
                EnsureDirectoryExists(csHtmlFilePath);
                string pageContent = handlebarsDirectory.Render("Page.cshtml", pageRenderModel);
                pageContent.SafeWriteToFile(csHtmlFilePath, true);
            }

            string csHtmlcsFilePath = $"{csHtmlFilePath}.cs";
            if (!File.Exists(csHtmlcsFilePath))
            {
                EnsureDirectoryExists(csHtmlcsFilePath);
                string codeBehindContent = handlebarsDirectory.Render("Page.cshtml.cs", pageRenderModel);
                codeBehindContent.SafeWriteToFile(csHtmlcsFilePath, true);
            }

            AddWebPackConfig(csprojFile, pageName);
        }

        private void EnsureDirectoryExists(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
        }

        private void AddWebPackConfig(FileInfo csprojFile, string pageName)
        {
            DirectoryInfo wwwroot = new DirectoryInfo(Path.Combine(csprojFile.Directory.FullName, "wwwroot"));
            DirectoryInfo projectParent = csprojFile.Directory;
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);
            string wwwrootPath = wwwroot.FullName;
            if (!wwwrootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                wwwrootPath += Path.DirectorySeparatorChar.ToString();
            }
            PageRenderModel pageRenderModel = new PageRenderModel { BaseNamespace = $"{appName}", PageName = pageName, WwwRoot = wwwrootPath };

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            string pageJsPath = Path.Combine(wwwroot.FullName, "bam.js", "pages", $"{pageName}.js");
            string webPackConfigPath = Path.Combine(wwwroot.FullName, "bam.js", "configs", pageName, "webpack.config.js");
            if (!File.Exists(pageJsPath))
            {
                handlebarsDirectory.Render("PageJs", pageRenderModel).SafeWriteToFile(pageJsPath, true);
            }
            if (!File.Exists(webPackConfigPath))
            {
                handlebarsDirectory.Render("WebpackConfig", pageRenderModel).SafeWriteToFile(webPackConfigPath, true);
            }
        }

        private void WriteBaseAppModules(FileInfo csprojFile)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            DirectoryInfo appModules = new DirectoryInfo(Path.Combine(projectParent.FullName, "AppModules"));
            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            string appName = Path.GetFileNameWithoutExtension(csprojFile.Name);

            AppModuleRenderModel model = new AppModuleRenderModel { BaseNamespace = $"{appName}", AppModuleName = appName };
            foreach(string moduleType in new string[] { "AppModule", "ScopedAppModule", "SingletonAppModule", "TransientAppModule" })
            {
                string moduleContent = handlebarsDirectory.Render($"{moduleType}.cs", model);
                if (string.IsNullOrEmpty(moduleContent))
                {
                    OutLineFormat("{0}: Template for {1} is empty", handlebarsDirectory.Directory.FullName, moduleType);
                }
                string filePath = Path.Combine(appModules.FullName, $"{appName}{moduleType}.cs");
                if (!File.Exists(filePath))
                {
                    moduleContent.SafeWriteToFile(filePath, true);
                    OutLineFormat("Wrote file {0}...", ConsoleColor.Green, filePath);
                }
            }            
        }

        private void WriteStartupCs(FileInfo csprojFile)
        {
            DirectoryInfo projectParent = csprojFile.Directory;
            FileInfo startupCs = new FileInfo(Path.Combine(projectParent.FullName, "Startup.cs"));
            if (startupCs.Exists)
            {
                string moveTo = startupCs.FullName.GetNextFileName();
                File.Move(startupCs.FullName, moveTo);
                OutLineFormat("Moved existing Startup.cs file to {0}", ConsoleColor.Yellow, moveTo);
            }

            HandlebarsDirectory handlebarsDirectory = GetHandlebarsDirectory();
            handlebarsDirectory.Render("Startup.cs", new { BaseNamespace = Path.GetFileNameWithoutExtension(csprojFile.Name) }).SafeWriteToFile(startupCs.FullName, true);
        }

        private HandlebarsDirectory GetHandlebarsDirectory()
        {
            DirectoryInfo bamDir = Assembly.GetExecutingAssembly().GetFileInfo().Directory;
            return new HandlebarsDirectory(Path.Combine(bamDir.FullName, "Templates"));
        }

        private DirectoryInfo FindProjectParent(out FileInfo csprojFile)
        {
            string startDir = Environment.CurrentDirectory;
            DirectoryInfo startDirInfo = new DirectoryInfo(startDir);
            DirectoryInfo projectParent = startDirInfo;
            FileInfo[] projectFiles = projectParent.GetFiles("*.csproj", SearchOption.TopDirectoryOnly);
            while (projectFiles.Length == 0)
            {
                if(projectParent.Parent != null)
                {
                    projectParent = projectParent.Parent;
                    projectFiles = projectParent.GetFiles("*.csrpoj", SearchOption.TopDirectoryOnly);
                }
                else
                {
                    break;
                }
            }
            csprojFile = null;
            if (projectFiles.Length > 0)
            {
                csprojFile = projectFiles[0];
            }

            if (projectFiles.Length > 1)
            {
                Warn("Multiple csproject files found, using {0}\r\n{1}", csprojFile.FullName, string.Join("\r\n\t", projectFiles.Select(p => p.FullName).ToArray()));
            }
            return projectParent;
        }
    }
}