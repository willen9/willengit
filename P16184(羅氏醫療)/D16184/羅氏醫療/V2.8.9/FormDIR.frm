VERSION 5.00
Begin VB.Form FormDIR 
   BackColor       =   &H00C0FFC0&
   ClientHeight    =   3720
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6495
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3720
   ScaleWidth      =   6495
   StartUpPosition =   2  'CenterScreen
   Begin VB.ComboBox CobFileStyle 
      Height          =   300
      Left            =   120
      TabIndex        =   10
      Top             =   3240
      Width           =   1935
   End
   Begin VB.CommandButton CmdExit 
      BackColor       =   &H00FFFFC0&
      Caption         =   "取消"
      BeginProperty Font 
         Name            =   "標楷體"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5160
      MaskColor       =   &H00E0E0E0&
      TabIndex        =   5
      Top             =   600
      Width           =   1215
   End
   Begin VB.CommandButton CmdEnter 
      BackColor       =   &H00C0C0C0&
      Caption         =   "確定"
      BeginProperty Font 
         Name            =   "標楷體"
         Size            =   12
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   5160
      TabIndex        =   4
      Top             =   120
      Width           =   1215
   End
   Begin VB.TextBox TxtFile 
      Height          =   270
      Left            =   120
      Locked          =   -1  'True
      TabIndex        =   3
      Top             =   360
      Width           =   1935
   End
   Begin VB.DirListBox Dir1 
      Height          =   2190
      Left            =   2280
      TabIndex        =   2
      Top             =   720
      Width           =   2655
   End
   Begin VB.FileListBox File1 
      Height          =   2235
      Left            =   120
      Pattern         =   "IB.TXT"
      TabIndex        =   1
      Top             =   720
      Width           =   1935
   End
   Begin VB.DriveListBox Drive1 
      Height          =   300
      Left            =   2280
      TabIndex        =   0
      Top             =   3240
      Width           =   2655
   End
   Begin VB.Shape Shape1 
      Height          =   255
      Left            =   2280
      Top             =   360
      Width           =   2655
   End
   Begin VB.Label Label4 
      BackColor       =   &H00C0FFC0&
      Caption         =   "檔案類型:"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   11
      Top             =   3000
      Width           =   975
   End
   Begin VB.Label LabDir 
      BackColor       =   &H80000009&
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2280
      TabIndex        =   9
      Top             =   360
      UseMnemonic     =   0   'False
      Width           =   2640
   End
   Begin VB.Label Label3 
      BackColor       =   &H00C0FFC0&
      Caption         =   "磁碟機:"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2280
      TabIndex        =   8
      Top             =   3000
      Width           =   975
   End
   Begin VB.Label Label2 
      BackColor       =   &H00C0FFC0&
      Caption         =   "資料夾:"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   2280
      TabIndex        =   7
      Top             =   120
      Width           =   975
   End
   Begin VB.Label Label1 
      BackColor       =   &H00C0FFC0&
      Caption         =   "檔案名稱:"
      BeginProperty Font 
         Name            =   "新細明體"
         Size            =   9.75
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   255
      Left            =   120
      TabIndex        =   6
      Top             =   120
      Width           =   975
   End
End
Attribute VB_Name = "FormDIR"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Activate()
   If CobFileStyle.ListCount > 0 Then CobFileStyle.ListIndex = 0
   LabDir = Dir1.Path
   If File1.ListCount > 0 Then
      TxtFile = File1.List(0)
      File1.ListIndex = 0
    Else
      TxtFile = ""
   End If
End Sub

Private Sub CmdENTER_Click()
   If TxtFile = "" Then
      MsgBox "請選擇檔案名稱!!", vbCritical + vbOKOnly, "系統訊息"
      Exit Sub
   End If
   FileName = TxtFile
   If Right(LabDir, 1) = "\" Then
      FilePath = LabDir
    Else
      FilePath = LabDir & "\"
   End If
   Unload Me
End Sub

Private Sub CmdExit_Click()
  FileName = ""
  FilePath = ""
  Unload Me
End Sub


Private Sub CobFileStyle_Click()
   File1.Pattern = CobFileStyle
   If File1.ListCount > 0 Then
      TxtFile = File1.List(0)
      File1.ListIndex = 0
    Else
      TxtFile = ""
   End If
End Sub

Private Sub Dir1_Change()
   File1.Path = Dir1.Path
   LabDir = Dir1.Path
   If File1.ListCount > 0 Then
      TxtFile = File1.List(0)
      File1.ListIndex = 0
    Else
      TxtFile = ""
   End If
End Sub

Private Sub Drive1_Change()
   Dir1.Path = Drive1.Drive
   If File1.ListCount > 0 Then
      TxtFile = File1.List(0)
      File1.ListIndex = 0
    Else
      TxtFile = ""
   End If
End Sub

Private Sub File1_DblClick()
   TxtFile = File1.List(File1.ListIndex)
End Sub

Private Sub CobFileStyle_KeyPress(KeyAscii As Integer)
   KeyAscii = 0
End Sub

