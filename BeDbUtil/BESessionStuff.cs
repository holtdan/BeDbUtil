using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeDbUtil
{
    public class BEClient
    {
        public object vwSystem { get; set; }
        public string ImageDir { get; set; }
    }
    public class BEUser
    {
        public int UserID { get; set; }
    }
    public interface IBESession
    {
        BEClient BEClient { get; set; }
        BEUser BEUser { get; set; }
    }
    public class BESession : IBESession
    {
        public BEClient BEClient { get; set; }
        public BEUser BEUser { get; set; }

        public void LoadClient()
        {
            // use EF to get real data
        }
        public void LoadUser()
        {
            // use EF to get real data
        }
    }
    public class BESessionTester : IBESession
    {
        public BEClient BEClient { get; set; }
        public BEUser BEUser { get; set; }

        public void LoadClient()
        {
            // stuff it with test data
        }
        public void LoadUser()
        {
            // stuff it with test data
        }
    }
    // Alternatively, BESession has NO data access inside itself ... i.e. no load methods (and no interface)...
    public class BESessionFactory
    {
        public static BESession CreateFromSession(object httpSession)
        {
            var beSess = new BESession();
            // stuff it from httpSession
            return beSess;
        }
    }
}
