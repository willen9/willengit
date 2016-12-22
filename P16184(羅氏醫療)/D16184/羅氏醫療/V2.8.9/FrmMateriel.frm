VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form FrmMateriel 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "物料資料"
   ClientHeight    =   7980
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11955
   LinkTopic       =   "Form2"
   MDIChild        =   -1  'True
   ScaleHeight     =   7980
   ScaleMode       =   0  '使用者自訂
   ScaleWidth      =   11055
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   27
      Top             =   0
      Width           =   4155
      _ExtentX        =   7329
      _ExtentY        =   1455
      ButtonWidth     =   1032
      ButtonHeight    =   1349
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   7
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "新增"
            Key             =   "add"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "修改"
            Key             =   "edit"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "儲存"
            Key             =   "save"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "取消"
            Key             =   "cancel"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "查詢"
            Key             =   "search"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "刪除"
            Key             =   "del"
            ImageIndex      =   4
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "返回"
            Key             =   "exit"
            ImageIndex      =   13
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frame2 
      Height          =   7095
      Left            =   0
      TabIndex        =   12
      Top             =   840
      Width           =   11775
      Begin VB.CommandButton CmdYes 
         BackColor       =   &H00C0C0C0&
         Default         =   -1  'True
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   420
         Left            =   6120
         Picture         =   "FrmMateriel.frx":0000
         Style           =   1  '圖片外觀
         TabIndex        =   24
         Tag             =   "2"
         ToolTipText     =   "確定"
         Top             =   1680
         Visible         =   0   'False
         Width           =   450
      End
      Begin VB.TextBox TxtRemark 
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
         Height          =   315
         Left            =   1560
         MaxLength       =   40
         TabIndex        =   7
         Top             =   1800
         Width           =   4215
      End
      Begin VB.TextBox TxtUSystem 
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
         Height          =   315
         Left            =   1560
         MaxLength       =   40
         TabIndex        =   6
         Top             =   1440
         Width           =   4215
      End
      Begin VB.TextBox TxtSafe_Qty 
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   8160
         TabIndex        =   5
         Top             =   1080
         Width           =   735
      End
      Begin VB.TextBox TxtStock_Qty 
         BackColor       =   &H00FFFFFF&
         Enabled         =   0   'False
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   315
         Left            =   6240
         MaxLength       =   4
         TabIndex        =   4
         Top             =   1080
         Width           =   735
      End
      Begin VB.TextBox TxtStyle 
         Alignment       =   2  '置中對齊
         Appearance      =   0  '平面
         BackColor       =   &H80000004&
         BorderStyle     =   0  '沒有框線
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   14.25
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   375
         Left            =   9840
         TabIndex        =   17
         Text            =   "瀏  覽"
         Top             =   360
         Width           =   1575
      End
      Begin VB.TextBox TxtLCODE 
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
         Height          =   285
         Left            =   1560
         MaxLength       =   10
         TabIndex        =   2
         Top             =   1080
         Width           =   1695
      End
      Begin VB.TextBox TxtGCODE 
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
         Height          =   315
         Left            =   1560
         MaxLength       =   15
         TabIndex        =   0
         Top             =   360
         Width           =   2655
      End
      Begin VB.TextBox TxtUNIT 
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
         Height          =   315
         Left            =   4320
         MaxLength       =   4
         TabIndex        =   3
         Top             =   1080
         Width           =   735
      End
      Begin VB.TextBox TxtDESC 
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
         Height          =   315
         Left            =   1560
         MaxLength       =   40
         TabIndex        =   1
         Top             =   720
         Width           =   7335
      End
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   4695
         Left            =   240
         TabIndex        =   13
         TabStop         =   0   'False
         Top             =   2280
         Width           =   11295
         _ExtentX        =   19923
         _ExtentY        =   8281
         _Version        =   393216
         BackColor       =   16777215
         ForeColor       =   16711680
         HeadLines       =   1
         RowHeight       =   19
         BeginProperty HeadFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ColumnCount     =   2
         BeginProperty Column00 
            DataField       =   ""
            Caption         =   ""
            BeginProperty DataFormat {6D835690-900B-11D0-9484-00A0C91110ED} 
               Type            =   0
               Format          =   ""
               HaveTrueFalseNull=   0
               FirstDayOfWeek  =   0
               FirstWeekOfYear =   0
               LCID            =   1028
               SubFormatType   =   0
            EndProperty
         EndProperty
         BeginProperty Column01 
            DataField       =   ""
            Caption         =   ""
            BeginProperty DataFormat {6D835690-900B-11D0-9484-00A0C91110ED} 
               Type            =   0
               Format          =   ""
               HaveTrueFalseNull=   0
               FirstDayOfWeek  =   0
               FirstWeekOfYear =   0
               LCID            =   1028
               SubFormatType   =   0
            EndProperty
         EndProperty
         SplitCount      =   1
         BeginProperty Split0 
            BeginProperty Column00 
               ColumnWidth     =   8.5878e5
            EndProperty
            BeginProperty Column01 
               ColumnWidth     =   8.5878e5
            EndProperty
         EndProperty
      End
      Begin VB.Label Label9 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   9480
         TabIndex        =   26
         Top             =   2040
         Width           =   1695
      End
      Begin VB.Label Label8 
         Caption         =   "Description"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   240
         TabIndex        =   25
         Top             =   720
         Width           =   1335
      End
      Begin VB.Label Label7 
         BackColor       =   &H80000004&
         Caption         =   "Remark:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   600
         TabIndex        =   22
         Top             =   1800
         Width           =   975
      End
      Begin VB.Label Label6 
         BackColor       =   &H80000004&
         Caption         =   "USystem:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   600
         TabIndex        =   21
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label3 
         BackColor       =   &H80000004&
         Caption         =   "安全量:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   7320
         TabIndex        =   20
         Top             =   1080
         Width           =   855
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000004&
         Caption         =   "庫存量:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   5400
         TabIndex        =   19
         Top             =   1080
         Width           =   855
      End
      Begin VB.Label Label5 
         BackColor       =   &H80000004&
         Caption         =   "Local Code:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   360
         TabIndex        =   16
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H80000004&
         Caption         =   "Global Code:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   240
         TabIndex        =   15
         Top             =   360
         Width           =   1455
      End
      Begin VB.Label Label4 
         BackColor       =   &H80000004&
         Caption         =   "單位:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   3720
         TabIndex        =   14
         Top             =   1080
         Width           =   615
      End
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   480
      Left            =   5760
      TabIndex        =   8
      Top             =   240
      Visible         =   0   'False
      Width           =   2700
      _ExtentX        =   4763
      _ExtentY        =   847
      ButtonWidth     =   609
      ButtonHeight    =   741
      Appearance      =   1
      _Version        =   393216
      Begin VB.CommandButton CmdQuery 
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   420
         Left            =   480
         Picture         =   "FrmMateriel.frx":0636
         Style           =   1  '圖片外觀
         TabIndex        =   23
         ToolTipText     =   "查詢"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton Cmddel 
         Height          =   420
         Left            =   1440
         Picture         =   "FrmMateriel.frx":0B25
         Style           =   1  '圖片外觀
         TabIndex        =   18
         ToolTipText     =   "刪除資料"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdClear 
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   420
         Left            =   960
         Picture         =   "FrmMateriel.frx":1086
         Style           =   1  '圖片外觀
         TabIndex        =   11
         Tag             =   "2"
         ToolTipText     =   "取消"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdExit 
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   420
         Left            =   1920
         Picture         =   "FrmMateriel.frx":131D
         Style           =   1  '圖片外觀
         TabIndex        =   10
         ToolTipText     =   "離開"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdNew 
         BackColor       =   &H00C0C0C0&
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   435
         Left            =   0
         Picture         =   "FrmMateriel.frx":1861
         Style           =   1  '圖片外觀
         TabIndex        =   9
         ToolTipText     =   "新增"
         Top             =   0
         Width           =   450
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   0
      Top             =   8040
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   17
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":1DB6
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":2E08
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":3183
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":3A5D
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":3DB2
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":433D
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":4657
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":56A9
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":5A16
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":6A68
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":7ABA
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":7DD4
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":80EE
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":9140
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":945A
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":97AC
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel.frx":9AFE
            Key             =   ""
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmMateriel"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'Dim rs As New ADODB.Recordset
'Dim rsTmp As New ADODB.Recordset
Dim flag As Integer  'flag=1 新增  flag=2修改 flag=3查詢


Private Sub DataGrid1_DblClick()
  Call GridToField
End Sub

Private Sub Form_Activate()
   FormsAttribute
   Call ExecuteSQL
   'Call GridToField
   '-----Gauss
   Call FieldClear
'   Toolbar2.Buttons("add").Enabled = True
'   Toolbar2.Buttons("save").Enabled = False
'   Toolbar2.Buttons("cancel").Enabled = False
'   Toolbar2.Buttons("search").Enabled = True
'   Toolbar2.Buttons("del").Enabled = False
'   Toolbar2.Buttons("exit").Enabled = True
   '---------------
      
End Sub

Private Sub Form_Load()
  FormsAttribute
  'Me.Left = 150
  'Me.Top = 300
  'Call OpenConn
  
  Call 瀏覽_Bar
  
'  TxtStock_Qty.Enabled = False
'  TxtDESC.Enabled = False
'  TxtUNIT.Enabled = False
'  TxtStock_Qty.Enabled = False
'  TxtSafe_Qty.Enabled = False
'  TxtRemark.Enabled = False
  
'  TxtStock_Qty.BackColor = &H80000004
'  TxtDESC.BackColor = &H80000004         '反白區域
'  TxtUNIT.BackColor = &H80000004
'  TxtStock_Qty.BackColor = &H80000004
'  TxtSafe_Qty.BackColor = &H80000004
'  TxtRemark.BackColor = &H80000004
  
'    Toolbar2.Buttons("add").Enabled = True
'    Toolbar2.Buttons("save").Enabled = False
'    Toolbar2.Buttons("cancel").Enabled = False
'    Toolbar2.Buttons("search").Enabled = True
'    Toolbar2.Buttons("del").Enabled = False
'    Toolbar2.Buttons("exit").Enabled = True
'
'    CmdClear.Enabled = False
'    CmdDel.Enabled = False
'    CmdYes.Enabled = False
'    CmdQuery.Enabled = True
'    CmdNew.Enabled = True
'    CmdEXIT.Enabled = True
    
    
End Sub

'========================================CmdControl=================================================
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~新增~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdNew_Click()
   flag = 1
   TxtStyle = "新  增"
   Call FieldClear
   TxtGCode.Locked = False
   TxtGCode.SetFocus
   
'  TxtDESC.Enabled = True
'  TxtUNIT.Enabled = True
'  TxtSafe_Qty.Enabled = True
'  TxtRemark.Enabled = True
  
'  TxtDESC.BackColor = &HFFFFFF          '空白區域
'  TxtUNIT.BackColor = &HFFFFFF
'  TxtSafe_Qty.BackColor = &HFFFFFF
'  TxtRemark.BackColor = &HFFFFFF
  
'    CmdClear.Enabled = True
'    CmdDel.Enabled = False
'    CmdYes.Enabled = True
'    CmdQuery.Enabled = False
'    CmdNew.Enabled = False
'    CmdEXIT.Enabled = False
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~刪除~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdDEL_Click()
   On Error Resume Next
   
   If TxtGCode.Text = "" And TxtSum.Text = "" Then
      MsgBox "請輸入料號，否則無法刪除！"
      Exit Sub
   End If
   
   If rsTmp.State Then rsTmp.Close
   If TxtSum.Text = "" Then
      Sql = "select * from STOCKMST WHERE GCode ='" & TxtGCode & "'  "
   Else
      Sql = "select * from STOCKMST WHERE GCode ='" & TxtGCode & "'  "
   End If
   rsTmp.Open Sql, conn, adOpenKeyset, adLockPessimistic
    
   If Not rsTmp.EOF Then
   If rsTmp.RecordCount > 0 Then
      MsgBox "尚有庫存無法刪除!!", vbExclamation + vbOKOnly, "系統訊息"
      Exit Sub
   End If
   End If

   If MsgBox("是否確定刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
      Exit Sub
   End If
   If TxtSum.Text = "" Then
      Sql = "Delete from MATERMST where GCode='" & TxtGCode & "' "
   Else
      Sql = "Delete from MATERMST where GCode='" & TxtGCode & "' "
   End If
   conn.Execute Sql
   FieldClear
   rs.Requery
   Call SETDG

End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~查詢~~~~~~~~~~~~~~~~~~~~~~~
Private Sub cmdQuery_Click()
   TxtStyle = "查  詢"
   ExecuteSQL
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~確定~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdYes_Click()

   If TxtGCode = "" Or TxtLCode.Text = "" Or TxtUSystem.Text = "" Or TxtUNIT.Text = "" Then
      MsgBox "不允許空值新值，請輸入值!!", vbExclamation + vbOKOnly, "系統訊息"
      TxtGCode.SetFocus
      Exit Sub
   End If
    
   'On Error GoTo ERR1

   If rs.State Then rs.Close
   
   
   Sql = "SELECT * FROM MATERMST WHERE GCode = '" & TxtGCode & "'  "
   rs.Open Sql, conn, adOpenStatic, adLockOptimistic
   If rs.RecordCount = 0 Then
      'Sql = "INSERT INTO MATERMST(GCode,SNum,Description,UNIT,LCode,STOCK_QTY,SAFE_QTY,USYSTEM,REMARK,USER_ID,CREATE_DATE) " & _
            "VALUES ('" & TxtGCODE & "','" & TXTSUM.Text & "','" & TxtDESC & "'," & TxtUNIT & ",'" & TxtLCODE & "'," & TxtStock_Qty & ", " & _
            "" & TxtSafe_Qty & ",'" & USYSTEM & "','" & TxtRemark & "','" & USER_ID & "','" & Todayf & "')"
      'conn.Execute Sql
       rs.AddNew
       rs.Fields("GCode") = TxtGCode.Text
       rs.Fields("Description") = TxtDESC.Text
       rs.Fields("UNIT") = Val(TxtUNIT.Text)
       rs.Fields("LCode") = TxtLCode.Text
       rs.Fields("SAFE_QTY") = Val(TxtSafe_Qty.Text)
       rs.Fields("USYSTEM") = TxtUSystem.Text
       rs.Fields("REMARK") = TxtRemark.Text
       rs.Fields("USER_ID") = USER_ID
       rs.Fields("CREATE_DATE") = Todayf
       rs.Update
    Else
       rs.Fields("GCode") = TxtGCode.Text
       rs.Fields("Description") = TxtDESC.Text
       rs.Fields("UNIT") = Val(TxtUNIT.Text)
       rs.Fields("LCode") = TxtLCode.Text
       rs.Fields("SAFE_QTY") = Val(TxtSafe_Qty.Text)
       rs.Fields("USYSTEM") = TxtUSystem.Text
       rs.Fields("REMARK") = TxtRemark.Text
       rs.Fields("USER_ID") = USER_ID
       rs.Fields("CREATE_DATE") = Todayf
       rs.Update
      
     'Print rec
      
      
    End If
    rs.Requery
    Call SETDG
    
'  TxtDESC.Enabled = False
'  TxtUNIT.Enabled = False
'  TxtStock_Qty.Enabled = False
'  TxtSafe_Qty.Enabled = False
'  TxtRemark.Enabled = False
  
'  TxtDESC.BackColor = &H80000004
'  TxtUNIT.BackColor = &H80000004
'  TxtStock_Qty.BackColor = &H80000004
'  TxtSafe_Qty.BackColor = &H80000004
'  TxtRemark.BackColor = &H80000004
    
    
    TxtGCode.SetFocus
    Exit Sub
   
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~取消~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdClear_Click()
 FieldClear
 
'  TxtStock_Qty.Enabled = False
'  TxtDESC.Enabled = False
'  TxtUNIT.Enabled = False
'  TxtStock_Qty.Enabled = False
'  TxtSafe_Qty.Enabled = False
'  TxtRemark.Enabled = False
  
'  TxtStock_Qty.BackColor = &H80000004
'  TxtDESC.BackColor = &H80000004         '反白區域
'  TxtUNIT.BackColor = &H80000004
'  TxtStock_Qty.BackColor = &H80000004
'  TxtSafe_Qty.BackColor = &H80000004
'  TxtRemark.BackColor = &H80000004
  
'    CmdClear.Enabled = False
'    CmdDel.Enabled = False
'    CmdYes.Enabled = False
'    CmdQuery.Enabled = True
'    CmdNew.Enabled = True
'    CmdEXIT.Enabled = True
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~離開~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExit_Click()
Unload Me
End Sub

Private Sub Form_Unload(CANCEL As Integer)
Set DataGrid1.DataSource = Nothing
'conn.Close
End Sub
Public Sub FieldToGrid()
  rs.Fields("GCode") = "" & TxtGCode
  rs.Fields("Description") = "" & TxtDESC
  rs.Fields("UNIT") = "" & TxtUNIT
  rs.Fields("LCode") = "" & TxtLCode
  rs.Fields("STOCK_QTY") = "" & TxtStock_Qty
  rs.Fields("SAFE_QTY") = "" & TxtSafe_Qty
  rs.Fields("USYSTEM") = "" & TxtUSystem
  rs.Fields("REMARK") = "" & TxtRemark
 
  
'   Call SETDG
End Sub

Public Sub GridToField()
  Call FieldClear
  
  If rs.RecordCount > 0 Then
  
  TxtGCode = "" & rs.Fields("GCode")
  TxtDESC = "" & rs.Fields("Description")
  TxtUNIT = "" & rs.Fields("UNIT")
  TxtLCode = "" & rs.Fields("LCode")
  TxtStock_Qty = "" & rs.Fields("STOCK_QTY")
  TxtSafe_Qty = "" & rs.Fields("SAFE_QTY")
  TxtUSystem = "" & rs.Fields("USYSTEM")
  TxtRemark = "" & rs.Fields("REMARK")
  
  End If
 ' TxtSum.Text = "" & rs.Fields("SNum")
 '  Call SETDG
' Toolbar2.Buttons("del").Enabled = True
 'Cmddel.Enabled = True
 
' '------Gauss
'新增
' Toolbar2.Buttons("add").Enabled = False
' Toolbar2.Buttons("save").Enabled = True
' Toolbar2.Buttons("cancel").Enabled = True
' Toolbar2.Buttons("search").Enabled = False
' Toolbar2.Buttons("del").Enabled = True
' Toolbar2.Buttons("exit").Enabled = True
'瀏覽
' Toolbar2.Buttons("add").Enabled = True
' Toolbar2.Buttons("save").Enabled = False
' Toolbar2.Buttons("cancel").Enabled = True
' Toolbar2.Buttons("search").Enabled = True
' Toolbar2.Buttons("del").Enabled = False
' Toolbar2.Buttons("exit").Enabled = True


'-----------------------------------------------

' Toolbar2.Buttons("add").Enabled = False
' Toolbar2.Buttons("save").Enabled = True
' Toolbar2.Buttons("cancel").Enabled = True
' Toolbar2.Buttons("search").Enabled = False
' Toolbar2.Buttons("del").Enabled = False
' Toolbar2.Buttons("exit").Enabled = False
 
End Sub

Public Sub FieldClear()
  TxtGCode = ""
  TxtDESC = ""
  TxtUNIT = ""
  TxtLCode = ""
  TxtStock_Qty = ""
  TxtSafe_Qty = ""
  TxtUSystem = ""
  TxtRemark = ""

End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
Private Sub DataGrid1_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
   'If NoMove Then Exit Sub
   Call GridToField
End Sub

Private Sub CobPURPOSE_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Public Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

Select Case Button.Key

   Case Is = "add"
             flag = 1
             TxtStyle = "新  增"
             'Call FieldClear
             TxtGCode.Locked = False
             TxtGCode.SetFocus
             
             Call 新增_Bar
             
'            TxtDESC.Enabled = True
'            TxtUNIT.Enabled = True
'            TxtSafe_Qty.Enabled = True
'            TxtRemark.Enabled = True
            
'            TxtDESC.BackColor = &HFFFFFF          '空白區域
'            TxtUNIT.BackColor = &HFFFFFF
            
           ' TxtStock_Qty.BackColor = &HFFFFFF
           
'            TxtSafe_Qty.BackColor = &HFFFFFF
'            TxtRemark.BackColor = &HFFFFFF
            
'            Toolbar2.Buttons("add").Enabled = False
'            Toolbar2.Buttons("save").Enabled = True
'            Toolbar2.Buttons("cancel").Enabled = True
'            Toolbar2.Buttons("search").Enabled = False
'            Toolbar2.Buttons("del").Enabled = False
'            Toolbar2.Buttons("exit").Enabled = False


   Case Is = "edit"
             flag = 1
             TxtStyle = "修  改"
             'Call FieldClear
             
             
             TxtGCode.Locked = False
             TxtGCode.SetFocus
             Call 修改_Bar

   Case Is = "save"
               'On Error GoTo ERR1
            
               Select Case TxtStyle
                  Case "新  增":
                     If TxtGCode = "" Or TxtLCode.Text = "" Then    'Or TxtUSystem.Text = "" Or TxtUNIT.Text = ""
                        MsgBox "不允許空值新值，請輸入值!!" & vbCrLf & "輸入 Global Code、Local Code", vbExclamation + vbOKOnly, "系統訊息"
                        TxtGCode.SetFocus
                        Exit Sub
                     End If
             
                     ExecuteSQL
                     If rs.RecordCount > 0 Then
                        MsgBox "資料重複！！不允許新增！！"
                        TxtGCode.SetFocus
                        Exit Sub
                     End If
                  Case "修  改":
                     If TxtGCode = "" Or TxtLCode.Text = "" Then    'Or TxtUSystem.Text = "" Or TxtUNIT.Text = ""
                        MsgBox "請搜尋相關資料予以修改", vbExclamation + vbOKOnly, "系統訊息"
                        Exit Sub
                     End If
                     'If rs.RecordCount <> 1 Then
                     '   MsgBox "無資料！！不允許修改！！"
                        'TxtGCode.SetFocus
                     '   Call FieldClear
                     '   Exit Sub
                     'Else
                     '   If MsgBox("已有相同資料，是否修改？", vbYesNo) = vbNo Then
                     '      Exit Sub
                     '   End If
                     'End If
               End Select
               
               If rs.State Then rs.Close
               
               Sql = "SELECT * FROM MATERMST WHERE GCode = '" & TxtGCode & "'  "
               rs.Open Sql, conn, adOpenStatic, adLockOptimistic
               If rs.RecordCount = 0 Then
                  'Sql = "INSERT INTO MATERMST(GCode,SNum,Description,UNIT,LCode,STOCK_QTY,SAFE_QTY,USYSTEM,REMARK,USER_ID,CREATE_DATE) " & _
                        "VALUES ('" & TxtGCODE & "','" & TXTSUM.Text & "','" & TxtDESC & "'," & TxtUNIT & ",'" & TxtLCODE & "'," & TxtStock_Qty & ", " & _
                        "" & TxtSafe_Qty & ",'" & USYSTEM & "','" & TxtRemark & "','" & USER_ID & "','" & Todayf & "')"
                  'conn.Execute Sql
                   rs.AddNew
                   rs.Fields("GCode") = TxtGCode.Text
                   rs.Fields("Description") = TxtDESC.Text
                   rs.Fields("UNIT") = Val(TxtUNIT.Text)
                   rs.Fields("LCode") = TxtLCode.Text
                   rs.Fields("SAFE_QTY") = Val(TxtSafe_Qty.Text)
                   rs.Fields("USYSTEM") = TxtUSystem.Text
                   rs.Fields("REMARK") = TxtRemark.Text
                   rs.Fields("USER_ID") = USER_ID
                   rs.Fields("CREATE_DATE") = Todayf
                   rs.Update
                Else
                   rs.Fields("GCode") = TxtGCode.Text
                   rs.Fields("Description") = TxtDESC.Text
                   rs.Fields("UNIT") = Val(TxtUNIT.Text)
                   rs.Fields("LCode") = TxtLCode.Text
                   rs.Fields("SAFE_QTY") = Val(TxtSafe_Qty.Text)
                   rs.Fields("USYSTEM") = TxtUSystem.Text
                   rs.Fields("REMARK") = TxtRemark.Text
                   rs.Fields("USER_ID") = USER_ID
                   rs.Fields("CREATE_DATE") = Todayf
                   rs.Update

                End If
                rs.Requery
                Call SETDG
                
'              TxtDESC.Enabled = False
'              TxtUNIT.Enabled = False
'              TxtStock_Qty.Enabled = False
'              TxtSafe_Qty.Enabled = False
'              TxtRemark.Enabled = False
              
'              TxtDESC.BackColor = &H80000004
'              TxtUNIT.BackColor = &H80000004
'              TxtStock_Qty.BackColor = &H80000004
'              TxtSafe_Qty.BackColor = &H80000004
'              TxtRemark.BackColor = &H80000004
                
                
                儲存_Bar
                MsgBox "資  料  " & TxtStyle & "  成  功  ！", vbExclamation + vbOKOnly, "系統訊息"
                
                Exit Sub
                
     Case Is = "cancel"
                FieldClear
'                TxtStock_Qty.Enabled = False
'                TxtDESC.Enabled = False
'                TxtUNIT.Enabled = False
'                TxtStock_Qty.Enabled = False
'                TxtSafe_Qty.Enabled = False
'                TxtRemark.Enabled = False
                
'                TxtStock_Qty.BackColor = &H80000004
'                TxtDESC.BackColor = &H80000004         '反白區域
'                TxtUNIT.BackColor = &H80000004
'                TxtStock_Qty.BackColor = &H80000004
'                TxtSafe_Qty.BackColor = &H80000004
'                TxtRemark.BackColor = &H80000004
                
'                Toolbar2.Buttons("add").Enabled = True
'                Toolbar2.Buttons("save").Enabled = False
'                Toolbar2.Buttons("cancel").Enabled = False
'                Toolbar2.Buttons("search").Enabled = True
'                Toolbar2.Buttons("del").Enabled = False
'                Toolbar2.Buttons("exit").Enabled = True
                
                '-----Gauss
                Call 瀏覽_Bar
'                Call FieldClear
                ExecuteSQL
'                Toolbar2.Buttons("add").Enabled = True
'                Toolbar2.Buttons("save").Enabled = False
'                Toolbar2.Buttons("cancel").Enabled = False
'                Toolbar2.Buttons("search").Enabled = True
'                Toolbar2.Buttons("del").Enabled = False
'                Toolbar2.Buttons("exit").Enabled = True
                Call FieldClear
                TxtStyle = "瀏  覽"
                '-----------
                
     Case Is = "search"
               TxtStyle = "查  詢"
               '----Gauss
               'FieldClear
               'Toolbar2.Buttons("search").Enabled = False
               '--------
               ExecuteSQL

    Case Is = "del"
               On Error Resume Next
               If TxtGCode.Text = "" And TxtSum.Text = "" Then
                  MsgBox "請搜尋相關資料予以刪除！", vbExclamation + vbOKOnly, "系統訊息"
                  Exit Sub
               End If
               
               If rsTmp.State Then rsTmp.Close
               If TxtSum.Text = "" Then
                  Sql = "select * from STOCKMST WHERE GCode ='" & TxtGCode & "' "
               Else
                  Sql = "select * from STOCKMST WHERE GCode ='" & TxtGCode & "'  "
               End If
               rsTmp.Open Sql, conn, adOpenKeyset, adLockPessimistic
                
               If Not rsTmp.EOF Then
               If rsTmp.RecordCount > 0 Then
                  MsgBox "尚有庫存無法刪除!!", vbExclamation + vbOKOnly, "系統訊息"
                  Exit Sub
               End If
               End If
            
               If MsgBox("是否確定刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
                  Exit Sub
               End If
               If TxtSum.Text = "" Then
                  Sql = "Delete from MATERMST where GCode='" & TxtGCode & "' "
               Else
                  Sql = "Delete from MATERMST where GCode='" & TxtGCode & "' "
               End If
               conn.Execute Sql
               FieldClear
               rs.Requery
               Call SETDG
    Case Is = "exit"
             Unload Me
End Select
End Sub

Private Sub Toolbar3_ButtonClick(ByVal Button As MSComctlLib.Button)

End Sub

Private Sub TxtLCODE_KeyPress(KeyAscii As Integer)
  If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Private Sub TxtGCode_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Private Sub TxtDESC_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Private Sub TxtUNIT_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Public Sub SETDG()
   Label9.Caption = "筆數：" & rs.RecordCount & " 筆"
   Set DataGrid1.DataSource = rs
   
   DataGrid1.Columns(0).Caption = "Global Code"
   DataGrid1.Columns(4).Caption = "Stock"
   DataGrid1.Columns(5).Caption = "Safe"
   DataGrid1.Columns(5).Alignment = dbgRight
   DataGrid1.Columns(6).Alignment = dbgRight
   DataGrid1.Columns(7).Alignment = dbgRight
   
   DataGrid1.Columns(0).Width = 2350
   DataGrid1.Columns(1).Width = 1200
   DataGrid1.Columns(2).Width = 2600
   DataGrid1.Columns(3).Width = 700
   DataGrid1.Columns(4).Width = 700
   DataGrid1.Columns(5).Width = 700
   DataGrid1.Columns(6).Width = 1000
   DataGrid1.Columns(7).Width = 1000
   
   DataGrid1.Columns(0).Locked = True
   DataGrid1.Columns(1).Locked = True
   DataGrid1.Columns(2).Locked = True
   DataGrid1.Columns(3).Locked = True
   DataGrid1.Columns(4).Locked = True
   DataGrid1.Columns(5).Locked = True
   DataGrid1.Columns(6).Locked = True
   DataGrid1.Columns(7).Locked = True
End Sub

Private Sub ExecuteSQL()
  If rs.State Then rs.Close
  'On Error Resume Next
  Sql = "select GCode,LCode,Description,Unit,Stock_QTY ,Safe_Qty ,USystem,Remark from MATERMST where "
  
  sql_1 = ""
  
  If TxtGCode.Text <> "" Then
     sql_1 = sql_1 & "and  GCode LIKE '%" & TxtGCode.Text & "%' "
  End If

  If TxtDESC.Text <> "" Then
     sql_1 = sql_1 & "and Description like '%" & TxtDESC.Text & "%' "
  End If
  
  If TxtUSystem.Text <> "" Then
      sql_1 = sql_1 & "and USystem like '%" & TxtUSystem.Text & "%' "
  End If
 
  If TxtLCode.Text <> "" Then
     sql_1 = sql_1 & "and LCode like '%" & TxtLCode.Text & "%' "
  End If
  
  If sql_1 <> "" Then
     sql_r = sql_1
     sql_r = Right(sql_r, Len(sql_r) - 4)
  End If
  
  If sql_r <> "" Then
     Sql = Sql & sql_r
  Else
    Sql = Left(Sql, Len(Sql) - 5)
  End If
  Sql = Sql & " order by GCode "
  
  'Sql = " Select GCODE ,LCODE,Mate_NAME,UNIT,STOCK_QTY,SAFE_QTY,USYSTEM,REMARK,create_date,USER_ID" & _
        " from MATERMST where GCODE LIKE '" & TxtGCODE & "%' AND Mate_NAME LIKE '" & TxtDESC & "%'" & _
        " AND LCODE LIKE '" & TxtLCODE & "%' AND UNIT LIKE '" & TxtUNIT & "%'" & _
        " AND USYSTEM LIKE '" & TxtUSystem & "%'" & _
        "order by GCODE "
  Debug.Print Sql
  rs.Open Sql, conn, adOpenDynamic, adLockPessimistic

  
  Print rs.RecordCount
  Label9.Caption = "筆數：" & rs.RecordCount & " 筆"
  Set DataGrid1.DataSource = rs
  Call SETDG
'  Toolbar2_ButtonClick (CANCEL)
  'Call FieldClear
End Sub

Public Sub 瀏覽_Bar()
   Call FieldClear
   Toolbar2.Buttons("add").Enabled = True
   Toolbar2.Buttons("edit").Enabled = True
   Toolbar2.Buttons("save").Enabled = False
   Toolbar2.Buttons("cancel").Enabled = True
   Toolbar2.Buttons("search").Enabled = True
   Toolbar2.Buttons("del").Enabled = False
   Toolbar2.Buttons("exit").Enabled = True
   TxtGCode.Enabled = True
   TxtLCode.Enabled = True
End Sub

Public Sub 新增_Bar()
   'Call FieldClear
   Toolbar2.Buttons("add").Enabled = False
   Toolbar2.Buttons("edit").Enabled = False
   Toolbar2.Buttons("save").Enabled = True
   Toolbar2.Buttons("cancel").Enabled = True
   Toolbar2.Buttons("search").Enabled = True
   Toolbar2.Buttons("del").Enabled = True
   Toolbar2.Buttons("exit").Enabled = True
End Sub

Public Sub 修改_Bar()
   'Call FieldClear
   Toolbar2.Buttons("add").Enabled = False
   Toolbar2.Buttons("edit").Enabled = False
   Toolbar2.Buttons("save").Enabled = True
   Toolbar2.Buttons("cancel").Enabled = True
   Toolbar2.Buttons("search").Enabled = True
   Toolbar2.Buttons("del").Enabled = True
   Toolbar2.Buttons("exit").Enabled = True
   
   TxtGCode.Enabled = False
   TxtLCode.Enabled = False
End Sub

Public Sub 查詢_Bar()
   Toolbar2.Buttons("search").Enabled = False
End Sub

Public Sub 儲存_Bar()
   Toolbar2.Buttons("save").Enabled = False
   Toolbar2.Buttons("del").Enabled = False
End Sub

