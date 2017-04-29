using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Joy.Service
{
    /// <summary>
    /// {"actions":[{"ins":"TestInstance","act":"TestResult"}]}
    /// </summary>
    public class QueueRequest
    {
        public List<QueueItem> actions;
    }
    public class QueueItem
    {
        public string ins;
        public string act;
        public string rlt;
        public string arg;
        public string queueid;
    }
}
