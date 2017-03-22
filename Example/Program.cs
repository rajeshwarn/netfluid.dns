using Netfluid.Dns;
using Netfluid.Dns.Records;
using System;
using System.Net;
using System.Threading;

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
            server.OnRecursive += Server_OnRecursive;
            server.RecursiveChain = new[] { IPAddress.Parse("192.127.35.30") };

            server.OnRequest = OnRequest;
            server.StartAsync();

            Console.ReadLine();
        }

        private static void Server_OnRecursive(Request arg1, Response arg2)
        {
            foreach(var r in  arg1)
                Console.WriteLine(r.Name);
        }

        private static Response OnRequest(Request request)
        {
            var response = new Response(request);
            return response;
        }
    }
}
