using Netfluid.Dns;
using Netfluid.Dns.Records;
using System;

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
            server.StartAsync();

            Console.ReadLine();
        }

        private static Response OnRequest(Request request)
        {
            var response = new Response(request);

            foreach (var question in request)
            {
                Record record;
                switch (question.Type)
                {
                    case RecordType.A:
                        record = new RecordA("127.0.0.1");
                        break;

                    case RecordType.AAAA:
                        record = new RecordAAAA("fe80::e1e1:9ccd:7696:17d%7");
                        break;
                    case RecordType.MX:
                        record = new RecordMX
                        {
                            Exchange = "mail.netfluid.org",
                            Preference = 10
                        };
                        break;
                    case RecordType.PTR:
                        record = new RecordPTR
                        {
                            PtrdName = "127.0.0.1"
                        };
                        break;
                    default:
                        throw new NotImplementedException("Add other record types to this example !!!");
                }

                response.Answers.Add(record);
            }
            return response;
        }
    }
}
