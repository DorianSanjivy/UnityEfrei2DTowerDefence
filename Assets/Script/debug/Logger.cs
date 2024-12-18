using UnityEngine;
using System.IO;
public class Logger : MonoBehaviour
{
    private string logFilePath;
    private void Awake()
    {
        // Set the path to the log file at the root of the project directory
        logFilePath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "game_log.txt");
        // Subscribe to the log message received event
        Application.logMessageReceived += HandleLog;
        Debug.Log("\n");
    }
    private void OnDestroy()
    {
        // Unsubscribe from the log message received event
        Application.logMessageReceived -= HandleLog;
    }
    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Format the log entry
        string logEntry = $"{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} [{type}] {logString}\n";
        if (type == LogType.Error || type == LogType.Exception)
        {
            logEntry += $"Stack Trace: {stackTrace}\n";
        }
        // Write the log entry to the file, ensuring the directory exists
        try
        {
            File.AppendAllText(logFilePath, logEntry);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to write to log file: " + ex.Message);
        }
    }
}