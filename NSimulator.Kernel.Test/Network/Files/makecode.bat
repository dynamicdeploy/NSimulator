@echo off
setlocal ENABLEDELAYEDEXPANSION
for /f %%i in ('dir /b valid\*.t') do (
  set NAME=%%~ni

  echo private void Check_!NAME! (^) {
  echo   // todo : insert code here
  echo }

  echo [TestMethod]
  echo public void Check!NAME!1 (^) {
  echo   this.PrepareFile ("!NAME!"^);
  echo   this.network = new Network ("temp"^);
  echo   this.Check_!NAME! (^);
  echo }

  echo [TestMethod]
  echo public void Check!NAME!2 (^) {
  echo   this.PrepareFile ("!NAME!"^);
  echo   this.network.Load ("temp"^);
  echo   this.Check_!NAME! (^);
  echo }

  echo.
)
for /f %%i in ('dir /b nonvalid\*.t') do (
  set NAME=%%~ni

  echo [TestMethod]
  echo [ExpectedException (typeof (SchemaValidationException^)^)]
  echo public void Check!NAME!1 (^) {
  echo   this.PrepareFile ("!NAME!"^);
  echo   this.network = new Network ("temp"^);
  echo }

  echo [TestMethod]
  echo [ExpectedException (typeof (SchemaValidationException^)^)]
  echo public void Check!NAME!2 (^) {
  echo   this.PrepareFile ("!NAME!"^);
  echo   this.network.Load ("temp"^);
  echo }

  echo.
)
endlocal
