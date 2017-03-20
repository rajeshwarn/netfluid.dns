using System;
using System.Collections.Generic;
using System.Linq;
using Netfluid.Dns.Records;

namespace Netfluid.Dns
{
    /// <summary>
    /// List of returned DNS record used by DNS server and resolver
    /// </summary>
    [Serializable]
    public class Response
    {
        /// <summary>
        ///     List of Record records
        /// </summary>
        public List<Record> Additionals;

        /// <summary>
        ///     List of Record records
        /// </summary>
        public List<Record> Answers;

        /// <summary>
        ///     List of Record records
        /// </summary>
        public List<Record> Authorities;

        /// <summary>
        /// DNS response header
        /// </summary>
        internal Header Header;

        /// <summary>
        ///     The Size of the message
        /// </summary>
        public int MessageSize;

        /// <summary>
        ///     List of Question records
        /// </summary>
        public List<Question> Questions;

        public Response()
        {
            Questions = new List<Question>();
            Answers = new List<Record>();
            Authorities = new List<Record>();
            Additionals = new List<Record>();

            MessageSize = 0;
            Header = new Header() { Flags = 33152 };
        }


        /// <summary>
        /// With same header of request
        /// </summary>
        /// <param name="request"></param>
        public Response(Request request)
        {
            Header = request.Header;
            Header.QR = true;
            Questions = new List<Question>(request);
            Answers = new List<Record>();
            Authorities = new List<Record>();
            Additionals = new List<Record>();

            MessageSize = 0;
        }

        /// <summary>
        /// With same header of request
        /// </summary>
        /// <param name="request"></param>
        public Response(Request request, Record record)
        {
            Header = request.Header;
            Header.QR = true;
            Questions = new List<Question>(request);
            Answers = new List<Record>(new[] { record });
            Authorities = new List<Record>();
            Additionals = new List<Record>();

            MessageSize = 0;
        }

        /// <summary>
        /// With same header of request
        /// </summary>
        /// <param name="request"></param>
        public Response(Request request, IEnumerable<Record> answers)
        {
            Header = request.Header;
            Header.QR = true;
            Questions = new List<Question>(request);
            Answers = answers.ToList();
            Authorities = new List<Record>();
            Additionals = new List<Record>();

            MessageSize = 0;
        }

        public Record[] AllRecords
        {
            get { return Answers.Concat(Authorities.Concat(Additionals)).ToArray(); }
        }
    }
}