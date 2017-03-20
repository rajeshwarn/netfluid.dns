using Netfluid.Dns.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System;

namespace Netfluid.Dns
{
    public static class DnsClient
    {
        static Lazy<IPAddress[]> _networkServers = new Lazy<IPAddress[]>(() => NetworkInterface.GetAllNetworkInterfaces()
                                                                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                                                                .SelectMany(x => x.GetIPProperties().DnsAddresses)
                                                                .ToArray());

        /// <summary>
        ///     Gets a list of default DNS servers used by system
        /// </summary>
        /// <returns></returns>
        public static IPAddress[] NetworkServers => _networkServers.Value;

        /// <summary>
        /// Ask a DNS question to the specified server
        /// </summary>
        /// <param name="question">Question</param>
        /// <param name="server">Server</param>
        /// <returns></returns>
        public static async Task<Response> Query(Question question, string server)
        {
            return await Query(question.Name, question.Type, question.Class, new[] { IPAddress.Parse(server) });
        }

        /// <summary>
        /// Ask a DNS question to the specified server
        /// </summary>
        /// <param name="question">Question</param>
        /// <param name="server">Server</param>
        /// <returns></returns>
        public static async Task<Response> Query(Question question, IPAddress server)
        {
            return await Query(question.Name, question.Type, question.Class, new[] { server });
        }

        /// <summary>
        /// Ask a DNS question  to a specific server
        /// </summary>
        public static async Task<Response> Query(string name, RecordType qtype, RecordClass qclass, string server)
        {
            return await Query(name, qtype, qclass, IPAddress.Parse(server));
        }

        /// <summary>
        /// Ask a DNS question  to a specific server
        /// </summary>
        public static async Task<Response> Query(string name, RecordType qtype, RecordClass qclass, IPAddress server)
        {
            return await Query(name, qtype, qclass, new[] { server });
        }

        /// <summary>
        /// Ask a DNS question to a specific server
        /// </summary>
        public static async Task<Response> Query(string name, RecordType qtype, RecordClass qclass = RecordClass.IN, IEnumerable<IPAddress> servers = null)
        {
            if (servers == null)
                servers = NetworkServers;

            var request = new Request { new Question(name, qtype, qclass) };

            return await Query(request, servers);
        }

        public static Task<Response> Query(Request req, string v)
        {
            return Query(req, new[] { IPAddress.Parse(v) });
        }

        public static async Task<Response> Query(Request request, IEnumerable<IPAddress> servers)
        {
            var requestByte = Writer.Serialize(request);
            var buffer = new byte[32 * 1024];

            var client = new UdpClient();

            for (int intAttempts = 0; intAttempts < 3; intAttempts++)
            {
                foreach (var server in servers)
                {
                    try
                    {
                        await client.SendAsync(requestByte, requestByte.Length, new IPEndPoint(server, 53));
                        var response = await client.ReceiveAsync();

                        return Reader.ReadResponse(response.Buffer);
                    }
                    catch (SocketException)
                    {
                        continue; // next try
                    }
                }
            }
            return new Response();
        }

        /// <summary>
        /// Query A records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> A(string name)
        {
            return (await Query(name, RecordType.A)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query AAAA records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> AAAA(string name)
        {
            return (await Query(name, RecordType.AAAA)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query CNAME records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> CNAME(string name)
        {
            return (await Query(name, RecordType.CNAME)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query MX records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> MX(string name)
        {
            return (await Query(name, RecordType.MX)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query NS records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> NS(string name)
        {
            return (await Query(name, RecordType.NS)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query PTR records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> PTR(string name)
        {
            return (await Query(name, RecordType.PTR)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query SOA records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> SOA(string name)
        {
            return (await Query(name, RecordType.SOA)).Answers.Select(x => x.ToString());
        }

        /// <summary>
        /// Query TXT records for name into system DNS server
        /// </summary>
        /// <param name="name">domain to query</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> TXT(string name)
        {
            return (await Query(name, RecordType.TXT)).Answers.Select(x => x.ToString());
        }
    }
}
