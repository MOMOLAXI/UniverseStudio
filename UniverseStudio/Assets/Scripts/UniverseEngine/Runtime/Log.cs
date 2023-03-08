using System;
using UnityEngine;

namespace Universe
{
    public enum ELogLevel
    {
        None,

        Exception,
        Error,
        Info,
        Warning,
        Debug,

        Max,
    }

    public static class Log
    {
        /// <summary>
        /// 日志等级
        /// 修改等级来屏蔽日志
        /// </summary>
        public static ELogLevel LogLevel => ELogLevel.Max;

        /// <summary>
        /// 日志前缀
        /// </summary>
        static readonly string s_Runtime_LOG_Tag = $"[<color=#33CC33>{nameof(UniverseEngine)}</color>]";

        public static void Info(object message)
        {
            if (LogLevel < ELogLevel.Info)
            {
                return;
            }

            Debug.Log($"{s_Runtime_LOG_Tag}{message}");
        }

        public static void Warning(object message)
        {
            if (LogLevel < ELogLevel.Warning)
            {
                return;
            }

            Debug.LogWarning($"{s_Runtime_LOG_Tag}{message}");
        }

        public static void Error(object message)
        {
            if (LogLevel < ELogLevel.Error)
            {
                return;
            }

            Debug.LogError($"{s_Runtime_LOG_Tag}{message}");
        }

        public static void Exception(Exception ex)
        {
            if (LogLevel < ELogLevel.Exception)
            {
                return;
            }

            Debug.LogError($"================={s_Runtime_LOG_Tag} Exception Start ===============");
            Debug.LogException(ex);
            Debug.LogError($"================={s_Runtime_LOG_Tag} Exception End ===============");
        }
    }
}