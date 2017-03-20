using System;
using System.Collections.Generic;
using Netfluid.Dns.Records;
using Netfluid.Dns.Serialization;

namespace Netfluid.Dns
{
    /// <summary>
    /// List of DNS query used by DNS server and resolver
    /// </summary>
    [Serializable]
    public class Request : List<Question>
    {
        /// <summary>
        /// Header
        /// </summary>
        internal Header Header;

        /// <summary>
        /// Instance a new DNS request
        /// </summary>
        public Request()
        {
            Header = new Header {OPCODE = OpCode.Query, QDCOUNT = 0, RD = true, ID = (ushort) DateTime.Now.Millisecond};
        }
    }
}