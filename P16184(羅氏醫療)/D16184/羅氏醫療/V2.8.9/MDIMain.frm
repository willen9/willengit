VERSION 5.00
Begin VB.MDIForm MDIMain 
   BackColor       =   &H80000004&
   Caption         =   "帝商庫存管理系統 V2.8.9"
   ClientHeight    =   9000
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   11895
   Icon            =   "MDIMain.frx":0000
   Picture         =   "MDIMain.frx":08CA
   WindowState     =   2  '最大化
   Begin VB.Timer Timer1 
      Interval        =   50
      Left            =   0
      Top             =   0
   End
   Begin VB.Menu 基本資料 
      Caption         =   "基本資料"
      Begin VB.Menu Materiel 
         Caption         =   "物料資料登錄"
      End
   End
   Begin VB.Menu 盤點作業 
      Caption         =   "盤點作業"
      Begin VB.Menu Materiel_Inventory 
         Caption         =   "物料盤點作業"
      End
   End
   Begin VB.Menu 查詢作業 
      Caption         =   "查詢作業"
      Begin VB.Menu Materiel_Query 
         Caption         =   "庫存查詢"
      End
      Begin VB.Menu Transation_Query 
         Caption         =   "異動紀錄查詢"
      End
   End
   Begin VB.Menu 報表作業 
      Caption         =   "報表作業"
      Begin VB.Menu Materiel_Label 
         Caption         =   "物料標籤列印"
      End
      Begin VB.Menu SafeStock_Report 
         Caption         =   "安全用量表"
      End
      Begin VB.Menu Materiel_Report 
         Caption         =   "物料進出表"
      End
   End
   Begin VB.Menu 異常作業 
      Caption         =   "異常作業"
      Begin VB.Menu Abnormal_S 
         Caption         =   "異常資料查詢"
      End
      Begin VB.Menu Abnormal_R 
         Caption         =   "轉檔異常表"
      End
   End
   Begin VB.Menu 系統管理 
      Caption         =   "系統管理"
      Visible         =   0   'False
      Begin VB.Menu Logout 
         Caption         =   "登出"
      End
      Begin VB.Menu USERID 
         Caption         =   "使用者帳號設定"
         Enabled         =   0   'False
         Visible         =   0   'False
      End
      Begin VB.Menu USERPOWER 
         Caption         =   "使用者權限設定"
         Enabled         =   0   'False
         Visible         =   0   'False
      End
      Begin VB.Menu HistoryData_Delete 
         Caption         =   "歷史資料刪除"
         Enabled         =   0   'False
         Visible         =   0   'False
      End
   End
   Begin VB.Menu 手動匯入作業 
      Caption         =   "手動匯入作業"
      Visible         =   0   'False
      Begin VB.Menu Received 
         Caption         =   "進貨檔"
         Enabled         =   0   'False
      End
      Begin VB.Menu Shipping 
         Caption         =   "出貨檔"
         Enabled         =   0   'False
      End
      Begin VB.Menu Inventory 
         Caption         =   "盤點檔"
         Enabled         =   0   'False
      End
   End
   Begin VB.Menu 盤點機作業 
      Caption         =   "盤點機作業"
      Begin VB.Menu RecvFile 
         Caption         =   "接收檔案"
      End
      Begin VB.Menu SendFile 
         Caption         =   "傳送主檔"
         Visible         =   0   'False
      End
      Begin VB.Menu UPGRADE 
         Caption         =   "程式更新"
      End
   End
   Begin VB.Menu EXIT 
      Caption         =   "離開"
   End
   Begin VB.Menu myForm 
      Caption         =   "myForm"
      Enabled         =   0   'False
      Visible         =   0   'False
   End
End
Attribute VB_Name = "MDIMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim rs As New ADODB.Recordset
Private Declare Function OpenProcess Lib "KERNEL32" _
      (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, _
       ByVal dwProcessId As Long) As Long
Private Declare Function GetExitCodeProcess Lib "KERNEL32" _
      (ByVal hProcess As Long, lpExitCode As Long) As Long
Private Declare Function CloseHandle Lib "KERNEL32" _
      (ByVal hObject As Long) As Long
Dim FS As New FileSystemObject
Const STILL_ALIVE = &H103

Dim FirstSend, FirstRecv As Boolean
Dim FileLOG() As String

Private Sub EXIT_Click()
  Unload Me
End Sub

Private Sub form111_Click()
 Form1.Show
End Sub

Private Sub Form222_Click()
   Form2.Show
End Sub

Private Sub fff_Click()
      Form1.Show
End Sub

Private Sub Form3_Click()
   Form3.Caption
End Sub

Private Sub MDIForm_Activate()
  If FsObj.FileExists(SysPath & "REGAL.INI") = False Then
     Open SysPath & "REGAL.INI" For Output As #1
     Print #1, "1"
     Close #1
  End If
  Open SysPath & "REGAL.INI" For Input As #1
  Line Input #1, READSTR
  Close
  ComPort = Val(Left(READSTR, 2))
 ' If First1 Then FormLogin.Show vbModals
   
 '  MDIMain.Caption = "帝商庫存管理系統" & Space(80) & "使用者[" & USER_ID & "]"
   'MDIMain.Caption = "帝商庫存管理系統 V2.7.5"
   MDIMain.Logout.Caption = "登出[" & USER_ID & "]..."
   First1 = False
   
End Sub

Private Sub MDIForm_Load()
  First1 = True
  OpenConn
  On Error GoTo LOADERR
  SysPath = App.Path + "\"
  sTU3_HT_Path = SysPath
  BackUp_Path_Recv = "C:\HTRECV\"
  BackUp_Path_Send = "C:\HTSEND\"
    
  If FsObj.FileExists(SysPath & "REGAL.INI") = False Then
     Open SysPath & "REGAL.INI" For Output As #1
     Print #1, "1"
     Close #1
  End If
  Open SysPath & "REGAL.INI" For Input As #1
  Line Input #1, READSTR
  Close #1
  ComPort = Val(Left(READSTR, 2))

  If FsObj.FolderExists(BackUp_Path_Recv) = False Then
     FsObj.CreateFolder (BackUp_Path_Recv)
  End If
  If FsObj.FolderExists(BackUp_Path_Send) = False Then
     FsObj.CreateFolder (BackUp_Path_Send)
  End If
  
  Exit Sub
LOADERR:
  MsgBox Err.Number & Err.Description, vbCritical
  
End Sub

Private Sub myForm_Click()
   Form2.Show
End Sub

Private Sub SafeStock_Report_Click()
Call CloseAll
FrmSafe_Report.Show
End Sub

Private Sub RecvFile_Click()
  
  'On Error GoTo RECVERR
  
  Dim RECV_DATA As Variant
  
  Recv_Success = True
  Do While Recv_Success = True
     sTU3_HT_File = ""
     FrmTU3.Show vbModal
     Unload FrmTU3
     If Recv_Success = True And sTU3_HT_File = "RECV.LOG" Then
        MsgBox "檔案接收完畢! ", vbInformation, Left(MDIMain.Caption, 20): Exit Sub
     ElseIf Recv_Success = True Then
        'MsgBox "檔案接收成功!" & sTU3_HT_File, vbInformation, Left(MDIMain.Caption, 20)
        FsObj.MoveFile SysPath & sTU3_HT_File, BackUp_Path_Recv & Format(Time, "HHMMSS") & sTU3_HT_File
     ElseIf Recv_Success = False And sTU3_HT_File = "" Then
        MsgBox "連線失敗!   ", vbCritical, Left(MDIMain.Caption, 20): Exit Sub
     ElseIf Recv_Success = False Then
        MsgBox "檔案接收失敗!   " & sTU3_HT_File, vbCritical, Left(MDIMain.Caption, 20)
     End If
  Loop
  
End Sub

'Private Sub SendFile_Click()
'  On Error GoTo SENDERR
'  Dim sFile() As String
'  Dim n, i, rec As Integer
'
'  '匯出主檔
'
' 'If rs.State Then rs.Close
'
'  If FsObj.FileExists(SysPath & "File.LOG") = False Then
'     MsgBox "系統檔案不存在!!!", vbCritical + vbOKOnly: Exit Sub
'  End If
'
'  Open SysPath & "FILE.LOG" For Input As #3
'  i = 0
'  While Not EOF(3)
'    i = i + 1: ReDim Preserve FileList(i)
'    Line Input #3, READSTR
'    FileList(i) = READSTR
'  Wend
'  Close #3
'
'  File1.Path = BackUp_Path_Send: rec = 0: 'ReDim Preserve sFile(rec):
'  File1.Refresh
'
'  FileNum = FreeFile
'  Open SysPath & "RECV.LOG" For Output As #FileNum
'
'  For n = 0 To File1.ListCount - 1
'    For i = 1 To UBound(FileList)
'        If UCase(File1.List(n)) = Trim(Mid(FileList(i), 1, 12)) Then
'           rec = rec + 1:
'           ReDim Preserve sFile(rec):
'           sFile(rec) = FileList(i)
'           Print #1, File1.List(n)
'        End If
'      Next i
'  Next n
'
'  If LOF(FileNum) = 0 Then msg.AddItem "無資料可傳送": Exit Sub
'  Close #FileNum
'
'
'  'SEND TRANS.LOG 通知 HT 目前是 傳送
'
'   sTU3_HT_Path = SysPath
'   sTU3_HT_File = "RECV.LOG"
'   FN_LAYOUT = "0112"
'   FrmTU3.Show vbModal
'   Unload FrmTU3
'  If Send_Success = False Then msg.AddItem "連線失敗!   ": Exit Sub
'
'  DELAY (1.3)
'  sTU3_HT_Path = BackUp_Path_Send
'  For n = 1 To UBound(sFile)
'     sTU3_HT_File = Trim(Mid(sFile(n), 1, 12))
'     FN_LAYOUT = Trim(Mid(sFile(n), 13, 34))
'     FN_Descrip = Trim(Mid(sFile(n), 47, 12))
'     FrmTU3.Show vbModal
'     Unload FrmTU3
'     If Send_Success = True Then
'        msg.AddItem "檔案傳送成功!   " & sTU3_HT_File & "(" & FN_Descrip & ")"
'     Else
'        msg.AddItem "檔案傳送失敗!   " & sTU3_HT_File & "(" & FN_Descrip & ")": Exit Sub
'     End If
'  Next n
'  Exit Sub
'SENDERR:
'  MsgBox Err.Number & Err.Description, vbCritical, "System Error"
'End Sub

Private Sub Timer1_Timer()

   Dim FLD(10) As String
   
   FileName = Dir(BackUp_Path_Recv & "*.TXT")
   If FileName = "" Then Exit Sub
   FileNum = FreeFile
   
   Open BackUp_Path_Recv & FileName For Input As #FileNum
   
   While Not EOF(1)
   
   On Error GoTo DataTransErr
   
   Line Input #FileNum, READSTR
   
   If Len(READSTR) = 62 Then
   
      FLD(1) = Trim(Mid(READSTR, 1, 5))            'LOC
      FLD(2) = Trim(Mid(READSTR, 6, 20))           'GLOBAL CODE & Serial Number
      'FLD(3) = Trim(Mid(READSTR, 17, 6))           ' Serial Numer
      FLD(4) = Val(Trim(Mid(READSTR, 26, 5)))      ' QTY
      FLD(5) = Left(Trim(Mid(READSTR, 31, 14)), 8) ' DATETIME
      FLD(6) = Trim(Mid(READSTR, 45, 10))          'USERID
      
      'FLD(7) = Trim(StrConv(MidB(READSTR, 55, 4), vbUnicode))           '作業別(1.進貨2.出貨3.退回4.盤點)
      FLD(7) = Trim(Mid(READSTR, 55, 2))           '作業別(1.進貨2.出貨3.退回4.盤點)
      'FLD(8) = Trim(StrConv(MidB(READSTR, 59, 6), vbUnicode))
      FLD(8) = Trim(Mid(READSTR, 57, 6))           'CUSTOMER ID
      FLD(6) = "HT"
      
      If "03024679001" = FLD(2) Then
         MsgBox "03024679001"
      End If
      
      If FLD(7) <> "盤點" Then
         sql = "INSERT INTO TRANSDTL(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" & FLD(2) & "','" & FLD(1) & "','" & FLD(4) & "' ,'" & FLD(5) & "','" & FLD(6) & "','" & FLD(7) & "','" & FLD(8) & "')"
         conn.Execute sql
      End If
      
      '進貨 -----------------------------------------------------------------------
      If FLD(7) = "進貨" Then
         
         sql = "Update STOCKMST Set IN_QTY=IN_QTY+ " & Val(FLD(4)) & " , REAL_QTY=LAST_QTY + (IN_QTY+ " & Val(FLD(4)) & ") - OUT_QTY  where gcode='" & FLD(2) & "' AND LOCATION='" & FLD(1) & "' "
         conn.Execute sql, RAC
         
         If RAC = 0 Then
            sql = "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) " & _
                  "VALUES ('" & FLD(2) & "' ,'" & FLD(1) & "','" & FLD(4) & "','" & FLD(5) & "','" & FLD(6) & "','" & FLD(7) & "','" & FLD(8) & "')"
            conn.Execute sql
         Else
            sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(FLD(4)) & " Where gcode='" & FLD(2) & "'"
            conn.Execute sql, RAC
            
            '         If RAC = 0 Then
            '            Sql = "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) VALUES ('" & FLD(2) & "' ,'" & FLD(1) & "','" & FLD(4) & "','" & FLD(5) & "','" & FLD(6) & "')"
            '            conn.Execute Sql
            '         End If
         End If
      End If
      
      '出貨 -----------------------------------------------------------------------
      
      If FLD(7) = "出貨" Then
      
         sql = "Update STOCKMST Set OUT_QTY = OUT_QTY+" & Val(FLD(4)) & ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " & Val(FLD(4)) & ") where GCODE='" & FLD(2) & "'  and LOCATION = '" & FLD(1) & "' "
         conn.Execute sql, RAC
      
         If RAC = 0 Then
            sql = "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" & FLD(2) & "' ,'" & FLD(1) & "','" & FLD(4) & "','" & FLD(5) & "','" & FLD(6) & "','" & FLD(7) & "','" & FLD(8) & "')"
            conn.Execute sql
         Else
            sql = "Update MATERMST Set Stock_QTY=Stock_QTY - " & Val(FLD(4)) & " Where gcode='" & FLD(2) & "'"
            conn.Execute sql, RAC
         End If
      
      End If
      
      '退回 -----------------------------------------------------------------------
      
      If FLD(7) = "退回" Then
      
         sql = "Update STOCKMST Set OUT_QTY=OUT_QTY - " & Val(FLD(4)) & " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " & Val(FLD(4)) & ")  where gcode='" & FLD(2) & "' AND LOCATION='" & FLD(1) & "' "
         conn.Execute sql, RAC
      
         If RAC = 0 Then
            sql = "INSERT INTO ERRDATA(GCODE,LOCATION,QTY,TRANS_DATE,USER_ID,STATUS,CUSTNO) VALUES ('" & FLD(2) & "' ,'" & FLD(1) & "','" & FLD(4) & "','" & FLD(5) & "','" & FLD(6) & "','" & FLD(7) & "','" & FLD(8) & "')"
            conn.Execute sql
         Else
            sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(FLD(4)) & " Where gcode='" & FLD(2) & "'"
            conn.Execute sql, RAC
         End If
      End If
      
      '盤點 -----------------------------------------------------------------------
      
      If FLD(7) = "盤點" Then
         sql = "Update INVENMST Set INVE_QTY=(INVE_QTY + " & Val(FLD(4)) & ") Where GCODE='" & FLD(2) & "' and location = '" & FLD(1) & "'"
         conn.Execute sql, RAC
         If RAC = 0 Then
            sql = "INSERT INTO INVENMST(GCODE,LOCATION,INVE_QTY,INVE_DATE,USER_ID) VALUES ('" & FLD(2) & "' ,'" & FLD(1) & "','" & FLD(4) & "','" & FLD(5) & "','" & FLD(6) & "')"
            conn.Execute sql
         End If
      End If
      
   Else
      
      If Len(READSTR) > 0 And Len(READSTR) < 62 Then
         Dim Fso As New FileSystemObject
         Dim FileData As TextStream
         
         Set FileData = Fso.OpenTextFile(App.Path & "\" & "ERRLog_" & Month(Now) & "-" & Day(Now) & ".Log", ForAppending, True)
         FileData.WriteLine "----------------------------------------------------------------------------"
         FileData.WriteLine Now & "讀入錯誤的資料為"
         FileData.WriteLine READSTR
         FileData.WriteLine "----------------------------------------------------------------------------"
         FileData.Close
         Set FileData = Nothing
         Set Fso = Nothing
      End If
   End If
   
   Wend
   Close #FileNum
   FileName = UCase(FileName)
   FsObj.CopyFile BackUp_Path_Recv & FileName, BackUp_Path_Recv & Replace(FileName, ".TXT", "") & "_" & Month(Now) & "-" & Day(Now) & ".BAK"
   FsObj.DeleteFile BackUp_Path_Recv & FileName
   Exit Sub
  
DataTransErr:
   Close #FileNum
   MsgBox "資料異常！" & vbCrLf & "停止匯入資料庫！"
   'FsObj.CopyFile BackUp_Path_Recv & FileName, BackUp_Path_Recv & Replace(FileName, ".txt", ".bak")
  
End Sub

Private Sub Transation_Query_Click()
  Call CloseAll
  FrmTransation_Q.Show
End Sub

Private Sub UPGRADE_Click()
  sTU3_HT_File = "P5028001.PD3"
  
  If FsObj.FileExists(sTU3_HT_Path & sTU3_HT_File) = True Then
     FrmTU3.Show vbModal
     Unload FrmTU3
     If Send_Success = True Then
        MsgBox "程式傳送成功!   " & sTU3_HT_File
     Else
        MsgBox "程式傳送失敗!   " & sTU3_HT_File
     End If
   Else
     msg.AddItem "找不到檔案!   " & sTU3_HT_File
  End If
End Sub

Private Sub USERID_Click()
   Call CloseAll
   FormUSERID.Show
End Sub

Private Sub USERPOWER_Click()
   Call CloseAll
   FormUSERPOWER.Show
End Sub

Private Sub MATERIEL_Click()
   Call CloseAll
   FrmMateriel.Show
End Sub

Private Sub MATERIEL_LABEL_Click()
   Call CloseAll
   FrmMateriel_Label.Show
End Sub
Private Sub MATERIEL_INVENTORY_Click()
   Call CloseAll
   FrmMateriel_INVT.Show
End Sub
Private Sub MATERIEL_QUERY_Click()
   Call CloseAll
   FrmMateriel_Q.Show
End Sub
Private Sub MATERIEL_REPORT_Click()
  Call CloseAll
  FrmMateriel_Report.Show
End Sub
Private Sub Abnormal_S_Click()
  Call CloseAll
  FrmAbnormal_S.Show
End Sub

Private Sub Abnormal_R_Click()
  Call CloseAll
  FrmAbnormal_R.Show
End Sub

Private Sub LOGOUT_Click()
  Call CloseAll
  FormLogin.Show vbModal
  FrmMain.Caption = "帝商庫存系統" & Space(80) & "使用者[" & USER_ID & "]"
  FrmMain.Logout.Caption = "登出[" & USER_ID & "]..."
  First1 = False
End Sub



