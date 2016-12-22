VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmSafe_Report 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "安全量表"
   ClientHeight    =   2685
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   9150
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   3115.864
   ScaleMode       =   0  '使用者自訂
   ScaleWidth      =   14279.75
   WhatsThisHelp   =   -1  'True
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   13
      Top             =   0
      Width           =   1815
      _ExtentX        =   3201
      _ExtentY        =   1455
      ButtonWidth     =   1032
      ButtonHeight    =   1349
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   3
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "列印"
            Key             =   "print"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "取消"
            Key             =   "cancel"
            ImageIndex      =   8
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "返回"
            Key             =   "exit"
            ImageIndex      =   13
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frame2 
      Height          =   1815
      Left            =   0
      TabIndex        =   0
      Top             =   840
      Width           =   9135
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
         TabIndex        =   5
         Top             =   1200
         Width           =   5055
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
         TabIndex        =   4
         Top             =   720
         Width           =   2175
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
         TabIndex        =   3
         Top             =   720
         Width           =   2175
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
         TabIndex        =   2
         Top             =   240
         Width           =   2175
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
         Height          =   375
         Index           =   0
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   1
         Top             =   240
         Width           =   2175
      End
      Begin VB.Line Line3 
         BorderWidth     =   3
         X1              =   3720
         X2              =   4080
         Y1              =   480
         Y2              =   480
      End
      Begin VB.Label Label1 
         Caption         =   "筆數:"
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
         Left            =   7080
         TabIndex        =   9
         Top             =   360
         Width           =   1815
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
         TabIndex        =   8
         Top             =   1200
         Width           =   975
      End
      Begin VB.Line Line2 
         BorderWidth     =   3
         X1              =   3720
         X2              =   4080
         Y1              =   960
         Y2              =   960
      End
      Begin VB.Label Label2 
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
         TabIndex        =   7
         Top             =   720
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
         TabIndex        =   6
         Top             =   240
         Width           =   1335
      End
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   480
      Left            =   5400
      TabIndex        =   10
      Top             =   120
      Visible         =   0   'False
      Width           =   1455
      _ExtentX        =   2566
      _ExtentY        =   847
      ButtonWidth     =   609
      ButtonHeight    =   741
      Appearance      =   1
      _Version        =   393216
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
         Left            =   2400
         Picture         =   "FrmSafe_Report.frx":0000
         Style           =   1  '圖片外觀
         TabIndex        =   12
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
         Height          =   345
         Left            =   1080
         Picture         =   "FrmSafe_Report.frx":057B
         Style           =   1  '圖片外觀
         TabIndex        =   11
         ToolTipText     =   "離開"
         Top             =   0
         Width           =   450
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   0
      Top             =   2760
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
            Picture         =   "FrmSafe_Report.frx":0ABF
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":1B11
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":1E8C
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":2766
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":2ABB
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":3046
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":3360
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":43B2
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":471F
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":5771
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":67C3
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":6ADD
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":6DF7
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":7E49
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":8163
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":84B5
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmSafe_Report.frx":8807
            Key             =   ""
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmSafe_Report"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub CmdExit_Click()
Unload Me

End Sub

Private Sub CmdPView_Click()
 Label1.Caption = ""
 ExecuteSQL
End Sub
Private Sub Form_Load()
  'FormsAttribute
End Sub

Private Sub ExecuteSQL()
 
  'On Error Resume Next
  If rs1.State Then rs1.Close
'Sql = "SELECT GCODE,LCODE,DESCRIPTION FROM MATERMST where "
  sql = "SELECT M.GCODE AS GlobalCode ,M.LCODE AS LocalCode,M.Description,M.USystem,M.Safe_Qty,M.STOCK_QTY " & _
         " FROM MATERMST AS M" & _
         " where M.STOCK_QTY< M.Safe_Qty"

  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(0) <> "" Then
     sql_1 = sql_1 & " and M.GCODE >= '" & TxtGCode(0) & "' AND M.GCODE <= '" & TxtGCode(1) & "'"
  End If
  If TxtLCode(0) <> "" Then
     sql_1 = sql_1 & " and M.LCODE >= '" & TxtLCode(0) & "' AND M.LCODE <= '" & TxtLCode(1) & "'"
  End If
  
  If Trim(TxtUSystem.Text) <> "" Then
     sql_3 = " and M.USystem like '" & TxtUSystem.Text & "' "
  End If
  
  If sql_1 <> "" Or sql_2 <> "" Or sql_3 <> "" Or sql_4 <> "" Or sql_5 <> "" Then
     sql_r = (sql_1 & sql_2 & sql_3 & sql_4 & sql_5)
   '  sql_r = Right(sql_r, Len(sql_r) - 4)
  End If
  
  If sql_r <> "" Then
     sql = sql & sql_r
 ' Else
 '   Sql = Left(Sql, Len(Sql) - 6)
  End If
  sql = sql & " order by M.GCode"
  
  'Sql = " Select MATE_ID ,LOCATION,Mate_NAME,UNIT,STOCK_QTY,SAFE_QTY,USYSTEM,REMARK,create_date,USER_ID" & _
        " from MATERMST where Mate_ID LIKE '" & TxtMATE_ID & "%' AND Mate_NAME LIKE '" & TxtDESC & "%'" & _
        " AND LOCATION LIKE '" & TxtLOCATION & "%' AND UNIT LIKE '" & TxtUNIT & "%'" & _
        " AND USYSTEM LIKE '" & TxtUSystem & "%'" & _
        "order by Mate_ID "
  Debug.Print sql
  rs1.Open sql, conn, adOpenDynamic, adLockPessimistic

  
  Print rs1.RecordCount
 ' Label3.Caption = "筆數：" & rs1.RecordCount & " 筆"
 ' Set DataGrid1.DataSource = rs1
  Label1.Caption = "搜尋筆數：" & rs1.RecordCount & " 筆"
  If rs1.RecordCount > 0 Then
     Set DR_3.DataSource = rs1
     DR_3.Sections("section2").Controls("Lab_Dt").Caption = Date
     DR_3.Show
  End If
End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

If TxtGCode(1) = "" Then TxtGCode(1).Text = TxtGCode(0).Text
If TxtLCode(1) = "" Then TxtLCode(1).Text = TxtLCode(0).Text

Select Case Button.Key

   Case Is = "print"
           Label1.Caption = ""
           ExecuteSQL
   Case Is = "cancel"
           Call FieldClear
   Case Is = "exit"
           Unload Me
End Select
End Sub


Public Sub FieldClear()
  TxtGCode(0) = "": TxtGCode(1) = ""
  TxtLCode(0) = "": TxtLCode(1) = ""
  TxtUSystem = ""

End Sub
