VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmTransation_Q 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "異動紀錄查詢"
   ClientHeight    =   8610
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11850
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   8610
   ScaleWidth      =   11850
   WhatsThisHelp   =   -1  'True
   Begin VB.Frame Frame2 
      Height          =   1815
      Left            =   0
      TabIndex        =   20
      Top             =   840
      Width           =   10215
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
         Height          =   375
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   0
         Top             =   240
         Width           =   2295
      End
      Begin VB.TextBox TxtGCode 
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
         Height          =   375
         Index           =   1
         Left            =   4320
         MaxLength       =   15
         TabIndex        =   1
         Top             =   240
         Width           =   2295
      End
      Begin VB.TextBox TxtLCode 
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
         TabIndex        =   2
         Top             =   720
         Width           =   2295
      End
      Begin VB.TextBox TxtLCode 
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
         Left            =   4320
         MaxLength       =   15
         TabIndex        =   3
         Top             =   720
         Width           =   2295
      End
      Begin VB.TextBox TxtUSystem 
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
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   4
         Top             =   1200
         Width           =   5175
      End
      Begin VB.Label Label4 
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
         TabIndex        =   23
         Top             =   240
         Width           =   1335
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000004&
         Caption         =   "Local Code:"
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
         Left            =   240
         TabIndex        =   22
         Top             =   720
         Width           =   1215
      End
      Begin VB.Line Line2 
         BorderWidth     =   3
         X1              =   3840
         X2              =   4200
         Y1              =   840
         Y2              =   840
      End
      Begin VB.Label Label5 
         BackColor       =   &H80000004&
         Caption         =   "USystem:"
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
         Left            =   480
         TabIndex        =   21
         Top             =   1200
         Width           =   975
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         X1              =   3840
         X2              =   4200
         Y1              =   360
         Y2              =   360
      End
   End
   Begin VB.Frame Frame1 
      Height          =   1815
      Left            =   10200
      TabIndex        =   15
      Top             =   840
      Width           =   1575
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
         Left            =   240
         TabIndex        =   19
         Top             =   240
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
         Left            =   240
         TabIndex        =   18
         Top             =   600
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
         Left            =   240
         TabIndex        =   17
         Top             =   960
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
         Left            =   240
         TabIndex        =   16
         Top             =   1320
         Value           =   -1  'True
         Width           =   1095
      End
   End
   Begin VB.Frame Frm2 
      BackColor       =   &H80000004&
      Height          =   5160
      Left            =   0
      TabIndex        =   5
      Top             =   2760
      Width           =   11775
      Begin MSDataGridLib.DataGrid DataGrid3 
         Height          =   1935
         Left            =   6240
         TabIndex        =   10
         Top             =   3000
         Width           =   5415
         _ExtentX        =   9551
         _ExtentY        =   3413
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
            EndProperty
            BeginProperty Column01 
            EndProperty
         EndProperty
      End
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   1935
         Left            =   120
         TabIndex        =   8
         Top             =   600
         Width           =   11535
         _ExtentX        =   20346
         _ExtentY        =   3413
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
            EndProperty
            BeginProperty Column01 
            EndProperty
         EndProperty
      End
      Begin MSDataGridLib.DataGrid DataGrid2 
         Height          =   1935
         Left            =   120
         TabIndex        =   9
         Top             =   3000
         Width           =   6015
         _ExtentX        =   10610
         _ExtentY        =   3413
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
            EndProperty
            BeginProperty Column01 
            EndProperty
         EndProperty
      End
      Begin VB.Label Label7 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   9840
         TabIndex        =   26
         Top             =   3000
         Width           =   1695
      End
      Begin VB.Label Label6 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   4200
         TabIndex        =   25
         Top             =   3000
         Width           =   1695
      End
      Begin VB.Label Label9 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   9840
         TabIndex        =   24
         Top             =   360
         Width           =   1695
      End
      Begin VB.Label Label3 
         BackColor       =   &H80000004&
         Caption         =   "物料瀏覽"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   240
         Width           =   975
      End
      Begin VB.Label Label2 
         BackColor       =   &H80000004&
         Caption         =   "物料庫存量表"
         BeginProperty Font 
            Name            =   "新細明體"
            Size            =   12
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   255
         Left            =   120
         TabIndex        =   6
         Top             =   2640
         Width           =   1575
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   0
      Top             =   8280
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
            Picture         =   "FrmTransation_Q.frx":0000
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":1052
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":13CD
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":1CA7
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":1FFC
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":2587
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":28A1
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":38F3
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":3C60
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":4CB2
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":5D04
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":601E
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":6338
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":738A
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":76A4
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":79F6
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmTransation_Q.frx":7D48
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   27
      Top             =   0
      Width           =   2415
      _ExtentX        =   4260
      _ExtentY        =   1455
      ButtonWidth     =   1032
      ButtonHeight    =   1349
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   4
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "查詢"
            Key             =   "search"
            ImageIndex      =   5
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "列印"
            Key             =   "print"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "取消"
            Key             =   "cancel"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "返回"
            Key             =   "exit"
            ImageIndex      =   13
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   480
      Left            =   3000
      TabIndex        =   11
      Top             =   120
      Visible         =   0   'False
      Width           =   2535
      _ExtentX        =   4471
      _ExtentY        =   847
      ButtonWidth     =   609
      ButtonHeight    =   741
      Appearance      =   1
      _Version        =   393216
      Begin VB.CommandButton CmdQuery 
         BackColor       =   &H80000004&
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
         Left            =   0
         Picture         =   "FrmTransation_Q.frx":8062
         Style           =   1  '圖片外觀
         TabIndex        =   14
         ToolTipText     =   "查詢"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdPView 
         BackColor       =   &H80000004&
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
         Left            =   1200
         Picture         =   "FrmTransation_Q.frx":8551
         Style           =   1  '圖片外觀
         TabIndex        =   13
         ToolTipText     =   "列印"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdExit 
         BackColor       =   &H80000004&
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
         Left            =   1800
         Picture         =   "FrmTransation_Q.frx":8ACC
         Style           =   1  '圖片外觀
         TabIndex        =   12
         ToolTipText     =   "離開"
         Top             =   0
         Width           =   450
      End
   End
End
Attribute VB_Name = "FrmTransation_Q"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub CmdExit_Click()
  Unload Me
End Sub

Private Sub CmdPView_Click()
            If rs1.RecordCount > 0 Then
                Set DR_2.DataSource = rs1
                DR_2.Sections("section2").Controls("Lab_Dt").Caption = Date
                DR_2.Show
            End If
End Sub

Private Sub cmdQuery_Click()
   ExecuteSQL
End Sub

Private Sub DataGrid1_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
  ExecuteGrid2
  ExecuteGrid3
End Sub

Private Sub DataGrid2_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
  ExecuteGrid3
End Sub

Private Sub Form_Load()
  FormsAttribute
  ExecuteGrid1
  If Not (rs1.EOF Or rs1.BOF) Then
     ExecuteGrid2
     If Not (rs2.EOF Or rs2.BOF) Then ExecuteGrid3
  End If
End Sub

Private Sub ExecuteGrid1()
  ExecuteSQL
End Sub

Private Sub ExecuteGrid2()
 
  If rs2.State Then rs2.Close
  
        Sql = "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S " & _
              "WHERE S.GCODE = '" & rs1.Fields(0) & "' ORDER BY S.LOCATION"
        rs2.Open Sql, conn, adOpenStatic, adLockReadOnly
        
        Set DataGrid2.DataSource = rs2
        Label6.Caption = "筆數：" & rs2.RecordCount & " 筆"
        
     DataGrid2.Columns(3).Caption = "Stock"
     
 
End Sub

Private Sub ExecuteGrid3()
  Dim STATUS As String
  
  If Option1(0).Value = True Then STATUS = "進貨"
  If Option1(1).Value = True Then STATUS = "出貨"
  If Option1(2).Value = True Then STATUS = "退回"
  If Option1(3).Value = True Then STATUS = ""
  
  If Not (rs1.EOF Or rs1.BOF) And Not (rs2.EOF Or rs2.BOF) Then
        If rs3.State Then rs3.Close
        
        Sql = "SELECT T.TRANS_DATE  ,T.LOCATION AS LOC,T.QTY,T.STATUS FROM TRANSDTL AS T " & _
              "WHERE T.GCODE = '" & rs1.Fields(0) & "' AND T.LOCATION = '" & rs2.Fields(0) & "' AND T.STATUS LIKE '" & STATUS & "%' ORDER BY T.TRANS_DATE,T.LOCATION "
        rs3.Open Sql, conn, adOpenStatic, adLockReadOnly
        
        Set DataGrid3.DataSource = rs3
        Label7.Caption = "筆數：" & rs3.RecordCount & " 筆"
        Call SETDG3
   Else
      Set DataGrid3.DataSource = Nothing
  End If
End Sub

Private Sub ExecuteSQL()
 
 If rs1.State Then rs1.Close
   Sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY" & _
        " FROM MATERMST AS M"
        
'  sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY," & _
'               "STOCKMST.LOCATION ,STOCKMST.REAL_QTY,TRANSDTL.STATUS," & _
'               "TRANSDTL.TRANS_DATE,TRANSDTL.QTY" & _
'        " FROM MATERMST AS M,STOCKMST,TRANSDTL " & _
'        " WHERE M.GCODE=STOCKMST.GCODE" & _
'              " AND STOCKMST.GCODE=TRANSDTL.GCODE "
              
   sql_1 = ""
   
   If TxtGCode(0).Text <> "" Then
      sql_1 = sql_1 & " M.GCode >= '" & TxtGCode(0).Text & "' and M.GCode <= '" & TxtGCode(1).Text & "'"
   End If
   If Trim(TxtLCode(0).Text) <> "" Then
      sql_1 = sql_1 & " M.LCode >= '" & TxtLCode(0).Text & "' and M.LCode <= '" & TxtLCode(1).Text & "'"
   End If
   
   
   If TxtUSystem.Text <> "" Then
      If sql_1 <> "" Then
         sql_1 = sql_1 & "and M.USystem like '%" & TxtUSystem.Text & "%' "
      Else
         sql_1 = sql_1 & "M.USystem like '%" & TxtUSystem.Text & "%' "
      End If
   End If
   
'   If TDate(0).Text <> "" Then
'      sql_1 = sql_1 & " TRANSDTL.TRANS_DATE >= '" & TDate(0).Text & "' and TRANSDTL.TRANS_DATE <= '" & TDate(1).Text & "'"
'      'sql_1 = sql_1 & " TRANS_DATE >= '" & TDate(0).Text & "' and TRANS_DATE <= '" & TDate(1).Text & "'"
'   End If
  
   If sql_1 <> "" Then
      Sql = Sql & " Where " & sql_1
   End If
  
   sql_1 = sql_1 & "M.USystem like '" & TxtUSystem.Text & "%' "
   Sql = Sql & " order by M.GCode "
  
   Debug.Print Sql
   rs1.Open Sql, conn, adOpenDynamic, adLockPessimistic
  
   Print rs1.RecordCount
   Label9.Caption = "筆數：" & rs1.RecordCount & " 筆"
   Set DataGrid1.DataSource = rs1
   Set DataGrid2.DataSource = Nothing
   Set DataGrid3.DataSource = Nothing
   If rs1.RecordCount > 0 Then
      ExecuteGrid2
   Else
      Label6.Caption = ""
   End If
  
   Call SETDG
 
End Sub

Public Sub SETDG()
       DataGrid1.Columns(0).Caption = "Global Code"
       DataGrid1.Columns(0).Width = 2350
       DataGrid1.Columns(1).Width = 1250
       DataGrid1.Columns(2).Width = 2800
       DataGrid1.Columns(3).Width = 1000
       DataGrid1.Columns(4).Width = 1000
      
       DataGrid1.Columns(0).Locked = True
       DataGrid1.Columns(1).Locked = True
       DataGrid1.Columns(2).Locked = True
       DataGrid1.Columns(3).Locked = True
       DataGrid1.Columns(4).Locked = True
End Sub
Public Sub SETDG2()
       DataGrid2.Columns(0).Width = 1500
       DataGrid2.Columns(1).Width = 1350
       DataGrid2.Columns(2).Width = 1350
       DataGrid2.Columns(3).Width = 1350
End Sub
Public Sub SETDG3()
       DataGrid3.Columns(0).Width = 1350
       DataGrid3.Columns(1).Width = 1350
       DataGrid3.Columns(2).Width = 1350
       DataGrid3.Columns(3).Width = 1350
End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

If TxtGCode(1) = "" Then TxtGCode(1).Text = TxtGCode(0).Text
If TxtLCode(1) = "" Then TxtLCode(1).Text = TxtLCode(0).Text

Select Case Button.Key
   Case Is = "search"
           ExecuteSQL
   Case Is = "print"
            ExecuteSQL
            ExecuteSQL_prt
            If Not rsprt.EOF Then
               Set DR_2.DataSource = rsprt
               DR_2.Sections("section2").Controls("Lab_Dt").Caption = Date
               DR_2.Show
            End If
   Case Is = "cancel"
           FieldClear
           ExecuteSQL
   Case Is = "exit"
           Unload Me
End Select
End Sub

Private Sub ExecuteSQL_prt()
 
  Dim STATUS As String
  
  If Option1(0).Value = True Then STATUS = "進貨"
  If Option1(1).Value = True Then STATUS = "出貨"
  If Option1(2).Value = True Then STATUS = "退回"
  If Option1(3).Value = True Then STATUS = ""
   
  If rsprt.State Then rsprt.Close
  Sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY," & _
               "STOCKMST.LOCATION ,STOCKMST.REAL_QTY,TRANSDTL.STATUS," & _
               "TRANSDTL.TRANS_DATE,TRANSDTL.QTY" & _
        " FROM MATERMST AS M,STOCKMST,TRANSDTL " & _
        " WHERE M.GCODE=STOCKMST.GCODE" & _
              " AND STOCKMST.GCODE=TRANSDTL.GCODE "
   
   If STATUS <> "" Then
      Sql = Sql & " AND TRANSDTL.STATUS='" & STATUS & "'"
   End If

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
     If TxtGCode(0).Text <> "" Then
        sql_1 = sql_1 & "and  M.GCode >= '" & TxtGCode(0).Text & "' "
     End If
     If TxtGCode(1).Text <> "" Then
        sql_1 = sql_1 & "and  M.GCode <= '" & TxtGCode(1).Text & "' "
     End If

     If TxtLCode(0).Text <> "" Then
        sql_1 = sql_1 & "and M.LCode >='" & TxtLCode(0).Text & "' "
     End If
     If TxtLCode(1).Text <> "" Then
        sql_1 = sql_1 & "and M.LCode <='" & TxtLCode(1).Text & "' "
     End If
     
     If TxtUSystem.Text <> "" Then
        sql_1 = sql_1 & "and M.USystem like '%" & TxtUSystem.Text & "%' "
     End If


     
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  Sql = Sql & " order by M.GCode "
  
  rsprt.Open Sql, conn, adOpenDynamic, adLockPessimistic
   
  
End Sub

Private Sub TxtGCode_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = 13 Then SendKeys "{tab}"
End Sub

Public Sub FieldClear()
   TxtUSystem.Text = ""
   TxtGCode(0).Text = ""
   TxtGCode(1).Text = ""
   TxtLCode(0).Text = ""
   TxtLCode(1).Text = ""
End Sub
