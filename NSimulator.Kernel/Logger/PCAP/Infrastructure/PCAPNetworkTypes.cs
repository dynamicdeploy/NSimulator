namespace NSimulator.Kernel {
    /// <summary>
    ///   Pcap-типы сетей.
    /// </summary>
    public enum PCAPNetworkTypes : uint {
        /// <summary>
        ///   No link layer information. A packet saved with this link layer contains
        ///   a raw L3 packet preceded by a 32-bit host-byte-order AF_ value indicating
        ///   the specific L3 type.
        /// </summary>
        LINKTYPE_NULL = 0,

        /// <summary>
        ///   D/I/X and 802.3 Ethernet
        /// </summary>
        LINKTYPE_ETHERNET = 1,

        /// <summary>
        ///   Experimental Ethernet (3Mb)
        /// </summary>
        LINKTYPE_EXP_ETHERNET = 2,

        /// <summary>
        ///   Amateur Radio AX.25
        /// </summary>
        LINKTYPE_AX25 = 3,

        /// <summary>
        ///   Proteon ProNET Token Ring
        /// </summary>
        LINKTYPE_PRONET = 4,

        /// <summary>
        ///   Chaos
        /// </summary>
        LINKTYPE_CHAOS = 5,

        /// <summary>
        ///   IEEE 802 Networks
        /// </summary>
        LINKTYPE_TOKEN_RING = 6,

        /// <summary>
        ///   ARCNET, with BSD-style header
        /// </summary>
        LINKTYPE_ARCNET = 7,

        /// <summary>
        ///   Serial Line IP
        /// </summary>
        LINKTYPE_SLIP = 8,

        /// <summary>
        ///   Point-to-point Protocol
        /// </summary>
        LINKTYPE_PPP = 9,

        /// <summary>
        ///   FDDI
        /// </summary>
        LINKTYPE_FDDI = 10,

        /// <summary>
        ///   PPP in HDLC-like framing
        /// </summary>
        LINKTYPE_PPP_HDLC = 50,

        /// <summary>
        ///   NetBSD PPP-over-Ethernet
        /// </summary>
        LINKTYPE_PPP_ETHER = 51,

        /// <summary>
        ///   Symantec Enterprise Firewall
        /// </summary>
        LINKTYPE_SYMANTEC_FIREWALL = 99,

        /// <summary>
        ///   LLC/SNAP-encapsulated ATM
        /// </summary>
        LINKTYPE_ATM_RFC1483 = 100,

        /// <summary>
        ///   Raw IP
        /// </summary>
        LINKTYPE_RAW = 101,

        /// <summary>
        ///   BSD/OS SLIP BPF header
        /// </summary>
        LINKTYPE_SLIP_BSDOS = 102,

        /// <summary>
        ///   BSD/OS PPP BPF header
        /// </summary>
        LINKTYPE_PPP_BSDOS = 103,

        /// <summary>
        ///   Cisco HDLC
        /// </summary>
        LINKTYPE_C_HDLC = 104,

        /// <summary>
        ///   IEEE 802.11 (wireless)
        /// </summary>
        LINKTYPE_IEEE802_11 = 105,

        /// <summary>
        ///   Linux Classical IP over ATM
        /// </summary>
        LINKTYPE_ATM_CLIP = 106,

        /// <summary>
        ///   Frame Relay
        /// </summary>
        LINKTYPE_FRELAY = 107,

        /// <summary>
        ///   OpenBSD loopback
        /// </summary>
        LINKTYPE_LOOP = 108,

        /// <summary>
        ///   OpenBSD IPSEC enc
        /// </summary>
        LINKTYPE_ENC = 109,

        /// <summary>
        ///   ATM LANE + 802.3 (Reserved for future use)
        /// </summary>
        LINKTYPE_LANE8023 = 110,

        /// <summary>
        ///   NetBSD HIPPI (Reserved for future use)
        /// </summary>
        LINKTYPE_HIPPI = 111,

        /// <summary>
        ///   NetBSD HDLC framing (Reserved for future use)
        /// </summary>
        LINKTYPE_HDLC = 112,

        /// <summary>
        ///   Linux cooked socket capture
        /// </summary>
        LINKTYPE_LINUX_SLL = 113,

        /// <summary>
        ///   Apple LocalTalk hardware
        /// </summary>
        LINKTYPE_LTALK = 114,

        /// <summary>
        ///   Acorn Econet
        /// </summary>
        LINKTYPE_ECONET = 115,

        /// <summary>
        ///   Reserved for use with OpenBSD ipfilter
        /// </summary>
        LINKTYPE_IPFILTER = 116,

        /// <summary>
        ///   OpenBSD DLT_PFLOG
        /// </summary>
        LINKTYPE_PFLOG = 117,

        /// <summary>
        ///   For Cisco-internal use
        /// </summary>
        LINKTYPE_CISCO_IOS = 118,

        /// <summary>
        ///   802.11+Prism II monitor mode
        /// </summary>
        LINKTYPE_PRISM_HEADER = 119,

        /// <summary>
        ///   FreeBSD Aironet driver stuff
        /// </summary>
        LINKTYPE_AIRONET_HEADER = 120,

        /// <summary>
        ///   Reserved for Siemens HiPath HDLC
        /// </summary>
        LINKTYPE_HHDLC = 121,

        /// <summary>
        ///   RFC 2625 IP-over-Fibre Channel
        /// </summary>
        LINKTYPE_IP_OVER_FC = 122,

        /// <summary>
        ///   Solaris+SunATM
        /// </summary>
        LINKTYPE_SUNATM = 123,

        /// <summary>
        ///   RapidIO - Reserved as per request from
        ///   Kent Dahlgren (kent@praesum.com) for private use.
        /// </summary>
        LINKTYPE_RIO = 124,

        /// <summary>
        ///   PCI Express - Reserved as per request from
        ///   Kent Dahlgren (kent@praesum.com) for private use.
        /// </summary>
        LINKTYPE_PCI_EXP = 125,

        /// <summary>
        ///   Xilinx Aurora link layer - Reserved as per request from
        ///   Kent Dahlgren (kent@praesum.com) for private use.
        /// </summary>
        LINKTYPE_AURORA = 126,

        /// <summary>
        ///   802.11 plus BSD radio header
        /// </summary>
        LINKTYPE_IEEE802_11_RADIO = 127,

        /// <summary>
        ///   Tazmen Sniffer Protocol - Reserved for the TZSP encapsulation, as per request from
        ///   Chris Waters (chris.waters@networkchemistry.com) TZSP is a generic encapsulation for
        ///   any other link type, which includes a means to include meta-information with the
        ///   packet, e.g. signal strength and channel for 802.11 packets.
        /// </summary>
        LINKTYPE_TZSP = 128,

        /// <summary>
        ///   Linux-style headers
        /// </summary>
        LINKTYPE_ARCNET_LINUX = 129,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_MLPPP = 130,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_MLFR = 131,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_ES = 132,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_GGSN = 133,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_MFR = 134,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_ATM2 = 135,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_SERVICES = 136,

        /// <summary>
        ///   Juniper-private data link type, as per request from Hannes Gredler (hannes@juniper.net).
        ///   The corresponding DLT_s are used for passing on chassis-internal metainformation
        ///   such as QOS profiles, etc..
        /// </summary>
        LINKTYPE_JUNIPER_ATM1 = 137,

        /// <summary>
        ///   Apple IP-over-IEEE 1394 cooked header
        /// </summary>
        LINKTYPE_APPLE_IP_OVER_IEEE1394 = 138,

        /// <summary>
        ///   ???
        /// </summary>
        LINKTYPE_MTP2_WITH_PHDR = 139,

        /// <summary>
        ///   ???
        /// </summary>
        LINKTYPE_MTP2 = 140,

        /// <summary>
        ///   ???
        /// </summary>
        LINKTYPE_MTP3 = 141,

        /// <summary>
        ///   ???
        /// </summary>
        LINKTYPE_SCCP = 142,

        /// <summary>
        ///   DOCSIS MAC frames
        /// </summary>
        LINKTYPE_DOCSIS = 143,

        /// <summary>
        ///   Linux-IrDA
        /// </summary>
        LINKTYPE_LINUX_IRDA = 144,

        /// <summary>
        ///   Reserved for IBM SP switch and IBM Next Federation switch.
        /// </summary>
        LINKTYPE_IBM_SP = 145,

        /// <summary>
        ///   Reserved for IBM SP switch and IBM Next Federation switch.
        /// </summary>
        LINKTYPE_IBM_SN = 146,
    }
}
