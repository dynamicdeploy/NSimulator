@echo off
setlocal enabledelayedexpansion
if errorlevel 1 (
	@echo Delayed expansion is not available
	goto END
)
:BEGIN

        md Documentation\dll
        md Documentation\dll\dependency
        md Documentation\comments
        md Documentation\vs2005
        md Documentation\vs2005\output
        md Documentation\vs2005\chm
        md Documentation\vs2005\output\html
        md Documentation\vs2005\output\icons
        md Documentation\vs2005\output\media
        md Documentation\vs2005\output\scripts
        md Documentation\vs2005\output\styles

        copy NSimulator.Kernel\bin\Debug\NSimulator.Kernel.dll Documentation\dll
        copy NSimulator.Kernel\bin\Debug\NSimulator.Kernel.xml Documentation\comments
        copy NSimulator.Library\bin\Debug\NSimulator.Library.dll Documentation\dll
        copy NSimulator.Library\bin\Debug\NSimulator.Library.xml Documentation\comments

rem        copy NSimulator.Kernel\bin\Debug\CodeContracts\NSimulator.Kernel.Contracts.dll Documentation\dll

        set reflectionfile=Documentation\vs2005\reflection.xml
        set CommentsDir=Documentation\comments
        set OutputDir=Documentation\vs2005\output\html

        "C:\Program Files (x86)\Sandcastle\ProductionTools\MRefBuilder.exe" Documentation\dll\*.dll Documentation\dll\*.exe /out:Documentation\vs2005\reflection.org /dep:"Documentation\dll\dependency\*.dll","Documentation\dll\dependency\*.exe"
        "C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" /xsl:"C:\Program Files (x86)\Sandcastle\ProductionTransforms\ApplyVSDocModel.xsl"  /xsl:"C:\Program Files (x86)\Sandcastle\ProductionTransforms\AddFriendlyFilenames.xsl"  Documentation\vs2005\reflection.org  /out:Documentation\vs2005\reflection.xml  /arg:IncludeAllMembersTopic=true  /arg:IncludeInheritedOverloadTopics=true
        "C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" /xsl:"C:\Program Files (x86)\Sandcastle\ProductionTransforms\ReflectionToManifest.xsl" Documentation\vs2005\reflection.xml /out:Documentation\manifest.xml
        "C:\Program Files (x86)\Sandcastle\ProductionTools\BuildAssembler.exe" /config:"C:\Program Files (x86)\Sandcastle\examples\generic\vs2005.config" Documentation\manifest.xml
        "C:\Program Files (x86)\Sandcastle\ProductionTools\XslTransform.exe" /xsl:"C:\Program Files (x86)\Sandcastle\ProductionTransforms\createvstoc.xsl" Documentation\vs2005\reflection.xml /out:Documentation\vs2005\toc.xml

        xcopy /s /e /y /r /q Documentation\vs2005\output\* Documentation\vs2005\chm
        xcopy /s /e /y /r /q "C:\Program Files (x86)\Sandcastle\Presentation\vs2005\icons\*" Documentation\vs2005\chm\icons
        xcopy /s /e /y /r /q "C:\Program Files (x86)\Sandcastle\Presentation\vs2005\scripts\*" Documentation\vs2005\chm\scripts
        xcopy /s /e /y /r /q "C:\Program Files (x86)\Sandcastle\Presentation\vs2005\styles\*" Documentation\vs2005\chm\styles

        "C:\Program Files (x86)\Sandcastle\ProductionTools\ChmBuilder.exe" /project:NetworkSimulator /html:Documentation\vs2005\output\html\ /lcid:1049 /toc:Documentation\vs2005\toc.xml /out:Documentation\vs2005\chm
        "C:\Program Files (x86)\Sandcastle\ProductionTools\DBCSFix.exe" /d:Documentation\vs2005\chm /l:1049
        "C:\Program Files (x86)\HTML Help Workshop\hhc.exe" Documentation\vs2005\chm\NetworkSimulator.hhp

        copy Documentation\vs2005\chm\NetworkSimulator.chm Documentation

        rd /s /q Documentation\dll
        rd /s /q Documentation\comments
        rd /s /q Documentation\vs2005
        del /f /q Documentation\manifest.xml

:END
endlocal
