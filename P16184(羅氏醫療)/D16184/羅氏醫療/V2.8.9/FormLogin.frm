VERSION 5.00
Begin VB.Form FormLogin 
   BackColor       =   &H00C0FFC0&
   Caption         =   "Login"
   ClientHeight    =   2250
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   3810
   Icon            =   "FormLogin.frx":0000
   LinkTopic       =   "Form2"
   ScaleHeight     =   2250
   ScaleWidth      =   3810
   StartUpPosition =   2  '螢幕中央
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0FFFF&
      BorderStyle     =   0  '沒有框線
      Height          =   2055
      Left            =   120
      TabIndex        =   0
      Top             =   120
      Width           =   3620
      Begin VB.CommandButton Cmdexit 
         BackColor       =   &H00C0C0C0&
         Caption         =   "取消"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   2160
         Style           =   1  '圖片外觀
         TabIndex        =   4
         Top             =   1440
         Width           =   975
      End
      Begin VB.CommandButton CmdENTER 
         BackColor       =   &H00C0C0C0&
         Caption         =   "確定"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   495
         Left            =   480
         Style           =   1  '圖片外觀
         TabIndex        =   3
         Top             =   1440
         Width           =   975
      End
      Begin VB.TextBox TxtPASSWORD 
         BackColor       =   &H00E0E0E0&
         BeginProperty DataFormat 
            Type            =   0
            Format          =   "******"
            HaveTrueFalseNull=   0
            FirstDayOfWeek  =   0
            FirstWeekOfYear =   0
            LCID            =   1028
            SubFormatType   =   0
         EndProperty
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
         Height          =   375
         IMEMode         =   3  '暫止
         Left            =   1560
         MaxLength       =   6
         PasswordChar    =   "*"
         TabIndex        =   2
         Top             =   720
         Width           =   1815
      End
      Begin VB.TextBox TxtUSER_ID 
         BackColor       =   &H00E0E0E0&
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
         Height          =   375
         Left            =   1560
         MaxLength       =   10
         TabIndex        =   1
         Top             =   240
         Width           =   1815
      End
      Begin VB.PictureBox Picture1 
         BackColor       =   &H0080C0FF&
         Height          =   735
         Left            =   0
         ScaleHeight     =   675
         ScaleWidth      =   3555
         TabIndex        =   5
         Top             =   1320
         Width           =   3615
      End
      Begin VB.Shape Shape1 
         BorderColor     =   &H00C0C0C0&
         BorderWidth     =   2
         Height          =   1335
         Left            =   0
         Top             =   0
         Width           =   3615
      End
      Begin VB.Label Label1 
         BackColor       =   &H00C0FFFF&
         Caption         =   "USER_ID"
         BeginProperty Font 
            Name            =   "MS Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   375
         Left            =   480
         TabIndex        =   7
         Top             =   240
         Width           =   1095
      End
      Begin VB.Label Label2 
         BackColor       =   &H00C0FFFF&
         Caption         =   "PASSWORD"
         BeginProperty Font 
            Name            =   "MS Serif"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H00FF0000&
         Height          =   375
         Left            =   120
         TabIndex        =   6
         Top             =   720
         Width           =   1455
      End
   End
   Begin VB.Line Line2 
      BorderColor     =   &H00C0C0C0&
      BorderWidth     =   3
      X1              =   120
      X2              =   3720
      Y1              =   2160
      Y2              =   2160
   End
   Begin VB.Line Line1 
      BorderColor     =   &H00C0C0C0&
      BorderWidth     =   3
      X1              =   3720
      X2              =   3720
      Y1              =   1440
      Y2              =   2160
   End
End
Attribute VB_Name = "FormLogin"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim rs As New ADODB.Recordset
Private times As Integer

Private Sub Form_Activate()
  If TxtUSER_ID = "" Then
     TxtUSER_ID.SetFocus
  Else
      TxtPASSWORD.SetFocus
  End If
  times = 1
End Sub

Private Sub Form_Load()
   Call OpenConn
   Open "USER.txt" For Append As #1: Print #1, " ": Close #1
   Open "USER.txt" For Input As #1
   Line Input #1, user
   Close #1
   TxtUSER_ID = user
End Sub

Private Sub CmdENTER_Click()
   CmdENTER.Enabled = False
   CmdEXIT.Enabled = False
   Me.MousePointer = 11
   
   sql = "select * from USERMST WHERE USER_ID='" & TxtUSER_ID & "' and USER_PASS='" & TxtPASSWORD & "'"
   rs.Open sql, conn, adOpenDynamic, adLockPessimistic
   
   Call PROGRAM_ENABLE(False)
   If rs.RecordCount = 0 Then
      MsgBox "登入失敗!!", vbCritical + vbOKOnly, "系統訊息"
      If times = 3 Then
         Unload Me
      Else
        If TxtUSER_ID = "" Then
           TxtUSER_ID.SetFocus
        Else
           TxtPASSWORD.SetFocus
        End If
      End If
      Me.MousePointer = 4
      CmdENTER.Enabled = True
      CmdEXIT.Enabled = True
      times = times + 1
      rs.Close
      Exit Sub
   End If
   
   rs.Close
   If UCase(TxtUSER_ID) = "ADMIN" Then
      Call PROGRAM_ENABLE(True)
   Else
      sql = "select * from USERMSD WHERE USER_ID='" & TxtUSER_ID & "'"
      rs.Open sql, conn, adOpenDynamic, adLockPessimistic
      While Not rs.EOF
         Select Case rs.Fields("PROGRAM_ID")
      
         End Select
         rs.MoveNext
      Wend
      rs.Close
   End If
   Open "USER.txt" For Output As #1
   Print #1, TxtUSER_ID
   Close #1

   USER_ID = TxtUSER_ID
   Me.MousePointer = 4
   CmdENTER.Enabled = True
   CmdEXIT.Enabled = True
   Unload Me
End Sub

Private Sub CmdExit_Click()
  Call PROGRAM_ENABLE(False)
   Unload Me
End Sub

Private Sub TxtPASSWORD_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then Call CmdENTER_Click
End Sub

Private Sub TxtUSER_ID_GotFocus()
TxtUSER_ID.SelStart = 0
TxtUSER_ID.SelLength = Len(TxtUSER_ID)
End Sub

Private Sub TxtUSER_ID_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{tab}"
End Sub
Private Sub PROGRAM_ENABLE(bRUN As Boolean)

End Sub
