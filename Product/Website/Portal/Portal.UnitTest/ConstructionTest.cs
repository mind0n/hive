using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Joy.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Joy.Users;

namespace Portal.UnitTest
{
    [TestClass]
    public class ConstructionTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //UserRoles.Enum();
            //List<UserRole> list = typeof (TRoles).EnumFieldValue<UserRole>();
            List<string> list = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                string id = UserIdentity.GenerateId();
                list.Add(id);
            }
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}
