grammar CTLParser;

options {
	language=CSharp3;
	backtrack=true;
}

@lexer::namespace { NModel.Extension }
@parser::namespace { NModel.Extension }

public
start	returns [CTLFormula f]
	:	ctlexpr { f = $ctlexpr.f; }
	;

ctlexpr	returns [CTLFormula f]
	:	e=t1 { $f = $e.f; }
		(OR e=t1 { $f |= $e.f; })*
	;
	
t1	returns [CTLFormula f]
	:	e=t2 { $f = $e.f; }
		(AND e=t2 { $f &= $e.f; })*
	;
	
t2	returns [CTLFormula f]
	:	(NOT e=t2 { $f = ! $e.f; })
	|	(EX e=t2 { $f = CTLFormula.EX ($e.f); })
	|	(AX e=t2 { $f = CTLFormula.AX ($e.f); })
	|	(EG e=t2 { $f = CTLFormula.EG ($e.f); })
	|	(AG e=t2 { $f = CTLFormula.AG ($e.f); })
	|	(EF e=t2 { $f = CTLFormula.EF ($e.f); })
	|	(AF e=t2 { $f = CTLFormula.AF ($e.f); })
	|	(E LP el=t2 U er=t2 RP { $f = CTLFormula.EU ($el.f, $er.f); })
	|	(A LP el=t2 U er=t2 RP { $f = CTLFormula.AU ($el.f, $er.f); })
	|	(E LP el=t2 R er=t2 RP { $f = CTLFormula.ER ($el.f, $er.f); })
	|	(A LP el=t2 R er=t2 RP { $f = CTLFormula.AR ($el.f, $er.f); })
	|	(LP e=ctlexpr RP { $f = $e.f; })
	|	(LB ATOM RB { $f = new CTLFormula ($ATOM.text); })
	|	(TRUE { $f = CTLFormula.TRUE; })
	|	(FALSE { $f = CTLFormula.FALSE; })
	;
	
OR	:	'||' | 'or';
AND	:	'&&' | 'and';
NOT	:	'!' | 'not';
AX	:	'AX';
EX	:	'EX';
AG	:	'AG';
EG	:	'EG';
AF	:	'AF';
EF	:	'EF';
A	:	'A';
E	:	'E';
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
