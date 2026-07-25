#define MyAppVersion "0.0.0"
[Setup]
AppId={{DBA8D0C5-AAB9-416A-ACD4-D5B45140B0B8}
AppName=OzzMarkdown
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputBaseFilename=OzzMarkdownSetup_{#MyAppVersion}
DefaultDirName={autopf}\OzzMarkdown
DefaultGroupName=OzzMarkdown
AppVerName=OzzMarkdown v{#MyAppVersion}
VersionInfoProductVersion={#MyAppVersion}
VersionInfoDescription=OzzMarkdown Installer v{#MyAppVersion}
UninstallDisplayIcon={app}\OzzMarkdown.WPF.exe
WizardStyle=modern
OutputDir=.
Compression=lzma
SolidCompression=yes
[Files]
Source: "OzzMarkdown\OzzMarkdown.WPF.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzMarkdown.WPF.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzMarkdown.WPF.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzMarkdown.WPF.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzMarkdown.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzMarkdown.i18n.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\OzzWpf.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\Markdig.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\MarkdigToc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\Microsoft.Web.WebView2.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\Microsoft.Web.WebView2.WinForms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\Microsoft.Web.WebView2.Wpf.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "OzzMarkdown\runtimes\win-x64\native\WebView2Loader.dll"; DestDir: "{app}\runtimes\win-x64\native"; Flags: ignoreversion
Source: "OzzMarkdown\tr\OzzMarkdown.i18n.resources.dll"; DestDir: "{app}\tr"; Flags: ignoreversion
[Icons]
Name: "{group}\OzzMarkdown"; Filename: "{app}\OzzMarkdown.WPF.exe"
Name: "{commondesktop}\OzzMarkdown"; Filename: "{app}\OzzMarkdown.WPF.exe"
[Run]
Filename: "{app}\OzzMarkdown.WPF.exe"; Description: "Launch OzzMarkdown"; Flags: nowait postinstall skipifsilent

[Code]
function IsDotNetDesktopRuntimeInstalled(): Boolean;
var
  DotNetSharedPath: String;
  FindRec: TFindRec;
begin
  Result := False;
  DotNetSharedPath := ExpandConstant('{pf}\dotnet\shared\Microsoft.WindowsDesktop.App');
  if DirExists(DotNetSharedPath) then
  begin
    if FindFirst(DotNetSharedPath + '\10.*', FindRec) then
    begin
      try
        Result := True;
      finally
        FindClose(FindRec);
      end;
    end;
  end;
end;

function InitializeSetup(): Boolean;
var
  ErrorCode: Integer;
begin
  Result := True;
  if not IsDotNetDesktopRuntimeInstalled() then
  begin
    if MsgBox('OzzMarkdown requires the .NET 10 Desktop Runtime, which was not detected on this computer.' + #13#10 + #13#10 +
      'Click OK to open the download page in your browser. After installing the runtime, re-run this installer.' + #13#10 + #13#10 +
      'Click Cancel to continue installing anyway (the application will not run without the runtime).',
      mbConfirmation, MB_OKCANCEL) = IDOK then
    begin
      ShellExec('open', 'https://dotnet.microsoft.com/download/dotnet/10.0/runtime', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode);
      Result := False;
    end;
  end;
end;
