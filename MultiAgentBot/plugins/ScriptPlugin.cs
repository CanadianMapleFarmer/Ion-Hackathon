using System;
using System.IO;
using System.Management.Automation;
using System.Text;

namespace MultiAgentBot.Plugins
{
    public class ScriptPlugin
    {
        /// <summary>
        /// Executes a PowerShell script file located on the system.
        /// </summary>
        /// <param name="scriptFilePath">Full path to the PowerShell script file (.ps1)</param>
        /// <param name="targetDirectory">Directory from which the script will execute</param>
        /// <returns>Output from script execution</returns>
        public string ExecuteScriptFromFile(string scriptFilePath, string targetDirectory)
        {
            if (!File.Exists(scriptFilePath))
                return $"Script file not found: {scriptFilePath}";

            try
            {
                string scriptContent = File.ReadAllText(scriptFilePath);

                if (!Directory.Exists(targetDirectory))
                    Directory.CreateDirectory(targetDirectory);

                using (PowerShell ps = PowerShell.Create())
                {
                    // Set the execution directory
                    ps.AddScript($"Set-Location -Path '{targetDirectory}'");
                    ps.AddScript(scriptContent);

                    var outputBuilder = new StringBuilder();
                    var errorBuilder = new StringBuilder();

                    // Execute script and collect output
                    var results = ps.Invoke();

                    foreach (var output in results)
                        outputBuilder.AppendLine(output.ToString());

                    // Handle errors
                    if (ps.HadErrors)
                    {
                        foreach (var error in ps.Streams.Error)
                            errorBuilder.AppendLine(error.ToString());

                        throw new Exception("PowerShell Error: " + errorBuilder.ToString());
                    }

                    return outputBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"Error executing PowerShell script: {ex.Message}";
            }
        }
    }
}
