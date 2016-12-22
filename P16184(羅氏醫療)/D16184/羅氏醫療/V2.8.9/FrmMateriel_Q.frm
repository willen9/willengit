VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmMateriel_Q 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "物料庫存查詢"
   ClientHeight    =   7845
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11505
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7845
   ScaleWidth      =   11505
   WhatsThisHelp   =   -1  'True
   Begin VB.Frame Frame2 
      Height          =   1815
      Left            =   0
      TabIndex        =   9
      Top             =   840
      Width           =   11415
      Begin VB.TextBox TxtDescription 
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
         Left            =   5760
         MaxLength       =   15
         TabIndex        =   21
         Top             =   1320
         Width           =   3135
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
         TabIndex        =   14
         Top             =   1320
         Width           =   2895
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
         TabIndex        =   13
         Top             =   840
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
         TabIndex        =   12
         Top             =   840
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
         Height          =   315
         Index           =   1
         Left            =   4320
         MaxLength       =   15
         TabIndex        =   11
         Top             =   360
         Width           =   2295
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
         Height          =   315
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   10
         Top             =   360
         Width           =   2295
      End
      Begin VB.Label Label7 
         BackColor       =   &H80000004&
         Caption         =   "Description:"
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
         Left            =   4560
         TabIndex        =   22
         Top             =   1320
         Width           =   1215
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         X1              =   3840
         X2              =   4200
         Y1              =   480
         Y2              =   480
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
         TabIndex        =   17
         Top             =   1320
         Width           =   975
      End
      Begin VB.Line Line2 
         BorderWidth     =   3
         X1              =   3840
         X2              =   4200
         Y1              =   960
         Y2              =   960
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
         TabIndex        =   16
         Top             =   840
         Width           =   1215
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
         TabIndex        =   15
         Top             =   360
         Width           =   1335
      End
   End
   Begin VB.Frame Frame1 
      Height          =   5175
      Left            =   0
      TabIndex        =   4
      Top             =   2640
      Width           =   11415
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   2055
         Left            =   120
         TabIndex        =   6
         Top             =   600
         Width           =   11175
         _ExtentX        =   19711
         _ExtentY        =   3625
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
         TabIndex        =   7
         Top             =   3120
         Width           =   11175
         _ExtentX        =   19711
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
      Begin VB.Label Label6 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   8760
         TabIndex        =   19
         Top             =   3360
         Width           =   1695
      End
      Begin VB.Label Label9 
         ForeColor       =   &H00800080&
         Height          =   255
         Left            =   8760
         TabIndex        =   18
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
         Left            =   240
         TabIndex        =   8
         Top             =   240
         Width           =   1215
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
         Left            =   240
         TabIndex        =   5
         Top             =   2760
         Width           =   1575
      End
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   20
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
      Left            =   3120
      TabIndex        =   0
      Top             =   120
      Visible         =   0   'False
      Width           =   1815
      _ExtentX        =   3201
      _ExtentY        =   847
      ButtonWidth     =   609
      ButtonHeight    =   741
      Appearance      =   1
      _Version        =   393216
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
         Left            =   1320
         Picture         =   "FrmMateriel_Q.frx":0000
         Style           =   1  '圖片外觀
         TabIndex        =   3
         ToolTipText     =   "離開"
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
         Left            =   840
         Picture         =   "FrmMateriel_Q.frx":0544
         Style           =   1  '圖片外觀
         TabIndex        =   2
         ToolTipText     =   "列印"
         Top             =   0
         Width           =   450
      End
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
         Picture         =   "FrmMateriel_Q.frx":0ABF
         Style           =   1  '圖片外觀
         TabIndex        =   1
         ToolTipText     =   "查詢"
         Top             =   0
         Width           =   450
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   720
      Top             =   120
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
            Picture         =   "FrmMateriel_Q.frx":0FAE
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":2000
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":237B
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":2C55
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":2FAA
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":3535
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":384F
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":48A1
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":4C0E
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":5C60
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":6CB2
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":6FCC
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":72E6
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":8338
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":8652
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":89A4
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Q.frx":8CF6
            Key             =   ""
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmMateriel_Q"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim P0 As String
Dim P1 As String
Dim P2 As String
Dim P3 As String
Dim P4 As String
Dim T1 As String
Dim T2 As String

Private Sub CmdExit_Click()
  Unload Me
End Sub

Private Sub CmdPView_Click()

   If rs1.RecordCount > 0 Then
       Set DR_1.DataSource = rs1
       DR_1.Sections("section2").Controls("Lab_Dt").Caption = Date
       DR_1.Show
   End If
   
End Sub

Private Sub cmdQuery_Click()
 ExecuteSQL
End Sub

Private Sub DataGrid1_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
 If Not (rs1.EOF Or rs1.BOF) Then
    ExecuteGrid2
 End If
End Sub

Private Sub Form_Activate()
   TxtGCode(0).Text = ""
   TxtGCode(1).Text = ""
   TxtLCode(0).Text = ""
End Sub

Private Sub Form_Load()
  FormsAttribute
  ExecuteGrid1
  If Not (rs1.EOF Or rs1.BOF) Then ExecuteGrid2
  
End Sub

Private Sub ExecuteGrid1()
'  If rs1.State Then rs1.Close
'  Sql = "SELECT M.GCODE AS GlobalCode,M.SNum,M.LCODE AS LocalCode,M.Description,M.USystem FROM MATERMST AS M ORDER BY M.GCODE"
'  rs1.Open Sql, conn, adOpenStatic, adLockReadOnly
  
'  Set DataGrid1.DataSource = rs1
'  Label9.Caption = "筆數：" & rs1.RecordCount & " 筆"
     
  ExecuteSQL
End Sub

Private Sub ExecuteGrid2()
   If rs2.State Then rs2.Close
  
      sql = "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,(S.LAST_QTY+S.IN_QTY-S.OUT_QTY) AS STOCK_QTY FROM STOCKMST AS S " & _
            "WHERE S.GCODE = '" & rs1.Fields(0) & "' ORDER BY S.LOCATION"
      rs2.Open sql, conn, adOpenStatic, adLockReadOnly

      Set DataGrid2.DataSource = rs2
      Label6.Caption = "筆數：" & rs2.RecordCount & " 筆"

      DataGrid2.Columns(3).Caption = "Stock"
  
  
'   Sql = "SELECT S.LOCATION,S.IN_QTY,S.OUT_QTY,S.REAL_QTY,M.Stock_QTY FROM STOCKMST AS S,MATERMST AS M " & _
'         "WHERE S.GCODE = '" & rs1.Fields(0) & "' AND M.GCODE = '" & rs1.Fields(0) & "' ORDER BY S.LOCATION"
'
'   rs2.Open Sql, conn, adOpenStatic, adLockReadOnly
'
'   Set DataGrid2.DataSource = rs2
'   Label6.Caption = "筆數：" & rs2.RecordCount & " 筆"
        
End Sub
Private Sub ExecuteSQL()
 
  If rs1.State Then rs1.Close
  sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem  FROM MATERMST AS M  "
  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""

   If TxtGCode(0).Text <> "" Then
      sql_1 = sql_1 & " M.GCode >= '" & TxtGCode(0).Text & "' and M.GCode <= '" & TxtGCode(1).Text & "'"
   End If
   If Trim(TxtLCode(0).Text) <> "" Then
      sql_1 = sql_1 & " M.LCode >= '" & TxtLCode(0).Text & "' and M.LCode <= '" & TxtLCode(1).Text & "'"
      'sql_1 = sql_1 & "and M.LCode >='" & TxtLCode(0).Text & "' "
   End If
  
   If TxtUSystem.Text <> "" Then
      If sql_1 <> "" Then
         sql_1 = sql_1 & "and M.USystem like '%" & TxtUSystem.Text & "%' "
      Else
         sql_1 = sql_1 & "M.USystem like '%" & TxtUSystem.Text & "%' "
      End If
   End If
   
   If TxtDescription.Text <> "" Then
      If sql_1 <> "" Then
         sql_1 = sql_1 & "and M.Description like '%" & TxtDescription.Text & "%' "
      Else
         sql_1 = sql_1 & "M.Description like '%" & TxtDescription.Text & "%' "
      End If
   End If
   
   If sql_1 <> "" Then
      sql = sql & " Where " & sql_1
   End If
  
  
   'sql_1 = sql_1 & "M.USystem like '" & TxtUSystem.Text & "%' "
   
   sql = sql & " order by M.GCode "
   rs1.Open sql, conn, adOpenDynamic, adLockPessimistic
  
'     If TxtGCode(0).Text <> "" Then
'        sql_1 = sql_1 & "and  M.GCode >= '" & TxtGCode(0).Text & "' "
'     End If
'
'     If TxtGCode(1).Text <> "" Then
'        sql_1 = sql_1 & "and  M.GCode <= '" & TxtGCode(1).Text & "' "
'     End If
'
'     If Trim(TxtUSystem.Text) <> "" Then
'        sql_1 = sql_1 & "and M.USystem like '%" & TxtUSystem.Text & "%' "
'     End If
'
'     If Trim(TxtLCode(0).Text) <> "" Then
'        sql_1 = sql_1 & "and M.LCode >='" & TxtLCode(0).Text & "' "
'     End If
'
'  If sql_1 <> "" Then
'     sql = sql & sql_1
'    ' sql_r = Right(sql_r, Len(sql_r) - 4)
'  End If
'
'  sql = sql & " order by M.GCode "
'
'  Debug.Print sql
'  rs1.Open sql, conn, adOpenDynamic, adLockPessimistic

  
  Print rs1.RecordCount
  Label9.Caption = "筆數：" & rs1.RecordCount & " 筆"
  Set DataGrid1.DataSource = rs1
  Set DataGrid2.DataSource = Nothing
  If rs1.RecordCount > 0 Then
     ExecuteGrid2
  Else
     Label6.Caption = ""
  End If
  'Call show_dg1
  Call SETDG
  
End Sub

Public Sub SETDG()

   DataGrid1.Columns(0).Caption = "Global Code"
   'DataGrid1.Columns(4).Caption = "Safe"
  ' DataGrid1.Columns(5).Caption = "Safe"
  ' DataGrid1.Columns(5).Alignment = dbgRight
  ' DataGrid1.Columns(6).Alignment = dbgRight
  ' DataGrid1.Columns(7).Alignment = dbgRight
   
   DataGrid1.Columns(0).Width = 2350
   DataGrid1.Columns(1).Width = 1200
   DataGrid1.Columns(2).Width = 2600
   DataGrid1.Columns(3).Width = 1000
  ' DataGrid1.Columns(4).Width = 700
  ' DataGrid1.Columns(5).Width = 700
  ' DataGrid1.Columns(6).Width = 1000
  ' DataGrid1.Columns(7).Width = 1000
   
   DataGrid1.Columns(0).Locked = True
   DataGrid1.Columns(1).Locked = True
   DataGrid1.Columns(2).Locked = True
   DataGrid1.Columns(3).Locked = True
   'DataGrid1.Columns(4).Locked = True
  ' DataGrid1.Columns(5).Locked = True
  ' DataGrid1.Columns(6).Locked = True
  ' DataGrid1.Columns(7).Locked = True
  'DataGrid1.Columns(5).Visible = False
  'DataGrid1.Columns(6).Visible = False
    
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
            If rsprt.RecordCount > 0 Then
                Set DR_1.DataSource = rsprt
                DR_1.Sections("section2").Controls("Lab_Dt").Caption = Date
                DR_1.Show
            End If
   Case Is = "cancel"
           FieldClear
           ExecuteSQL
   Case Is = "exit"
           Unload Me
End Select
End Sub

Private Sub ExecuteSQL_prt()
 
  'On Error Resume Next
  If rsprt.State Then rsprt.Close
  sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem,M.STOCK_QTY," & _
               "STOCKMST.LOCATION ,STOCKMST.REAL_QTY" & _
        " FROM MATERMST AS M,STOCKMST" & _
        " WHERE M.GCODE=STOCKMST.GCODE "
        
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
     sql = sql & sql_1
  End If

  sql = sql & " order by M.GCode "

  rsprt.Open sql, conn, adOpenDynamic, adLockPessimistic

End Sub

Public Sub FieldClear()
   TxtUSystem.Text = ""
   TxtGCode(0).Text = ""
   TxtGCode(1).Text = ""
   TxtLCode(0).Text = ""
   TxtLCode(1).Text = ""
End Sub

