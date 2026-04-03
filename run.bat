@echo off
setlocal

if not defined JAVA_HOME (
    java -version >nul 2>&1
    if errorlevel 1 (
        echo Java was not found. Please install JDK 17 or newer and reopen the terminal.
        exit /b 1
    )
) else (
    "%JAVA_HOME%\bin\java.exe" -version >nul 2>&1
    if errorlevel 1 (
        echo JAVA_HOME is set but invalid. Please point it to a JDK 17 or newer installation.
        exit /b 1
    )
)

call gradlew.bat bootRun
