using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Boggle;

namespace Boggle
{
    [TestClass]
    public class SQLUtilsTest
    {
        [TestMethod]
        public void TestBuildMappings()
        {
            IDictionary<string, object> dic = SQLUtils.BuildMappings("@user", "me");
            Assert.AreEqual("me", dic["@user"]);

            dic = SQLUtils.BuildMappings("@user", "me", "@pass", "password", "@token", "silly");
            Assert.AreEqual("me", dic["@user"]);
            Assert.AreEqual("password", dic["@pass"]);
            Assert.AreEqual("silly", dic["@token"]);
        }
    }
}
