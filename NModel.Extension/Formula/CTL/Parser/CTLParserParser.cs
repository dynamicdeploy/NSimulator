#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162

#region

using System;
using System.CodeDom.Compiler;
using Antlr.Runtime;
using Stack = System.Collections.Generic.Stack <object>;
using List = System.Collections.IList;
using ArrayList = System.Collections.Generic.List <object>;
using Map = System.Collections.IDictionary;
using HashMap = System.Collections.Generic.Dictionary <object, object>;

#endregion

namespace NModel.Extension {
    [GeneratedCode ("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
    internal partial class CTLParserParser : Parser {
        internal static readonly string [] tokenNames = new [] {
                                                                   "<invalid>", "<EOR>", "<DOWN>", "<UP>", "OR", "AND",
                                                                   "NOT", "EX", "AX", "EG", "AG", "EF", "AF", "E", "LP",
                                                                   "U", "RP", "A", "R", "LB", "ATOM", "RB", "TRUE",
                                                                   "FALSE", "WS"
                                                               };

        public const int EOF = -1;
        public const int OR = 4;
        public const int AND = 5;
        public const int NOT = 6;
        public const int EX = 7;
        public const int AX = 8;
        public const int EG = 9;
        public const int AG = 10;
        public const int EF = 11;
        public const int AF = 12;
        public const int E = 13;
        public const int LP = 14;
        public const int U = 15;
        public const int RP = 16;
        public const int A = 17;
        public const int R = 18;
        public const int LB = 19;
        public const int ATOM = 20;
        public const int RB = 21;
        public const int TRUE = 22;
        public const int FALSE = 23;
        public const int WS = 24;

        // delegates
        // delegators

#if ANTLR_DEBUG
		private static readonly bool[] decisionCanBacktrack =
			new bool[]
			{
				false, // invalid decision
				false, false, true
			};
#else
        private static readonly bool [] decisionCanBacktrack = new bool[0];
#endif

        public CTLParserParser (ITokenStream input)
            : this (input, new RecognizerSharedState ()) {}

        public CTLParserParser (ITokenStream input, RecognizerSharedState state)
            : base (input, state) {
            this.OnCreated ();
        }


        public override string [] TokenNames {
            get { return tokenNames; }
        }

        public override string GrammarFileName {
            get { return "D:\\Compilers\\Verification\\CTLParser.g"; }
        }


        partial void OnCreated ();
        partial void EnterRule (string ruleName, int ruleIndex);
        partial void LeaveRule (string ruleName, int ruleIndex);

        #region Rules

        partial void Enter_start ();
        partial void Leave_start ();

        // $ANTLR start "start"
        // D:\\Compilers\\Verification\\CTLParser.g:11:1: public start returns [CTLFormula f] : ctlexpr ;
        [GrammarRule ("start")]
        public CTLFormula start () {
            this.Enter_start ();
            this.EnterRule ("start", 1);
            this.TraceIn ("start", 1);
            CTLFormula f = default(CTLFormula);

            CTLFormula ctlexpr1 = default(CTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "start");
                this.DebugLocation (11, 1);
                try {
                    // D:\\Compilers\\Verification\\CTLParser.g:13:2: ( ctlexpr )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:13:4: ctlexpr
                    {
                        this.DebugLocation (13, 4);
                        this.PushFollow (Follow._ctlexpr_in_start51);
                        ctlexpr1 = this.ctlexpr ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return f;
                        this.DebugLocation (13, 12);
                        if (this.state.backtracking == 0)
                            f = ctlexpr1;
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
                this.DebugLocation (14, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "start");
            }
            return f;
        }

        // $ANTLR end "start"


        partial void Enter_ctlexpr ();
        partial void Leave_ctlexpr ();

        // $ANTLR start "ctlexpr"
        // D:\\Compilers\\Verification\\CTLParser.g:16:1: ctlexpr returns [CTLFormula f] : e= t1 ( OR e= t1 )* ;
        [GrammarRule ("ctlexpr")]
        private CTLFormula ctlexpr () {
            this.Enter_ctlexpr ();
            this.EnterRule ("ctlexpr", 2);
            this.TraceIn ("ctlexpr", 2);
            CTLFormula f = default(CTLFormula);

            CTLFormula e = default(CTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "ctlexpr");
                this.DebugLocation (16, 1);
                try {
                    // D:\\Compilers\\Verification\\CTLParser.g:17:2: (e= t1 ( OR e= t1 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:17:4: e= t1 ( OR e= t1 )*
                    {
                        this.DebugLocation (17, 5);
                        this.PushFollow (Follow._t1_in_ctlexpr70);
                        e = this.t1 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return f;
                        this.DebugLocation (17, 9);
                        if (this.state.backtracking == 0)
                            f = e;
                        this.DebugLocation (18, 3);
                        // D:\\Compilers\\Verification\\CTLParser.g:18:3: ( OR e= t1 )*
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
                                        // D:\\Compilers\\Verification\\CTLParser.g:18:4: OR e= t1
                                    {
                                        this.DebugLocation (18, 4);
                                        this.Match (this.input, OR, Follow._OR_in_ctlexpr77);
                                        if (this.state.failed)
                                            return f;
                                        this.DebugLocation (18, 8);
                                        this.PushFollow (Follow._t1_in_ctlexpr81);
                                        e = this.t1 ();
                                        this.PopFollow ();
                                        if (this.state.failed)
                                            return f;
                                        this.DebugLocation (18, 12);
                                        if (this.state.backtracking == 0)
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
                    this.TraceOut ("ctlexpr", 2);
                    this.LeaveRule ("ctlexpr", 2);
                    this.Leave_ctlexpr ();
                }
                this.DebugLocation (19, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "ctlexpr");
            }
            return f;
        }

        // $ANTLR end "ctlexpr"


        partial void Enter_t1 ();
        partial void Leave_t1 ();

        // $ANTLR start "t1"
        // D:\\Compilers\\Verification\\CTLParser.g:21:1: t1 returns [CTLFormula f] : e= t2 ( AND e= t2 )* ;
        [GrammarRule ("t1")]
        private CTLFormula t1 () {
            this.Enter_t1 ();
            this.EnterRule ("t1", 3);
            this.TraceIn ("t1", 3);
            CTLFormula f = default(CTLFormula);

            CTLFormula e = default(CTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t1");
                this.DebugLocation (21, 1);
                try {
                    // D:\\Compilers\\Verification\\CTLParser.g:22:2: (e= t2 ( AND e= t2 )* )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:22:4: e= t2 ( AND e= t2 )*
                    {
                        this.DebugLocation (22, 5);
                        this.PushFollow (Follow._t2_in_t1103);
                        e = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return f;
                        this.DebugLocation (22, 9);
                        if (this.state.backtracking == 0)
                            f = e;
                        this.DebugLocation (23, 3);
                        // D:\\Compilers\\Verification\\CTLParser.g:23:3: ( AND e= t2 )*
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
                                        // D:\\Compilers\\Verification\\CTLParser.g:23:4: AND e= t2
                                    {
                                        this.DebugLocation (23, 4);
                                        this.Match (this.input, AND, Follow._AND_in_t1110);
                                        if (this.state.failed)
                                            return f;
                                        this.DebugLocation (23, 9);
                                        this.PushFollow (Follow._t2_in_t1114);
                                        e = this.t2 ();
                                        this.PopFollow ();
                                        if (this.state.failed)
                                            return f;
                                        this.DebugLocation (23, 13);
                                        if (this.state.backtracking == 0)
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
                this.DebugLocation (24, 1);
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
        // D:\\Compilers\\Verification\\CTLParser.g:26:1: t2 returns [CTLFormula f] : ( ( NOT e= t2 ) | ( EX e= t2 ) | ( AX e= t2 ) | ( EG e= t2 ) | ( AG e= t2 ) | ( EF e= t2 ) | ( AF e= t2 ) | ( E LP el= t2 U er= t2 RP ) | ( A LP el= t2 U er= t2 RP ) | ( E LP el= t2 R er= t2 RP ) | ( A LP el= t2 R er= t2 RP ) | ( LP e= ctlexpr RP ) | ( LB ATOM RB ) | ( TRUE ) | ( FALSE ) );
        [GrammarRule ("t2")]
        private CTLFormula t2 () {
            this.Enter_t2 ();
            this.EnterRule ("t2", 4);
            this.TraceIn ("t2", 4);
            CTLFormula f = default(CTLFormula);

            IToken ATOM2 = null;
            CTLFormula e = default(CTLFormula);
            CTLFormula el = default(CTLFormula);
            CTLFormula er = default(CTLFormula);

            try {
                this.DebugEnterRule (this.GrammarFileName, "t2");
                this.DebugLocation (26, 1);
                try {
                    // D:\\Compilers\\Verification\\CTLParser.g:27:2: ( ( NOT e= t2 ) | ( EX e= t2 ) | ( AX e= t2 ) | ( EG e= t2 ) | ( AG e= t2 ) | ( EF e= t2 ) | ( AF e= t2 ) | ( E LP el= t2 U er= t2 RP ) | ( A LP el= t2 U er= t2 RP ) | ( E LP el= t2 R er= t2 RP ) | ( A LP el= t2 R er= t2 RP ) | ( LP e= ctlexpr RP ) | ( LB ATOM RB ) | ( TRUE ) | ( FALSE ) )
                    int alt3 = 15;
                    try {
                        this.DebugEnterDecision (3, decisionCanBacktrack [3]);
                        try {
                            alt3 = this.dfa3.Predict (this.input);
                        }
                        catch (NoViableAltException nvae) {
                            this.DebugRecognitionException (nvae);
                            throw;
                        }
                    }
                    finally {
                        this.DebugExitDecision (3);
                    }
                    switch (alt3) {
                        case 1 :
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:27:4: ( NOT e= t2 )
                        {
                            this.DebugLocation (27, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:27:4: ( NOT e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:27:5: NOT e= t2
                            {
                                this.DebugLocation (27, 5);
                                this.Match (this.input, NOT, Follow._NOT_in_t2135);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (27, 10);
                                this.PushFollow (Follow._t2_in_t2139);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (27, 14);
                                if (this.state.backtracking == 0)
                                    f = ! e;
                            }
                        }
                            break;
                        case 2 :
                            this.DebugEnterAlt (2);
                            // D:\\Compilers\\Verification\\CTLParser.g:28:4: ( EX e= t2 )
                        {
                            this.DebugLocation (28, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:28:4: ( EX e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:28:5: EX e= t2
                            {
                                this.DebugLocation (28, 5);
                                this.Match (this.input, EX, Follow._EX_in_t2148);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (28, 9);
                                this.PushFollow (Follow._t2_in_t2152);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (28, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.EX (e);
                            }
                        }
                            break;
                        case 3 :
                            this.DebugEnterAlt (3);
                            // D:\\Compilers\\Verification\\CTLParser.g:29:4: ( AX e= t2 )
                        {
                            this.DebugLocation (29, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:29:4: ( AX e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:29:5: AX e= t2
                            {
                                this.DebugLocation (29, 5);
                                this.Match (this.input, AX, Follow._AX_in_t2161);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (29, 9);
                                this.PushFollow (Follow._t2_in_t2165);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (29, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.AX (e);
                            }
                        }
                            break;
                        case 4 :
                            this.DebugEnterAlt (4);
                            // D:\\Compilers\\Verification\\CTLParser.g:30:4: ( EG e= t2 )
                        {
                            this.DebugLocation (30, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:30:4: ( EG e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:30:5: EG e= t2
                            {
                                this.DebugLocation (30, 5);
                                this.Match (this.input, EG, Follow._EG_in_t2174);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (30, 9);
                                this.PushFollow (Follow._t2_in_t2178);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (30, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.EG (e);
                            }
                        }
                            break;
                        case 5 :
                            this.DebugEnterAlt (5);
                            // D:\\Compilers\\Verification\\CTLParser.g:31:4: ( AG e= t2 )
                        {
                            this.DebugLocation (31, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:31:4: ( AG e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:31:5: AG e= t2
                            {
                                this.DebugLocation (31, 5);
                                this.Match (this.input, AG, Follow._AG_in_t2187);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (31, 9);
                                this.PushFollow (Follow._t2_in_t2191);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (31, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.AG (e);
                            }
                        }
                            break;
                        case 6 :
                            this.DebugEnterAlt (6);
                            // D:\\Compilers\\Verification\\CTLParser.g:32:4: ( EF e= t2 )
                        {
                            this.DebugLocation (32, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:32:4: ( EF e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:32:5: EF e= t2
                            {
                                this.DebugLocation (32, 5);
                                this.Match (this.input, EF, Follow._EF_in_t2200);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (32, 9);
                                this.PushFollow (Follow._t2_in_t2204);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (32, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.EF (e);
                            }
                        }
                            break;
                        case 7 :
                            this.DebugEnterAlt (7);
                            // D:\\Compilers\\Verification\\CTLParser.g:33:4: ( AF e= t2 )
                        {
                            this.DebugLocation (33, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:33:4: ( AF e= t2 )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:33:5: AF e= t2
                            {
                                this.DebugLocation (33, 5);
                                this.Match (this.input, AF, Follow._AF_in_t2213);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (33, 9);
                                this.PushFollow (Follow._t2_in_t2217);
                                e = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (33, 13);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.AF (e);
                            }
                        }
                            break;
                        case 8 :
                            this.DebugEnterAlt (8);
                            // D:\\Compilers\\Verification\\CTLParser.g:34:4: ( E LP el= t2 U er= t2 RP )
                        {
                            this.DebugLocation (34, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:34:4: ( E LP el= t2 U er= t2 RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:34:5: E LP el= t2 U er= t2 RP
                            {
                                this.DebugLocation (34, 5);
                                this.Match (this.input, E, Follow._E_in_t2226);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 7);
                                this.Match (this.input, LP, Follow._LP_in_t2228);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 12);
                                this.PushFollow (Follow._t2_in_t2232);
                                el = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 16);
                                this.Match (this.input, U, Follow._U_in_t2234);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 20);
                                this.PushFollow (Follow._t2_in_t2238);
                                er = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 24);
                                this.Match (this.input, RP, Follow._RP_in_t2240);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (34, 27);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.EU (el, er);
                            }
                        }
                            break;
                        case 9 :
                            this.DebugEnterAlt (9);
                            // D:\\Compilers\\Verification\\CTLParser.g:35:4: ( A LP el= t2 U er= t2 RP )
                        {
                            this.DebugLocation (35, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:35:4: ( A LP el= t2 U er= t2 RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:35:5: A LP el= t2 U er= t2 RP
                            {
                                this.DebugLocation (35, 5);
                                this.Match (this.input, A, Follow._A_in_t2249);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 7);
                                this.Match (this.input, LP, Follow._LP_in_t2251);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 12);
                                this.PushFollow (Follow._t2_in_t2255);
                                el = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 16);
                                this.Match (this.input, U, Follow._U_in_t2257);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 20);
                                this.PushFollow (Follow._t2_in_t2261);
                                er = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 24);
                                this.Match (this.input, RP, Follow._RP_in_t2263);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (35, 27);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.AU (el, er);
                            }
                        }
                            break;
                        case 10 :
                            this.DebugEnterAlt (10);
                            // D:\\Compilers\\Verification\\CTLParser.g:36:4: ( E LP el= t2 R er= t2 RP )
                        {
                            this.DebugLocation (36, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:36:4: ( E LP el= t2 R er= t2 RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:36:5: E LP el= t2 R er= t2 RP
                            {
                                this.DebugLocation (36, 5);
                                this.Match (this.input, E, Follow._E_in_t2272);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 7);
                                this.Match (this.input, LP, Follow._LP_in_t2274);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 12);
                                this.PushFollow (Follow._t2_in_t2278);
                                el = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 16);
                                this.Match (this.input, R, Follow._R_in_t2280);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 20);
                                this.PushFollow (Follow._t2_in_t2284);
                                er = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 24);
                                this.Match (this.input, RP, Follow._RP_in_t2286);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (36, 27);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.ER (el, er);
                            }
                        }
                            break;
                        case 11 :
                            this.DebugEnterAlt (11);
                            // D:\\Compilers\\Verification\\CTLParser.g:37:4: ( A LP el= t2 R er= t2 RP )
                        {
                            this.DebugLocation (37, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:37:4: ( A LP el= t2 R er= t2 RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:37:5: A LP el= t2 R er= t2 RP
                            {
                                this.DebugLocation (37, 5);
                                this.Match (this.input, A, Follow._A_in_t2295);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 7);
                                this.Match (this.input, LP, Follow._LP_in_t2297);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 12);
                                this.PushFollow (Follow._t2_in_t2301);
                                el = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 16);
                                this.Match (this.input, R, Follow._R_in_t2303);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 20);
                                this.PushFollow (Follow._t2_in_t2307);
                                er = this.t2 ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 24);
                                this.Match (this.input, RP, Follow._RP_in_t2309);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (37, 27);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.AR (el, er);
                            }
                        }
                            break;
                        case 12 :
                            this.DebugEnterAlt (12);
                            // D:\\Compilers\\Verification\\CTLParser.g:38:4: ( LP e= ctlexpr RP )
                        {
                            this.DebugLocation (38, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:38:4: ( LP e= ctlexpr RP )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:38:5: LP e= ctlexpr RP
                            {
                                this.DebugLocation (38, 5);
                                this.Match (this.input, LP, Follow._LP_in_t2318);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (38, 9);
                                this.PushFollow (Follow._ctlexpr_in_t2322);
                                e = this.ctlexpr ();
                                this.PopFollow ();
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (38, 18);
                                this.Match (this.input, RP, Follow._RP_in_t2324);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (38, 21);
                                if (this.state.backtracking == 0)
                                    f = e;
                            }
                        }
                            break;
                        case 13 :
                            this.DebugEnterAlt (13);
                            // D:\\Compilers\\Verification\\CTLParser.g:39:4: ( LB ATOM RB )
                        {
                            this.DebugLocation (39, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:39:4: ( LB ATOM RB )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:39:5: LB ATOM RB
                            {
                                this.DebugLocation (39, 5);
                                this.Match (this.input, LB, Follow._LB_in_t2333);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (39, 8);
                                ATOM2 = (IToken) this.Match (this.input, ATOM, Follow._ATOM_in_t2335);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (39, 13);
                                this.Match (this.input, RB, Follow._RB_in_t2337);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (39, 16);
                                if (this.state.backtracking == 0)
                                    f = new CTLFormula ((ATOM2 != null ? ATOM2.Text : null));
                            }
                        }
                            break;
                        case 14 :
                            this.DebugEnterAlt (14);
                            // D:\\Compilers\\Verification\\CTLParser.g:40:4: ( TRUE )
                        {
                            this.DebugLocation (40, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:40:4: ( TRUE )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:40:5: TRUE
                            {
                                this.DebugLocation (40, 5);
                                this.Match (this.input, TRUE, Follow._TRUE_in_t2346);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (40, 10);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.TRUE;
                            }
                        }
                            break;
                        case 15 :
                            this.DebugEnterAlt (15);
                            // D:\\Compilers\\Verification\\CTLParser.g:41:4: ( FALSE )
                        {
                            this.DebugLocation (41, 4);
                            // D:\\Compilers\\Verification\\CTLParser.g:41:4: ( FALSE )
                            this.DebugEnterAlt (1);
                            // D:\\Compilers\\Verification\\CTLParser.g:41:5: FALSE
                            {
                                this.DebugLocation (41, 5);
                                this.Match (this.input, FALSE, Follow._FALSE_in_t2355);
                                if (this.state.failed)
                                    return f;
                                this.DebugLocation (41, 11);
                                if (this.state.backtracking == 0)
                                    f = CTLFormula.FALSE;
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
                    this.TraceOut ("t2", 4);
                    this.LeaveRule ("t2", 4);
                    this.Leave_t2 ();
                }
                this.DebugLocation (42, 1);
            }
            finally {
                this.DebugExitRule (this.GrammarFileName, "t2");
            }
            return f;
        }

        // $ANTLR end "t2"

        partial void Enter_synpred10_CTLParser_fragment ();
        partial void Leave_synpred10_CTLParser_fragment ();

        // $ANTLR start synpred10_CTLParser
        public void synpred10_CTLParser_fragment () {
            CTLFormula el = default(CTLFormula);
            CTLFormula er = default(CTLFormula);

            this.Enter_synpred10_CTLParser_fragment ();
            this.EnterRule ("synpred10_CTLParser_fragment", 14);
            this.TraceIn ("synpred10_CTLParser_fragment", 14);
            try {
                // D:\\Compilers\\Verification\\CTLParser.g:34:4: ( ( E LP el= t2 U er= t2 RP ) )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:34:4: ( E LP el= t2 U er= t2 RP )
                {
                    this.DebugLocation (34, 4);
                    // D:\\Compilers\\Verification\\CTLParser.g:34:4: ( E LP el= t2 U er= t2 RP )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:34:5: E LP el= t2 U er= t2 RP
                    {
                        this.DebugLocation (34, 5);
                        this.Match (this.input, E, Follow._E_in_synpred10_CTLParser226);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (34, 7);
                        this.Match (this.input, LP, Follow._LP_in_synpred10_CTLParser228);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (34, 12);
                        this.PushFollow (Follow._t2_in_synpred10_CTLParser232);
                        el = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (34, 16);
                        this.Match (this.input, U, Follow._U_in_synpred10_CTLParser234);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (34, 20);
                        this.PushFollow (Follow._t2_in_synpred10_CTLParser238);
                        er = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (34, 24);
                        this.Match (this.input, RP, Follow._RP_in_synpred10_CTLParser240);
                        if (this.state.failed)
                            return;
                    }
                }
            }
            finally {
                this.TraceOut ("synpred10_CTLParser_fragment", 14);
                this.LeaveRule ("synpred10_CTLParser_fragment", 14);
                this.Leave_synpred10_CTLParser_fragment ();
            }
        }

        // $ANTLR end synpred10_CTLParser

        partial void Enter_synpred11_CTLParser_fragment ();
        partial void Leave_synpred11_CTLParser_fragment ();

        // $ANTLR start synpred11_CTLParser
        public void synpred11_CTLParser_fragment () {
            CTLFormula el = default(CTLFormula);
            CTLFormula er = default(CTLFormula);

            this.Enter_synpred11_CTLParser_fragment ();
            this.EnterRule ("synpred11_CTLParser_fragment", 15);
            this.TraceIn ("synpred11_CTLParser_fragment", 15);
            try {
                // D:\\Compilers\\Verification\\CTLParser.g:35:4: ( ( A LP el= t2 U er= t2 RP ) )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:35:4: ( A LP el= t2 U er= t2 RP )
                {
                    this.DebugLocation (35, 4);
                    // D:\\Compilers\\Verification\\CTLParser.g:35:4: ( A LP el= t2 U er= t2 RP )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:35:5: A LP el= t2 U er= t2 RP
                    {
                        this.DebugLocation (35, 5);
                        this.Match (this.input, A, Follow._A_in_synpred11_CTLParser249);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (35, 7);
                        this.Match (this.input, LP, Follow._LP_in_synpred11_CTLParser251);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (35, 12);
                        this.PushFollow (Follow._t2_in_synpred11_CTLParser255);
                        el = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (35, 16);
                        this.Match (this.input, U, Follow._U_in_synpred11_CTLParser257);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (35, 20);
                        this.PushFollow (Follow._t2_in_synpred11_CTLParser261);
                        er = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (35, 24);
                        this.Match (this.input, RP, Follow._RP_in_synpred11_CTLParser263);
                        if (this.state.failed)
                            return;
                    }
                }
            }
            finally {
                this.TraceOut ("synpred11_CTLParser_fragment", 15);
                this.LeaveRule ("synpred11_CTLParser_fragment", 15);
                this.Leave_synpred11_CTLParser_fragment ();
            }
        }

        // $ANTLR end synpred11_CTLParser

        partial void Enter_synpred12_CTLParser_fragment ();
        partial void Leave_synpred12_CTLParser_fragment ();

        // $ANTLR start synpred12_CTLParser
        public void synpred12_CTLParser_fragment () {
            CTLFormula el = default(CTLFormula);
            CTLFormula er = default(CTLFormula);

            this.Enter_synpred12_CTLParser_fragment ();
            this.EnterRule ("synpred12_CTLParser_fragment", 16);
            this.TraceIn ("synpred12_CTLParser_fragment", 16);
            try {
                // D:\\Compilers\\Verification\\CTLParser.g:36:4: ( ( E LP el= t2 R er= t2 RP ) )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:36:4: ( E LP el= t2 R er= t2 RP )
                {
                    this.DebugLocation (36, 4);
                    // D:\\Compilers\\Verification\\CTLParser.g:36:4: ( E LP el= t2 R er= t2 RP )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:36:5: E LP el= t2 R er= t2 RP
                    {
                        this.DebugLocation (36, 5);
                        this.Match (this.input, E, Follow._E_in_synpred12_CTLParser272);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (36, 7);
                        this.Match (this.input, LP, Follow._LP_in_synpred12_CTLParser274);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (36, 12);
                        this.PushFollow (Follow._t2_in_synpred12_CTLParser278);
                        el = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (36, 16);
                        this.Match (this.input, R, Follow._R_in_synpred12_CTLParser280);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (36, 20);
                        this.PushFollow (Follow._t2_in_synpred12_CTLParser284);
                        er = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (36, 24);
                        this.Match (this.input, RP, Follow._RP_in_synpred12_CTLParser286);
                        if (this.state.failed)
                            return;
                    }
                }
            }
            finally {
                this.TraceOut ("synpred12_CTLParser_fragment", 16);
                this.LeaveRule ("synpred12_CTLParser_fragment", 16);
                this.Leave_synpred12_CTLParser_fragment ();
            }
        }

        // $ANTLR end synpred12_CTLParser

        partial void Enter_synpred13_CTLParser_fragment ();
        partial void Leave_synpred13_CTLParser_fragment ();

        // $ANTLR start synpred13_CTLParser
        public void synpred13_CTLParser_fragment () {
            CTLFormula el = default(CTLFormula);
            CTLFormula er = default(CTLFormula);

            this.Enter_synpred13_CTLParser_fragment ();
            this.EnterRule ("synpred13_CTLParser_fragment", 17);
            this.TraceIn ("synpred13_CTLParser_fragment", 17);
            try {
                // D:\\Compilers\\Verification\\CTLParser.g:37:4: ( ( A LP el= t2 R er= t2 RP ) )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:37:4: ( A LP el= t2 R er= t2 RP )
                {
                    this.DebugLocation (37, 4);
                    // D:\\Compilers\\Verification\\CTLParser.g:37:4: ( A LP el= t2 R er= t2 RP )
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:37:5: A LP el= t2 R er= t2 RP
                    {
                        this.DebugLocation (37, 5);
                        this.Match (this.input, A, Follow._A_in_synpred13_CTLParser295);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (37, 7);
                        this.Match (this.input, LP, Follow._LP_in_synpred13_CTLParser297);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (37, 12);
                        this.PushFollow (Follow._t2_in_synpred13_CTLParser301);
                        el = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (37, 16);
                        this.Match (this.input, R, Follow._R_in_synpred13_CTLParser303);
                        if (this.state.failed)
                            return;
                        this.DebugLocation (37, 20);
                        this.PushFollow (Follow._t2_in_synpred13_CTLParser307);
                        er = this.t2 ();
                        this.PopFollow ();
                        if (this.state.failed)
                            return;
                        this.DebugLocation (37, 24);
                        this.Match (this.input, RP, Follow._RP_in_synpred13_CTLParser309);
                        if (this.state.failed)
                            return;
                    }
                }
            }
            finally {
                this.TraceOut ("synpred13_CTLParser_fragment", 17);
                this.LeaveRule ("synpred13_CTLParser_fragment", 17);
                this.Leave_synpred13_CTLParser_fragment ();
            }
        }

        // $ANTLR end synpred13_CTLParser

        #endregion Rules

        #region Synpreds

        private bool EvaluatePredicate (Action fragment) {
            bool success = false;
            this.state.backtracking++;
            try {
                this.DebugBeginBacktrack (this.state.backtracking);
                int start = this.input.Mark ();
                try {
                    fragment ();
                }
                catch (RecognitionException re) {
                    Console.Error.WriteLine ("impossible: " + re);
                }
                success = !this.state.failed;
                this.input.Rewind (start);
            }
            finally {
                this.DebugEndBacktrack (this.state.backtracking, success);
            }
            this.state.backtracking--;
            this.state.failed = false;
            return success;
        }

        #endregion Synpreds

        #region DFA

        private DFA3 dfa3;

        protected override void InitDFAs () {
            base.InitDFAs ();
            this.dfa3 = new DFA3 (this, this.SpecialStateTransition3);
        }

        private class DFA3 : DFA {
            private const string DFA3_eotS =
                "\x12\xFFFF";

            private const string DFA3_eofS =
                "\x12\xFFFF";

            private const string DFA3_minS =
                "\x1\x6\x7\xFFFF\x2\x0\x8\xFFFF";

            private const string DFA3_maxS =
                "\x1\x17\x7\xFFFF\x2\x0\x8\xFFFF";

            private const string DFA3_acceptS =
                "\x1\xFFFF\x1\x1\x1\x2\x1\x3\x1\x4\x1\x5\x1\x6\x1\x7\x2\xFFFF\x1\xC\x1" +
                "\xD\x1\xE\x1\xF\x1\x8\x1\xA\x1\x9\x1\xB";

            private const string DFA3_specialS =
                "\x8\xFFFF\x1\x0\x1\x1\x8\xFFFF}>";

            private static readonly string [] DFA3_transitionS =
                {
                    "\x1\x1\x1\x2\x1\x3\x1\x4\x1\x5\x1\x6\x1\x7\x1\x8\x1\xA\x2\xFFFF\x1" +
                    "\x9\x1\xFFFF\x1\xB\x2\xFFFF\x1\xC\x1\xD",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "\x1\xFFFF",
                    "\x1\xFFFF",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                };

            private static readonly short [] DFA3_eot = UnpackEncodedString (DFA3_eotS);
            private static readonly short [] DFA3_eof = UnpackEncodedString (DFA3_eofS);
            private static readonly char [] DFA3_min = UnpackEncodedStringToUnsignedChars (DFA3_minS);
            private static readonly char [] DFA3_max = UnpackEncodedStringToUnsignedChars (DFA3_maxS);
            private static readonly short [] DFA3_accept = UnpackEncodedString (DFA3_acceptS);
            private static readonly short [] DFA3_special = UnpackEncodedString (DFA3_specialS);
            private static readonly short [] [] DFA3_transition;

            static DFA3 () {
                int numStates = DFA3_transitionS.Length;
                DFA3_transition = new short[numStates] [];
                for (int i = 0; i < numStates; i++)
                    DFA3_transition [i] = UnpackEncodedString (DFA3_transitionS [i]);
            }

            public DFA3 (BaseRecognizer recognizer, SpecialStateTransitionHandler specialStateTransition)
                : base (specialStateTransition) {
                this.recognizer = recognizer;
                this.decisionNumber = 3;
                this.eot = DFA3_eot;
                this.eof = DFA3_eof;
                this.min = DFA3_min;
                this.max = DFA3_max;
                this.accept = DFA3_accept;
                this.special = DFA3_special;
                this.transition = DFA3_transition;
            }

            public override string Description {
                get {
                    return
                        "26:1: t2 returns [CTLFormula f] : ( ( NOT e= t2 ) | ( EX e= t2 ) | ( AX e= t2 ) | ( EG e= t2 ) | ( AG e= t2 ) | ( EF e= t2 ) | ( AF e= t2 ) | ( E LP el= t2 U er= t2 RP ) | ( A LP el= t2 U er= t2 RP ) | ( E LP el= t2 R er= t2 RP ) | ( A LP el= t2 R er= t2 RP ) | ( LP e= ctlexpr RP ) | ( LB ATOM RB ) | ( TRUE ) | ( FALSE ) );";
                }
            }

            public override void Error (NoViableAltException nvae) {
                this.DebugRecognitionException (nvae);
            }
        }

        private int SpecialStateTransition3 (DFA dfa, int s, IIntStream _input) {
            ITokenStream input = (ITokenStream) _input;
            int _s = s;
            switch (s) {
                case 0 :
                    int LA3_8 = input.LA (1);


                    int index3_8 = input.Index;
                    input.Rewind ();
                    s = -1;
                    if ((this.EvaluatePredicate (this.synpred10_CTLParser_fragment)))
                        s = 14;

                    else if ((this.EvaluatePredicate (this.synpred12_CTLParser_fragment)))
                        s = 15;


                    input.Seek (index3_8);
                    if (s >= 0)
                        return s;
                    break;
                case 1 :
                    int LA3_9 = input.LA (1);


                    int index3_9 = input.Index;
                    input.Rewind ();
                    s = -1;
                    if ((this.EvaluatePredicate (this.synpred11_CTLParser_fragment)))
                        s = 16;

                    else if ((this.EvaluatePredicate (this.synpred13_CTLParser_fragment)))
                        s = 17;


                    input.Seek (index3_9);
                    if (s >= 0)
                        return s;
                    break;
            }
            if (this.state.backtracking > 0) {
                this.state.failed = true;
                return -1;
            }
            NoViableAltException nvae = new NoViableAltException (dfa.Description, 3, _s, input);
            dfa.Error (nvae);
            throw nvae;
        }

        #endregion DFA

        #region Follow sets

        private static class Follow {
            public static readonly BitSet _ctlexpr_in_start51 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _t1_in_ctlexpr70 = new BitSet (new [] { 0x12UL });
            public static readonly BitSet _OR_in_ctlexpr77 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t1_in_ctlexpr81 = new BitSet (new [] { 0x12UL });
            public static readonly BitSet _t2_in_t1103 = new BitSet (new [] { 0x22UL });
            public static readonly BitSet _AND_in_t1110 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t1114 = new BitSet (new [] { 0x22UL });
            public static readonly BitSet _NOT_in_t2135 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2139 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _EX_in_t2148 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2152 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _AX_in_t2161 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2165 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _EG_in_t2174 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2178 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _AG_in_t2187 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2191 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _EF_in_t2200 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2204 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _AF_in_t2213 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2217 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _E_in_t2226 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_t2228 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2232 = new BitSet (new [] { 0x8000UL });
            public static readonly BitSet _U_in_t2234 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2238 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_t2240 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _A_in_t2249 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_t2251 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2255 = new BitSet (new [] { 0x8000UL });
            public static readonly BitSet _U_in_t2257 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2261 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_t2263 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _E_in_t2272 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_t2274 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2278 = new BitSet (new [] { 0x40000UL });
            public static readonly BitSet _R_in_t2280 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2284 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_t2286 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _A_in_t2295 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_t2297 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2301 = new BitSet (new [] { 0x40000UL });
            public static readonly BitSet _R_in_t2303 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_t2307 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_t2309 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _LP_in_t2318 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _ctlexpr_in_t2322 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_t2324 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _LB_in_t2333 = new BitSet (new [] { 0x100000UL });
            public static readonly BitSet _ATOM_in_t2335 = new BitSet (new [] { 0x200000UL });
            public static readonly BitSet _RB_in_t2337 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _TRUE_in_t2346 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _FALSE_in_t2355 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _E_in_synpred10_CTLParser226 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_synpred10_CTLParser228 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred10_CTLParser232 = new BitSet (new [] { 0x8000UL });
            public static readonly BitSet _U_in_synpred10_CTLParser234 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred10_CTLParser238 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_synpred10_CTLParser240 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _A_in_synpred11_CTLParser249 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_synpred11_CTLParser251 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred11_CTLParser255 = new BitSet (new [] { 0x8000UL });
            public static readonly BitSet _U_in_synpred11_CTLParser257 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred11_CTLParser261 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_synpred11_CTLParser263 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _E_in_synpred12_CTLParser272 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_synpred12_CTLParser274 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred12_CTLParser278 = new BitSet (new [] { 0x40000UL });
            public static readonly BitSet _R_in_synpred12_CTLParser280 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred12_CTLParser284 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_synpred12_CTLParser286 = new BitSet (new [] { 0x2UL });
            public static readonly BitSet _A_in_synpred13_CTLParser295 = new BitSet (new [] { 0x4000UL });
            public static readonly BitSet _LP_in_synpred13_CTLParser297 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred13_CTLParser301 = new BitSet (new [] { 0x40000UL });
            public static readonly BitSet _R_in_synpred13_CTLParser303 = new BitSet (new [] { 0xCA7FC0UL });
            public static readonly BitSet _t2_in_synpred13_CTLParser307 = new BitSet (new [] { 0x10000UL });
            public static readonly BitSet _RP_in_synpred13_CTLParser309 = new BitSet (new [] { 0x2UL });
        }

        #endregion Follow sets
    }
}

// namespace  NModel.Extension 
