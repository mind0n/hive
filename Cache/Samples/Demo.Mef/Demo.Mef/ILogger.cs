using System.Collections.ObjectModel;

namespace Demo.Mef
{
    public interface ILogger
    {
        ObservableCollection<string> Logs { get; }
        void Log(string message);
    }
}
