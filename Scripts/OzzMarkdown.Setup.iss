#define MyAppVersion "0.0.0"
[Setup]
AppId={{DBA8D0C5-AAB9-416A-ACD4-D5B45140B0B8}
AppName=OzzMarkdown
AppVersion={#MyAppVersion}
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputBaseFilename=OzzMarkdown_{#MyAppVersion}_Windows_x64_Installer
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
[Tasks]
Name: "associatemd"; Description: "Associate .md files with OzzMarkdown and set its icon"; GroupDescription: "File associations:"; Flags: unchecked
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
Source: "OzzMarkdown\Assets\icon-doc-M-03.ico"; DestDir: "{app}\Assets"; Flags: ignoreversion
[Icons]
Name: "{group}\OzzMarkdown"; Filename: "{app}\OzzMarkdown.WPF.exe"
Name: "{commondesktop}\OzzMarkdown"; Filename: "{app}\OzzMarkdown.WPF.exe"
[Registry]
Root: HKA; Subkey: "Software\Classes\.md"; ValueType: string; ValueName: ""; ValueData: "OzzMarkdown.MarkdownFile"; Flags: uninsdeletevalue; Tasks: associatemd
Root: HKA; Subkey: "Software\Classes\OzzMarkdown.MarkdownFile"; ValueType: string; ValueName: ""; ValueData: "Markdown Document"; Flags: uninsdeletekey; Tasks: associatemd
Root: HKA; Subkey: "Software\Classes\OzzMarkdown.MarkdownFile\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\Assets\icon-doc-M-03.ico"; Tasks: associatemd
Root: HKA; Subkey: "Software\Classes\OzzMarkdown.MarkdownFile\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\OzzMarkdown.WPF.exe"" ""%1"""; Tasks: associatemd
[Run]
Filename: "{app}\OzzMarkdown.WPF.exe"; Description: "Launch OzzMarkdown"; Flags: nowait postinstall skipifsilent

[Code]
const
  SHCNE_ASSOCCHANGED = $8000000;
  SHCNF_IDLIST = $0;

procedure SHChangeNotify(wEventId: Longint; uFlags: Longint; dwItem1: Longint; dwItem2: Longint);
external 'SHChangeNotify@shell32.dll stdcall';

procedure RefreshShellIcons();
begin
  SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, 0, 0);
end;

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

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep = ssPostInstall) and WizardIsTaskSelected('associatemd') then
    RefreshShellIcons();
end;
