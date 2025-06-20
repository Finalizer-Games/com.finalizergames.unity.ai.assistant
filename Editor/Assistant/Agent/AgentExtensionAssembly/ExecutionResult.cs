using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Unity.AI.Assistant.Agent.Dynamic.Extension.Editor
{
    struct ExecutionLog
    {
        public string Log;
        public LogType LogType;
        public object[] LoggedObjects;

        public ExecutionLog(string formattedLog, LogType logType, object[] loggedObjects = null)
        {
            Log = formattedLog;
            LogType = logType;
            LoggedObjects = loggedObjects;
        }
    }

#if CODE_LIBRARY_INSTALLED
    public
#else
    internal
#endif
    class ExecutionResult
    {
        internal static readonly string LinkTextColor = EditorGUIUtility.isProSkin ? "#8facef" : "#055b9f";
        internal static readonly string WarningTextColor = EditorGUIUtility.isProSkin ? "#DFB33D" : "#B76300";

        static int k_NextExecutionId = 1;

        static readonly Regex k_PlaceholderRegex = new(@"%(\d+)", RegexOptions.Compiled);

        List<ExecutionLog> m_Logs = new();
        string m_ConsoleLogs;
        int UndoGroup;

        public readonly int Id;
        public readonly string CommandName;

        internal List<ExecutionLog> Logs => m_Logs;
        public string ConsoleLogs => m_ConsoleLogs;
        public bool SuccessfullyStarted { get; private set; }

        public ExecutionResult(string commandName)
        {
            Id = k_NextExecutionId++;
            CommandName = commandName;
        }

        public void RegisterObjectCreation(Object objectCreated)
        {
            if (objectCreated != null)
                Undo.RegisterCreatedObjectUndo(objectCreated, $"{objectCreated.name} was created");
        }

        public void RegisterObjectCreation(Component component)
        {
            if (component != null)
                Undo.RegisterCreatedObjectUndo(component, $"{component} was attached to {component.gameObject.name}");
        }

        public void RegisterObjectModification(Object objectToRegister, string operationDescription = "")
        {
            if (!string.IsNullOrEmpty(operationDescription))
                Undo.RecordObject(objectToRegister, operationDescription);
            else
                Undo.RegisterCompleteObjectUndo(objectToRegister, $"{objectToRegister.name} was modified");
        }

        public void DestroyObject(Object objectToDestroy)
        {
            if (EditorUtility.IsPersistent(objectToDestroy))
            {
                var path = AssetDatabase.GetAssetPath(objectToDestroy);
                AssetDatabase.DeleteAsset(path);
            }
            else
            {
                if (!EditorApplication.isPlaying)
                    Undo.DestroyObjectImmediate(objectToDestroy);
                else
                    Object.Destroy(objectToDestroy);
            }
        }

        public void Start()
        {
            SuccessfullyStarted = true;

            Undo.IncrementCurrentGroup();
            Undo.SetCurrentGroupName(CommandName ?? "Run command execution");
            UndoGroup = Undo.GetCurrentGroup();

            Application.logMessageReceived += HandleConsoleLog;
        }

        public void End()
        {
            Application.logMessageReceived -= HandleConsoleLog;

            Undo.CollapseUndoOperations(UndoGroup);
        }

        public void Log(string log, params object[] references)
        {
            m_Logs.Add(new ExecutionLog(log, LogType.Log, references));
        }

        public void LogWarning(string log, params object[] references)
        {
            m_Logs.Add(new ExecutionLog(log, LogType.Warning, references));
        }

        public void LogError(string log, params object[] references)
        {
            m_Logs.Add(new ExecutionLog(log, LogType.Error, references));
        }

        void HandleConsoleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Warning)
            {
                m_ConsoleLogs += $"{type}: {logString}\n";
            }
        }
    }
}
