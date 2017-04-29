using System.ComponentModel.Composition;
using System.Collections.ObjectModel;

namespace Demo.Mef
{
    // We set the Logger class to be exported as ILogger
    [Export(typeof(ILogger))]
    public class Logger : ILogger
    {
        private ObservableCollection<string> logs = new ObservableCollection<string>();

        public void Log(string message)
        {
            Logs.Add(message);
        }

        public ObservableCollection<string> Logs
        {
            get { return logs; }
        }
    }
}
