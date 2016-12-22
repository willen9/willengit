VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmAbnormal_R 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "轉檔異常查詢"
   ClientHeight    =   7875
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11835
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7875
   ScaleWidth      =   11835
   WhatsThisHelp   =   -1  'True
   Begin MSDataGridLib.DataGrid DataGrid2 
      Height          =   5295
      Left            =   0
      TabIndex        =   7
      Top             =   2520
      Width           =   11775
      _ExtentX        =   20770
      _ExtentY        =   9340
      _Version        =   393216
      AllowUpdate     =   0   'False
      ForeColor       =   16711680
      HeadLines       =   1.5
      RowHeight       =   19
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
   Begin VB.Frame Frame2 
      Height          =   1575
      Left            =   0
      TabIndex        =   13
      Top             =   840
      Width           =   11775
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
         Left            =   1440
         TabIndex        =   4
         Top             =   840
         Width           =   975
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
         Left            =   2400
         TabIndex        =   5
         Top             =   840
         Width           =   975
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
         Left            =   3360
         TabIndex        =   6
         Top             =   840
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
         TabIndex        =   3
         Top             =   840
         Value           =   -1  'True
         Width           =   1095
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
         Left            =   1200
         MaxLength       =   8
         TabIndex        =   1
         Top             =   240
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
         Left            =   3000
         MaxLength       =   8
         TabIndex        =   2
         Top             =   240
         Width           =   1215
      End
      Begin VB.Label Label4 
         BackColor       =   &H80000004&
         Caption         =   "查詢日期:"
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
         TabIndex        =   16
         Top             =   240
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
         TabIndex        =   15
         Top             =   2040
         Width           =   4815
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         X1              =   2520
         X2              =   2880
         Y1              =   360
         Y2              =   360
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
         Left            =   7440
         TabIndex        =   14
         Top             =   240
         Width           =   3255
      End
   End
   Begin VB.Frame FrmAbnormal_Q 
      BackColor       =   &H00C0C0C0&
      BorderStyle     =   0  '沒有框線
      Height          =   6255
      Left            =   12840
      TabIndex        =   0
      Top             =   6960
      Visible         =   0   'False
      Width           =   2415
      Begin VB.ComboBox Cob查詢 
         Height          =   300
         Left            =   360
         TabIndex        =   10
         TabStop         =   0   'False
         Top             =   600
         Width           =   1695
      End
      Begin VB.TextBox Txt內容 
         Height          =   270
         Left            =   360
         TabIndex        =   9
         TabStop         =   0   'False
         Top             =   960
         Width           =   1695
      End
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   4695
         Index           =   0
         Left            =   360
         TabIndex        =   11
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
         TabIndex        =   12
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
         Left            =   1440
         Top             =   240
         Width           =   1935
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   240
      Top             =   7080
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
            Picture         =   "FrmAbnormal_Q.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":1052
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":13CD
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":1CA7
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":1FFC
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":2587
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":28A1
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":38F3
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":3C60
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":4CB2
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":5D04
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":601E
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":6338
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":738A
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":76A4
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":79F6
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmAbnormal_Q.frx":7D48
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   825
      Left            =   0
      TabIndex        =   8
      Top             =   0
      Width           =   2445
      _ExtentX        =   4313
      _ExtentY        =   1455
      ButtonWidth     =   1032
      ButtonHeight    =   1349
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   5
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "搜尋"
            Key             =   "search"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "列印"
            Key             =   "print"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Enabled         =   0   'False
            Object.Visible         =   0   'False
            Caption         =   "重整"
            Key             =   "sort"
            ImageIndex      =   7
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "取消"
            Key             =   "cancel"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "返回"
            Key             =   "exit"
            ImageIndex      =   13
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmAbnormal_R"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Load()
   FormsAttribute
  ExecuteSQL
  Set DataGrid2.DataSource = rs
  Label3.Caption = "筆數：" & rs.RecordCount & " 筆"
  If rs.RecordCount > 0 Then
     Set DataGrid2.DataSource = rs
  End If
End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
   Select Case Button.Key
      Case Is = "search"
         ExecuteSQL
         Label3.Caption = ""
         Label3.Caption = "搜尋筆數：" & rs.RecordCount & " 筆"
            If rs.RecordCount > 0 Then
            Set DataGrid2.DataSource = rs
         Else
            Label3.Caption = "無符合的資料！"
         End If

      Case Is = "print"
         ExecuteSQL
         Label3.Caption = ""
         Label3.Caption = "搜尋筆數：" & rs.RecordCount & " 筆"
         If rs.RecordCount > 0 Then
         Set DR_4.DataSource = rs
             Set DR_7.DataSource = rs
             DR_7.Sections("section2").Controls("Lab_Dt").Caption = Date
             DR_7.Show
         Else
            Label3.Caption = "無符合的資料！"
         End If
      
      Case Is = "cancel"
         Label3.Caption = ""
         TxtDate(0) = ""
         TxtDate(1) = ""
         Option1(3).Value = True
         Set DataGrid2.DataSource = Nothing
         Form_Load
      Case Is = "exit"
         Unload Me
End Select
End Sub


Private Sub ExecuteSQL()
 
If TxtDate(1) = "" Then TxtDate(1) = TxtDate(0)
 
  Dim STATUS As String

  If Option1(0).Value = True Then STATUS = "進貨"
  If Option1(1).Value = True Then STATUS = "出貨"
  If Option1(2).Value = True Then STATUS = "退回"
  If Option1(3).Value = True Then STATUS = ""
   
  If rs.State Then rs.Close
        
  Sql = "SELECT * " & _
        " FROM ERRDATA " & _
        " WHERE  STATUS LIKE '" & STATUS & "%' "

  sql_1 = ""
  
  If TxtDate(0).Text <> "" Then
     sql_1 = sql_1 & "And TRANS_DATE >= '" & TxtDate(0) & "' AND TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  'ql = sql & " GROUP by GCode"
  Debug.Print Sql
  
  rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
  
   
End Sub



