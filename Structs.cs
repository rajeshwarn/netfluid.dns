/*
 * http://www.iana.org/assignments/dns-parameters
 * 
 * 
 * 
 */


using System;

namespace Netfluid.Dns
{
    /*
	 * 3.2.2. TYPE values
	 *
	 * TYPE fields are used in resource records.
	 * Note that these types are a subset of QTYPEs.
	 *
	 *		TYPE		value			meaning
	 */
    
    /// <summary>
    /// DNS protocol record type
    /// </summary>
    [Serializable]
    public enum RecordType : ushort
    {
        A = 1, // a IPV4 host address
        NS = 2, // an authoritative name server
        MD = 3, // a mail destination (Obsolete - use MX)
        MF = 4, // a mail forwarder (Obsolete - use MX)
        CNAME = 5, // the canonical name for an alias
        SOA = 6, // marks the start of a zone of authority
        MB = 7, // a mailbox domain name (EXPERIMENTAL)
        MG = 8, // a mail group member (EXPERIMENTAL)
        MR = 9, // a mail rename domain name (EXPERIMENTAL)
        NULL = 10, // a null RR (EXPERIMENTAL)
        WKS = 11, // a well known service description
        PTR = 12, // a domain name pointer
        HINFO = 13, // host information
        MINFO = 14, // mailbox or mail list information
        MX = 15, // mail exchange
        TXT = 16, // text strings

        RP = 17, // The Responsible Person rfc1183
        AFSDB = 18, // AFS Write Base location
        X25 = 19, // X.25 address rfc1183
        ISDN = 20, // ISDN address rfc1183 
        RT = 21, // The Route Through rfc1183

        NSAP = 22, // Network service access point address rfc1706
        NSAPPTR = 23, // Obsolete, rfc1348

        SIG = 24, // Cryptographic public key signature rfc2931 / rfc2535
        KEY = 25, // Public key as used in DNSSEC rfc2535

        PX = 26, // Pointer to X.400/RFC822 mail mapping information rfc2163

        GPOS = 27, // Geographical position rfc1712 (obsolete)

        AAAA = 28, // a IPV6 host address, rfc3596

        LOC = 29, // Location information rfc1876

        NXT = 30, // Next Domain, Obsolete rfc2065 / rfc2535

        EID = 31, // *** Endpoint Identifier (Patton)
        NIMLOC = 32, // *** Nimrod Locator (Patton)

        SRV = 33, // Location of services rfc2782

        ATMA = 34, // *** ATM Address (Dobrowski)

        NAPTR = 35, // The Naming Authority Pointer rfc3403

        KX = 36, // Key Exchange Delegation Record rfc2230

        CERT = 37, // *** CERT RFC2538

        A6 = 38, // IPv6 address rfc3363 (rfc2874 rfc3226)
        DNAME = 39, // A way to provide aliases for a whole domain, not just a single domain name as with CNAME. rfc2672

        SINK = 40, // *** SINK Eastlake
        OPT = 41, // *** OPT RFC2671

        APL = 42, // *** APL [RFC3123]

        DS = 43, // Delegation Signer rfc3658

        SSHFP = 44, // SSH Key Fingerprint rfc4255
        IPSECKEY = 45, // IPSECKEY rfc4025
        RRSIG = 46, // RRSIG rfc3755
        NSEC = 47, // NSEC rfc3755
        DNSKEY = 48, // DNSKEY 3755
        DHCID = 49, // DHCID rfc4701

        NSEC3 = 50, // NSEC3 rfc5155
        NSEC3PARAM = 51, // NSEC3PARAM rfc5155

        HIP = 55, // Domain Identity Protocol  [RFC-ietf-hip-dns-09.txt]

        SPF = 99, // SPF rfc4408

        UINFO = 100, // *** IANA-Reserved
        UID = 101, // *** IANA-Reserved
        GID = 102, // *** IANA-Reserved
        UNSPEC = 103, // *** IANA-Reserved

        TKEY = 249, // Transaction key rfc2930
        TSIG = 250, // Transaction signature rfc2845

        TA = 32768, // DNSSEC Trust Authorities          [Weiler]  13 December 2005
        DLV = 32769, // DNSSEC Lookaside Validation       [RFC4431]

        //////// QUESTION ONLY TYPES

        IXFR = 251, // incremental transfer                  [RFC1995]
        AXFR = 252, // transfer of an entire zone            [RFC1035]
        MAILB = 253, // mailbox-related RRs (MB, MG or MR)    [RFC1035]
        MAILA = 254, // mail agent RRs (Obsolete - see MX)    [RFC1035]
        ANY = 255, // A request for all records             [RFC1035]


    }

    /*
	 * 3.2.4. CLASS values
	 *
	 * CLASS fields appear in resource records.  The following CLASS mnemonics
	 *and values are defined:
	 *
	 *		CLASS		value			meaning
	 */

    /// <summary>
    /// DNS protocol address class
    /// </summary>
    [Serializable]
    public enum Class : ushort
    {
        IN = 1, // the Internet
        CS = 2, // the CSNET class (Obsolete - used only for examples in some obsolete RFCs)
        CH = 3, // the CHAOS class
        HS = 4, // Hesiod [Dyer 87]

        //VALID ONLY INTO QUESTIONS
       ANY = 255 // any class
    }


    /*
RCODE           Response code - this 4 bit field is set as part of
                responses.  The values have the following
                interpretation:
	 */

    /// <summary>
    /// DNS protocol response code
    /// </summary>
    [Serializable]
    public enum RCode
    {
        NoError = 0, // No Error                           [RFC1035]
        FormErr = 1, // Format Error                       [RFC1035]
        ServFail = 2, // Server Failure                     [RFC1035]
        NXDomain = 3, // Non-Existent Domain                [RFC1035]
        NotImp = 4, // Not Implemented                    [RFC1035]
        Refused = 5, // Query Refused                      [RFC1035]
        YXDomain = 6, // Name Exists when it should not     [RFC2136]
        YXRRSet = 7, // RR Set Exists when it should not   [RFC2136]
        NXRRSet = 8, // RR Set that should exist does not  [RFC2136]
        NotAuth = 9, // Server Not Authoritative for zone  [RFC2136]
        NotZone = 10, // Name not contained in zone         [RFC2136]

        RESERVED11 = 11, // Reserved
        RESERVED12 = 12, // Reserved
        RESERVED13 = 13, // Reserved
        RESERVED14 = 14, // Reserved
        RESERVED15 = 15, // Reserved

        BADVERSSIG = 16, // Bad OPT Version                    [RFC2671]
        // TSIG Signature Failure             [RFC2845]
        BADKEY = 17, // Key not recognized                 [RFC2845]
        BADTIME = 18, // Signature out of time window       [RFC2845]
        BADMODE = 19, // Bad TKEY Mode                      [RFC2930]
        BADNAME = 20, // Duplicate key name                 [RFC2930]
        BADALG = 21, // Algorithm not supported            [RFC2930]
        BADTRUNC = 22 // Bad Truncation                     [RFC4635]
        /*
			23-3840              available for assignment
				0x0016-0x0F00
			3841-4095            Private Use
				0x0F01-0x0FFF
			4096-65535           available for assignment
				0x1000-0xFFFF
		*/
    }

    /*
OPCODE          A four bit field that specifies kind of query in this
                message.  This value is set by the originator of a query
                and copied into the response.  The values are:

                0               a standard query (QUERY)

                1               an inverse query (IQUERY)

                2               a server status request (STATUS)

                3-15            reserved for future use
	 */

    /// <summary>
    /// DNS protocol query code
    /// </summary>
     [Serializable]
    public enum OPCode
    {
        Query = 0, // a standard query (QUERY)
        IQUERY = 1, // OpCode Retired (previously IQUERY - No further [RFC3425]
        // assignment of this code available)
        Status = 2, // a server status request (STATUS) RFC1035
        RESERVED3 = 3, // IANA

        Notify = 4, // RFC1996
        Update = 5, // RFC2136

        RESERVED6 = 6,
        RESERVED7 = 7,
        RESERVED8 = 8,
        RESERVED9 = 9,
        RESERVED10 = 10,
        RESERVED11 = 11,
        RESERVED12 = 12,
        RESERVED13 = 13,
        RESERVED14 = 14,
        RESERVED15 = 15,
    }
}