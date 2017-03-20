﻿
using System;

namespace Netfluid.Dns
{
    /*
RCODE           Response code - this 4 bit field is set as part of
                responses.  The values have the following
                interpretation:
	 */

    /// <summary>
    /// DNS protocol response code
    /// </summary>
    [Serializable]
    public enum ResponseCode
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
}
