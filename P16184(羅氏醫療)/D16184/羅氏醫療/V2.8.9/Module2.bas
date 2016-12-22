Attribute VB_Name = "Module2"
Public Declare Function OpenProcess Lib "KERNEL32" _
      (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, _
       ByVal dwProcessId As Long) As Long
Public Declare Function GetExitCodeProcess Lib "KERNEL32" _
      (ByVal hProcess As Long, lpExitCode As Long) As Long
Public Declare Function CloseHandle Lib "KERNEL32" _
      (ByVal hObject As Long) As Long
Const STILL_ALIVE = &H103

Public FsObj As New FileSystemObject
Public PARA As Variant

'Transmit
Public FileNum As Integer
Public ComPort As String
Public SendFile As String
Public FN_PATH As String
Public FN_LAYOUT As String
Public FN_Descrip As String
Public WSTR() As String
'Public SysPath As String
Public BackUp_Path_Recv As String
Public BackUp_Path_Send As String
Public BackUP_File As String
'Transmit
'Public ComPort As String
Public Recv_Success As Boolean
Public Send_Success As Boolean
Public PD3File As String
Public sTU3_ORG_Path As String
Public sTU3_ORG_File As String
Public sTU3_HT_File As String
Public sTU3_HT_Path As String
Public sTU3_HT_Layout As String

'''民國年
Public Function Todayf1(day1 As Date, sp As String)
a = Year(day1) - 1911
b = Month(day1)
c = Day(day1)

a = Format(a, "00")
b = Format(b, "00")
c = Format(c, "00")
Todayf1 = a & sp & b & sp & c
End Function
Public Sub DELAY(n As Integer)
  Dim a As Double
  a = Timer
  While Timer - a < n
    DoEvents
  Wend
End Sub
Public Sub iWait()
   On Error Resume Next
   Forms(1).Timer1.Interval = 1000
   Forms(1).FrmMsg.Visible = True
   DoEvents
   Forms(1).MousePointer = 11
   Forms(1).FrmCmd.Enabled = False
   Forms(1).CmdNew.Enabled = False
   Forms(1).CmdUpDate.Enabled = False
   Forms(1).CmdDel.Enabled = False
   Forms(1).CmdQuery.Enabled = False
   Forms(1).CmdEXIT.Enabled = False
   Forms(1).CmdYes.Enabled = False
   Forms(1).CmdNo.Enabled = False
'   Form主選單.基本資料 = False
End Sub

Public Sub eWait()
   'DoEvents
   On Error Resume Next
   Forms(1).Timer1.Interval = 0
   Forms(1).MousePointer = 4
   Forms(1).FrmMsg.Visible = False
   Forms(1).FrmCmd.Enabled = True
   Forms(1).CmdNew.Enabled = True
   Forms(1).CmdEXIT.Enabled = True
   Forms(1).CmdYes.Enabled = True
   Forms(1).CmdNo.Enabled = True
   'If rs.RecordCount = 0 Then
   If TxtMATE_ID <> "" Then
      Forms(1).CmdUpDate.Enabled = False
      Forms(1).CmdQuery.Enabled = False
      Forms(1).CmdDel.Enabled = False
     Else
      Forms(1).CmdUpDate.Enabled = True
      Forms(1).CmdDel.Enabled = True
      Forms(1).CmdQuery.Enabled = True
   End If
 '  Form主選單.基本資料.Enabled = True
End Sub

