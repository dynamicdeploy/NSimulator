#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162

#region

using System.CodeDom.Compiler;
using Antlr.Runtime;
using Stack = System.Collections.Generic.Stack <object>;
using List = System.Collections.IList;
using ArrayList = System.Collections.Generic.List <object>;

#endregion

namespace NModel.Extension {
    [GeneratedCode ("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
    internal partial class LTLParserParser : Parser {
        internal static readonly string [] tokenNames = new [] {
                                                                   "<invalid>", "<EOR>", "<DOWN>", "<UP>", "OR", "AND",
                                                                   "U", "R", "NOT", "X", "F", "G", "LP", "RP", "LB",
                                                                   "ATOM", "RB", "TRUE", "FALSE", "WS"
                                                               };

        public const int EOF = -1;
        public const int OR = 4;
        public const int AND = 5;
        public const int U = 6;
        public const int R = 7;
        public const int NOT = 8;
        public const int X = 9;
        public const int F = 10;
        public const int G = 11;
        public const int LP = 12;
        public const int RP = 13;
        public const int LB = 14;
        public const int ATOM = 15;
        public const int RB = 16;
        public const int TRUE = 17;
        public const int FALSE = 18;
        public const int WS = 19;

        // delegates
        // delegators

#if ANTLR_DEBUG
		private static readonly bool[] decisionCanBacktrack =
			new bool[]
			{
				false, // invalid decision
				false, false, false, false, false
			};
#else
        private static readonly bool [] decisionCanBacktrack = new bool[0];
#endif

        public LTLParserParser (ITokenStream input)
            : this (input, new RecognizerSharedState ()) {}

        public LTLParserParser (ITokenStream input, RecognizerSharedState state)
            : base (input, state) {
            this.OnCreated ();
        }


        public override string [] TokenNames {
            get { return tokenNames; }
        }

        public override string GrammarFileName {
            get { return "D:\\Compilers\\Verification\\LTLParser.g"; }
        }


        partial void OnCreated ();
        partial void EnterRule (string ruleName, int ruleIndex);
        partial void LeaveRule (string ruleName, int ruleIndex);

        #region Rules

        partial void Enter_start ();
        partial void Leave_start ();

        // $ANTLR start "start"
        // D:\\Compilers\\Verification\\LTLParser.g:10:1: public start returns [LTLFormula f] : ltlexpr ;
        [GrammarRule ("start")]
        public LTLFormula start () {
            this.Enter_start ();
            this.EnterRule ("start", 1);
            this.TraceIn ("start", 1);
            LTLFormula f = default(LTLFormula);

            LTLFormula ltlexpr1 = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "start");
                this.DebugLocation (10, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:12:2: ( ltlexpr )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\LTLParser.g:12:4: ltlexpr
                    {
                        this.DebugLocation (12, 4);
                        this.PushFollow (Follow._ltlexpr_in_start45);
                        ltlexpr1 = this.ltlexpr ();
                        this.PopFollow ();

                        this.DebugLocation (12, 12);
                        f = ltlexpr1;
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("start", 1);
                    this.LeaveRule ("start", 1);
                    this.Leave_start ();
                }
                this.DebugLocation (13, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "start");
            }
            return f;
        }

        // $ANTLR end "start"


        partial void Enter_ltlexpr ();
        partial void Leave_ltlexpr ();

        // $ANTLR start "ltlexpr"
        // D:\\Compilers\\Verification\\LTLParser.g:15:1: ltlexpr returns [LTLFormula f] : e= t1 ( OR e= t1 )* ;
        [GrammarRule ("ltlexpr")]
        private LTLFormula ltlexpr () {
            this.Enter_ltlexpr ();
            this.EnterRule ("ltlexpr", 2);
            this.TraceIn ("ltlexpr", 2);
            LTLFormula f = default(LTLFormula);

            LTLFormula e = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "ltlexpr");
                this.DebugLocation (15, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:16:2: (e= t1 ( OR e= t1 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\LTLParser.g:16:4: e= t1 ( OR e= t1 )*
                    {
                        this.DebugLocation (16, 5);
                        this.PushFollow (Follow._t1_in_ltlexpr64);
                        e = this.t1 ();
                        this.PopFollow ();

                        this.DebugLocation (16, 9);
                        f = e;
                        this.DebugLocation (17, 3);
                        // D:\\Compilers\\Verification\\LTLParser.g:17:3: ( OR e= t1 )*
                        try {
                            this.DebugEnterSubRule (1);
                            while (true) {
                                int alt1 = 2;
                                try {
                                    this.DebugEnterDecision (1, decisionCanBacktrack [1]);
                                    int LA1_0 = this.input.LA (1);

                                    if ((LA1_0 == OR))
                                        alt1 = 1;
                                }
                                finally {
                                    this.DebugExitDecision (1);
                                }
                                switch (alt1) {
                                    case 1 :
                                        this.DebugEnterAlt (1);
                                        // D:\\Compilers\\Verification\\LTLParser.g:17:4: OR e= t1
                                    {
                                        this.DebugLocation (17, 4);
                                        this.Match (this.input, OR, Follow._OR_in_ltlexpr71);
                                        this.DebugLocation (17, 8);
                                        this.PushFollow (Follow._t1_in_ltlexpr75);
                                        e = this.t1 ();
                                        this.PopFollow ();

                                        this.DebugLocation (17, 12);
                                        f |= e;
                                    }
                                        break;

                                    default :
                                        goto loop1;
                                }
                            }

                            loop1:
                            ;
                        }
                        finally {
                            this.DebugExitSubRule (1);
                        }
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("ltlexpr", 2);
                    this.LeaveRule ("ltlexpr", 2);
                    this.Leave_ltlexpr ();
                }
                this.DebugLocation (18, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "ltlexpr");
            }
            return f;
        }

        // $ANTLR end "ltlexpr"


        partial void Enter_t1 ();
        partial void Leave_t1 ();

        // $ANTLR start "t1"
        // D:\\Compilers\\Verification\\LTLParser.g:20:1: t1 returns [LTLFormula f] : e= t2 ( AND e= t2 )* ;
        [GrammarRule ("t1")]
        private LTLFormula t1 () {
            this.Enter_t1 ();
            this.EnterRule ("t1", 3);
            this.TraceIn ("t1", 3);
            LTLFormula f = default(LTLFormula);

            LTLFormula e = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t1");
                this.DebugLocation (20, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:21:2: (e= t2 ( AND e= t2 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\LTLParser.g:21:4: e= t2 ( AND e= t2 )*
                    {
                        this.DebugLocation (21, 5);
                        this.PushFollow (Follow._t2_in_t197);
                        e = this.t2 ();
                        this.PopFollow ();

                        this.DebugLocation (21, 9);
                        f = e;
                        this.DebugLocation (22, 3);
                        // D:\\Compilers\\Verification\\LTLParser.g:22:3: ( AND e= t2 )*
                        try {
                            this.DebugEnterSubRule (2);
                            while (true) {
                                int alt2 = 2;
                                try {
                                    this.DebugEnterDecision (2, decisionCanBacktrack [2]);
                                    int LA2_0 = this.input.LA (1);

                                    if ((LA2_0 == AND))
                                        alt2 = 1;
                                }
                                finally {
                                    this.DebugExitDecision (2);
                                }
                                switch (alt2) {
                                    case 1 :
                                        this.DebugEnterAlt (1);
                                        // D:\\Compilers\\Verification\\LTLParser.g:22:4: AND e= t2
                                    {
                                        this.DebugLocation (22, 4);
                                        this.Match (this.input, AND, Follow._AND_in_t1104);
                                        this.DebugLocation (22, 9);
                                        this.PushFollow (Follow._t2_in_t1108);
                                        e = this.t2 ();
                                        this.PopFollow ();

                                        this.DebugLocation (22, 13);
                                        f &= e;
                                    }
                                        break;

                                    default :
                                        goto loop2;
                                }
                            }

                            loop2:
                            ;
                        }
                        finally {
                            this.DebugExitSubRule (2);
                        }
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("t1", 3);
                    this.LeaveRule ("t1", 3);
                    this.Leave_t1 ();
                }
                this.DebugLocation (23, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "t1");
            }
            return f;
        }

        // $ANTLR end "t1"


        partial void Enter_t2 ();
        partial void Leave_t2 ();

        // $ANTLR start "t2"
        // D:\\Compilers\\Verification\\LTLParser.g:25:1: t2 returns [LTLFormula f] : (e= t3 ) ( U e= t3 )* ;
        [GrammarRule ("t2")]
        private LTLFormula t2 () {
            this.Enter_t2 ();
            this.EnterRule ("t2", 4);
            this.TraceIn ("t2", 4);
            LTLFormula f = default(LTLFormula);

            LTLFormula e = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t2");
                this.DebugLocation (25, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:26:2: ( (e= t3 ) ( U e= t3 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\LTLParser.g:26:5: (e= t3 ) ( U e= t3 )*
                    {
                        this.DebugLocation (26, 5);
                        // D:\\Compilers\\Verification\\LTLParser.g:26:5: (e= t3 )
                        this.DebugEnterAlt (1);
                        // D:\\Compilers\\Verification\\LTLParser.g:26:6: e= t3
                        {
                            this.DebugLocation (26, 7);
                            this.PushFollow (Follow._t3_in_t2132);
                            e = this.t3 ();
                            this.PopFollow ();

                            this.DebugLocation (26, 11);
                            f = e;
                        }

                        this.DebugLocation (27, 3);
                        // D:\\Compilers\\Verification\\LTLParser.g:27:3: ( U e= t3 )*
                        try {
                            this.DebugEnterSubRule (3);
                            while (true) {
                                int alt3 = 2;
                                try {
                                    this.DebugEnterDecision (3, decisionCanBacktrack [3]);
                                    int LA3_0 = this.input.LA (1);

                                    if ((LA3_0 == U))
                                        alt3 = 1;
                                }
                                finally {
                                    this.DebugExitDecision (3);
                                }
                                switch (alt3) {
                                    case 1 :
                                        this.DebugEnterAlt (1);
                                        // D:\\Compilers\\Verification\\LTLParser.g:27:4: U e= t3
                                    {
                                        this.DebugLocation (27, 4);
                                        this.Match (this.input, U, Follow._U_in_t2140);
                                        this.DebugLocation (27, 7);
                                        this.PushFollow (Follow._t3_in_t2144);
                                        e = this.t3 ();
                                        this.PopFollow ();

                                        this.DebugLocation (27, 11);
                                        f = LTLFormula.U (f, e);
                                    }
                                        break;

                                    default :
                                        goto loop3;
                                }
                            }

                            loop3:
                            ;
                        }
                        finally {
                            this.DebugExitSubRule (3);
                        }
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("t2", 4);
                    this.LeaveRule ("t2", 4);
                    this.Leave_t2 ();
                }
                this.DebugLocation (28, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "t2");
            }
            return f;
        }

        // $ANTLR end "t2"


        partial void Enter_t3 ();
        partial void Leave_t3 ();

        // $ANTLR start "t3"
        // D:\\Compilers\\Verification\\LTLParser.g:30:1: t3 returns [LTLFormula f] : e= t4 ( R e= t4 )* ;
        [GrammarRule ("t3")]
        private LTLFormula t3 () {
            this.Enter_t3 ();
            this.EnterRule ("t3", 5);
            this.TraceIn ("t3", 5);
            LTLFormula f = default(LTLFormula);

            LTLFormula e = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t3");
                this.DebugLocation (30, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:31:2: (e= t4 ( R e= t4 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\LTLParser.g:31:4: e= t4 ( R e= t4 )*
                    {
                        this.DebugLocation (31, 5);
                        this.PushFollow (Follow._t4_in_t3166);
                        e = this.t4 ();
                        this.PopFollow ();

                        this.DebugLocation (31, 9);
                        f = e;
                        this.DebugLocation (32, 3);
                        // D:\\Compilers\\Verification\\LTLParser.g:32:3: ( R e= t4 )*
                        try {
                            this.DebugEnterSubRule (4);
                            while (true) {
                                int alt4 = 2;
                                try {
                                    this.DebugEnterDecision (4, decisionCanBacktrack [4]);
                                    int LA4_0 = this.input.LA (1);

                                    if ((LA4_0 == R))
                                        alt4 = 1;
                                }
                                finally {
                                    this.DebugExitDecision (4);
                                }
                                switch (alt4) {
                                    case 1 :
                                        this.DebugEnterAlt (1);
                                        // D:\\Compilers\\Verification\\LTLParser.g:32:4: R e= t4
                                    {
                                        this.DebugLocation (32, 4);
                                        this.Match (this.input, R, Follow._R_in_t3173);
                                        this.DebugLocation (32, 7);
                                        this.PushFollow (Follow._t4_in_t3177);
                                        e = this.t4 ();
                                        this.PopFollow ();

                                        this.DebugLocation (32, 11);
                                        f = LTLFormula.R (f, e);
                                    }
                                        break;

                                    default :
                                        goto loop4;
                                }
                            }

                            loop4:
                            ;
                        }
                        finally {
                            this.DebugExitSubRule (4);
                        }
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("t3", 5);
                    this.LeaveRule ("t3", 5);
                    this.Leave_t3 ();
                }
                this.DebugLocation (33, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "t3");
            }
            return f;
        }

        // $ANTLR end "t3"


        partial void Enter_t4 ();
        partial void Leave_t4 ();

        // $ANTLR start "t4"
        // D:\\Compilers\\Verification\\LTLParser.g:35:1: t4 returns [LTLFormula f] : ( ( NOT e= t4 ) | ( X e= t4 ) | ( F e= t4 ) | ( G e= t4 ) | ( LP e= ltlexpr RP ) | ( LB ATOM RB ) | ( TRUE ) | ( FALSE ) );
        [GrammarRule ("t4")]
        private LTLFormula t4 () {
            this.Enter_t4 ();
            this.EnterRule ("t4", 6);
            this.TraceIn ("t4", 6);
            LTLFormula f = default(LTLFormula);

            IToken ATOM2 = null;
            LTLFormula e = default(LTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t4");
                this.DebugLocation (35, 1);
                try {
                    // D:\\Compilers\\Verification\\LTLParser.g:36:2: ( ( NOT e= t4 ) | ( X e= t4 ) | ( F e= t4 ) | ( G e= t4 ) | ( LP e= ltlexpr RP ) | ( LB ATOM RB ) | ( TRUE ) | ( FALSE ) )
                    int alt5 = 8;
                    try {
                        this.DebugEnterDecision (5, decisionCanBacktrack [5]);
                        switch (this.input.LA (1)) {
                            case NOT : {
                                alt5 = 1;
                            }
                                break;
                            case X : {
                                alt5 = 2;
                            }
                                break;
                            case F : {
                                alt5 = 3;
                            }
                                break;
                            case G : {
                                alt5 = 4;
                            }
                                break;
                            case LP : {
                                alt5 = 5;
                            }
                                break;
                            case LB : {
                                alt5 = 6;
                            }
                                break;
                            case TRUE : {
                                alt5 = 7;
                            }
                                break;
                            case FALSE : {
                                alt5 = 8;
                            }
                                break;
                            default : {
                                NoViableAltException nvae = new NoViableAltException ("", 5, 0, this.input);

                                this.DebugRecognitionException (nvae);
                                throw nvae;
                            }
                        }
                    }
                    finally {
                        this.DebugExitDecision (5);
                    }
                    switch (alt5) {
                        case 1 :
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:36:4: ( NOT e= t4 )
                        {
                            this.DebugLocation (36, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:36:4: ( NOT e= t4 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:36:5: NOT e= t4
                            {
                                this.DebugLocation (36, 5);
                                this.Match (this.input, NOT, Follow._NOT_in_t4198);
                                this.DebugLocation (36, 10);
                                this.PushFollow (Follow._t4_in_t4202);
                                e = this.t4 ();
                                this.PopFollow ();

                                this.DebugLocation (36, 14);
                                f = ! e;
                            }
                        }
                            break;
                        case 2 :
                            this.DebugEnterAlt (2);
                            // D:\\Compilers\\Verification\\LTLParser.g:37:4: ( X e= t4 )
                        {
                            this.DebugLocation (37, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:37:4: ( X e= t4 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:37:5: X e= t4
                            {
                                this.DebugLocation (37, 5);
                                this.Match (this.input, X, Follow._X_in_t4211);
                                this.DebugLocation (37, 8);
                                this.PushFollow (Follow._t4_in_t4215);
                                e = this.t4 ();
                                this.PopFollow ();

                                this.DebugLocation (37, 12);
                                f = LTLFormula.X (e);
                            }
                        }
                            break;
                        case 3 :
                            this.DebugEnterAlt (3);
                            // D:\\Compilers\\Verification\\LTLParser.g:38:4: ( F e= t4 )
                        {
                            this.DebugLocation (38, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:38:4: ( F e= t4 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:38:5: F e= t4
                            {
                                this.DebugLocation (38, 5);
                                this.Match (this.input, F, Follow._F_in_t4224);
                                this.DebugLocation (38, 8);
                                this.PushFollow (Follow._t4_in_t4228);
                                e = this.t4 ();
                                this.PopFollow ();

                                this.DebugLocation (38, 12);
                                f = LTLFormula.F (e);
                            }
                        }
                            break;
                        case 4 :
                            this.DebugEnterAlt (4);
                            // D:\\Compilers\\Verification\\LTLParser.g:39:4: ( G e= t4 )
                        {
                            this.DebugLocation (39, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:39:4: ( G e= t4 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:39:5: G e= t4
                            {
                                this.DebugLocation (39, 5);
                                this.Match (this.input, G, Follow._G_in_t4237);
                                this.DebugLocation (39, 8);
                                this.PushFollow (Follow._t4_in_t4241);
                                e = this.t4 ();
                                this.PopFollow ();

                                this.DebugLocation (39, 12);
                                f = LTLFormula.G (e);
                            }
                        }
                            break;
                        case 5 :
                            this.DebugEnterAlt (5);
                            // D:\\Compilers\\Verification\\LTLParser.g:40:4: ( LP e= ltlexpr RP )
                        {
                            this.DebugLocation (40, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:40:4: ( LP e= ltlexpr RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:40:5: LP e= ltlexpr RP
                            {
                                this.DebugLocation (40, 5);
                                this.Match (this.input, LP, Follow._LP_in_t4250);
                                this.DebugLocation (40, 9);
                                this.PushFollow (Follow._ltlexpr_in_t4254);
                                e = this.ltlexpr ();
                                this.PopFollow ();

                                this.DebugLocation (40, 18);
                                this.Match (this.input, RP, Follow._RP_in_t4256);
                                this.DebugLocation (40, 21);
                                f = e;
                            }
                        }
                            break;
                        case 6 :
                            this.DebugEnterAlt (6);
                            // D:\\Compilers\\Verification\\LTLParser.g:41:4: ( LB ATOM RB )
                        {
                            this.DebugLocation (41, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:41:4: ( LB ATOM RB )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:41:5: LB ATOM RB
                            {
                                this.DebugLocation (41, 5);
                                this.Match (this.input, LB, Follow._LB_in_t4265);
                                this.DebugLocation (41, 8);
                                ATOM2 = (IToken) this.Match (this.input, ATOM, Follow._ATOM_in_t4267);
                                this.DebugLocation (41, 13);
                                this.Match (this.input, RB, Follow._RB_in_t4269);
                                this.DebugLocation (41, 16);
                                f = new LTLFormula ((ATOM2 != null ? ATOM2.Text : null));
                            }
                        }
                            break;
                        case 7 :
                            this.DebugEnterAlt (7);
                            // D:\\Compilers\\Verification\\LTLParser.g:42:4: ( TRUE )
                        {
                            this.DebugLocation (42, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:42:4: ( TRUE )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:42:5: TRUE
                            {
                                this.DebugLocation (42, 5);
                                this.Match (this.input, TRUE, Follow._TRUE_in_t4278);
                                this.DebugLocation (42, 10);
                                f = LTLFormula.TRUE;
                            }
                        }
                            break;
                        case 8 :
                            this.DebugEnterAlt (8);
                            // D:\\Compilers\\Verification\\LTLParser.g:43:4: ( FALSE )
                        {
                            this.DebugLocation (43, 4);
                            // D:\\Compilers\\Verification\\LTLParser.g:43:4: ( FALSE )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\LTLParser.g:43:5: FALSE
                            {
                                this.DebugLocation (43, 5);
                                this.Match (this.input, FALSE, Follow._FALSE_in_t4287);
                                this.DebugLocation (43, 11);
                                f = LTLFormula.FALSE;
                            }
                        }
                            break;
                    }
                }
                catch (RecognitionException re) {
                    this.ReportError (re);
                    this.Recover (this.input, re);
                }
                finally {
                    this.TraceOut ("t4", 6);
                    this.LeaveRule ("t4", 6);
                    this.Leave_t4 ();
                }
                this.DebugLocation (44, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "t4");
            }
            return f;
        }

        // $ANTLR end "t4"

        #endregion Rules

        #region Follow sets

        private static class Follow {
            public static readonly BitSet _ltlexpr_in_start45 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _t1_in_ltlexpr64 = new BitSet (new [] { 0x12UL });
            public static readonly BitSet _OR_in_ltlexpr71 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t1_in_ltlexpr75 = new BitSet (new [] { 0x12UL });
            public static readonly BitSet _t2_in_t197 = new BitSet (new [] { 0x22UL });
            public static readonly BitSet _AND_in_t1104 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t2_in_t1108 = new BitSet (new [] { 0x22UL });
            public static readonly BitSet _t3_in_t2132 = new BitSet (new [] { 0x42UL });
            public static readonly BitSet _U_in_t2140 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t3_in_t2144 = new BitSet (new [] { 0x42UL });
            public static readonly BitSet _t4_in_t3166 = new BitSet (new [] { 0x82UL });
            public static readonly BitSet _R_in_t3173 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t4_in_t3177 = new BitSet (new [] { 0x82UL });
            public static readonly BitSet _NOT_in_t4198 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t4_in_t4202 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _X_in_t4211 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t4_in_t4215 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _F_in_t4224 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t4_in_t4228 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _G_in_t4237 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _t4_in_t4241 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _LP_in_t4250 = new BitSet (new [] { 0x65F00UL });
            public static readonly BitSet _ltlexpr_in_t4254 = new BitSet (new [] { 0x2000UL });
            public static readonly BitSet _RP_in_t4256 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _LB_in_t4265 = new BitSet (new [] { 0x8000UL });
            public static readonly BitSet _ATOM_in_t4267 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RB_in_t4269 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _TRUE_in_t4278 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _FALSE_in_t4287 = new BitSet (new [] { 0x2UL });
        }

        #endregion Follow sets
    }
}

// namespace  NModel.Extension 
