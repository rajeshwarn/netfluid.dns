using Netfluid.Dns;
using Netfluid.Dns.Records;
using System.Linq;

namespace Example
{
    class Program
    {
        static DnsServer server;

        static void Main(string[] args)
        {
            server = new DnsServer();

            //If he doens't have the answer it will ask to upper dns server for solving the question
            server.Recursive = true;
            server.OnRequest = OnRequest;
            server.Start();
        }

        private static Response OnRequest(Request req)
        {
            var questions = req.Where(x => x.QName.EndsWith("netfluid.org") && x.QType == QType.MX);

            if (!questions.Any())
                return new Response();

            return new Response(req, new RecordMX()
            {
                Name ="netfluid.org",
                Exchange = "178.23.169.22",
                Class = Class.IN,
                Preference = 10,
                TTL = 127
            });
        }
    }
}
