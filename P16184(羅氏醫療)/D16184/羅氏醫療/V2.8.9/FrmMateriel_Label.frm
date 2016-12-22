VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "mscomctl.ocx"
Begin VB.Form FrmMateriel_Label 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "物料標籤列印"
   ClientHeight    =   7890
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11850
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7890
   ScaleWidth      =   11850
   WhatsThisHelp   =   -1  'True
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   15
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
   Begin MSDataGridLib.DataGrid DataGrid1 
      Height          =   4575
      Left            =   0
      TabIndex        =   13
      Top             =   3240
      Width           =   11775
      _ExtentX        =   20770
      _ExtentY        =   8070
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
      Height          =   2295
      Left            =   0
      TabIndex        =   0
      Top             =   840
      Width           =   11775
      Begin VB.TextBox TxtQty 
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
         Left            =   9000
         TabIndex        =   18
         Text            =   "1"
         Top             =   1800
         Width           =   495
      End
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
         Left            =   1440
         MaxLength       =   15
         TabIndex        =   16
         Top             =   1800
         Width           =   5895
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
         MaxLength       =   20
         TabIndex        =   8
         Top             =   360
         Width           =   2655
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
         Left            =   4680
         MaxLength       =   20
         TabIndex        =   4
         Top             =   360
         Width           =   2655
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
         Top             =   840
         Width           =   2655
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
         Left            =   4680
         MaxLength       =   15
         TabIndex        =   2
         Top             =   840
         Width           =   2655
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
         TabIndex        =   1
         Top             =   1320
         Width           =   5895
      End
      Begin VB.Label Label3 
         Caption         =   "列印數量:"
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
         Left            =   7920
         TabIndex        =   19
         Top             =   1800
         Width           =   1095
      End
      Begin VB.Label Label1 
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
         Left            =   240
         TabIndex        =   17
         Top             =   1800
         Width           =   1215
      End
      Begin VB.Line Line3 
         BorderWidth     =   3
         X1              =   4200
         X2              =   4560
         Y1              =   480
         Y2              =   480
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
         TabIndex        =   7
         Top             =   360
         Width           =   1335
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
         TabIndex        =   6
         Top             =   840
         Width           =   1215
      End
      Begin VB.Line Line2 
         BorderWidth     =   3
         X1              =   4200
         X2              =   4560
         Y1              =   960
         Y2              =   960
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
         TabIndex        =   5
         Top             =   1320
         Width           =   975
      End
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   480
      Left            =   2880
      TabIndex        =   9
      Top             =   120
      Visible         =   0   'False
      Width           =   1935
      _ExtentX        =   3413
      _ExtentY        =   847
      ButtonWidth     =   609
      ButtonHeight    =   741
      Appearance      =   1
      _Version        =   393216
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
         Left            =   480
         Picture         =   "FrmMateriel_Label.frx":0000
         Style           =   1  '圖片外觀
         TabIndex        =   14
         Tag             =   "2"
         ToolTipText     =   "取消查詢"
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
         Picture         =   "FrmMateriel_Label.frx":0297
         Style           =   1  '圖片外觀
         TabIndex        =   12
         ToolTipText     =   "查詢"
         Top             =   0
         Width           =   450
      End
      Begin VB.CommandButton CmdPRT 
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
         Left            =   960
         Picture         =   "FrmMateriel_Label.frx":0786
         Style           =   1  '圖片外觀
         TabIndex        =   11
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
         Left            =   1440
         Picture         =   "FrmMateriel_Label.frx":0D01
         Style           =   1  '圖片外觀
         TabIndex        =   10
         ToolTipText     =   "離開"
         Top             =   0
         Width           =   450
      End
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   0
      Top             =   7680
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
            Picture         =   "FrmMateriel_Label.frx":1245
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":2297
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":2612
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":2EEC
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":3241
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":37CC
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":3AE6
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":4B38
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":4EA5
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":5EF7
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":6F49
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":7263
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":757D
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":85CF
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":88E9
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":8C3B
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_Label.frx":8F8D
            Key             =   ""
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmMateriel_Label"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub CmdClear_Click()
  TxtGCode(0) = ""
  TxtGCode(1) = ""
  TxtLCode(0) = ""
  TxtLCode(1) = ""
  TxtUSystem = ""
End Sub

Private Sub ExecuteSQL()
  If rs.State Then rs.Close
    
  sql = "SELECT GCODE,LCODE,DESCRIPTION,USYSTEM FROM MATERMST where "
  sql_1 = ""
  sql_2 = ""
  sql_3 = ""
  sql_4 = ""
  sql_5 = ""
  
  If TxtGCode(1) <> "" Or TxtLCode(1) <> "" Then
     If TxtGCode(0) <> "" Then
        sql_1 = sql_1 & "GCODE >= '" & TxtGCode(0) & "' AND GCODE <= '" & TxtGCode(1) & "'"
     End If
     If TxtLCode(0) <> "" Then
        If sql_1 <> "" Then
         sql_1 = sql_1 & " and "
        End If
        sql_1 = sql_1 & "LCODE >= '" & TxtLCode(0) & "' AND LCODE <= '" & TxtLCode(1) & "'"
     End If
     
'  Else
'     If TxtGCode(0) <> "" Then
'        sql_1 = sql_1 & "GCODE like '%" & TxtGCode(0) & "%'"
'     End If
'     If TxtLCode(0) <> "" Then
'        sql_1 = sql_1 & "LCODE like '%" & TxtLCode(0) & "%'"
'     End If
  End If
  
'  If TxtUSystem <> "" Then
'     If sql_1 <> "" Then
'        sql_1 = sql_1 & " and "
'     End If
'  End If
  
  If TxtUSystem <> "" Then
     If sql_1 <> "" Then
        sql_1 = sql_1 & " and "
     End If
     sql_1 = sql_1 & " USystem like '%" & TxtUSystem.Text & "%' "
  End If
  
  If TxtDescription <> "" Then
     If sql_1 <> "" Then
        sql_1 = sql_1 & " and "
     End If
     sql_1 = sql_1 & " Description like '%" & TxtDescription.Text & "%' "
  End If
  
  If sql_1 <> "" Then
     sql_r = sql_1
  Else
   sql_r = sql_1 & " USystem like '%' "
  End If
    
  If sql_r <> "" Then
     sql = sql & sql_r
  End If
  
  sql = sql & " ORDER BY GCODE "
  Debug.Print sql
  rs.Open sql, conn, adOpenStatic, adLockReadOnly

  Set DataGrid1.DataSource = rs
  Call SETDG

End Sub


Private Sub CmdPRT_Click()
  
  Call ExecuteSQL
  
  While Not rs.EOF
  
   Call PrtLabel("" & rs.Fields(0), "" & rs.Fields(1), "" & Left(rs.Fields(2), 25), 1)
   rs.MoveNext
  Wend
  
End Sub

Private Sub DataGrid1_Click()
'   For ii = 1 To DataGrid1.Columns.Count
'      DataGrid1.Columns(ii).Caption
'   Next ii
'   MsgBox DataGrid1.ScrollBars
End Sub

Private Sub Form_Load()
  FormsAttribute
End Sub
Private Sub Form_Activate()
   
   Call FieldCls
   TxtGCode(0).SetFocus
   Toolbar2.Buttons("print").Enabled = False

End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~離開~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExit_Click()
   Unload Me
End Sub
Private Sub FieldCls()
  TxtGCode(0) = ""
  TxtGCode(1) = ""
  TxtLCode(0) = ""
  TxtLCode(1) = ""
End Sub

Public Sub PrtLabel(barcode As String, String_1 As String, String_2 As String, paper As Integer)
  
   StartPrint ("Epson LQ-1070C")
   FMT 65, 30, True                    '紙張大小
   'BOX 0, 0, 60, 40, 0, 5, 0          '測試 紙張大小
  
   Font_Name = "Arial": Font_Bold = True
   
   'barcode = "12345678901234567890"
   
   'CODE 39
   If Len(barcode) > 11 Then
      BFL 5, 5, 0, 1, 10, "*" & barcode & "*", 2, 5
   Else
      BFL 5, 5, 0, 1, 10, "*" & barcode & "*", 3, 7
   End If
     
   
   getBim 5, 16, barcode, 3, Word_Cnt(barcode, 2), True
   
   getBim 5, 21, String_1, 3, Word_Cnt(String_1, 2), True
   
   'Font_Name = "細明體": Font_Bold = True
   getBim 5, 25, String_2, 2.5, Word_Cnt(String_2, 1.8), True
   
   Prt (paper)
   EndPrint
   
End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)

If TxtGCode(1) = "" Then TxtGCode(1).Text = TxtGCode(0).Text
If TxtLCode(1) = "" Then TxtLCode(1).Text = TxtLCode(0).Text

Select Case Button.Key
               
     Case Is = "cancel"
            TxtGCode(0) = "": TxtGCode(1) = ""
            TxtLCode(0) = "": TxtLCode(1) = ""
            TxtUSystem = ""
            Set DataGrid1.DataSource = Nothing
                
     Case Is = "search"
            TxtStyle = "查  詢"
            ExecuteSQL
            Toolbar2.Buttons("print").Enabled = True
            
     Case Is = "print"
       'Call ExecuteSQL
                   
        While Not rs.EOF
           Call PrtLabel("" & rs.Fields(0), "" & rs.Fields(1), "" & Left(rs.Fields(2), 25), TxtQty)
           rs.MoveNext
        Wend
        Toolbar2.Buttons("print").Enabled = False

     Case Is = "exit"
          Unload Me
End Select
End Sub

Public Sub SETDG()
'   Label3.Caption = "筆數：" & rs.RecordCount & " 筆"
'   Set DataGrid1.DataSource = rs
   
   'For I = 0 To 16
   '   DataGrid1.Columns(I).Width = 2050
   '   DataGrid1.Columns(I).Locked = True
   'Next I

End Sub
