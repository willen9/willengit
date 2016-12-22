VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmAbnormal_S 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "轉檔異常查詢"
   ClientHeight    =   7890
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11790
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7890
   ScaleWidth      =   11790
   WhatsThisHelp   =   -1  'True
   Begin VB.Frame Frame2 
      Height          =   1935
      Left            =   0
      TabIndex        =   12
      Top             =   840
      Width           =   9975
      Begin VB.TextBox TxtCustno 
         Height          =   375
         Left            =   5520
         MaxLength       =   6
         TabIndex        =   25
         Top             =   840
         Width           =   1335
      End
      Begin VB.TextBox TxtLocation 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Index           =   1
         Left            =   3240
         MaxLength       =   5
         TabIndex        =   23
         Top             =   1320
         Width           =   1215
      End
      Begin VB.TextBox TxtLocation 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Index           =   0
         Left            =   1440
         MaxLength       =   5
         TabIndex        =   22
         Top             =   1320
         Width           =   1215
      End
      Begin VB.TextBox TxtDate 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   16
         Top             =   840
         Width           =   1215
      End
      Begin VB.TextBox TxtDate 
         BackColor       =   &H00FFFFFF&
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Index           =   1
         Left            =   3240
         MaxLength       =   15
         TabIndex        =   15
         Top             =   840
         Width           =   1215
      End
      Begin VB.TextBox TxtGCode 
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
         Height          =   390
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   14
         Top             =   285
         Width           =   1935
      End
      Begin VB.TextBox TxtGCode 
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
         Height          =   390
         Index           =   1
         Left            =   3960
         MaxLength       =   15
         TabIndex        =   13
         Top             =   285
         Width           =   1935
      End
      Begin VB.Label Label8 
         BackColor       =   &H80000004&
         Caption         =   "客編:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   4680
         TabIndex        =   26
         Top             =   840
         Width           =   735
      End
      Begin VB.Label Label7 
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
         Left            =   7560
         TabIndex        =   24
         Top             =   360
         Width           =   2175
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         Index           =   2
         X1              =   2760
         X2              =   3120
         Y1              =   1440
         Y2              =   1440
      End
      Begin VB.Label Label6 
         BackColor       =   &H80000004&
         Caption         =   "Location:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   360
         TabIndex        =   21
         Top             =   1320
         Width           =   1095
      End
      Begin VB.Label Label3 
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
         Left            =   7560
         TabIndex        =   20
         Top             =   1440
         Width           =   2295
      End
      Begin VB.Label Label5 
         BackColor       =   &H80000004&
         Caption         =   "Global Code:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   120
         TabIndex        =   19
         Top             =   360
         Width           =   1335
      End
      Begin VB.Label Label4 
         BackColor       =   &H80000004&
         Caption         =   "日期:"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   720
         TabIndex        =   18
         Top             =   840
         Width           =   1095
      End
      Begin VB.Label Label2 
         Alignment       =   2  '置中對齊
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   1320
         TabIndex        =   17
         Top             =   2040
         Width           =   4815
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         Index           =   1
         X1              =   2760
         X2              =   3120
         Y1              =   960
         Y2              =   960
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         Index           =   0
         X1              =   3480
         X2              =   3840
         Y1              =   480
         Y2              =   480
      End
   End
   Begin VB.Frame Frame3 
      Height          =   1935
      Left            =   10080
      TabIndex        =   7
      Top             =   840
      Width           =   1695
      Begin VB.OptionButton Option1 
         Caption         =   "進貨"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   0
         Left            =   360
         TabIndex        =   11
         Top             =   360
         Width           =   1095
      End
      Begin VB.OptionButton Option1 
         Caption         =   "出貨"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   1
         Left            =   360
         TabIndex        =   10
         Top             =   720
         Width           =   1095
      End
      Begin VB.OptionButton Option1 
         Caption         =   "退回"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   2
         Left            =   360
         TabIndex        =   9
         Top             =   1080
         Width           =   975
      End
      Begin VB.OptionButton Option1 
         Caption         =   "全部"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Index           =   3
         Left            =   360
         TabIndex        =   8
         Top             =   1440
         Value           =   -1  'True
         Width           =   1095
      End
   End
   Begin MSDataGridLib.DataGrid DataGrid2 
      Height          =   4935
      Left            =   0
      TabIndex        =   5
      TabStop         =   0   'False
      Top             =   2880
      Width           =   11775
      _ExtentX        =   20770
      _ExtentY        =   8705
      _Version        =   393216
      AllowUpdate     =   0   'False
      BackColor       =   16777215
      Enabled         =   -1  'True
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
         EndProperty
         BeginProperty Column01 
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frm1 
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  '沒有框線
      Height          =   6255
      Left            =   11760
      TabIndex        =   0
      Top             =   5400
      Visible         =   0   'False
      Width           =   2415
      Begin VB.ComboBox Cob查詢 
         Height          =   300
         Left            =   360
         TabIndex        =   2
         TabStop         =   0   'False
         Top             =   600
         Width           =   1695
      End
      Begin VB.TextBox Txt內容 
         Height          =   270
         Left            =   360
         TabIndex        =   1
         TabStop         =   0   'False
         Top             =   960
         Width           =   1695
      End
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   4695
         Index           =   0
         Left            =   360
         TabIndex        =   3
         Top             =   1320
         Width           =   1695
         _ExtentX        =   2990
         _ExtentY        =   8281
         _Version        =   393216
         AllowUpdate     =   0   'False
         HeadLines       =   1
         RowHeight       =   15
         BeginProperty HeadFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "新細明體"
            Size            =   9
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
            Name            =   "新細明體"
            Size            =   9
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
               ColumnWidth     =   0
            EndProperty
            BeginProperty Column01 
               ColumnWidth     =   0
            EndProperty
         EndProperty
      End
      Begin VB.Label Label1 
         BackColor       =   &H00E0E0E0&
         Caption         =   "快速查詢(Q):"
         Height          =   375
         Left            =   360
         TabIndex        =   4
         Top             =   360
         Width           =   1575
      End
      Begin VB.Shape Shape4 
         Height          =   5895
         Left            =   240
         Top             =   240
         Width           =   1935
      End
      Begin VB.Shape Shape2 
         BorderColor     =   &H80000005&
         BorderWidth     =   2
         Height          =   5895
         Left            =   240
         Top             =   240
         Width           =   1935
      End
      Begin VB.Shape Shape1 
         BorderColor     =   &H00C0C0C0&
         BorderWidth     =   5
         Height          =   5895
         Left            =   2520
         Top             =   5880
         Width           =   1935
      End
      Begin VB.Shape Shape3 
         BackColor       =   &H8000000A&
         BorderColor     =   &H00FFFFFF&
         BorderStyle     =   0  '透明
         BorderWidth     =   3
         FillColor       =   &H00E0E0E0&
         FillStyle       =   0  '實心
         Height          =   5895
         Left            =   240
         Top             =   240
         Width           =   1935
      End
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   825
      Left            =   0
      TabIndex        =   6
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
            Caption         =   "修改"
            Key             =   "edit"
            ImageIndex      =   3
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "儲存"
            Key             =   "save"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "取消"
            Key             =   "cancel"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "查詢"
            Key             =   "search"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "匯入"
            Key             =   "sort"
            ImageIndex      =   7
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
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   120
      Top             =   6240
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
            Picture         =   "FrmAbnormal_R.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":1052
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":13CD
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":1CA7
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":1FFC
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":2587
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":28A1
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":38F3
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":3C60
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":4CB2
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":5D04
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":601E
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":6338
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":738A
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":76A4
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":79F6
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_R.frx":7D48
            Key             =   ""
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmAbnormal_S"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Public Sub GridToField()
  Call FieldClear
  
  If rs.State = 1 Then
     If rs.RecordCount > 0 Then
  
     TxtGCODE(0) = rs.Fields("GCode")
     TxtDate(0) = rs.Fields("TRANS_DATE")
     TxtLocation(0) = rs.Fields("Location")
     TxtCustno = rs.Fields("Custno")
     End If
  End If
  
  Toolbar1.Buttons("edit").Enabled = True
  Toolbar1.Buttons("save").Enabled = False
  Toolbar1.Buttons("del").Enabled = False
  Toolbar1.Buttons("search").Enabled = False
  Toolbar1.Buttons("sort").Enabled = False

End Sub

Private Sub DataGrid2_Click()

'   Dim Button1 As MSComctlLib.Button
'
'   Button1 = "修改"
'   MsgBox Button1
'   Button1.Key = "edit"
'   Toolbar1_ButtonClick (Button1)
   
      TxtGCODE(1).Visible = False
      TxtDate(1).Visible = False
      TxtLocation(1).Visible = False
      
      Label8.Visible = True
      TxtCustno.Visible = True
      
      TxtGCODE(0).Enabled = False
      TxtDate(0).Enabled = False
      
      For i = 0 To 2
         Line1(i).Visible = False
      Next i
   
   GridToField
End Sub

Private Sub Form_Load()

   Toolbar1.Buttons("edit").Enabled = True
   Toolbar1.Buttons("save").Enabled = False
   Toolbar1.Buttons("cancel").Enabled = True
   Toolbar1.Buttons("search").Enabled = True
   Toolbar1.Buttons("del").Enabled = False
   Toolbar1.Buttons("exit").Enabled = True

    Label8.Visible = False
    TxtCustno.Visible = False
    
  FormsAttribute
  ExecuteSQL
  
  Set DataGrid2.DataSource = rs
  Label3.Caption = "筆數：" & rs.RecordCount & " 筆"
  If rs.RecordCount > 0 Then
     Set DataGrid2.DataSource = rs
     Toolbar1.Buttons("edit").Enabled = True
  End If
  Label7.Caption = "查詢模式"
End Sub

Public Sub FieldClear()

  TxtGCODE(0) = ""
  TxtGCODE(1) = ""
  TxtDate(0) = ""
  TxtDate(1) = ""
  TxtLocation(0) = ""
  TxtLocation(1) = ""
  TxtCustno = ""

End Sub


Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
'DoEvents
Select Case Button.Key
    Case Is = "edit"
       TxtGCODE(1).Visible = False
       TxtDate(1).Visible = False
       TxtLocation(1).Visible = False
       Label8.Visible = True
       TxtCustno.Visible = True
       
       TxtGCODE(0).Enabled = False
       TxtDate(0).Enabled = False
       
       For i = 0 To 2
          Line1(i).Visible = False
       Next i
       Toolbar1.Buttons("edit").Enabled = False
       Toolbar1.Buttons("save").Enabled = True
       Toolbar1.Buttons("del").Enabled = True
       Toolbar1.Buttons("search").Enabled = False
       Toolbar1.Buttons("sort").Enabled = False
       Label7.Caption = "編輯模式"
    Case Is = "save"
       
       If rs.State = 1 And TxtLocation(0).Text <> "" Then
         If rs.RecordCount = 0 Then
            MsgBox "無資料不可更新"
         Else
            rs.Fields("Location") = TxtLocation(0).Text
            rs.Fields("Custno") = TxtCustno.Text
            rs.Update
         End If
         rs.Requery
       End If
       
       TxtGCODE(1).Visible = True
       TxtDate(1).Visible = True
       TxtLocation(1).Visible = True
       Label8.Visible = False
       TxtCustno.Visible = False
       
       TxtGCODE(0).Enabled = True
       TxtDate(0).Enabled = True
       
       For i = 0 To 2
          Line1(i).Visible = True
       Next i
       
       FieldClear
       Toolbar1.Buttons("edit").Enabled = True
       Toolbar1.Buttons("save").Enabled = False
       Toolbar1.Buttons("del").Enabled = False
       Toolbar1.Buttons("search").Enabled = True
       Toolbar1.Buttons("sort").Enabled = True

       Label7.Caption = "查詢模式"
                
    Case Is = "cancel"
       TxtGCODE(1).Visible = True
       TxtDate(1).Visible = True
       TxtLocation(1).Visible = True
       Label8.Visible = False
       TxtCustno.Visible = False
       
       TxtGCODE(0).Enabled = True
       TxtDate(0).Enabled = True
       
       For i = 0 To 2
          Line1(i).Visible = True
       Next i
    
       FieldClear
       Set DataGrid2.DataSource = Nothing
       
       Toolbar1.Buttons("edit").Enabled = True
       Toolbar1.Buttons("save").Enabled = False
       Toolbar1.Buttons("del").Enabled = False
       Toolbar1.Buttons("search").Enabled = True
       Toolbar1.Buttons("sort").Enabled = True
       
       Label7.Caption = "查詢模式"
       Form_Load
       
    Case Is = "search"
       ExecuteSQL
       Label3.Caption = "筆數：" & rs.RecordCount & " 筆"
       If rs.RecordCount > 0 Then
          Set DataGrid2.DataSource = rs
          Toolbar1.Buttons("edit").Enabled = True
          'DR_1.Sections("section2").Controls("Lab_Dt").Caption = Date
          'DataGrid2.Show
       End If
       Toolbar1.Buttons("search").Enabled = False
       Toolbar1.Buttons("sort").Enabled = False
       Label7.Caption = "查詢模式"

     Case Is = "sort"
         TxtDate(0) = ""
         TxtDate(1) = ""
         Option1(3).Value = True
         ExecuteSQL
         
         Label3.Caption = ""
         Label3.Caption = "搜尋筆數：" & rs.RecordCount & " 筆"
         If rs.RecordCount > 0 Then
            Set DataGrid2.DataSource = rs
         End If
         
         If MsgBox("將異常資料將再次轉入!!", vbExclamation + vbYesNo, "系統訊息") = vbYes Then
            Data_Sort
         End If
         Form_Load
         
    Case Is = "del"
    
       If TxtGCODE(0).Text = "" Then
          MsgBox "請搜尋相關資料予以刪除！", vbExclamation + vbOKOnly, "系統訊息"
          Exit Sub
       End If
               
       If MsgBox("是否確定刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
          Exit Sub
       End If
       Sql = "Delete from ERRDATA where GCode='" & TxtGCODE(0) & "' and TRANS_DATE ='" & TxtDate(0) & "' " & _
            "and Location='" & TxtLocation(0) & "' and Custno='" & TxtCustno & "'"
       conn.Execute Sql
       FieldClear
       rs.Requery
    
       TxtGCODE(1).Visible = True
       TxtDate(1).Visible = True
       TxtLocation(1).Visible = True
       Label8.Visible = False
       TxtCustno.Visible = False
       
       TxtGCODE(0).Enabled = True
       TxtDate(0).Enabled = True
       
       For i = 0 To 2
          Line1(i).Visible = True
       Next i
    
       FieldClear
       Toolbar1.Buttons("edit").Enabled = True
       Toolbar1.Buttons("save").Enabled = False
       Toolbar1.Buttons("del").Enabled = False
       Toolbar1.Buttons("search").Enabled = True
       Toolbar1.Buttons("sort").Enabled = True
       Label7.Caption = "查詢模式"
       
    Case Is = "exit"
             Unload Me
End Select
End Sub

Private Sub ExecuteSQL()

  If TxtGCODE(1) = "" Then TxtGCODE(1).Text = TxtGCODE(0).Text
  If TxtDate(1) = "" Then TxtDate(1).Text = TxtDate(0).Text
 
  Dim STATUS As String

  If Option1(0).Value = True Then STATUS = "進貨"
  If Option1(1).Value = True Then STATUS = "出貨"
  If Option1(2).Value = True Then STATUS = "退回"
  If Option1(3).Value = True Then STATUS = ""
   
  If rs.State Then rs.Close
        
  Sql = "SELECT *" & _
        " FROM ERRDATA AS ERR" & _
        " WHERE  STATUS LIKE '" & STATUS & "%' "

  sql_1 = ""
  
  If TxtGCODE(0) <> "" Then
     sql_1 = sql_1 & "And GCODE >= '" & TxtGCODE(0) & "' AND GCODE <= '" & TxtGCODE(1) & "' "
  End If
  If TxtDate(0) <> "" Then
     sql_1 = sql_1 & "And TRANS_DATE >= '" & TxtDate(0) & "' AND TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  If TxtLocation(0) <> "" Then
     sql_1 = sql_1 & "And Location >= '" & TxtLocation(0) & "' AND Location <= '" & TxtLocation(1) & "' "
  End If

  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If

'  Sql = Sql & " GROUP by GCode"
  Debug.Print Sql
  
  rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
 
End Sub

Private Sub Data_Sort()

   If rs.State Then rs.Close
   
      Sql = "SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%'"
      Debug.Print Sql
  
      rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
  
      If rs.RecordCount <> 0 Then rs.MoveFirst
      
      While Not rs.EOF
      
'         For i = 0 To rs.Fields.Count - 1
'            Debug.Print i; rs.Fields(i)
'         Next i
         
         Select Case rs.Fields(4)
            Case "進貨":
               Sql = "Update STOCKMST Set IN_QTY=IN_QTY+ " & Val(rs.Fields(2)) & " , REAL_QTY=LAST_QTY + (IN_QTY+ " & Val(rs.Fields(2)) & ") - OUT_QTY  where gcode='" & rs.Fields(0) & "' AND LOCATION='" & rs.Fields(1) & "' "
'              Sql = "Update STOCKMST Set IN_QTY=IN_QTY+ " &       Val(FLD(4)) & " , REAL_QTY=LAST_QTY + IN_QTY - (IN_QTY+ " &       Val(FLD(4)) & ")  where gcode='" &       FLD(2) & "' AND LOCATION='" &       FLD(1) & "' "
               Debug.Print Sql
               conn.Execute Sql, RAC
               If RAC = 0 Then
                  Sql = "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) " & _
                        "VALUES ('" & rs.Fields(0) & "' ,'" & rs.Fields(1) & "','" & Val(rs.Fields(2)) & "','" & rs.Fields(3) & "','" & rs.Fields(5) & "')"
                  conn.Execute Sql
               End If
                  Sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                  conn.Execute Sql, RAC
                  rs.Delete
            
            
            Case "出貨":
                Sql = "Update STOCKMST Set OUT_QTY = OUT_QTY+" & Val(rs.Fields(2)) & ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " & Val(rs.Fields(2)) & ") where GCODE='" & rs.Fields(0) & "'  and LOCATION = '" & rs.Fields(1) & "' "
               'Sql = "Update STOCKMST Set OUT_QTY = OUT_QTY+" & Val(FLD(4))       & ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " & Val(FLD(4))       & ") where GCODE='" & FLD(2)       & "'  and LOCATION = '" & FLD(1) & "' "
               conn.Execute Sql, RAC

               If RAC <> 0 Then
                  Sql = "Update MATERMST Set Stock_QTY=Stock_QTY - " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                 'Sql = "Update MATERMST Set Stock_QTY=Stock_QTY - " & Val(FLD(4))       & " Where gcode='" & FLD(2) & "'"
                  conn.Execute Sql, RAC
                  rs.Delete
               End If
            
            Case "退貨":
               Sql = "Update STOCKMST Set OUT_QTY=OUT_QTY - " & Val(rs.Fields(2)) & " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " & Val(rs.Fields(2)) & ")  where gcode='" & rs.Fields(0) & "' AND LOCATION='" & rs.Fields(1) & "' "
              'Sql = "Update STOCKMST Set OUT_QTY=OUT_QTY - " & Val(FLD(4)) & " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " & Val(FLD(4)) & ")  where gcode='" & FLD(2) & "' AND LOCATION='" & FLD(1) & "' "
               conn.Execute Sql, RAC
               If RAC <> 0 Then
                  Sql = "Update MATERMST Set Stock_QTY=Stock_QTY + " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                 'Sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(FLD(4))        & " Where gcode='" & FLD(2) & "'"
                  conn.Execute Sql, RAC
                  rs.Delete
               End If
            
         End Select
        
         rs.MoveNext
      
      Wend
   
    
End Sub
