grammar LTLParser;

options {
	language=CSharp3;
}

@lexer::namespace { NModel.Extension }
@parser::namespace { NModel.Extension }

public
start	returns [LTLFormula f]
	:	ltlexpr { f = $ltlexpr.f; }
	;

ltlexpr	returns [LTLFormula f]
	:	e=t1 { $f = $e.f; }
		(OR e=t1 { $f |= $e.f; })*
	;
	
t1	returns [LTLFormula f]
	:	e=t2 { $f = $e.f; }
		(AND e=t2 { $f &= $e.f; })*
	;
	
t2	returns [LTLFormula f]
	: 	(e=t3 { $f = $e.f; })
		(U e=t3 { $f = LTLFormula.U ($f, $e.f); })*
	;
	
t3	returns [LTLFormula f]
	:	e=t4 { $f = $e.f; }
		(R e=t4 { $f = LTLFormula.R ($f, $e.f); })*
	;
	
t4	returns [LTLFormula f]
	:	(NOT e=t4 { $f = ! $e.f; })
	|	(X e=t4 { $f = LTLFormula.X ($e.f); })
	|	(F e=t4 { $f = LTLFormula.F ($e.f); })
	|	(G e=t4 { $f = LTLFormula.G ($e.f); })
	|	(LP e=ltlexpr RP { $f = $e.f; })
	|	(LB ATOM RB { $f = new LTLFormula ($ATOM.text); })
	|	(TRUE { $f = LTLFormula.TRUE; })
	|	(FALSE { $f = LTLFormula.FALSE; })
	;
	
OR	:	'||' | 'or';
AND	:	'&&' | 'and';
NOT	:	'!' | 'not';
X	:	'X';
F	:	'F';
G	:	'G';
U	:	'U';
R	:	'R';
LP	:	'(';
RP	:	')';
LB	:	'{';
RB	:	'}';
TRUE	:	'TRUE';
FALSE	:	'FALSE';
ATOM	:	('a'..'z' | 'A'..'Z' | '0'..'9' | '=')+;

WS	:	(' ' | '\t' | '\r' | '\n')+ { Skip (); };
