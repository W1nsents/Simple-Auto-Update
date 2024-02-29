using System.Net;

namespace AutoUpdate
{
    public class Internet
    {
        public static bool OK()
        {
            try
            {
                Dns.GetHostEntry("google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
