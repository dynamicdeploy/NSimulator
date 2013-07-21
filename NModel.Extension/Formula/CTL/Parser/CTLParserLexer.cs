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
    internal partial class CTLParserLexer : Lexer {
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
        private static readonly bool [] decisionCanBacktrack = new bool[0];

        // delegates
        // delegators

        public CTLParserLexer () {
            this.OnCreated ();
        }

        public CTLParserLexer (ICharStream input)
            : this (input, new RecognizerSharedState ()) {}

        public CTLParserLexer (ICharStream input, RecognizerSharedState state)
            : base (input, state) {
            this.OnCreated ();
        }

        public override string GrammarFileName {
            get { return "D:\\Compilers\\Verification\\CTLParser.g"; }
        }


        partial void OnCreated ();
        partial void EnterRule (string ruleName, int ruleIndex);
        partial void LeaveRule (string ruleName, int ruleIndex);

        partial void Enter_OR ();
        partial void Leave_OR ();

        // $ANTLR start "OR"
        [GrammarRule ("OR")]
        private void mOR () {
            this.Enter_OR ();
            this.EnterRule ("OR", 1);
            this.TraceIn ("OR", 1);
            try {
                int _type = OR;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:44:4: ( '||' | 'or' )
                int alt1 = 2;
                try {
                    this.DebugEnterDecision (1, decisionCanBacktrack [1]);
                    int LA1_0 = this.input.LA (1);

                    if ((LA1_0 == '|'))
                        alt1 = 1;
                    else if ((LA1_0 == 'o'))
                        alt1 = 2;
                    else {
                        NoViableAltException nvae = new NoViableAltException ("", 1, 0, this.input);

                        this.DebugRecognitionException (nvae);
                        throw nvae;
                    }
                }
                finally {
                    this.DebugExitDecision (1);
                }
                switch (alt1) {
                    case 1 :
                        this.DebugEnterAlt (1);
                        // D:\\Compilers\\Verification\\CTLParser.g:44:6: '||'
                    {
                        this.DebugLocation (44, 6);
                        this.Match ("||");
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\CTLParser.g:44:13: 'or'
                    {
                        this.DebugLocation (44, 13);
                        this.Match ("or");
                    }
                        break;
                }
                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("OR", 1);
                this.LeaveRule ("OR", 1);
                this.Leave_OR ();
            }
        }

        // $ANTLR end "OR"

        partial void Enter_AND ();
        partial void Leave_AND ();

        // $ANTLR start "AND"
        [GrammarRule ("AND")]
        private void mAND () {
            this.Enter_AND ();
            this.EnterRule ("AND", 2);
            this.TraceIn ("AND", 2);
            try {
                int _type = AND;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:45:5: ( '&&' | 'and' )
                int alt2 = 2;
                try {
                    this.DebugEnterDecision (2, decisionCanBacktrack [2]);
                    int LA2_0 = this.input.LA (1);

                    if ((LA2_0 == '&'))
                        alt2 = 1;
                    else if ((LA2_0 == 'a'))
                        alt2 = 2;
                    else {
                        NoViableAltException nvae = new NoViableAltException ("", 2, 0, this.input);

                        this.DebugRecognitionException (nvae);
                        throw nvae;
                    }
                }
                finally {
                    this.DebugExitDecision (2);
                }
                switch (alt2) {
                    case 1 :
                        this.DebugEnterAlt (1);
                        // D:\\Compilers\\Verification\\CTLParser.g:45:7: '&&'
                    {
                        this.DebugLocation (45, 7);
                        this.Match ("&&");
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\CTLParser.g:45:14: 'and'
                    {
                        this.DebugLocation (45, 14);
                        this.Match ("and");
                    }
                        break;
                }
                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("AND", 2);
                this.LeaveRule ("AND", 2);
                this.Leave_AND ();
            }
        }

        // $ANTLR end "AND"

        partial void Enter_NOT ();
        partial void Leave_NOT ();

        // $ANTLR start "NOT"
        [GrammarRule ("NOT")]
        private void mNOT () {
            this.Enter_NOT ();
            this.EnterRule ("NOT", 3);
            this.TraceIn ("NOT", 3);
            try {
                int _type = NOT;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:46:5: ( '!' | 'not' )
                int alt3 = 2;
                try {
                    this.DebugEnterDecision (3, decisionCanBacktrack [3]);
                    int LA3_0 = this.input.LA (1);

                    if ((LA3_0 == '!'))
                        alt3 = 1;
                    else if ((LA3_0 == 'n'))
                        alt3 = 2;
                    else {
                        NoViableAltException nvae = new NoViableAltException ("", 3, 0, this.input);

                        this.DebugRecognitionException (nvae);
                        throw nvae;
                    }
                }
                finally {
                    this.DebugExitDecision (3);
                }
                switch (alt3) {
                    case 1 :
                        this.DebugEnterAlt (1);
                        // D:\\Compilers\\Verification\\CTLParser.g:46:7: '!'
                    {
                        this.DebugLocation (46, 7);
                        this.Match ('!');
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\CTLParser.g:46:13: 'not'
                    {
                        this.DebugLocation (46, 13);
                        this.Match ("not");
                    }
                        break;
                }
                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("NOT", 3);
                this.LeaveRule ("NOT", 3);
                this.Leave_NOT ();
            }
        }

        // $ANTLR end "NOT"

        partial void Enter_AX ();
        partial void Leave_AX ();

        // $ANTLR start "AX"
        [GrammarRule ("AX")]
        private void mAX () {
            this.Enter_AX ();
            this.EnterRule ("AX", 4);
            this.TraceIn ("AX", 4);
            try {
                int _type = AX;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:47:4: ( 'AX' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:47:6: 'AX'
                {
                    this.DebugLocation (47, 6);
                    this.Match ("AX");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("AX", 4);
                this.LeaveRule ("AX", 4);
                this.Leave_AX ();
            }
        }

        // $ANTLR end "AX"

        partial void Enter_EX ();
        partial void Leave_EX ();

        // $ANTLR start "EX"
        [GrammarRule ("EX")]
        private void mEX () {
            this.Enter_EX ();
            this.EnterRule ("EX", 5);
            this.TraceIn ("EX", 5);
            try {
                int _type = EX;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:48:4: ( 'EX' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:48:6: 'EX'
                {
                    this.DebugLocation (48, 6);
                    this.Match ("EX");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("EX", 5);
                this.LeaveRule ("EX", 5);
                this.Leave_EX ();
            }
        }

        // $ANTLR end "EX"

        partial void Enter_AG ();
        partial void Leave_AG ();

        // $ANTLR start "AG"
        [GrammarRule ("AG")]
        private void mAG () {
            this.Enter_AG ();
            this.EnterRule ("AG", 6);
            this.TraceIn ("AG", 6);
            try {
                int _type = AG;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:49:4: ( 'AG' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:49:6: 'AG'
                {
                    this.DebugLocation (49, 6);
                    this.Match ("AG");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("AG", 6);
                this.LeaveRule ("AG", 6);
                this.Leave_AG ();
            }
        }

        // $ANTLR end "AG"

        partial void Enter_EG ();
        partial void Leave_EG ();

        // $ANTLR start "EG"
        [GrammarRule ("EG")]
        private void mEG () {
            this.Enter_EG ();
            this.EnterRule ("EG", 7);
            this.TraceIn ("EG", 7);
            try {
                int _type = EG;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:50:4: ( 'EG' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:50:6: 'EG'
                {
                    this.DebugLocation (50, 6);
                    this.Match ("EG");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("EG", 7);
                this.LeaveRule ("EG", 7);
                this.Leave_EG ();
            }
        }

        // $ANTLR end "EG"

        partial void Enter_AF ();
        partial void Leave_AF ();

        // $ANTLR start "AF"
        [GrammarRule ("AF")]
        private void mAF () {
            this.Enter_AF ();
            this.EnterRule ("AF", 8);
            this.TraceIn ("AF", 8);
            try {
                int _type = AF;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:51:4: ( 'AF' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:51:6: 'AF'
                {
                    this.DebugLocation (51, 6);
                    this.Match ("AF");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("AF", 8);
                this.LeaveRule ("AF", 8);
                this.Leave_AF ();
            }
        }

        // $ANTLR end "AF"

        partial void Enter_EF ();
        partial void Leave_EF ();

        // $ANTLR start "EF"
        [GrammarRule ("EF")]
        private void mEF () {
            this.Enter_EF ();
            this.EnterRule ("EF", 9);
            this.TraceIn ("EF", 9);
            try {
                int _type = EF;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:52:4: ( 'EF' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:52:6: 'EF'
                {
                    this.DebugLocation (52, 6);
                    this.Match ("EF");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("EF", 9);
                this.LeaveRule ("EF", 9);
                this.Leave_EF ();
            }
        }

        // $ANTLR end "EF"

        partial void Enter_A ();
        partial void Leave_A ();

        // $ANTLR start "A"
        [GrammarRule ("A")]
        private void mA () {
            this.Enter_A ();
            this.EnterRule ("A", 10);
            this.TraceIn ("A", 10);
            try {
                int _type = A;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:53:3: ( 'A' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:53:5: 'A'
                {
                    this.DebugLocation (53, 5);
                    this.Match ('A');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("A", 10);
                this.LeaveRule ("A", 10);
                this.Leave_A ();
            }
        }

        // $ANTLR end "A"

        partial void Enter_E ();
        partial void Leave_E ();

        // $ANTLR start "E"
        [GrammarRule ("E")]
        private void mE () {
            this.Enter_E ();
            this.EnterRule ("E", 11);
            this.TraceIn ("E", 11);
            try {
                int _type = E;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:54:3: ( 'E' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:54:5: 'E'
                {
                    this.DebugLocation (54, 5);
                    this.Match ('E');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("E", 11);
                this.LeaveRule ("E", 11);
                this.Leave_E ();
            }
        }

        // $ANTLR end "E"

        partial void Enter_U ();
        partial void Leave_U ();

        // $ANTLR start "U"
        [GrammarRule ("U")]
        private void mU () {
            this.Enter_U ();
            this.EnterRule ("U", 12);
            this.TraceIn ("U", 12);
            try {
                int _type = U;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:55:3: ( 'U' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:55:5: 'U'
                {
                    this.DebugLocation (55, 5);
                    this.Match ('U');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("U", 12);
                this.LeaveRule ("U", 12);
                this.Leave_U ();
            }
        }

        // $ANTLR end "U"

        partial void Enter_R ();
        partial void Leave_R ();

        // $ANTLR start "R"
        [GrammarRule ("R")]
        private void mR () {
            this.Enter_R ();
            this.EnterRule ("R", 13);
            this.TraceIn ("R", 13);
            try {
                int _type = R;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:56:3: ( 'R' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:56:5: 'R'
                {
                    this.DebugLocation (56, 5);
                    this.Match ('R');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("R", 13);
                this.LeaveRule ("R", 13);
                this.Leave_R ();
            }
        }

        // $ANTLR end "R"

        partial void Enter_LP ();
        partial void Leave_LP ();

        // $ANTLR start "LP"
        [GrammarRule ("LP")]
        private void mLP () {
            this.Enter_LP ();
            this.EnterRule ("LP", 14);
            this.TraceIn ("LP", 14);
            try {
                int _type = LP;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:57:4: ( '(' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:57:6: '('
                {
                    this.DebugLocation (57, 6);
                    this.Match ('(');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("LP", 14);
                this.LeaveRule ("LP", 14);
                this.Leave_LP ();
            }
        }

        // $ANTLR end "LP"

        partial void Enter_RP ();
        partial void Leave_RP ();

        // $ANTLR start "RP"
        [GrammarRule ("RP")]
        private void mRP () {
            this.Enter_RP ();
            this.EnterRule ("RP", 15);
            this.TraceIn ("RP", 15);
            try {
                int _type = RP;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:58:4: ( ')' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:58:6: ')'
                {
                    this.DebugLocation (58, 6);
                    this.Match (')');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("RP", 15);
                this.LeaveRule ("RP", 15);
                this.Leave_RP ();
            }
        }

        // $ANTLR end "RP"

        partial void Enter_LB ();
        partial void Leave_LB ();

        // $ANTLR start "LB"
        [GrammarRule ("LB")]
        private void mLB () {
            this.Enter_LB ();
            this.EnterRule ("LB", 16);
            this.TraceIn ("LB", 16);
            try {
                int _type = LB;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:59:4: ( '{' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:59:6: '{'
                {
                    this.DebugLocation (59, 6);
                    this.Match ('{');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("LB", 16);
                this.LeaveRule ("LB", 16);
                this.Leave_LB ();
            }
        }

        // $ANTLR end "LB"

        partial void Enter_RB ();
        partial void Leave_RB ();

        // $ANTLR start "RB"
        [GrammarRule ("RB")]
        private void mRB () {
            this.Enter_RB ();
            this.EnterRule ("RB", 17);
            this.TraceIn ("RB", 17);
            try {
                int _type = RB;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:60:4: ( '}' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:60:6: '}'
                {
                    this.DebugLocation (60, 6);
                    this.Match ('}');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("RB", 17);
                this.LeaveRule ("RB", 17);
                this.Leave_RB ();
            }
        }

        // $ANTLR end "RB"

        partial void Enter_TRUE ();
        partial void Leave_TRUE ();

        // $ANTLR start "TRUE"
        [GrammarRule ("TRUE")]
        private void mTRUE () {
            this.Enter_TRUE ();
            this.EnterRule ("TRUE", 18);
            this.TraceIn ("TRUE", 18);
            try {
                int _type = TRUE;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:61:6: ( 'TRUE' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:61:8: 'TRUE'
                {
                    this.DebugLocation (61, 8);
                    this.Match ("TRUE");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("TRUE", 18);
                this.LeaveRule ("TRUE", 18);
                this.Leave_TRUE ();
            }
        }

        // $ANTLR end "TRUE"

        partial void Enter_FALSE ();
        partial void Leave_FALSE ();

        // $ANTLR start "FALSE"
        [GrammarRule ("FALSE")]
        private void mFALSE () {
            this.Enter_FALSE ();
            this.EnterRule ("FALSE", 19);
            this.TraceIn ("FALSE", 19);
            try {
                int _type = FALSE;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:62:7: ( 'FALSE' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:62:9: 'FALSE'
                {
                    this.DebugLocation (62, 9);
                    this.Match ("FALSE");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("FALSE", 19);
                this.LeaveRule ("FALSE", 19);
                this.Leave_FALSE ();
            }
        }

        // $ANTLR end "FALSE"

        partial void Enter_ATOM ();
        partial void Leave_ATOM ();

        // $ANTLR start "ATOM"
        [GrammarRule ("ATOM")]
        private void mATOM () {
            this.Enter_ATOM ();
            this.EnterRule ("ATOM", 20);
            this.TraceIn ("ATOM", 20);
            try {
                int _type = ATOM;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:63:6: ( ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+ )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:63:8: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+
                {
                    this.DebugLocation (63, 8);
                    // D:\\Compilers\\Verification\\CTLParser.g:63:8: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+
                    int cnt4 = 0;
                    try {
                        this.DebugEnterSubRule (4);
                        while (true) {
                            int alt4 = 2;
                            try {
                                this.DebugEnterDecision (4, decisionCanBacktrack [4]);
                                int LA4_0 = this.input.LA (1);

                                if (((LA4_0 >= '0' && LA4_0 <= '9') || LA4_0 == '=' || LA4_0 == '(' ||
                                     LA4_0 == ')' || LA4_0 == '"' || (LA4_0 >= 'A' && LA4_0 <= 'Z') ||
                                     (LA4_0 >= 'a' && LA4_0 <= 'z')))
                                    alt4 = 1;
                            }
                            finally {
                                this.DebugExitDecision (4);
                            }
                            switch (alt4) {
                                case 1 :
                                    this.DebugEnterAlt (1);
                                    // D:\\Compilers\\Verification\\CTLParser.g:
                                {
                                    this.DebugLocation (63, 8);
                                    if ((this.input.LA (1) >= '0' && this.input.LA (1) <= '9') ||
                                        this.input.LA (1) == '=' ||
                                        this.input.LA (1) == '(' || this.input.LA (1) == ')' ||
                                        this.input.LA (1) == '"' ||
                                        (this.input.LA (1) >= 'A' && this.input.LA (1) <= 'Z') ||
                                        (this.input.LA (1) >= 'a' && this.input.LA (1) <= 'z'))
                                        this.input.Consume ();
                                    else {
                                        MismatchedSetException mse = new MismatchedSetException (null, this.input);
                                        this.DebugRecognitionException (mse);
                                        this.Recover (mse);
                                        throw mse;
                                    }
                                }
                                    break;

                                default :
                                    if (cnt4 >= 1)
                                        goto loop4;

                                    EarlyExitException eee4 = new EarlyExitException (4, this.input);
                                    this.DebugRecognitionException (eee4);
                                    throw eee4;
                            }
                            cnt4++;
                        }
                        loop4:
                        ;
                    }
                    finally {
                        this.DebugExitSubRule (4);
                    }
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("ATOM", 20);
                this.LeaveRule ("ATOM", 20);
                this.Leave_ATOM ();
            }
        }

        // $ANTLR end "ATOM"

        partial void Enter_WS ();
        partial void Leave_WS ();

        // $ANTLR start "WS"
        [GrammarRule ("WS")]
        private void mWS () {
            this.Enter_WS ();
            this.EnterRule ("WS", 21);
            this.TraceIn ("WS", 21);
            try {
                int _type = WS;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\CTLParser.g:65:4: ( ( ' ' | '\\t' | '\\r' | '\\n' )+ )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\CTLParser.g:65:6: ( ' ' | '\\t' | '\\r' | '\\n' )+
                {
                    this.DebugLocation (65, 6);
                    // D:\\Compilers\\Verification\\CTLParser.g:65:6: ( ' ' | '\\t' | '\\r' | '\\n' )+
                    int cnt5 = 0;
                    try {
                        this.DebugEnterSubRule (5);
                        while (true) {
                            int alt5 = 2;
                            try {
                                this.DebugEnterDecision (5, decisionCanBacktrack [5]);
                                int LA5_0 = this.input.LA (1);

                                if (((LA5_0 >= '\t' && LA5_0 <= '\n') || LA5_0 == '\r' || LA5_0 == ' '))
                                    alt5 = 1;
                            }
                            finally {
                                this.DebugExitDecision (5);
                            }
                            switch (alt5) {
                                case 1 :
                                    this.DebugEnterAlt (1);
                                    // D:\\Compilers\\Verification\\CTLParser.g:
                                {
                                    this.DebugLocation (65, 6);
                                    if ((this.input.LA (1) >= '\t' && this.input.LA (1) <= '\n') ||
                                        this.input.LA (1) == '\r' || this.input.LA (1) == ' ')
                                        this.input.Consume ();
                                    else {
                                        MismatchedSetException mse = new MismatchedSetException (null, this.input);
                                        this.DebugRecognitionException (mse);
                                        this.Recover (mse);
                                        throw mse;
                                    }
                                }
                                    break;

                                default :
                                    if (cnt5 >= 1)
                                        goto loop5;

                                    EarlyExitException eee5 = new EarlyExitException (5, this.input);
                                    this.DebugRecognitionException (eee5);
                                    throw eee5;
                            }
                            cnt5++;
                        }
                        loop5:
                        ;
                    }
                    finally {
                        this.DebugExitSubRule (5);
                    }

                    this.DebugLocation (65, 34);
                    this.Skip ();
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("WS", 21);
                this.LeaveRule ("WS", 21);
                this.Leave_WS ();
            }
        }

        // $ANTLR end "WS"

        public override void mTokens () {
            // D:\\Compilers\\Verification\\CTLParser.g:1:8: ( OR | AND | NOT | AX | EX | AG | EG | AF | EF | A | E | U | R | LP | RP | LB | RB | TRUE | FALSE | ATOM | WS )
            int alt6 = 21;
            try {
                this.DebugEnterDecision (6, decisionCanBacktrack [6]);
                try {
                    alt6 = this.dfa6.Predict (this.input);
                }
                catch (NoViableAltException nvae) {
                    this.DebugRecognitionException (nvae);
                    throw;
                }
            }
            finally {
                this.DebugExitDecision (6);
            }
            switch (alt6) {
                case 1 :
                    this.DebugEnterAlt (1);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:10: OR
                {
                    this.DebugLocation (1, 10);
                    this.mOR ();
                }
                    break;
                case 2 :
                    this.DebugEnterAlt (2);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:13: AND
                {
                    this.DebugLocation (1, 13);
                    this.mAND ();
                }
                    break;
                case 3 :
                    this.DebugEnterAlt (3);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:17: NOT
                {
                    this.DebugLocation (1, 17);
                    this.mNOT ();
                }
                    break;
                case 4 :
                    this.DebugEnterAlt (4);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:21: AX
                {
                    this.DebugLocation (1, 21);
                    this.mAX ();
                }
                    break;
                case 5 :
                    this.DebugEnterAlt (5);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:24: EX
                {
                    this.DebugLocation (1, 24);
                    this.mEX ();
                }
                    break;
                case 6 :
                    this.DebugEnterAlt (6);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:27: AG
                {
                    this.DebugLocation (1, 27);
                    this.mAG ();
                }
                    break;
                case 7 :
                    this.DebugEnterAlt (7);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:30: EG
                {
                    this.DebugLocation (1, 30);
                    this.mEG ();
                }
                    break;
                case 8 :
                    this.DebugEnterAlt (8);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:33: AF
                {
                    this.DebugLocation (1, 33);
                    this.mAF ();
                }
                    break;
                case 9 :
                    this.DebugEnterAlt (9);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:36: EF
                {
                    this.DebugLocation (1, 36);
                    this.mEF ();
                }
                    break;
                case 10 :
                    this.DebugEnterAlt (10);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:39: A
                {
                    this.DebugLocation (1, 39);
                    this.mA ();
                }
                    break;
                case 11 :
                    this.DebugEnterAlt (11);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:41: E
                {
                    this.DebugLocation (1, 41);
                    this.mE ();
                }
                    break;
                case 12 :
                    this.DebugEnterAlt (12);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:43: U
                {
                    this.DebugLocation (1, 43);
                    this.mU ();
                }
                    break;
                case 13 :
                    this.DebugEnterAlt (13);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:45: R
                {
                    this.DebugLocation (1, 45);
                    this.mR ();
                }
                    break;
                case 14 :
                    this.DebugEnterAlt (14);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:47: LP
                {
                    this.DebugLocation (1, 47);
                    this.mLP ();
                }
                    break;
                case 15 :
                    this.DebugEnterAlt (15);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:50: RP
                {
                    this.DebugLocation (1, 50);
                    this.mRP ();
                }
                    break;
                case 16 :
                    this.DebugEnterAlt (16);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:53: LB
                {
                    this.DebugLocation (1, 53);
                    this.mLB ();
                }
                    break;
                case 17 :
                    this.DebugEnterAlt (17);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:56: RB
                {
                    this.DebugLocation (1, 56);
                    this.mRB ();
                }
                    break;
                case 18 :
                    this.DebugEnterAlt (18);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:59: TRUE
                {
                    this.DebugLocation (1, 59);
                    this.mTRUE ();
                }
                    break;
                case 19 :
                    this.DebugEnterAlt (19);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:64: FALSE
                {
                    this.DebugLocation (1, 64);
                    this.mFALSE ();
                }
                    break;
                case 20 :
                    this.DebugEnterAlt (20);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:70: ATOM
                {
                    this.DebugLocation (1, 70);
                    this.mATOM ();
                }
                    break;
                case 21 :
                    this.DebugEnterAlt (21);
                    // D:\\Compilers\\Verification\\CTLParser.g:1:75: WS
                {
                    this.DebugLocation (1, 75);
                    this.mWS ();
                }
                    break;
            }
        }

        #region DFA

        private DFA6 dfa6;

        protected override void InitDFAs () {
            base.InitDFAs ();
            this.dfa6 = new DFA6 (this);
        }

        private class DFA6 : DFA {
            private const string DFA6_eotS =
                "\x2\xFFFF\x1\x11\x1\xFFFF\x1\x11\x1\xFFFF\x1\x11\x1\x19\x1\x1D\x1\x1E" +
                "\x1\x1F\x4\xFFFF\x2\x11\x2\xFFFF\x1\x1\x2\x11\x1\x24\x1\x25\x1\x26\x1" +
                "\xFFFF\x1\x27\x1\x28\x1\x29\x3\xFFFF\x2\x11\x1\x3\x1\x5\x6\xFFFF\x2\x11" +
                "\x1\x2E\x1\x11\x1\xFFFF\x1\x30\x1\xFFFF";

            private const string DFA6_eofS =
                "\x31\xFFFF";

            private const string DFA6_minS =
                "\x1\x9\x1\xFFFF\x1\x72\x1\xFFFF\x1\x6E\x1\xFFFF\x1\x6F\x4\x30\x4\xFFFF" +
                "\x1\x52\x1\x41\x2\xFFFF\x1\x30\x1\x64\x1\x74\x3\x30\x1\xFFFF\x3\x30\x3" +
                "\xFFFF\x1\x55\x1\x4C\x2\x30\x6\xFFFF\x1\x45\x1\x53\x1\x30\x1\x45\x1\xFFFF" +
                "\x1\x30\x1\xFFFF";

            private const string DFA6_maxS =
                "\x1\x7D\x1\xFFFF\x1\x72\x1\xFFFF\x1\x6E\x1\xFFFF\x1\x6F\x4\x7A\x4\xFFFF" +
                "\x1\x52\x1\x41\x2\xFFFF\x1\x7A\x1\x64\x1\x74\x3\x7A\x1\xFFFF\x3\x7A\x3" +
                "\xFFFF\x1\x55\x1\x4C\x2\x7A\x6\xFFFF\x1\x45\x1\x53\x1\x7A\x1\x45\x1\xFFFF" +
                "\x1\x7A\x1\xFFFF";

            private const string DFA6_acceptS =
                "\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x1\xFFFF\x1\x3\x5\xFFFF\x1\xE\x1\xF\x1" +
                "\x10\x1\x11\x2\xFFFF\x1\x14\x1\x15\x6\xFFFF\x1\xA\x3\xFFFF\x1\xB\x1\xC" +
                "\x1\xD\x4\xFFFF\x1\x4\x1\x6\x1\x8\x1\x5\x1\x7\x1\x9\x4\xFFFF\x1\x12\x1" +
                "\xFFFF\x1\x13";

            private const string DFA6_specialS =
                "\x31\xFFFF}>";

            private static readonly string [] DFA6_transitionS =
                {
                    "\x2\x12\x2\xFFFF\x1\x12\x12\xFFFF\x1\x12\x1\x5\x4\xFFFF\x1\x3\x1\xFFFF" +
                    "\x1\xB\x1\xC\x6\xFFFF\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1\x7\x3\x11\x1" +
                    "\x8\x1\x10\xB\x11\x1\xA\x1\x11\x1\xF\x1\x9\x5\x11\x6\xFFFF\x1\x4\xC" +
                    "\x11\x1\x6\x1\x2\xB\x11\x1\xD\x1\x1\x1\xE",
                    "",
                    "\x1\x13",
                    "",
                    "\x1\x14",
                    "",
                    "\x1\x15",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x5\x11\x1\x18\x1\x17\x10\x11\x1\x16" +
                    "\x2\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x5\x11\x1\x1C\x1\x1B\x10\x11\x1\x1A" +
                    "\x2\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
                    "",
                    "",
                    "",
                    "\x1\x20",
                    "\x1\x21",
                    "",
                    "",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\x1\x22",
                    "\x1\x23",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
                    "",
                    "",
                    "\x1\x2A",
                    "\x1\x2B",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "\x1\x2C",
                    "\x1\x2D",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\x1\x2F",
                    "",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    ""
                };

            private static readonly short [] DFA6_eot = UnpackEncodedString (DFA6_eotS);
            private static readonly short [] DFA6_eof = UnpackEncodedString (DFA6_eofS);
            private static readonly char [] DFA6_min = UnpackEncodedStringToUnsignedChars (DFA6_minS);
            private static readonly char [] DFA6_max = UnpackEncodedStringToUnsignedChars (DFA6_maxS);
            private static readonly short [] DFA6_accept = UnpackEncodedString (DFA6_acceptS);
            private static readonly short [] DFA6_special = UnpackEncodedString (DFA6_specialS);
            private static readonly short [] [] DFA6_transition;

            static DFA6 () {
                int numStates = DFA6_transitionS.Length;
                DFA6_transition = new short[numStates] [];
                for (int i = 0; i < numStates; i++)
                    DFA6_transition [i] = UnpackEncodedString (DFA6_transitionS [i]);
            }

            public DFA6 (BaseRecognizer recognizer) {
                this.recognizer = recognizer;
                this.decisionNumber = 6;
                this.eot = DFA6_eot;
                this.eof = DFA6_eof;
                this.min = DFA6_min;
                this.max = DFA6_max;
                this.accept = DFA6_accept;
                this.special = DFA6_special;
                this.transition = DFA6_transition;
            }

            public override string Description {
                get {
                    return
                        "1:1: Tokens : ( OR | AND | NOT | AX | EX | AG | EG | AF | EF | A | E | U | R | LP | RP | LB | RB | TRUE | FALSE | ATOM | WS );";
                }
            }

            public override void Error (NoViableAltException nvae) {
                this.DebugRecognitionException (nvae);
            }
        }

        #endregion
    }
}

// namespace  NModel.Extension 
