using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeDbUtil;
using System.Text;
using System.Data;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class DBUtilTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var names = DBUtil.GetDbNames();

            foreach ( var dbn in names.Where(n=>n.ToLower().EndsWith("_beta")))
            {
                var useName = dbn.Substring(0,dbn.Length-5);
                var sql = 
                    "select MenuBar, MenuBarFont, MenuDDBackground, DashboardBackground, SysRollOverColor from BESystemColors";
                try
                {
                    var colors = DBUtil.Execute(DBUtil.GetDbConnStr(dbName: dbn), sql);

                    var sb = new StringBuilder();

                    foreach (var r in colors.Cast<DataRow>())
                    {
                        sb.AppendLine(string.Format("@beMenuBar: {0};", r["MenuBar"].ToString().Trim()));
                        sb.AppendLine(string.Format("@beMenuBarFont: {0};", r["MenuBarFont"].ToString().Trim()));
                        sb.AppendLine(string.Format("@beMenuDDBackground: {0};", r["MenuDDBackground"].ToString().Trim()));
                        sb.AppendLine(string.Format("@beDashboardBackground: {0};", r["DashboardBackground"].ToString().Trim()));
                        sb.AppendLine(string.Format("@beSysRollOverColor: {0};", r["SysRollOverColor"].ToString().Trim()));
                    }

                    WriteFile(useName,sb);

                    Console.WriteLine(sb.ToString());
                }
                catch (Exception)
                {
                    Console.WriteLine("Error on " + dbn);
                }
            }
        }

        private void WriteFile(string useName, StringBuilder sb)
        {
            PrepFolder(useName);

            using ( var file = new System.IO.StreamWriter(useName+"\\ClientStyle.less"))
            {
                file.WriteLine("/* {0} */", useName);
                file.WriteLine("@import \"~/Content/SiteDefaults.less\";");
                file.WriteLine();
                file.WriteLine(sb.ToString());
                file.WriteLine("@import \"~/Content/Site.less\";");
            }
        }

        private void PrepFolder(string name)
        {
            if (Directory.Exists(name))
                Directory.Delete(name, true);

            Directory.CreateDirectory(name);
        }
    }
}
