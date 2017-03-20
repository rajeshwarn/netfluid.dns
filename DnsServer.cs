using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Netfluid.Dns
{
    //DNS INTEGRATION, QUERY AND RECORDS FROM http://www.codeproject.com/Articles/23673/DNS-NET-Resolver-C

    /// <summary>
    ///  DNS Server and Client
    /// </summary>
    public class DnsServer
    {
        static Lazy<IPAddress[]> _roots = new Lazy<IPAddress[]>(() => new[]
        {
            IPAddress.Parse("198.41.0.4"),
            IPAddress.Parse("192.228.79.201"),
            IPAddress.Parse("192.33.4.12"),
            IPAddress.Parse("199.7.91.13"),
            IPAddress.Parse("192.203.230.10"),
            IPAddress.Parse("192.5.5.241"),
            IPAddress.Parse("192.112.36.4"),
            IPAddress.Parse("128.63.2.53"),
            IPAddress.Parse("192.36.148.17"),
            IPAddress.Parse("192.58.128.30"),
            IPAddress.Parse("193.0.14.129"),
            IPAddress.Parse("198.32.64.12"),
            IPAddress.Parse("202.12.27.33")
        });

        public static IPAddress[] Roots => _roots.Value;

        /// <summary>
        /// True if DNS Server is running
        /// </summary>
        public bool AcceptingRequest { get; private set; }

        /// <summary>
        /// Implement this function to fill the response
        /// </summary>
        public Func<Request, Response> OnRequest { get; set; }

        /// <summary>
        /// If true when a local server response is empty it will hask to roots servers
        /// </summary>
        public bool Recursive { get; set; }

        CancellationTokenSource stopper;
        UdpClient endpoint;
        Task task;

        public DnsServer():this(IPAddress.Any)
        {
        }

        public DnsServer(IPAddress ip, int port=53)
        {
            endpoint = new UdpClient(new IPEndPoint(ip, port));
            stopper = new CancellationTokenSource();
            

            task = new Task(async () =>
            {
                while (true)
                {
                    var client = await endpoint.ReceiveAsync();

                    Task.Run(() =>
                    {
                        if (OnRequest == null) return;

                        var req  = Serializer.ReadRequest(new MemoryStream(client.Buffer));
                        var resp = OnRequest(req);

                        if (Recursive && resp.Answers.Count == 0 && resp.Authorities.Count == 0 && resp.Additionals.Count == 0)
                            resp = DnsClient.Query(req, Roots);

                        var output = Serializer.WriteResponse(resp);
                        endpoint.SendAsync(output, output.Length, client.RemoteEndPoint);
                    });
                }


            }, stopper.Token);
        }

        /// <summary>
        /// Start local DNS Server
        /// </summary>
        /// <param name="ip"></param>
        public void StartAsync()
        {
            AcceptingRequest = true;
            task.Start();
        }

        /// <summary>
        /// Start local DNS server
        /// </summary>
        public void Stop()
        {
            stopper.Cancel();
        }
    }
}