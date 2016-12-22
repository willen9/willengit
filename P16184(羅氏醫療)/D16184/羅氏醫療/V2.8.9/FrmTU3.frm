VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "Mscomm32.ocx"
Begin VB.Form FrmTU3 
   Appearance      =   0  '平面
   BackColor       =   &H00FFFFFF&
   BorderStyle     =   0  '沒有框線
   Caption         =   "傳輸中..."
   ClientHeight    =   1680
   ClientLeft      =   4005
   ClientTop       =   3930
   ClientWidth     =   3990
   ControlBox      =   0   'False
   BeginProperty Font 
      Name            =   "新細明體"
      Size            =   9
      Charset         =   136
      Weight          =   700
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1680
   ScaleWidth      =   3990
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  '所屬視窗中央
   Visible         =   0   'False
   Begin MSComctlLib.ProgressBar PBar1 
      Height          =   375
      Left            =   120
      TabIndex        =   1
      Top             =   600
      Width           =   3735
      _ExtentX        =   6588
      _ExtentY        =   661
      _Version        =   393216
      Appearance      =   1
      Scrolling       =   1
   End
   Begin VB.CommandButton CmdEXIT 
      Caption         =   "中   斷"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   495
      Left            =   3000
      TabIndex        =   0
      Top             =   1080
      Width           =   735
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   1000
      Left            =   525
      Top             =   2055
   End
   Begin VB.Timer Timer1 
      Left            =   45
      Top             =   2055
   End
   Begin MSCommLib.MSComm MSComm1 
      Left            =   1005
      Top             =   2010
      _ExtentX        =   794
      _ExtentY        =   794
      _Version        =   393216
      DTREnable       =   -1  'True
      InputLen        =   1
      RThreshold      =   1
      BaudRate        =   115200
      InputMode       =   1
   End
   Begin VB.Shape Shape1 
      BorderWidth     =   3
      Height          =   1695
      Left            =   0
      Top             =   0
      Width           =   3975
   End
   Begin VB.Label Label1 
      BackColor       =   &H80000009&
      Caption         =   "傳輸中......"
      BeginProperty Font 
         Name            =   "標楷體"
         Size            =   15.75
         Charset         =   136
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00C00000&
      Height          =   375
      Left            =   120
      TabIndex        =   3
      Top             =   120
      Width           =   2175
   End
   Begin VB.Label Lab1 
      AutoSize        =   -1  'True
      BackStyle       =   0  '透明
      BeginProperty Font 
         Name            =   "標楷體"
         Size            =   14.25
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000000C0&
      Height          =   285
      Left            =   240
      TabIndex        =   2
      Top             =   1200
      Width           =   150
   End
End
Attribute VB_Name = "FrmTU3"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
  'Option Explicit
Dim InByte() As Byte
Dim SendByte() As Byte

Dim State As Integer  '=1上傳,=2下傳
Dim online As Integer   '是否正傳輸
Dim ERR1 As Integer     '錯誤次數
Dim Err2 As Integer     '錯誤次數
Dim Start As Boolean   '是否接收字元
Dim CRC As Integer
Dim HeadingText As String 'Heading Text
Dim FileName As String
Dim RecordCount As Integer    '總筆數
Dim first As Boolean
Dim BeforeStr As Integer
Dim READSTR As String
Dim BookMark  As Integer  '筆數
Private Sub Form_Activate()
    DoEvents
    
'    On Error GoTo ONErr1
     MSComm1.Settings = "115200,n,8,1"
      If MSComm1.PortOpen = False Then
         MSComm1.CommPort = ComPort
         MSComm1.PortOpen = True
        
      End If
      Start = False
      State = 1
      Lab1 = ""
      Call Recv
      If Right(sTU3_HT_File, 3) = "PD3" Then
             Call DownLoadPD3(sTU3_HT_File)
      ElseIf Len(sTU3_HT_File) > 0 Then
         Call DownLoad主檔
      End If
      Exit Sub

ONErr1:
      MsgBox "COM" & ComPort & "開起失敗!", vbCritical + vbOKOnly, "系統訊息"
      'Unload Me
      Me.Hide
End Sub

Private Sub Form_Load()
  FrmTU3.ZOrder 0
End Sub

Private Sub MsComm1_OnComm()
Select Case MSComm1.CommEvent
   Case comEvCD
   Case comEvCTS
   Case comEvDSR
   Case comEvRing
   Case comEvReceive
     While MSComm1.InBufferCount <> 0
        InByte() = MSComm1.Input
        Select Case State
           Case 1
              Call Recv_Sub
           Case 2
              Call DowLoad_Sub
        End Select
     Wend
   Case comEvSend
End Select
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~上傳~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Public Sub Recv_Sub()
           online = 1
           If InByte(0) = 5 Then
              MSComm1.InBufferCount = 0
              If HeadingText <> "" And BeforeStr <> 5 Then
                 MSComm1.Output = Chr(21)
                Else
                 MSComm1.Output = Chr(6)
                 BookMark = 0
              End If
              Timer1.Interval = 1000
              BeforeStr = 5
              Exit Sub
           End If
           BeforeStr = 0
           If InByte(0) = 1 And Timer1.Interval = 1000 And Start = False Then
              HeadingText = ""
              Start = True
              first = True
              Exit Sub
           End If
           If InByte(0) = 2 And Timer1.Interval = 1000 Then
              If HeadingText = "" Then
                 Timer1.Interval = 0
                 MSComm1.InBufferCount = 0
                 MSComm1.Output = Chr(4)
                 MSComm1.Output = Chr(6)
                 Exit Sub
              End If
              Start = True
              Exit Sub
           End If
           
           If InByte(0) = 3 And Timer1.Interval = 1000 Then
              While MSComm1.InBufferCount = 0
                 DoEvents
              Wend
              InByte() = MSComm1.Input
              CRC = InByte(0)
              
              If first Then
                 If CRC <> Checking(StrConv(HeadingText, vbUnicode)) Then
                    MSComm1.InBufferCount = 0
                    MSComm1.Output = Chr(21)
                    HeadingText = ""
                    WSTR(BookMark) = ""
                    Exit Sub
                 End If
                Else
                 If CRC <> Checking(StrConv(WSTR(BookMark), vbUnicode)) Then
                    MSComm1.InBufferCount = 0
                    MSComm1.Output = Chr(21)
                    WSTR(BookMark) = ""
                    Exit Sub
                 End If
              End If
              If BookMark > 1 Then
                 If Val(Left(StrConv(WSTR(BookMark), vbUnicode), 5)) = Val(Left(StrConv(WSTR(BookMark - 1), vbUnicode), 5)) Then
                    MSComm1.InBufferCount = 0
                    MSComm1.Output = Chr(6)
                    Exit Sub
                 End If
                 If Val(Left(StrConv(WSTR(BookMark), vbUnicode), 5)) <> Val(Left(StrConv(WSTR(BookMark - 1), vbUnicode), 5)) + 1 Then
                    MSComm1.InBufferCount = 0
                    MSComm1.Output = Chr(4)
                    MSComm1.Output = Chr(6)
                    Exit Sub
                 End If
              End If
              If first Then
                 HeadingText = StrConv(HeadingText, vbUnicode)
                 FileName = Trim(Left(HeadingText, 12))
                 RecordCount = Val(Mid(HeadingText, 13, 5))
                 BookMark = 0
                 Call RePBar(RecordCount + 1)
                 '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                 ReDim WSTR(RecordCount)
              End If
              ' WStr(BookMark) = StrConv(WStr(BookMark), vbUnicode)
              MSComm1.InBufferCount = 0
              MSComm1.Output = Chr(6)
              PBar1.Value = PBar1.Value + 1   '`````````````````````````````PBar
              first = False
              Start = False
              BookMark = BookMark + 1
              Exit Sub
           End If
           If InByte(0) = 4 Then
              If RecordCount <> BookMark - 1 Then
                 Call ErrSub
                 Exit Sub
              End If
              MSComm1.InBufferCount = 0
              MSComm1.Output = Chr(6)
              If Timer1.Interval = 1000 Then
                 Call RecvSuccess
                 Exit Sub
              End If
           End If
           If Start And Timer1.Interval = 1000 Then
              If first Then
                 HeadingText = HeadingText & ChrB(InByte(0))
                 WSTR(BookMark) = WSTR(BookMark) & ChrB(InByte(0))
               Else
                 WSTR(BookMark) = WSTR(BookMark) & ChrB(InByte(0))
              End If
           End If
End Sub
Public Sub Recv()
   Close #1
   Start = False
   State = 1
   online = 0
   ReDim WSTR(0)
   Timer1.Interval = 0
   HeadingText = ""
   MSComm1.RTSEnable = False
   MSComm1.RTSEnable = True
   BookMark = 0
   ERR1 = 0
   Err2 = 0
   HeadingText = ""
   Lab1 = "資料接收中..."
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~下傳~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
Public Sub DownLoad()
   MSComm1.RTSEnable = True
   State = 2
   online = 0
   Start = False
   ERR1 = 0
   Err2 = 0
   BookMark = 0
   Timer2.Enabled = True
   Call RePBar(RecordCount + 1)
   
End Sub

Public Sub DowLoad_Sub()
           online = 1    '開始傳輸
           If InByte(0) = 6 And Not Start Then
                Start = True  '開始接收字元
                Timer1.Interval = 1000
                CRC = CheckCRC(HeadingText)
                MSComm1.InBufferCount = 0
                SendByte = ChrB(1) & HeadingText & ChrB(3) & ChrB(CRC)
                MSComm1.Output = SendByte
                BookMark = 0
                Exit Sub
           End If
           
           If InByte(0) = 6 Then
              If BookMark < RecordCount Then
                 BookMark = BookMark + 1
                 CRC = CheckCRC(WSTR(BookMark))
                 SendByte = ChrB(2) & WSTR(BookMark) & ChrB(3) & ChrB(CRC)
                 MSComm1.Output = SendByte()
                 PBar1.Value = PBar1.Value + 1
               Else
                 Call DownLoadSuccess
              End If
              Exit Sub
           End If
           If InByte(0) = 21 Then
              DoEvents
              CRC = CheckCRC(WSTR(BookMark))
              SendByte = ChrB(2) & WSTR(BookMark) & ChrB(3) & ChrB(CRC)
              MSComm1.Output = SendByte
           End If
           
           If InByte(0) = 4 Then
              Close #1
              Call ErrSub
           End If
End Sub
'~~~~~~~~~~~~~DownLoad主檔~~~~~~~~~
Public Sub DownLoad主檔()
   Dim TMP1 As String, i As Integer, j As Integer, a As Integer
  
   Open sTU3_HT_Path & sTU3_HT_File For Input As #1
   If LOF(1) = 0 Then
      Close #1: Me.Hide: Exit Sub
   End If
   Line Input #1, READSTR
   
   RecordCount = Int((LOF(1) / (LenB(StrConv(READSTR, vbFromUnicode)) + 2)) + 0.99999)
   Close #1
   sLen = 0
   For i = 3 To Len(FN_LAYOUT) Step 2
      sLen = sLen + Val(Mid(FN_LAYOUT, i, 2))
   Next
   
   PBar1.Max = RecordCount
   PBar1.Value = 1
   Lab1 = "資料轉檔中..."
   
   Open sTU3_HT_Path & sTU3_HT_File For Input As #1
      i = 1
      While Not EOF(1)
          Line Input #1, READSTR
          If Trim(READSTR) <> "" Then
             ReDim Preserve WSTR(i)
             
             WSTR(i) = LeftB(StrConv(Format(i, "00000") & READSTR & Space(sLen + 5), vbFromUnicode), sLen + 5)
             i = i + 1
             If i < RecordCount Then PBar1.Value = i
             DoEvents
         End If
      Wend
   Close #1
   RecordCount = UBound(WSTR)
   HeadingText = Left(sTU3_HT_File & "            ", 12) & Format(RecordCount, "00000") & FN_LAYOUT
   HeadingText = StrConv(UCase(HeadingText), vbFromUnicode)
   WSTR(0) = HeadingText
   Lab1 = "資料傳送中..."
   Call DownLoad
End Sub
'~~~~~~~~~~~~~DownLoad程式~~~~~~~~~
Public Sub DownLoadPD3(sTMP As String)
   Dim i As Integer
   Call DELAY(1)
   If sTMP = sTU3_HT_File Then
      FileName = sTU3_HT_File
      sDwonName = sTU3_HT_File
      FieldCount = sTU3_HT_File
      Lab1 = "程式下載中..."
   End If
   Open sTU3_HT_Path & FileName For Input As #1
      Line Input #1, READSTR
      RecordCount = Int((LOF(1) / (LenB(StrConv(READSTR, vbFromUnicode)) + 2)) + 0.99999)
      ReDim WSTR(RecordCount)
      HeadingText = Left(sDwonName & "            ", 12) & Format(RecordCount, "00000")
      HeadingText = StrConv(UCase(HeadingText), vbFromUnicode)
    Close #1
   Open sTU3_HT_Path & FileName For Input As #1
      WSTR(0) = HeadingText
      For i = 1 To RecordCount
          Line Input #1, READSTR
          WSTR(i) = StrConv(Format(i, "00000") & Left(READSTR & Space(128), 128), vbFromUnicode)
      Next
   Close #1
   Call DownLoad
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~下傳成功~~~~~~~~~~~~~
Public Sub DownLoadSuccess()
    MSComm1.Output = Chr(4)
    Timer2.Enabled = False
    PBar1.Value = PBar1.Max
    Call Recv
    PBar1.Value = PBar1.Min
    Send_Success = True
    'Unload Me
    Me.Hide
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~上傳成功~~~~~~~~~~~~~
Public Sub RecvSuccess()
    Dim i As Integer, j As Integer
    MSComm1.Output = Chr(4)
    sTU3_HT_File = FileName
    Open sTU3_HT_Path & sTU3_HT_File For Output As #1
    For i = 1 To RecordCount
        WSTR(i) = Mid(StrConv(WSTR(i), vbUnicode), 6)
        Print #1, WSTR(i)
    Next
    Close #1
    MSComm1.PortOpen = False
    Call Recv
    PBar1.Value = PBar1.Min
    Recv_Success = True
    'Unload Me
    Me.Hide
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~EXIT!~~~~~~~~~~~~~~~~~~~~~~~~~~`
Private Sub CmdExit_Click()
    MSComm1.PortOpen = False
    Call Recv
    Send_Success = False: Recv_Success = False
    Me.Hide
End Sub

Public Sub ErrSub()
    MSComm1.Output = Chr(4)
    MSComm1.Output = Chr(6)
    Timer2.Enabled = False
    Call Recv
    Me.BackColor = &HFF&
    Lab1 = "傳送失敗!"
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~CRC CHECK~~~~~~~~~~~~~~~
Public Function CheckCRC(ByVal TmpStr As String)
   Dim CS As Integer, i As Integer
   TmpStr = StrConv(TmpStr, vbUnicode)
   TmpStr = StrConv(TmpStr, vbFromUnicode)
   If TmpStr = "" Then
      CheckCRC = CS
      Exit Function
   End If
   CS = AscB(MidB(TmpStr, 1, 1))
   For i = 2 To LenB(TmpStr)
       CS = CS Xor AscB(MidB(TmpStr, i, 1))
   Next
   CS = CS Xor 3
   CheckCRC = CS
End Function

Public Function Checking(ByVal TmpStr As String)
   Dim CS As Integer, i As Integer
   TmpStr = StrConv(TmpStr, vbFromUnicode)
   If TmpStr = "" Then
      Checking = CS
      Exit Function
   End If
   CS = AscB(MidB(TmpStr, 1, 1))
   For i = 2 To LenB(TmpStr)
       CS = CS Xor AscB(MidB(TmpStr, i, 1))
   Next
   CS = CS Xor 3
   Checking = CS
End Function
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~OTHER SUB~~~~~~~~~~~~

Public Sub RePBar(Max As Integer)
   PBar1.Min = 0
   PBar1.Value = 0
   PBar1.Max = Max
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~TIME CONTROL~~~~~~~~~~~~~~~~
Private Sub Timer2_Timer()
   If Not Start And State = 2 Then
      MSComm1.InBufferCount = 0
      MSComm1.Output = Chr(5)
   End If
   Err2 = Err2 + 1
   If Err2 > 100 Then
      Timer2.Enabled = False
      Call ErrSub
      ERR1 = 0
      Err2 = 0
   End If
   DoEvents
End Sub
Private Sub Timer1_Timer()
      DoEvents
      If online = 1 Then
         online = 0
         ERR1 = 0
         Err2 = 0
         Exit Sub
       Else
         ERR1 = ERR1 + 1
         If ERR1 >= 3 And State = 2 Then
            MSComm1.InBufferCount = 0
            MSComm1.Output = Chr(5)
         End If
         If ERR1 > 100 Then
            Call ErrSub
         End If
      End If
End Sub
Public Function Timef()
   Timef = Format(Hour(Time), "00") & Format(Minute(Time), "00")
End Function

Public Function TodayD()
   TodayD = Format(Year(Date), "0000") & "/" & Format(Month(Date), "00") & "/" & Format(Day(Date), "00")
End Function
Public Function TimeD()
   TimeD = Format(Hour(Time), "00") & ":" & Format(Minute(Time), "00") & ":" & Format(Second(Time), "00")
End Function


