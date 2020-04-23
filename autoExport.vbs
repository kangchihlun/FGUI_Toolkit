Option Explicit

Dim fguiFolderName, layaFolderName, fguiPkgName

fguiFolderName = "gd_mt\"
fguiPkgName = "MT"
layaFolderName = "multi_texas\"


Dim objFSO, objFolder, objSubFolders, objSubFolder, fsn
Dim WshShell, oExec
Set WshShell = CreateObject("WScript.Shell")

' 正則用
Dim oRE, bMatch
Set oRE = New RegExp
oRE.Pattern = "assets_"

' IO
Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFolder = objFSO.GetFolder(".\" & fguiFolderName)
Set objSubFolders = objFolder.SubFolders

Dim cmdFairyPath, pwd
cmdFairyPath = "C:\FairyGUI-Editor\FairyGUI-Editor"
pwd = Replace(WScript.ScriptFullName, WScript.ScriptName, "")

Dim execString
' 分支先發布
For each objSubFolder In objSubFolders
    bMatch = oRE.Test(objSubFolder.name)
    If bMatch Then
        fsn = Replace(objSubFolder.name, "assets_", "")
        WScript.Stdout.WriteLine "fgui branch: " & fsn
        execString = cmdFairyPath & " -p " & pwd & fguiFolderName & fguiPkgName & ".fairy -t " & fsn ' & " -o " & pwd & layaFolderName
        ' WScript.Stdout.WriteLine execString
        Set oExec = WshShell.Exec(execString)
        Do While oExec.Status = 0
            WScript.Sleep 100
        Loop
    End If
Next

' 主幹發布
execString = cmdFairyPath & " -p " & pwd & fguiFolderName & fguiPkgName & ".fairy"
Set oExec = WshShell.Exec(execString)
Do While oExec.Status = 0
    WScript.Sleep 100
Loop

' 結束
WScript.Stdout.WriteLine "OK!"
