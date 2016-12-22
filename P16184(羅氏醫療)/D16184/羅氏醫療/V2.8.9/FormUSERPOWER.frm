VERSION 5.00
Begin VB.Form FrmUSERPOWER 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "程式權限設定"
   ClientHeight    =   7290
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   6960
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7290
   ScaleWidth      =   6960
   Begin VB.ListBox LstPROG 
      BackColor       =   &H00FFFFFF&
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   12
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   4920
      Left            =   360
      Style           =   1  '項目包含核取方塊
      TabIndex        =   2
      Top             =   840
      Width           =   6255
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0C0C0&
      Height          =   1095
      Left            =   360
      TabIndex        =   3
      Top             =   5760
      Width           =   6255
      Begin VB.CommandButton CmdExit 
         BackColor       =   &H80000004&
         Caption         =   "離開"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   4680
         Style           =   1  '圖片外觀
         TabIndex        =   7
         Top             =   240
         Width           =   1095
      End
      Begin VB.CommandButton CmdUpDate 
         BackColor       =   &H80000004&
         Caption         =   "套用"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   3240
         Style           =   1  '圖片外觀
         TabIndex        =   6
         Top             =   240
         Width           =   1095
      End
      Begin VB.CommandButton CmdSelAll 
         BackColor       =   &H80000004&
         Caption         =   "全選"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   480
         Style           =   1  '圖片外觀
         TabIndex        =   5
         Top             =   240
         Width           =   1095
      End
      Begin VB.CommandButton CmdSelClear 
         BackColor       =   &H80000004&
         Caption         =   "清除"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   1920
         Style           =   1  '圖片外觀
         TabIndex        =   4
         Top             =   240
         Width           =   1095
      End
   End
   Begin VB.ComboBox CobUSER_ID 
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   1800
      TabIndex        =   0
      Top             =   360
      Width           =   2652
   End
   Begin VB.Label Label2 
      BackColor       =   &H00C0C0C0&
      Caption         =   "使用者代碼:"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   12
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00000000&
      Height          =   255
      Left            =   360
      TabIndex        =   1
      Top             =   360
      Width           =   1335
   End
   Begin VB.Shape Shape9 
      Height          =   7095
      Left            =   120
      Top             =   120
      Width           =   6735
   End
   Begin VB.Shape Shape10 
      BorderColor     =   &H80000005&
      BorderWidth     =   2
      Height          =   7095
      Left            =   120
      Top             =   120
      Width           =   6735
   End
   Begin VB.Shape Shape11 
      BorderColor     =   &H00C0C0C0&
      BorderWidth     =   5
      Height          =   7095
      Left            =   120
      Top             =   120
      Width           =   6735
   End
   Begin VB.Shape Shape12 
      BackColor       =   &H00C0FFC0&
      BorderColor     =   &H00FFFFFF&
      BorderStyle     =   0  '透明
      BorderWidth     =   3
      FillColor       =   &H00C0C0C0&
      FillStyle       =   0  '實心
      Height          =   7095
      Left            =   120
      Top             =   120
      Width           =   6735
   End
End
Attribute VB_Name = "FrmUSERPOWER"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim rs As New ADODB.Recordset
Attribute rs.VB_VarHelpID = -1
Dim rsTmp As New ADODB.Recordset
Attribute rsTmp.VB_VarHelpID = -1
Dim flag As Integer
Private Sub Form_Activate()
   If rs.State Then rs.Close
   If rsTmp.State Then rsTmp.Close
   DoEvents
   Sql = "select USER_ID from USERMSD GROUP by USER_ID"
   rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
   While Not rs.EOF
      CobUSER_ID.AddItem rs.Fields("USER_ID")
      rs.MoveNext
   Wend
   rs.Close
   Sql = "select * from PROGMREF ORDER BY PROG_ID "
   rsTmp.Open Sql, conn, adOpenDynamic, adLockPessimistic
   While Not rsTmp.EOF
      LstPROG.AddItem rsTmp.Fields("PROG_ID") & "         " & rsTmp.Fields("PROG_NAME")
      rsTmp.MoveNext
   Wend
   
   CobUSER_ID.ListIndex = 0
   CmdUpDate.Enabled = False
  End Sub

Private Sub Form_Load()
   If App.PrevInstance Then End
   flag = 0
   Me.Left = 2500
   Me.Top = 200
   'Call OpenConn
End Sub

'========================================CmdControl=================================================


'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~套用~~~~~~~~~~~~~~~~~~~~~~~
Private Sub cmdUpdate_Click()
   If rs.State Then rs.Close
   Sql = "select * from USERMSD WHERE USER_ID='" & CobUSER_ID & "' order by PROGRAM_ID"
   rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
   If rs.RecordCount = 0 Then Exit Sub
   i = 0
   For i = 0 To LstPROG.ListCount - 1
      rs.MoveFirst
      a = Trim(Left(Trim(LstPROG.List(i)), 10))
      rs.Find "PROGRAM_ID='" & Trim(Left(Trim(LstPROG.List(i)), 10)) & "'"
      If Not (rs.EOF) Then
         If (rs.Fields("FLAG") = "0" And LstPROG.Selected(i) = True) Or (rs.Fields("FLAG") = "1" And LstPROG.Selected(i) = False) Then
            If LstPROG.Selected(i) = True Then
               tmp = "1"
              Else
               tmp = "0"
            End If
            Sql = "Update USERMSD Set FLAG='" & tmp & "' where USER_ID='" & CobUSER_ID & "' AND PROGRAM_ID='" & rs.Fields("PROGRAM_ID") & "'"
            conn.Execute Sql
         End If
         rs.MoveNext
      End If
   Next
   rs.Close
   CmdUpDate.Enabled = False
   MsgBox "套用後要登出設定才會發生做用!!", vbExclamation + vbOKOnly, "系統訊息"
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~離開~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExit_Click()
 ' FormExit
  Unload Me
End Sub
Private Sub Form_Unload(CANCEL As Integer)
   'conn.Close
End Sub


Public Sub SetList()
   If rs.State Then rs.Close
   Sql = "select * from USERMSD WHERE USER_ID='" & CobUSER_ID & "' order by PROGRAM_ID"
   rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
   rsTmp.MoveFirst
 If rs.RecordCount = 0 Then Exit Sub
   While Not rsTmp.EOF
      rs.MoveFirst
      rs.Find "PROGRAM_ID='" & rsTmp.Fields("PROG_ID") & "'"
      If rs.EOF Then
         Sql = "INSERT INTO USERMSD(USER_ID,PROGRAM_ID,FLAG) VALUES ('" & CobUSER_ID & "','" & rsTmp.Fields("PROG_ID") & "','0')"
         conn.Execute Sql
      End If
      rsTmp.MoveNext
   Wend
   rs.Requery
   
   For i = 0 To LstPROG.ListCount - 1
      rs.MoveFirst
      rs.Find "PROGRAM_ID='" & Trim(Left(Trim(LstPROG.List(i)), 10)) & "'"
      If Not (rs.EOF) Then
         If rs.Fields("FLAG") = "1" Then
            LstPROG.Selected(i) = True
           Else
            LstPROG.Selected(i) = False
         End If
      End If
   Next
   rs.Close
End Sub

Private Sub LstPROG_Click()
CmdUpDate.Enabled = True
End Sub
Private Sub CobLEVEL_KeyPress(KeyAscii As Integer)
KeyAscii = 0
End Sub

Private Sub CobUSER_ID_Click()
'CobUSER_NAME.ListIndex = CobUSER_ID.ListIndex
Call SetList
End Sub

Private Sub CobUSER_ID_Validate(CANCEL As Boolean)
If CobUSER_ID.ListIndex = -1 Then
   MsgBox "使用者代碼輸入錯誤!!", vbExclamation + vbOKOnly, "系統訊息"
   CANCEL = True
End If
End Sub

Private Sub CobUSER_NAME_Click()
CobUSER_ID.ListIndex = CobUSER_NAME.ListIndex
End Sub

Private Sub CobUSER_NAME_Validate(CANCEL As Boolean)
If CobUSER_NAME.ListIndex = -1 Then
   MsgBox "使用者名稱輸入錯誤!!", vbExclamation + vbOKOnly, "系統訊息"
   CANCEL = True
End If
End Sub

Private Sub CmdSelAll_Click()
   For i = 0 To LstPROG.ListCount - 1
       LstPROG.Selected(i) = True
   Next
End Sub

Private Sub CmdSelClear_Click()
   For i = 0 To LstPROG.ListCount - 1
       LstPROG.Selected(i) = False
   Next
End Sub


