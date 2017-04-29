using System;
using System.Configuration;
using System.Collections.Specialized;

namespace System.Transactions.Configuration
{
    internal static class AppSettings
    {
        private static volatile bool settingsInitalized = false;
        private static object appSettingsLock = new object();

        private static void EnsureSettingsLoaded()
        {
            if (!settingsInitalized)
            {
                lock (appSettingsLock)
                {
                    if (!settingsInitalized)
                    {
                        NameValueCollection settings = null;
                        try
                        {
                            settings = ConfigurationManager.AppSettings;
                        }
                        catch (ConfigurationErrorsException)
                        {
                        }
                        finally
                        {
                            if (settings == null || !Boolean.TryParse(settings["Transactions:IncludeDistributedTransactionIdInExceptionMessage"], out _includeDistributedTxIdInExceptionMessage))
                            {
                                _includeDistributedTxIdInExceptionMessage = false;
                            }
 
                            settingsInitalized = true;
                        }
                    }
               }
            }
        }

        // Bug 954268 Extend Default TransactionException to include the Distributed Transaction ID in error message
        // When the appsetting "Transactions:IncludeDistributedTransactionIdInExceptionMessage" is "true", we display the distributed transaction ID in TransactionException message if the distributed transaction ID is availble.
        private static  bool _includeDistributedTxIdInExceptionMessage;
        internal static bool IncludeDistributedTxIdInExceptionMessage
        {
            get
            {
                EnsureSettingsLoaded();
                return _includeDistributedTxIdInExceptionMessage;
            }
        }
    }
}
