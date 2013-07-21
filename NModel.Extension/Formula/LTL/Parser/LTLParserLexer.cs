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
    internal partial class LTLParserLexer : Lexer {
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
        private static readonly bool [] decisionCanBacktrack = new bool[0];

        // delegates
        // delegators

        public LTLParserLexer () {
            this.OnCreated ();
        }

        public LTLParserLexer (ICharStream input)
            : this (input, new RecognizerSharedState ()) {}

        public LTLParserLexer (ICharStream input, RecognizerSharedState state)
            : base (input, state) {
            this.OnCreated ();
        }

        public override string GrammarFileName {
            get { return "D:\\Compilers\\Verification\\LTLParser.g"; }
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
                // D:\\Compilers\\Verification\\LTLParser.g:46:4: ( '||' | 'or' )
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
                        // D:\\Compilers\\Verification\\LTLParser.g:46:6: '||'
                    {
                        this.DebugLocation (46, 6);
                        this.Match ("||");
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\LTLParser.g:46:13: 'or'
                    {
                        this.DebugLocation (46, 13);
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
                // D:\\Compilers\\Verification\\LTLParser.g:47:5: ( '&&' | 'and' )
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
                        // D:\\Compilers\\Verification\\LTLParser.g:47:7: '&&'
                    {
                        this.DebugLocation (47, 7);
                        this.Match ("&&");
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\LTLParser.g:47:14: 'and'
                    {
                        this.DebugLocation (47, 14);
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
                // D:\\Compilers\\Verification\\LTLParser.g:48:5: ( '!' | 'not' )
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
                        // D:\\Compilers\\Verification\\LTLParser.g:48:7: '!'
                    {
                        this.DebugLocation (48, 7);
                        this.Match ('!');
                    }
                        break;
                    case 2 :
                        this.DebugEnterAlt (2);
                        // D:\\Compilers\\Verification\\LTLParser.g:48:13: 'not'
                    {
                        this.DebugLocation (48, 13);
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

        partial void Enter_X ();
        partial void Leave_X ();

        // $ANTLR start "X"
        [GrammarRule ("X")]
        private void mX () {
            this.Enter_X ();
            this.EnterRule ("X", 4);
            this.TraceIn ("X", 4);
            try {
                int _type = X;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:49:3: ( 'X' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:49:5: 'X'
                {
                    this.DebugLocation (49, 5);
                    this.Match ('X');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("X", 4);
                this.LeaveRule ("X", 4);
                this.Leave_X ();
            }
        }

        // $ANTLR end "X"

        partial void Enter_F ();
        partial void Leave_F ();

        // $ANTLR start "F"
        [GrammarRule ("F")]
        private void mF () {
            this.Enter_F ();
            this.EnterRule ("F", 5);
            this.TraceIn ("F", 5);
            try {
                int _type = F;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:50:3: ( 'F' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:50:5: 'F'
                {
                    this.DebugLocation (50, 5);
                    this.Match ('F');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("F", 5);
                this.LeaveRule ("F", 5);
                this.Leave_F ();
            }
        }

        // $ANTLR end "F"

        partial void Enter_G ();
        partial void Leave_G ();

        // $ANTLR start "G"
        [GrammarRule ("G")]
        private void mG () {
            this.Enter_G ();
            this.EnterRule ("G", 6);
            this.TraceIn ("G", 6);
            try {
                int _type = G;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:51:3: ( 'G' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:51:5: 'G'
                {
                    this.DebugLocation (51, 5);
                    this.Match ('G');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("G", 6);
                this.LeaveRule ("G", 6);
                this.Leave_G ();
            }
        }

        // $ANTLR end "G"

        partial void Enter_U ();
        partial void Leave_U ();

        // $ANTLR start "U"
        [GrammarRule ("U")]
        private void mU () {
            this.Enter_U ();
            this.EnterRule ("U", 7);
            this.TraceIn ("U", 7);
            try {
                int _type = U;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:52:3: ( 'U' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:52:5: 'U'
                {
                    this.DebugLocation (52, 5);
                    this.Match ('U');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("U", 7);
                this.LeaveRule ("U", 7);
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
            this.EnterRule ("R", 8);
            this.TraceIn ("R", 8);
            try {
                int _type = R;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:53:3: ( 'R' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:53:5: 'R'
                {
                    this.DebugLocation (53, 5);
                    this.Match ('R');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("R", 8);
                this.LeaveRule ("R", 8);
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
            this.EnterRule ("LP", 9);
            this.TraceIn ("LP", 9);
            try {
                int _type = LP;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:54:4: ( '(' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:54:6: '('
                {
                    this.DebugLocation (54, 6);
                    this.Match ('(');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("LP", 9);
                this.LeaveRule ("LP", 9);
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
            this.EnterRule ("RP", 10);
            this.TraceIn ("RP", 10);
            try {
                int _type = RP;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:55:4: ( ')' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:55:6: ')'
                {
                    this.DebugLocation (55, 6);
                    this.Match (')');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("RP", 10);
                this.LeaveRule ("RP", 10);
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
            this.EnterRule ("LB", 11);
            this.TraceIn ("LB", 11);
            try {
                int _type = LB;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:56:4: ( '{' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:56:6: '{'
                {
                    this.DebugLocation (56, 6);
                    this.Match ('{');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("LB", 11);
                this.LeaveRule ("LB", 11);
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
            this.EnterRule ("RB", 12);
            this.TraceIn ("RB", 12);
            try {
                int _type = RB;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:57:4: ( '}' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:57:6: '}'
                {
                    this.DebugLocation (57, 6);
                    this.Match ('}');
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("RB", 12);
                this.LeaveRule ("RB", 12);
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
            this.EnterRule ("TRUE", 13);
            this.TraceIn ("TRUE", 13);
            try {
                int _type = TRUE;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:58:6: ( 'TRUE' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:58:8: 'TRUE'
                {
                    this.DebugLocation (58, 8);
                    this.Match ("TRUE");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("TRUE", 13);
                this.LeaveRule ("TRUE", 13);
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
            this.EnterRule ("FALSE", 14);
            this.TraceIn ("FALSE", 14);
            try {
                int _type = FALSE;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:59:7: ( 'FALSE' )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:59:9: 'FALSE'
                {
                    this.DebugLocation (59, 9);
                    this.Match ("FALSE");
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("FALSE", 14);
                this.LeaveRule ("FALSE", 14);
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
            this.EnterRule ("ATOM", 15);
            this.TraceIn ("ATOM", 15);
            try {
                int _type = ATOM;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:60:6: ( ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+ )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:60:8: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+
                {
                    this.DebugLocation (60, 8);
                    // D:\\Compilers\\Verification\\LTLParser.g:60:8: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '=' )+
                    int cnt4 = 0;
                    try {
                        this.DebugEnterSubRule (4);
                        while (true) {
                            int alt4 = 2;
                            try {
                                this.DebugEnterDecision (4, decisionCanBacktrack [4]);
                                int LA4_0 = this.input.LA (1);

                                if (((LA4_0 >= '0' && LA4_0 <= '9') || LA4_0 == '=' || (LA4_0 >= 'A' && LA4_0 <= 'Z') ||
                                     (LA4_0 >= 'a' && LA4_0 <= 'z')))
                                    alt4 = 1;
                            }
                            finally {
                                this.DebugExitDecision (4);
                            }
                            switch (alt4) {
                                case 1 :
                                    this.DebugEnterAlt (1);
                                    // D:\\Compilers\\Verification\\LTLParser.g:
                                {
                                    this.DebugLocation (60, 8);
                                    if ((this.input.LA (1) >= '0' && this.input.LA (1) <= '9') ||
                                        this.input.LA (1) == '=' ||
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
                this.TraceOut ("ATOM", 15);
                this.LeaveRule ("ATOM", 15);
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
            this.EnterRule ("WS", 16);
            this.TraceIn ("WS", 16);
            try {
                int _type = WS;
                int _channel = DefaultTokenChannel;
                // D:\\Compilers\\Verification\\LTLParser.g:62:4: ( ( ' ' | '\\t' | '\\r' | '\\n' )+ )
                this.DebugEnterAlt (1);
                // D:\\Compilers\\Verification\\LTLParser.g:62:6: ( ' ' | '\\t' | '\\r' | '\\n' )+
                {
                    this.DebugLocation (62, 6);
                    // D:\\Compilers\\Verification\\LTLParser.g:62:6: ( ' ' | '\\t' | '\\r' | '\\n' )+
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
                                    // D:\\Compilers\\Verification\\LTLParser.g:
                                {
                                    this.DebugLocation (62, 6);
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

                    this.DebugLocation (62, 34);
                    this.Skip ();
                }

                this.state.type = _type;
                this.state.channel = _channel;
            }
            finally {
                this.TraceOut ("WS", 16);
                this.LeaveRule ("WS", 16);
                this.Leave_WS ();
            }
        }

        // $ANTLR end "WS"

        public override void mTokens () {
            // D:\\Compilers\\Verification\\LTLParser.g:1:8: ( OR | AND | NOT | X | F | G | U | R | LP | RP | LB | RB | TRUE | FALSE | ATOM | WS )
            int alt6 = 16;
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
                    // D:\\Compilers\\Verification\\LTLParser.g:1:10: OR
                {
                    this.DebugLocation (1, 10);
                    this.mOR ();
                }
                    break;
                case 2 :
                    this.DebugEnterAlt (2);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:13: AND
                {
                    this.DebugLocation (1, 13);
                    this.mAND ();
                }
                    break;
                case 3 :
                    this.DebugEnterAlt (3);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:17: NOT
                {
                    this.DebugLocation (1, 17);
                    this.mNOT ();
                }
                    break;
                case 4 :
                    this.DebugEnterAlt (4);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:21: X
                {
                    this.DebugLocation (1, 21);
                    this.mX ();
                }
                    break;
                case 5 :
                    this.DebugEnterAlt (5);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:23: F
                {
                    this.DebugLocation (1, 23);
                    this.mF ();
                }
                    break;
                case 6 :
                    this.DebugEnterAlt (6);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:25: G
                {
                    this.DebugLocation (1, 25);
                    this.mG ();
                }
                    break;
                case 7 :
                    this.DebugEnterAlt (7);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:27: U
                {
                    this.DebugLocation (1, 27);
                    this.mU ();
                }
                    break;
                case 8 :
                    this.DebugEnterAlt (8);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:29: R
                {
                    this.DebugLocation (1, 29);
                    this.mR ();
                }
                    break;
                case 9 :
                    this.DebugEnterAlt (9);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:31: LP
                {
                    this.DebugLocation (1, 31);
                    this.mLP ();
                }
                    break;
                case 10 :
                    this.DebugEnterAlt (10);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:34: RP
                {
                    this.DebugLocation (1, 34);
                    this.mRP ();
                }
                    break;
                case 11 :
                    this.DebugEnterAlt (11);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:37: LB
                {
                    this.DebugLocation (1, 37);
                    this.mLB ();
                }
                    break;
                case 12 :
                    this.DebugEnterAlt (12);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:40: RB
                {
                    this.DebugLocation (1, 40);
                    this.mRB ();
                }
                    break;
                case 13 :
                    this.DebugEnterAlt (13);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:43: TRUE
                {
                    this.DebugLocation (1, 43);
                    this.mTRUE ();
                }
                    break;
                case 14 :
                    this.DebugEnterAlt (14);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:48: FALSE
                {
                    this.DebugLocation (1, 48);
                    this.mFALSE ();
                }
                    break;
                case 15 :
                    this.DebugEnterAlt (15);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:54: ATOM
                {
                    this.DebugLocation (1, 54);
                    this.mATOM ();
                }
                    break;
                case 16 :
                    this.DebugEnterAlt (16);
                    // D:\\Compilers\\Verification\\LTLParser.g:1:59: WS
                {
                    this.DebugLocation (1, 59);
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
                "\x2\xFFFF\x1\x11\x1\xFFFF\x1\x11\x1\xFFFF\x1\x11\x1\x16\x1\x18\x1\x19" +
                "\x1\x1A\x1\x1B\x4\xFFFF\x1\x11\x2\xFFFF\x1\x1\x2\x11\x1\xFFFF\x1\x11" +
                "\x4\xFFFF\x1\x11\x1\x3\x1\x5\x3\x11\x1\x24\x1\x25\x2\xFFFF";

            private const string DFA6_eofS =
                "\x26\xFFFF";

            private const string DFA6_minS =
                "\x1\x9\x1\xFFFF\x1\x72\x1\xFFFF\x1\x6E\x1\xFFFF\x1\x6F\x5\x30\x4\xFFFF" +
                "\x1\x52\x2\xFFFF\x1\x30\x1\x64\x1\x74\x1\xFFFF\x1\x4C\x4\xFFFF\x1\x55" +
                "\x2\x30\x1\x53\x2\x45\x2\x30\x2\xFFFF";

            private const string DFA6_maxS =
                "\x1\x7D\x1\xFFFF\x1\x72\x1\xFFFF\x1\x6E\x1\xFFFF\x1\x6F\x5\x7A\x4\xFFFF" +
                "\x1\x52\x2\xFFFF\x1\x7A\x1\x64\x1\x74\x1\xFFFF\x1\x4C\x4\xFFFF\x1\x55" +
                "\x2\x7A\x1\x53\x2\x45\x2\x7A\x2\xFFFF";

            private const string DFA6_acceptS =
                "\x1\xFFFF\x1\x1\x1\xFFFF\x1\x2\x1\xFFFF\x1\x3\x6\xFFFF\x1\x9\x1\xA\x1" +
                "\xB\x1\xC\x1\xFFFF\x1\xF\x1\x10\x3\xFFFF\x1\x4\x1\xFFFF\x1\x5\x1\x6\x1" +
                "\x7\x1\x8\x8\xFFFF\x1\xD\x1\xE";

            private const string DFA6_specialS =
                "\x26\xFFFF}>";

            private static readonly string [] DFA6_transitionS =
                {
                    "\x2\x12\x2\xFFFF\x1\x12\x12\xFFFF\x1\x12\x1\x5\x4\xFFFF\x1\x3\x1\xFFFF" +
                    "\x1\xC\x1\xD\x6\xFFFF\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x5\x11\x1\x8\x1" +
                    "\x9\xA\x11\x1\xB\x1\x11\x1\x10\x1\xA\x2\x11\x1\x7\x2\x11\x6\xFFFF\x1" +
                    "\x4\xC\x11\x1\x6\x1\x2\xB\x11\x1\xE\x1\x1\x1\xF",
                    "",
                    "\x1\x13",
                    "",
                    "\x1\x14",
                    "",
                    "\x1\x15",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1\x17\x19\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
                    "",
                    "",
                    "",
                    "\x1\x1C",
                    "",
                    "",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\x1\x1D",
                    "\x1\x1E",
                    "",
                    "\x1\x1F",
                    "",
                    "",
                    "",
                    "",
                    "\x1\x20",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\x1\x21",
                    "\x1\x22",
                    "\x1\x23",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "\xA\x11\x3\xFFFF\x1\x11\x3\xFFFF\x1A\x11\x6\xFFFF\x1A\x11",
                    "",
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
                        "1:1: Tokens : ( OR | AND | NOT | X | F | G | U | R | LP | RP | LB | RB | TRUE | FALSE | ATOM | WS );";
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
