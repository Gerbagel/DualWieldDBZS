# MUI2 include
!include "MUI2.nsh"

# MUI2 defines
!define MUI_ICON "DBZSDualWieldAva\Assets\favicon.ico"

# Installer name
OutFile "DualWieldDBZS_WinInstaller.exe"
Name "Dual Wield DBZS"

# Default INSTDIR
InstallDir "$PROGRAMFILES32\DualWieldDBZS\"

# Make shortcut function
Function MakeShortcut
    CreateShortCut "$DESKTOP\Dual Wield DBZS.lnk" "$INSTDIR\DBZSDualWieldAva.Desktop.exe"
FunctionEnd

!insertmacro MUI_PAGE_LICENSE "LICENSE.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_RUN "$INSTDIR\DBZSDualWieldAva.Desktop.exe"
!define MUI_FINISHPAGE_SHOWREADME ""
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
!define MUI_FINISHPAGE_SHOWREADME_TEXT "Create Desktop Shortcut"
!define MUI_FINISHPAGE_SHOWREADME_FUNCTION "MakeShortcut"
!insertmacro MUI_PAGE_FINISH

# Language
!insertmacro MUI_LANGUAGE "English"

Section

SetOutPath $INSTDIR
File /nonfatal /r "DBZSDualWieldAva.Desktop\bin\Release\net7.0-windows\*.*"

WriteUninstaller "$INSTDIR\uninstall.exe"

# Give app full acces to its own folder
AccessControl::GrantOnFile $INSTDIR "(BU)" "FullAccess"

SectionEnd

Section "uninstall"

# Remove shortcut
Delete "$DESKTOP\Dual Wield DBZS.lnk"

# Remove uninstaller
Delete $INSTDIR\uninstall.exe

RMDir /r $INSTDIR

SectionEnd