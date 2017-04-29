using Joy.Server.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.Storage
{
    public class StorageService : ServiceHandler
    {
        [Method(IsPage = true)]
        public void LoadManageView()
        {
            LoadView("Storage/Local/LocalStorageManager");
        }
    }
}