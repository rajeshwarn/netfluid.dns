using System;
using System.Collections.Generic;
using Netfluid.Dns.Records;

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
            Header = new Header {OPCODE = OPCode.Query, QDCOUNT = 0, RD = true, ID = (ushort) DateTime.Now.Millisecond};
        }

        /// <summary>
        /// Binary seriliazed
        /// </summary>
        internal byte[] Write
        {
            get
            {
                var data = new List<byte>();
                Header.QDCOUNT = (ushort) Count;
                data.AddRange(Header.Data);

                foreach (Question q in this)
                    data.AddRange(q.Data);

                return data.ToArray();
            }
        }
    }
}