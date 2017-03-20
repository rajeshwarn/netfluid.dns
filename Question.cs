using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Netfluid.Dns
{

    #region Rfc 1034/1035

    /*
	4.1.2. Question section format

	The question section is used to carry the "question" in most queries,
	i.e., the parameters that define what is being asked.  The section
	contains QDCOUNT (usually 1) entries, each of the following format:

										1  1  1  1  1  1
		  0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
		+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
		|                                               |
		/                     QNAME                     /
		/                                               /
		+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
		|                     QTYPE                     |
		+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
		|                     QCLASS                    |
		+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+

	where:

	QNAME           a domain name represented as a sequence of labels, where
					each label consists of a length octet followed by that
					number of octets.  The domain name terminates with the
					zero length octet for the null label of the root.  Note
					that this field may be an odd number of octets; no
					padding is used.

	QTYPE           a two octet code which specifies the type of the query.
					The values for this field include all codes valid for a
					TYPE field, together with some more general codes which
					can match more than one type of RR.


	QCLASS          a two octet code that specifies the class of the query.
					For example, the QCLASS field is IN for the Internet.
	*/

    #endregion

    /// <summary>
    /// DNS query used by DNS server and resolver
    /// </summary>
    [Serializable]
    public class Question
    {
        /// <summary>
        /// Address class (Internet, CSNET, CHAOS, HESIOD, ANY)
        /// </summary>
        public Class Class;

        /// <summary>
        /// Requested records type
        /// </summary>
        public RecordType Type;

        /// <summary>
        /// Domain of the query
        /// </summary>
        public string Name;

        /// <summary>
        /// Create a new DNS question
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="qclass"></param>
        public Question(string name, RecordType type, Class qclass)
        {
            this.Name = name;
            this.Type = type;
            this.Class = qclass;
        }

        /// <summary>
        /// DNS question serialized
        /// </summary>
        public byte[] Data
        {
            get
            {
                var data = new List<byte>();
                data.AddRange(WriteName(Name));
                data.AddRange(WriteShort((ushort) Type));
                data.AddRange(WriteShort((ushort) Class));
                return data.ToArray();
            }
        }

        private static IEnumerable<byte> WriteName(string src)
        {
            if (!src.EndsWith("."))
                src += ".";

            if (src == ".")
                return new byte[1];

            var sb = new StringBuilder();
            int intI, intJ, intLen = src.Length;
            sb.Append('\0');
            for (intI = 0, intJ = 0; intI < intLen; intI++, intJ++)
            {
                sb.Append(src[intI]);

                if (src[intI] != '.')
                    continue;

                sb[intI - intJ] = (char) (intJ & 0xff);
                intJ = -1;
            }
            sb[sb.Length - 1] = '\0';
            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        private static IEnumerable<byte> WriteShort(ushort sValue)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short) sValue));
        }

        public override string ToString()
        {
            return string.Format($"{Name,-32}\t{Class}\t{Type}");
        }
    }
}