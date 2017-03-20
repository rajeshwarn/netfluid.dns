using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Netfluid.Dns.Serialization
{
    class Writer
    {
        public static byte[] Serialize(Request request)
        {
            var data = new List<byte>();
            request.Header.QDCOUNT = (ushort)request.Count;
            data.AddRange(request.Header.Data);

            foreach (Question q in request)
                data.AddRange(Serialize(q));

            return data.ToArray();
        }

        public static byte[] Serialize(Question question)
        {
            var data = new List<byte>();
            data.AddRange(WriteName(question.Name));
            data.AddRange(WriteShort((ushort)question.Type));
            data.AddRange(WriteShort((ushort)question.Class));
            return data.ToArray();
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

                sb[intI - intJ] = (char)(intJ & 0xff);
                intJ = -1;
            }
            sb[sb.Length - 1] = '\0';
            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        private static IEnumerable<byte> WriteShort(ushort sValue)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)sValue));
        }
    }
}
