VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form FrmMateriel_Report 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "物料進出統計表"
   ClientHeight    =   7830
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11835
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7830
   ScaleWidth      =   11835
   WhatsThisHelp   =   -1  'True
   Begin VB.Frame Frame3 
      Height          =   1935
      Left            =   10080
      TabIndex        =   14
      Top             =   840
      Width           =   1695
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
         TabIndex        =   18
         Top             =   1440
         Value           =   -1  'True
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
         TabIndex        =   17
         Top             =   1080
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
         Left            =   360
         TabIndex        =   16
         Top             =   720
         Width           =   1095
      End
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
         TabIndex        =   15
         Top             =   360
         Width           =   1095
      End
   End
   Begin MSDataGridLib.DataGrid DataGrid1 
      Height          =   4935
      Left            =   0
      TabIndex        =   13
      Top             =   2880
      Width           =   11775
      _ExtentX        =   20770
      _ExtentY        =   8705
      _Version        =   393216
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
         EndProperty
         BeginProperty Column01 
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frame2 
      Height          =   1935
      Left            =   0
      TabIndex        =   3
      Top             =   840
      Width           =   9975
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
         TabIndex        =   9
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
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   8
         Top             =   285
         Width           =   1935
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
         TabIndex        =   5
         Text            =   " "
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
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   4
         Text            =   " "
         Top             =   840
         Width           =   1215
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
         Left            =   6240
         TabIndex        =   12
         Top             =   240
         Width           =   3255
      End
      Begin VB.Line Line1 
         BorderWidth     =   3
         X1              =   2760
         X2              =   3120
         Y1              =   960
         Y2              =   960
      End
      Begin VB.Line Line3 
         BorderWidth     =   3
         X1              =   3480
         X2              =   3840
         Y1              =   480
         Y2              =   480
      End
      Begin VB.Label Label1 
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
         TabIndex        =   11
         Top             =   2040
         Width           =   4815
      End
      Begin VB.Label Label2 
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
         Left            =   360
         TabIndex        =   7
         Top             =   840
         Width           =   1095
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
         TabIndex        =   6
         Top             =   360
         Width           =   1335
      End
   End
   Begin VB.Frame Frame1 
      BackColor       =   &H80000004&
      Height          =   1935
      Left            =   0
      TabIndex        =   0
      Top             =   3720
      Visible         =   0   'False
      Width           =   7455
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
         Left            =   3960
         Picture         =   "FrmMateriel_Report.frx":0000
         Style           =   1  '圖片外觀
         TabIndex        =   2
         Top             =   240
         Width           =   975
      End
      Begin VB.CommandButton CmdPView 
         BackColor       =   &H80000004&
         Caption         =   "列印"
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
         Left            =   2040
         Picture         =   "FrmMateriel_Report.frx":0544
         Style           =   1  '圖片外觀
         TabIndex        =   1
         Top             =   240
         Width           =   975
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   0
      Top             =   5640
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
            Picture         =   "FrmMateriel_Report.frx":0ABF
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":1B11
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":1E8C
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":2766
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":2ABB
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":3046
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":3360
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":43B2
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":471F
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":5771
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":67C3
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":6ADD
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":6DF7
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":7E49
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":8163
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":84B5
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Report.frx":8807
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   10
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
         NumButtons      =   4
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
End
Attribute VB_Name = "FrmMateriel_Report"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim rs As New ADODB.Recordset
Attribute rs.VB_VarHelpID = -1
Dim P0 As String
Dim P1 As String

'Private Sub DataGrid1_HeadClick(ByVal ColIndex As Integer)
'   MsgBox DataGrid1.Columns(ColIndex).Caption
'
'   DataGrid1.Tag = " Order by " & DataGrid1.Columns(ColIndex).Caption
'   ExecuteSQL_prt
'End Sub

Private Sub Form_Load()
   FormsAttribute
   Label3.Caption = "筆數：0 筆"
   Toolbar2.Buttons("print").Enabled = False
End Sub
Private Sub Form_Activate()
   DoEvents
   Call FieldCls
   TxtDate(0) = Year(Now) & Format(Month(Now), "00") & Format(Day(Now), "00")
   TxtDate(1) = Year(Now) & Format(Month(Now), "00") & Format(Day(Now), "00")
   TxtDate(0).SetFocus
End Sub

Private Sub CmdPView_Click()
  If rs.State Then rs.Close
  ' Sql = " SELECT MATE_ID,SUM(QTY) AS IN_QTY,0 AS OUT_QTY,0 AS REAL_QTY INTO TRANSQTY FROM TRANSDTL WHERE TRANS_DATE>= '" & TxtDate(0) & "' and TRANS_DATE <= '" & TxtDate(1) & "' GROUP BY MATE_ID"
   Sql = "drop table TRANSQTY"
   conn.Execute Sql
   
   Sql = "SELECT GCode,SUM(QTY) AS IN_QTY,0 AS OUT_QTY,0 AS REAL_QTY INTO TRANSQTY FROM TRANSDTL WHERE TRANS_DATE>= '" & TxtDate(0) & "' and TRANS_DATE <= '" & TxtDate(1) & "'  GROUP BY  GCode"
   conn.Execute Sql
     
   Sql = "SELECT GCode,SUM(QTY) as out_qty FROM TRANSDTL WHERE TRANS_DATE>='" & TxtDate(0) & "' AND TRANS_DATE <= '" & TxtDate(1) & "'  GROUP BY GCode "
   rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
   a = rs.RecordCount
   While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY SET OUT_QTY = '" & rs.Fields("out_qty") & "' WHERE GCode = '" & rs.Fields("GCode") & "'  "
     conn.Execute Sql, RA
     If RA = 0 Then
        Sql = "INSERT INTO TRANSQTY(GCode,OUT_QTY) VALUES('" & rs.Fields("GCode") & "','" & rs.Fields("out_qty") & "')"
        conn.Execute Sql
        RA = 0
     End If
     rs.MoveNext
   Wend
   rs.Close
   
   Sql = "SELECT GCode,REAL_QTY FROM STOCKMST"
   rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
   a = rs.RecordCount
   While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY SET REAL_QTY = '" & rs.Fields("REAL_QTY") & "' WHERE GCode = '" & rs.Fields("GCode") & "'  "
     conn.Execute Sql, RA
     rs.MoveNext
   Wend
   rs.Close
   
   Sql = "select * from view_2"
   rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
   
   Label1.Caption = ""
   If rs.RecordCount > 0 Then
       Set DR_4.DataSource = rs
       DR_4.Sections("section2").Controls("Lab_Dt").Caption = Date
       DR_4.Show
   Else
      Label1.Caption = "無符合的資料供列印！"
   End If
  ' Set DR當日物料進出表.DataSource = rs
  ' DR當日物料進出表.Sections("Section2").Controls("LABQdate").Caption = "查詢日期:" & TxtDate(0) & " ~ " & TxtDate(1)
  ' DR當日物料進出表.Show vbModal
  ' rs.Close
  ' Sql = "DROP TABLE TRANSQTY"
  ' conn.Execute Sql
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~離開~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExit_Click()
   Unload Me
End Sub


Public Sub FieldCls()
  TxtDate(0) = ""
  TxtDate(1) = ""
End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)
Select Case Button.Key

   Case Is = "print"
'===============================================Amy
'         If rs.State Then rs.Close
'
'         Sql = "SELECT MATE_ID,SUM(QTY) AS IN_QTY,0 AS OUT_QTY,0 AS REAL_QTY INTO TRANSQTY FROM TRANSDTL WHERE TRANS_DATE>= '" & TxtDate(0) & "' and TRANS_DATE <= '" & TxtDate(1) & "' GROUP BY MATE_ID"
'         Sql = "drop table TRANSQTY"
'         conn.Execute Sql
'
'         Sql = "SELECT GCode,SUM(QTY) AS IN_QTY,0 AS OUT_QTY,0 AS REAL_QTY INTO TRANSQTY FROM TRANSDTL WHERE TRANS_DATE>= '" & TxtDate(0) & "' and TRANS_DATE <= '" & TxtDate(1) & "'  GROUP BY  GCode"
'         conn.Execute Sql
'
'         Sql = "SELECT GCode,SUM(QTY) as out_qty FROM TRANSDTL WHERE TRANS_DATE>='" & TxtDate(0) & "' AND TRANS_DATE <= '" & TxtDate(1) & "'  GROUP BY GCode "
'         rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
'         a = rs.RecordCount
'         While (Not rs.EOF)
'           Sql = "UPDATE TRANSQTY SET OUT_QTY = '" & rs.Fields("out_qty") & "' WHERE GCode = '" & rs.Fields("GCode") & "'  "
'           conn.Execute Sql, RA
'           If RA = 0 Then
'              Sql = "INSERT INTO TRANSQTY(GCode,OUT_QTY) VALUES('" & rs.Fields("GCode") & "','" & rs.Fields("out_qty") & "')"
'              conn.Execute Sql
'              RA = 0
'           End If
'           rs.MoveNext
'         Wend
'         rs.Close
'
'         Sql = "SELECT GCode,REAL_QTY FROM STOCKMST"
'         rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
'         a = rs.RecordCount
'         While (Not rs.EOF)
'           Sql = "UPDATE TRANSQTY SET REAL_QTY = '" & rs.Fields("REAL_QTY") & "' WHERE GCode = '" & rs.Fields("GCode") & "'  "
'           conn.Execute Sql, RA
'           rs.MoveNext
'         Wend
'         rs.Close
'
'         Sql = "select * from view_2"
'         rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
'================================================================

         ExecuteSQL_prt
         
         Label3.Caption = ""
         
         Sql = "select * from TRANSQTY"
         rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
         
         Set DR_4.DataSource = rs
         
         If rs.RecordCount > 0 Then
             Set DR_4.DataSource = rs
             DR_4.Sections("section2").Controls("Lab_Dt").Caption = Date
             DR_4.Show
         Else
            Label3.Caption = "無符合的資料供列印！"
         End If
         
   Case Is = "search"
        ExecuteSQL_prt
        
        Label3.Caption = ""
         
          If rs.State Then rs.Close
          
        Sql = "select * from TRANSQTY"
        rs.Open Sql, conn, adOpenDynamic, adLockReadOnly
        
        If rs.RecordCount = 0 Then
            Label3.Caption = "無符合的資料供列印！"
        End If
        Call SETDG
        Toolbar2.Buttons("print").Enabled = True
        
   Case Is = "cancel"
        Label3.Caption = ""
        Call FieldClear
        Toolbar2.Buttons("print").Enabled = False
        Set DataGrid1.DataSource = Nothing
   Case Is = "exit"
        Unload Me
End Select
End Sub

Private Sub TxtDATE_KeyPress(Index As Integer, KeyAscii As Integer)
  If KeyAscii = 13 Then SendKeys "{TAB}"
End Sub

Private Sub TxtDATE_LostFocus(Index As Integer)
  If Index = 0 Then
     If TxtDate(Index).Text = "" Then TxtDate(Index) = Year(Now) & Format(Month(Now), "00") & Format(Day(Now), "00")
  ElseIf Index = 1 Then
     If TxtDate(Index).Text = "" Then TxtDate(Index) = TxtDate(0)
  End If
  
End Sub


Public Sub FieldClear()
  TxtGCode(0) = "": TxtGCode(1) = ""
'  TxtLCode(0) = "": TxtLCode(1) = ""
'  TxtUSystem = ""

End Sub


Private Sub ExecuteSQL_prt()
 
If TxtGCode(1) = "" Then TxtGCode(1).Text = TxtGCode(0).Text
 
  Dim STATUS As String

  If Option1(0).Value = True Then STATUS = "進貨"
  If Option1(1).Value = True Then STATUS = "出貨"
  If Option1(2).Value = True Then STATUS = "退回"
  If Option1(3).Value = True Then STATUS = ""


  Select Case STATUS
  '===================================================================================
  Case ""     '查詢全部
  If rs.State Then rs.Close
'  sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description," & _
'               "T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY" & _
'        " INTO TRANSQTY" & _
'        " FROM MATERMST AS M" & "," & _
'               "TRANSDTL AS T" & _
'        " WHERE M.GCODE=T.GCODE And T.STATUS='" & "進貨" & "' "
        
        
  Sql = "SELECT T.GCode As GCode,NULL As LCode,NULL As Description," & _
               "T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY," & _
               "NULL As Stock_QTY,T.Custno As CustNo" & _
        " INTO TRANSQTY" & _
        " FROM TRANSDTL AS T" & _
        " WHERE  T.STATUS='" & "進貨" & "' " & _
             "OR T.STATUS='" & "退回" & "' "

        '" WHERE  T.STATUS LIKE '" & STATUS & "%' "
        '" WHERE  T.STATUS='" & "進貨" & "' "

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(0) <> "" Then
     sql_1 = sql_1 & "And T.GCODE >= '" & TxtGCode(0) & "' AND T.GCODE <= '" & TxtGCode(1) & "' "
  End If
  If TxtDate(0).Text <> "" Then
     sql_1 = sql_1 & "And T.TRANS_DATE >= '" & TxtDate(0) & "' AND T.TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  Sql = Sql & " GROUP by T.GCode,T.Location,T.Custno"
'  Sql = Sql & " order by M.GCode"
  Debug.Print Sql
  
  conn.Execute "drop table TRANSQTY"
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  
  If rs.State Then rs.Close
  
  Sql = "SELECT T.GCode As GCode,T.Location As Location,Sum(T.QTY) As OUT_QTY" & _
        " FROM TRANSDTL AS T" & _
        " WHERE  T.STATUS='" & "出貨" & "' "
        
        '" WHERE  T.STATUS LIKE '" & STATUS & "%' "
        '" WHERE  T.STATUS='" & "出貨" & "' "
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  Sql = Sql & " GROUP by T.GCode,T.Location"
  
  
'  sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description," & _
'               "T.Location As Location,Sum(T.QTY) AS OUT_QTY" & _
'        " FROM MATERMST AS M" & "," & _
'               "TRANSDTL AS T" & _
'        " WHERE M.GCODE=T.GCODE And T.STATUS='" & "出貨" & "' "
'
'  sql = sql & " GROUP by M.GCode,M.LCode,T.Location,M.Description"
  
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  a = rs.RecordCount
  While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY SET OUT_QTY = '" & rs.Fields("OUT_QTY") & "' " & _
           "WHERE GCode = '" & rs.Fields("GCode") & "' " & _
                 "And Location = '" & rs.Fields("Location") & "' "
     conn.Execute Sql, RA
     If RA = 0 Then
        Sql = "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) " & _
              "VALUES('" & rs.Fields("GCode") & "','" & _
                           rs.Fields("Location") & "','" & _
                           "0" & "','" & _
                           rs.Fields("OUT_QTY") & "')"
        conn.Execute Sql
        RA = 0
     End If
     rs.MoveNext
  Wend
  
  
  If rs.State Then rs.Close
  
  Sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY," & _
               "T.GCode" & _
        " FROM MATERMST AS M" & "," & _
               "TRANSDTL AS T" & _
        " WHERE M.GCODE=T.GCODE"
  
  If DataGrid1.Tag = "" Then
     Sql = Sql & " Order by T.GCode"
  Else
     Sql = Sql & DataGrid1.Tag
  End If
  
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  a = rs.RecordCount
  While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY " & _
           "SET LCode = '" & rs.Fields("LCode") & "', " & _
               "Description = '" & rs.Fields("Description") & "', " & _
               "Stock_QTY = '" & rs.Fields("Stock_QTY") & "' " & _
           "WHERE GCode = '" & rs.Fields("GCode") & "' "

     conn.Execute Sql, RA
'     If RA = 0 Then
'        sql = "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) " & _
'              "VALUES('" & rs.Fields("GCode") & "','" & _
'                           rs.Fields("Location") & "','" & _
'                           "0" & "','" & _
'                           rs.Fields("OUT_QTY") & "')"
'        conn.Execute sql
'        RA = 0
'     End If
     rs.MoveNext
  Wend
  
  rs.Close
  '===================================================================================
  Case "進貨"     '查詢進貨
  If rs.State Then rs.Close

  Sql = "SELECT T.GCode As GCode,NULL As LCode,NULL As Description," & _
               "T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY," & _
               "NULL As Stock_QTY,T.Custno As CustNo" & _
        " INTO TRANSQTY" & _
        " FROM TRANSDTL AS T" & _
        " WHERE  T.STATUS='" & "進貨" & "' "

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(0) <> "" Then
     sql_1 = sql_1 & "And T.GCODE >= '" & TxtGCode(0) & "' AND T.GCODE <= '" & TxtGCode(1) & "' "
  End If
  If TxtDate(0).Text <> "" Then
     sql_1 = sql_1 & "And T.TRANS_DATE >= '" & TxtDate(0) & "' AND T.TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  Sql = Sql & " GROUP by T.GCode,T.Location,T.Custno"
'  Sql = Sql & " order by M.GCode"
  Debug.Print Sql
  
  conn.Execute "drop table TRANSQTY"
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic

  If rs.State Then rs.Close
  
  Sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY," & _
               "T.GCode" & _
        " FROM MATERMST AS M" & "," & _
               "TRANSDTL AS T" & _
        " WHERE M.GCODE=T.GCODE"
        
  Sql = Sql & " Order by T.GCode"
 
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  a = rs.RecordCount
  While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY " & _
           "SET LCode = '" & rs.Fields("LCode") & "', " & _
               "Description = '" & rs.Fields("Description") & "', " & _
               "Stock_QTY = '" & rs.Fields("Stock_QTY") & "' " & _
           "WHERE GCode = '" & rs.Fields("GCode") & "' "

     conn.Execute Sql, RA
'     If RA = 0 Then
'        sql = "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) " & _
'              "VALUES('" & rs.Fields("GCode") & "','" & _
'                           rs.Fields("Location") & "','" & _
'                           "0" & "','" & _
'                           rs.Fields("OUT_QTY") & "')"
'        conn.Execute sql
'        RA = 0
'     End If
     rs.MoveNext
  Wend
  rs.Close
  
  '===================================================================================
  Case "出貨"     '查詢出貨
  If rs.State Then rs.Close

  Sql = "SELECT T.GCode As GCode,NULL As LCode,NULL As Description," & _
               "T.Location As Location,0 As IN_QTY,Sum(T.QTY) AS OUT_QTY," & _
               "NULL As Stock_QTY,T.Custno As CustNo" & _
        " INTO TRANSQTY" & _
        " FROM TRANSDTL AS T" & _
        " WHERE  T.STATUS='" & "出貨" & "' "

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(0) <> "" Then
     sql_1 = sql_1 & "And T.GCODE >= '" & TxtGCode(0) & "' AND T.GCODE <= '" & TxtGCode(1) & "' "
  End If
  If TxtDate(0).Text <> "" Then
     sql_1 = sql_1 & "And T.TRANS_DATE >= '" & TxtDate(0) & "' AND T.TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  Sql = Sql & " GROUP by T.GCode,T.Location,T.Custno"
'  Sql = Sql & " order by M.GCode"
  Debug.Print Sql
  
  conn.Execute "drop table TRANSQTY"
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic


  If rs.State Then rs.Close
  
  Sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY," & _
               "T.GCode" & _
        " FROM MATERMST AS M" & "," & _
               "TRANSDTL AS T" & _
        " WHERE M.GCODE=T.GCODE"
        
  Sql = Sql & " Order by T.GCode"
 
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  a = rs.RecordCount
  While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY " & _
           "SET LCode = '" & rs.Fields("LCode") & "', " & _
               "Description = '" & rs.Fields("Description") & "', " & _
               "Stock_QTY = '" & rs.Fields("Stock_QTY") & "' " & _
           "WHERE GCode = '" & rs.Fields("GCode") & "' "

     conn.Execute Sql, RA
'     If RA = 0 Then
'        sql = "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) " & _
'              "VALUES('" & rs.Fields("GCode") & "','" & _
'                           rs.Fields("Location") & "','" & _
'                           "0" & "','" & _
'                           rs.Fields("OUT_QTY") & "')"
'        conn.Execute sql
'        RA = 0
'     End If
     rs.MoveNext
  Wend
  
  rs.Close
  
  '===================================================================================
  Case "退回"     '查詢退回
  If rs.State Then rs.Close

  Sql = "SELECT T.GCode As GCode,NULL As LCode,NULL As Description," & _
               "T.Location As Location,Sum(T.QTY) As IN_QTY,0 AS OUT_QTY," & _
               "NULL As Stock_QTY,T.Custno As CustNo" & _
        " INTO TRANSQTY" & _
        " FROM TRANSDTL AS T" & _
        " WHERE  T.STATUS='" & "退回" & "' "

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(0) <> "" Then
     sql_1 = sql_1 & "And T.GCODE >= '" & TxtGCode(0) & "' AND T.GCODE <= '" & TxtGCode(1) & "' "
  End If
  If TxtDate(0).Text <> "" Then
     sql_1 = sql_1 & "And T.TRANS_DATE >= '" & TxtDate(0) & "' AND T.TRANS_DATE <= '" & TxtDate(1) & "' "
  End If
  
  If sql_1 <> "" Then
     Sql = Sql & sql_1
  End If
  
  Sql = Sql & " GROUP by T.GCode,T.Location,T.Custno"
'  Sql = Sql & " order by M.GCode"
  Debug.Print Sql
  
  conn.Execute "drop table TRANSQTY"
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic


  If rs.State Then rs.Close
  
  Sql = "SELECT M.GCODE AS GCode,M.LCODE AS LCode,M.Description As Description,M.Stock_QTY As Stock_QTY," & _
               "T.GCode" & _
        " FROM MATERMST AS M" & "," & _
               "TRANSDTL AS T" & _
        " WHERE M.GCODE=T.GCODE"
        
  Sql = Sql & " Order by T.GCode"
 
  rs.Open Sql, conn, adOpenStatic, adLockBatchOptimistic
  
  a = rs.RecordCount
  While (Not rs.EOF)
     Sql = "UPDATE TRANSQTY " & _
           "SET LCode = '" & rs.Fields("LCode") & "', " & _
               "Description = '" & rs.Fields("Description") & "', " & _
               "Stock_QTY = '" & rs.Fields("Stock_QTY") & "' " & _
           "WHERE GCode = '" & rs.Fields("GCode") & "' "

     conn.Execute Sql, RA
'     If RA = 0 Then
'        sql = "INSERT INTO TRANSQTY(GCode,Location,IN_QTY,OUT_QTY) " & _
'              "VALUES('" & rs.Fields("GCode") & "','" & _
'                           rs.Fields("Location") & "','" & _
'                           "0" & "','" & _
'                           rs.Fields("OUT_QTY") & "')"
'        conn.Execute sql
'        RA = 0
'     End If
     rs.MoveNext
  Wend
  
  rs.Close
  
  End Select

  
End Sub


Public Sub SETDG()
   Label3.Caption = "筆數：" & rs.RecordCount & " 筆"
   Set DataGrid1.DataSource = rs
   
   DataGrid1.Columns(0).Width = 2050
   DataGrid1.Columns(1).Width = 1050
   DataGrid1.Columns(2).Width = 3050
   DataGrid1.Columns(3).Width = 1000
   DataGrid1.Columns(4).Width = 1000
   DataGrid1.Columns(5).Width = 1000
   DataGrid1.Columns(6).Width = 1000
   
   DataGrid1.Columns(0).Locked = True
   DataGrid1.Columns(1).Locked = True
   DataGrid1.Columns(2).Locked = True
   DataGrid1.Columns(3).Locked = True
   DataGrid1.Columns(4).Locked = True
   DataGrid1.Columns(5).Locked = True
   DataGrid1.Columns(6).Locked = True

End Sub
