using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ULib;
using ULib.Exceptions;
using ULib.DataSchema;

namespace Utilities.Settings
{
    public class AppSettings
    {
        public const string KeyDeploySettings = "DeploySettings";

        public readonly static AppSettings Instance = new AppSettings();
        private static Dict<string, ISettings> settings = new Dict<string, ISettings>();
        public void Initialize(string key, ISettings item = null)
        {
            settings[key] = item;
        }
        public void Save(string key)
        {
            ISettings item = settings[key];
            if (item != null)
            {
                string file = GetConfigFile(key);
                File.WriteAllText(file, item.ToXml());
            }
        }
        public bool TryLoad<T>(string key) where T : ISettings, new()
        {
            try
            {
                string file = GetConfigFile(key);
                if (File.Exists(file))
                {
                    string content = File.ReadAllText(file);
                    T rlt = content.FromXml<T>();
                    settings[key] = rlt;
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
            return false;
        }
        public T Read<T>(string key) where T : ISettings, new()
        {
            T rlt;
            if (settings[key] == null)
            {
                rlt = Load<T>(key);
                settings[key] = rlt;
                return rlt;
            }
            else
            {
                return (T)settings[key];
            }
        }
        public T Load<T>(string key) where T:ISettings,new()
        {
            try
            {
                string file = GetConfigFile(key);
                if (File.Exists(file))
                {
                    string content = File.ReadAllText(file);
                    T rlt = content.FromXml<T>();
                    settings[key] = rlt;
                    return rlt;
                }
                else
                {
                    T rlt = new T();
                    File.WriteAllText(file, rlt.ToXml());
                    return rlt;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                return default(T);
            }
        }
        public void Erase(string key)
        {
            string file = GetConfigFile(key);
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                File.Delete(file);
            }
        }
        private static string GetConfigFile(string key)
        {
            string file = string.Concat(AppDomain.CurrentDomain.BaseDirectory, key, ".xml");

            return file;
        }
    }
    public class SettingCollection : Dict<string, Setting>
    {
    }
    public interface ISettings
    {
        T GetValue<T>(string key) where T : Setting;
        void SetValue(string key, Setting value);
        bool ContainsKey(string key);
    }
    public class Setting
    {
    }
}
