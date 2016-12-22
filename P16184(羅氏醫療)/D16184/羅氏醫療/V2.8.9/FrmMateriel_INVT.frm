VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form FrmMateriel_INVT 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '單線固定
   Caption         =   "物料盤點"
   ClientHeight    =   7755
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   11700
   ControlBox      =   0   'False
   FillStyle       =   0  '實心
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   7755
   ScaleMode       =   0  '使用者自訂
   ScaleWidth      =   11700
   WhatsThisHelp   =   -1  'True
   Begin VB.Frame Frame1 
      Height          =   6735
      Left            =   0
      TabIndex        =   1
      Top             =   960
      Width           =   11655
      Begin VB.Frame FrmCmd 
         BackColor       =   &H80000004&
         BorderStyle     =   0  '沒有框線
         Height          =   1300
         Left            =   360
         TabIndex        =   2
         Top             =   5400
         Visible         =   0   'False
         Width           =   10215
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
            Left            =   8760
            Picture         =   "FrmMateriel_INVT.frx":0000
            Style           =   1  '圖片外觀
            TabIndex        =   9
            Top             =   360
            Visible         =   0   'False
            Width           =   1335
         End
         Begin VB.CommandButton CmdMakeInvt 
            BackColor       =   &H80000004&
            Caption         =   "產生主檔"
            BeginProperty Font 
               Name            =   "標楷體"
               Size            =   12
               Charset         =   136
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   732
            Left            =   480
            Picture         =   "FrmMateriel_INVT.frx":08CA
            Style           =   1  '圖片外觀
            TabIndex        =   8
            Top             =   360
            Visible         =   0   'False
            Width           =   1332
         End
         Begin VB.CommandButton Cmdinventory 
            BackColor       =   &H80000004&
            Caption         =   "盤點結轉"
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
            Left            =   4800
            Picture         =   "FrmMateriel_INVT.frx":170C
            Style           =   1  '圖片外觀
            TabIndex        =   7
            Top             =   360
            Visible         =   0   'False
            Width           =   1335
         End
         Begin VB.CommandButton CmdPView 
            BackColor       =   &H80000004&
            Caption         =   "列印盤差表"
            BeginProperty Font 
               Name            =   "標楷體"
               Size            =   12
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   735
            Left            =   6120
            Picture         =   "FrmMateriel_INVT.frx":254E
            Style           =   1  '圖片外觀
            TabIndex        =   6
            Top             =   360
            Visible         =   0   'False
            Width           =   1335
         End
         Begin VB.CommandButton Cmdimport 
            BackColor       =   &H80000004&
            Caption         =   "轉入盤點檔"
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
            Left            =   1920
            Picture         =   "FrmMateriel_INVT.frx":2858
            Style           =   1  '圖片外觀
            TabIndex        =   5
            Top             =   360
            Visible         =   0   'False
            Width           =   1332
         End
         Begin VB.CommandButton Cmdreplace 
            BackColor       =   &H80000004&
            Caption         =   "盤點重置"
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
            Left            =   3360
            Picture         =   "FrmMateriel_INVT.frx":369A
            Style           =   1  '圖片外觀
            TabIndex        =   4
            Top             =   360
            Visible         =   0   'False
            Width           =   1335
         End
         Begin VB.CommandButton CmdPLIST 
            BackColor       =   &H80000004&
            Caption         =   "列印盤點表"
            BeginProperty Font 
               Name            =   "標楷體"
               Size            =   12
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   735
            Left            =   7440
            Picture         =   "FrmMateriel_INVT.frx":44DC
            Style           =   1  '圖片外觀
            TabIndex        =   3
            Top             =   360
            Visible         =   0   'False
            Width           =   1335
         End
      End
      Begin MSDataGridLib.DataGrid DataGrid1 
         Height          =   2340
         Left            =   120
         TabIndex        =   10
         Top             =   600
         Width           =   11295
         _ExtentX        =   19923
         _ExtentY        =   4128
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
         Height          =   2055
         Left            =   120
         TabIndex        =   11
         Top             =   3300
         Width           =   11415
         _ExtentX        =   20135
         _ExtentY        =   3625
         _Version        =   393216
         AllowUpdate     =   -1  'True
         AllowArrows     =   -1  'True
         BackColor       =   16777215
         ForeColor       =   16711680
         HeadLines       =   1
         RowHeight       =   19
         TabAction       =   1
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
      Begin VB.Label Label3 
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
         Height          =   375
         Left            =   120
         TabIndex        =   13
         Top             =   240
         Width           =   1215
      End
      Begin VB.Label Label4 
         Caption         =   "物料盤點紀錄"
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
         Height          =   285
         Left            =   120
         TabIndex        =   12
         Top             =   3000
         Width           =   1815
      End
   End
   Begin VB.Timer Timer1 
      Left            =   12120
      Top             =   2640
   End
   Begin MSComctlLib.ImageList ImageList1 
      Left            =   120
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
            Picture         =   "FrmMateriel_INVT.frx":47E6
            Key             =   ""
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":5838
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":5BB3
            Key             =   ""
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":648D
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":67E2
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":6D6D
            Key             =   ""
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":7087
            Key             =   ""
         EndProperty
         BeginProperty ListImage8 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":80D9
            Key             =   "IMG7"
         EndProperty
         BeginProperty ListImage9 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":8446
            Key             =   ""
         EndProperty
         BeginProperty ListImage10 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":9498
            Key             =   ""
         EndProperty
         BeginProperty ListImage11 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":A4EA
            Key             =   ""
         EndProperty
         BeginProperty ListImage12 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":A804
            Key             =   ""
         EndProperty
         BeginProperty ListImage13 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":AB1E
            Key             =   "IMG8"
         EndProperty
         BeginProperty ListImage14 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":BB70
            Key             =   ""
         EndProperty
         BeginProperty ListImage15 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":BE8A
            Key             =   ""
         EndProperty
         BeginProperty ListImage16 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":C1DC
            Key             =   ""
         EndProperty
         BeginProperty ListImage17 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "FrmMateriel_INVT.frx":C52E
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.Toolbar Toolbar2 
      Height          =   825
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   5085
      _ExtentX        =   8969
      _ExtentY        =   1455
      ButtonWidth     =   1773
      ButtonHeight    =   1349
      Appearance      =   1
      ImageList       =   "ImageList1"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   5
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "產生主檔"
            Key             =   "create"
            ImageIndex      =   1
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "盤點結轉"
            Key             =   "trans"
            ImageIndex      =   2
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "列印盤差表"
            Key             =   "print1"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "列印盤點表"
            Key             =   "print2"
            ImageIndex      =   6
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "返回"
            Key             =   "exit"
            ImageIndex      =   13
         EndProperty
      EndProperty
   End
End
Attribute VB_Name = "FrmMateriel_INVT"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub CmdExit_Click()
  Unload Me
End Sub

Private Sub Cmdinventory_Click()
If MsgBox("之前舊的主檔將被刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
   Exit Sub
End If
If rs3.State Then rs3.Close
sql = "SELECT * from INVENMST order by GCode"
rs3.Open sql, conn, adOpenStatic, adLockReadOnly
    If Not rs3.EOF Then
        Do While Not rs3.EOF
        If rs4.State Then rs4.Close
        sql = "SELECT * from STOCKMST where gcode='" & rs3.Fields("gcode") & "' order by GCode"
        rs4.Open sql, conn, adOpenStatic, adLockOptimistic
        If Not rs4.EOF Then
           rs4.Fields("LAST_QTY") = Val(rs3.Fields("INVE_QTY")) - Val(rs3.Fields("ADJUST_QTY"))
           rs4.Fields("REAL_QTY") = 0
           rs4.Fields("IN_QTY") = 0
           rs4.Fields("OUT_QTY") = 0
           rs4.Fields("LOCATION") = rs3.Fields("LOCATION")
           rs4.Fields("USER_ID") = rs3.Fields("USER_ID")
           rs4.Fields("CREATE_DATE") = rs3.Fields("INVE_DATE")
           rs4.Update
        Else
           rs4.AddNew
           rs4.Fields("GCode") = rs3.Fields("GCode")
           rs4.Fields("LAST_QTY") = Val(rs3.Fields("INVE_QTY")) - Val(rs3.Fields("ADJUST_QTY"))
           rs4.Fields("REAL_QTY") = 0
           rs4.Fields("IN_QTY") = 0
           rs4.Fields("OUT_QTY") = 0
           rs4.Fields("LOCATION") = rs3.Fields("LOCATION")
           rs4.Fields("USER_ID") = rs3.Fields("USER_ID")
           rs4.Fields("CREATE_DATE") = rs3.Fields("INVE_DATE")
           rs4.Update
        End If
     
   rs3.MoveNext
   Loop
   End If
   MsgBox "主檔結轉完成!!"
End Sub

Private Sub CmdMakeInvt_Click()
If MsgBox("之前舊的主檔將被刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
   Exit Sub
End If
If rs3.State Then rs3.Close
   sql = "SELECT * from STOCKMST order by GCode"
   rs3.Open sql, conn, adOpenStatic, adLockReadOnly
   If rs4.State Then rs4.Close
   sql = "SELECT * from INVENMST "
   rs4.Open sql, conn, adOpenStatic, adLockOptimistic
   If Not rs4.EOF Then
      conn.Execute ("delete * from INVENMST")
   End If

   If Not rs3.EOF Then
      Do While Not rs3.EOF
         reg_last = rs3.Fields("LAST_QTY") + rs3.Fields("IN_QTY") - rs3.Fields("OUT_QTY")
         rs4.AddNew
         rs4.Fields("GCode") = rs3.Fields("GCode")
         rs4.Fields("Location") = rs3.Fields("Location")
         rs4.Fields("LAST_QTY") = reg_last
         rs4.Fields("INVE_QTY") = 0
         rs4.Fields("REAL_QTY") = 0
         rs4.Fields("ADJUST_QTY") = 0
         rs4.Update
         rs3.MoveNext
      Loop
   End If
   MsgBox "主檔轉檔完成!!"
End Sub

Private Sub DataGrid1_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
  ExecuteGrid2
End Sub

Private Sub DataGrid2_AfterColEdit(ByVal ColIndex As Integer)
  rs2.Requery
   Set DataGrid2.DataSource = rs2
   SETDG2
End Sub

Private Sub Form_Load()
  FormsAttribute
  ExecuteGrid1

  Call LoadState

  ' ExecuteGrid2
  'MDIMain.Tag = "盤點中"
  If MDIMain.Tag = "盤點中" Then
     Toolbar2.Buttons("create").Enabled = False
     Toolbar2.Buttons("trans").Enabled = True
  Else
     Toolbar2.Buttons("create").Enabled = True
     Toolbar2.Buttons("trans").Enabled = False
  End If
  
If rs2.State Then rs2.Close
  sql = "SELECT GCODE,LOCATION,REAL_QTY,INVE_QTY,ADJUST_QTY FROM INVENMST ORDER BY GCode"
  rs2.Open sql, conn, adOpenStatic, adLockReadOnly
  If Not rs2.EOF Then
     Toolbar2.Buttons("print1").Enabled = True
     Toolbar2.Buttons("print2").Enabled = True
  Else
     Toolbar2.Buttons("print1").Enabled = False
     Toolbar2.Buttons("print2").Enabled = False
  End If
Toolbar2.Buttons("exit").Enabled = True
End Sub

Private Sub ExecuteGrid1()
  If rs1.State Then rs1.Close
  sql = "SELECT M.GCODE AS GlobalCode,M.LCODE AS LocalCode,M.Description,M.USystem " & _
        "FROM MATERMST AS M ORDER BY M.GCODE"
  rs1.Open sql, conn, adOpenStatic, adLockReadOnly
  
  Set DataGrid1.DataSource = rs1
  
   SETDG1
End Sub

Private Sub ExecuteGrid2()
  If rs2.State Then rs2.Close
  sql = "SELECT GCODE,LOCATION,REAL_QTY,INVE_QTY,ADJUST_QTY FROM INVENMST where GCode='" & rs1.Fields("GlobalCode") & "' ORDER BY GCode"
  rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
 
  If Not rs2.EOF Then
     Set DataGrid2.DataSource = rs2
     
     SETDG2
  End If
End Sub

Public Sub SETDG2()
   
     
   DataGrid2.Columns(0).Width = 2350
   DataGrid2.Columns(1).Width = 1200
   DataGrid2.Columns(2).Width = 1400
   DataGrid2.Columns(3).Width = 1400
   DataGrid2.Columns(4).Width = 1400
   
   
   DataGrid2.Columns(0).Locked = True
   DataGrid2.Columns(1).Locked = True
   DataGrid2.Columns(2).Locked = True
   DataGrid2.Columns(3).Locked = True
   DataGrid2.Columns(4).Locked = False
   
   'Text1.Text = rs2.Fields("ADJUST_QTY")
   'Text1.SetFocus
End Sub

Public Sub SETDG1()
   DataGrid1.Columns(0).Width = 1500
   DataGrid1.Columns(1).Width = 1200
   DataGrid1.Columns(2).Width = 6500
   DataGrid1.Columns(3).Width = 1200

   'DataGrid2.Columns(4).Width = 80
End Sub

Private Sub Toolbar2_ButtonClick(ByVal Button As MSComctlLib.Button)
Select Case Button.Key
   Case Is = "create"
           
           If MsgBox("異常資料將再次轉入!!", vbExclamation + vbYesNo, "系統訊息") = vbYes Then
              Data_Sort
              If rs.State Then rs.Close
              sql = "SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%'"
'              Debug.Print Sql
              rs.Open sql, conn, adOpenDynamic, adLockPessimistic
              If rs.RecordCount > 0 Then
                 Response = MsgBox("仍有異常資料尚未處理完畢" & vbCrLf & "是否清除 轉檔異常資料 ？", vbYesNo + vbCritical)
                 If Response = vbYes Then   ' 若使用者按下 [是]。
                    Data_Clear
                 Else
                    MsgBox "停止盤點"
                    Exit Sub
                 End If
              End If
           Else
              Exit Sub
           End If
           
           If MsgBox("盤點主檔將被刪除!!", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
               Exit Sub
           End If
            
               sql = "update matermst set stock_qty = '0'"
               conn.Execute sql
               
               If rs3.State Then rs3.Close
               sql = "SELECT * from STOCKMST  order by GCode"
               rs3.Open sql, conn, adOpenStatic, adLockReadOnly
               
               If rs4.State Then rs4.Close
               sql = "SELECT GCODE,LOCATION,REAL_QTY,INVE_QTY,ADJUST_QTY from INVENMST "
               rs4.Open sql, conn, adOpenStatic, adLockOptimistic
               
               If Not rs4.EOF Then
                  conn.Execute ("delete * from INVENMST")
               End If
               If Not rs3.EOF Then
                  Do While Not rs3.EOF
                     REG = rs3.Fields("LAST_QTY") + rs3.Fields("IN_QTY") - rs3.Fields("OUT_QTY")
                     rs4.AddNew
                     rs4.Fields("GCode") = rs3.Fields("GCode")
                     rs4.Fields("Location") = rs3.Fields("Location")
                     rs4.Fields("REAL_QTY") = REG
                     rs4.Fields("INVE_QTY") = 0
                     rs4.Fields("ADJUST_QTY") = 0
                     rs4.Update
                     rs3.MoveNext
                  Loop
               End If
               MsgBox "主檔轉檔完成!!"
               Toolbar2.Buttons("create").Enabled = False
               Toolbar2.Buttons("trans").Enabled = True
               Toolbar2.Buttons("print1").Enabled = True
               Toolbar2.Buttons("print2").Enabled = True
               Toolbar2.Buttons("exit").Enabled = True
               MDIMain.Tag = "盤點中"
               Call WriteState
     Case Is = "trans"
                If MsgBox("執行盤點結轉,將過帳到商品主檔", vbExclamation + vbYesNo, "系統訊息") = vbNo Then
                   Exit Sub
                End If
                
                sql = "update matermst set stock_qty = '0' "
                conn.Execute sql
                
                If rs3.State Then rs3.Close
                sql = "SELECT * from INVENMST order by GCode"
                rs3.Open sql, conn, adOpenStatic, adLockReadOnly
                
                    If Not rs3.EOF Then
                        Do While Not rs3.EOF
                       
                        If rs4.State Then rs4.Close
                        If rs3.Fields("gcode") <> "" And rs3.Fields("location") <> "" Then
                           
                           sql = "SELECT * FROM STOCKMST Where GCode = '" & rs3.Fields("Gcode") & "' and LOCATION = '" & rs3.Fields("LOCATION") & "' ORDER BY GCode"
                           rs4.Open sql, conn, adOpenStatic, adLockOptimistic
                           If Not rs4.EOF Then
                              rs4.Fields("LAST_QTY") = Val(rs3.Fields("INVE_QTY")) + Val(rs3.Fields("ADJUST_QTY"))
                              rs4.Fields("REAL_QTY") = 0
                              rs4.Fields("IN_QTY") = 0
                              rs4.Fields("OUT_QTY") = 0
                              rs4.Fields("LOCATION") = rs3.Fields("LOCATION")
                              rs4.Fields("USER_ID") = rs3.Fields("USER_ID")
                              rs4.Fields("CREATE_DATE") = rs3.Fields("INVE_DATE")
                              rs4.Update
                              
                              sql = "update matermst set stock_qty = stock_qty + '" & rs4.Fields("last_qty") & "'  where gcode = '" & rs3.Fields("gcode") & "'"
                              conn.Execute sql, RAC
                              
                           Else
                              rs4.AddNew
                              rs4.Fields("GCode") = rs3.Fields("GCode")
                              rs4.Fields("LAST_QTY") = Val(rs3.Fields("INVE_QTY")) + Val(rs3.Fields("ADJUST_QTY"))
                              rs4.Fields("REAL_QTY") = 0
                              rs4.Fields("IN_QTY") = 0
                              rs4.Fields("OUT_QTY") = 0
                              rs4.Fields("LOCATION") = rs3.Fields("LOCATION")
                              rs4.Fields("USER_ID") = rs3.Fields("USER_ID")
                              rs4.Fields("CREATE_DATE") = rs3.Fields("INVE_DATE")
                              rs4.Update
                              sql = "SELECT * FROM MATERMST WHERE GCode" = rs4.Fields("GCode")
                              If sql <> "" Then
                              sql = "UPDATE MATERMST set STOCK_QTY =  '" & rs4.Fields("last_qty") & "'where gcode = '" & rs4.Fields("GCode") & "'"
                              sql = "UPDATE MATERMST set USER_ID =  '" & rs4.Fields("USER_ID") & "'where gcode = '" & rs4.Fields("GCode") & "'"
                              sql = "UPDATE MATERMST set CREATE_DATE=  '" & rs4.Fields("create_date") & "'where gcode = '" & rs4.Fields("GCode") & "'"
                                    conn.Execute sql
                              Else
                              sql = "INSERT INTO MATERMST(GCode,STOCK_QTY,USER_ID,CREATE_DATE) " & _
                                    "VALUES ('" & rs4.Fields("GCode") & "'," & rs4.Fields("last_qty") & ", '" & rs4.Fields("USER_ID") & "','" & rs4.Fields("create_date") & "')"
                                    conn.Execute sql
                              End If
                           End If
                           
                           
                           
                           
                         Else
      
                         '================================================================================================
                           Dim Fso As New FileSystemObject
                           Dim FileData As TextStream
                           
                           Set FileData = Fso.OpenTextFile(App.Path & "\" & "ERRLog_" & Month(Now) & "-" & Day(Now) & ".Log", ForAppending, True)
      
                              FileData.WriteLine "----------------------------------------------------------------------------"
                              FileData.WriteLine Now & "盤點轉結錯誤的資料為"
                              Select Case True
                              Case rs3.Fields("GCode") = ""
                                 FileData.WriteLine "沒有GCode" & vbTab & _
                                                    rs3.Fields("Location") & vbTab & _
                                                    rs3.Fields("LAST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_QTY") & vbTab & _
                                                    rs3.Fields("Real_QTY") & vbTab & _
                                                    rs3.Fields("ADJUST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_Date")
                              Case rs3.Fields("location") = ""
                                 FileData.WriteLine rs3.Fields("GCode") & vbTab & _
                                                    "沒有 Location" & vbTab & _
                                                    rs3.Fields("LAST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_QTY") & vbTab & _
                                                    rs3.Fields("Real_QTY") & vbTab & _
                                                    rs3.Fields("ADJUST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_Date")
                              Case Else
                                 FileData.WriteLine "沒有 GCode" & vbTab & _
                                                    "沒有 Location" & vbTab & _
                                                    rs3.Fields("LAST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_QTY") & vbTab & _
                                                    rs3.Fields("Real_QTY") & vbTab & _
                                                    rs3.Fields("ADJUST_QTY") & vbTab & _
                                                    rs3.Fields("INVE_Date")
                              End Select
                           FileData.WriteLine "----------------------------------------------------------------------------"
                           FileData.Close
                           Set FileData = Nothing
                           Set Fso = Nothing
                         '================================================================================================

                        End If
                   rs3.MoveNext
                   Loop
                   End If
                   MsgBox "主檔結轉完成!!"
                    Toolbar2.Buttons("create").Enabled = False
                    Toolbar2.Buttons("trans").Enabled = False
                    Toolbar2.Buttons("print1").Enabled = True
                    Toolbar2.Buttons("print2").Enabled = True
                    Toolbar2.Buttons("exit").Enabled = True
                    MDIMain.Tag = "未盤點"
                    Call WriteState
     Case Is = "print1"
                If rs3.State Then rs3.Close
                sql = "SELECT INVENMST.GCode as GlobalCode,INVENMST.Location,INVENMST.LAST_QTY,INVENMST.REAL_QTY,INVENMST.REAL_QTY-INVENMST.LAST_QTY as R1 ,MATERMST.LCode as LocalCode " & _
                      "from INVENMST,MATERMST " & _
                      "where MATERMST.GCode=INVENMST.GCode and INVENMST.REAL_QTY-INVENMST.LAST_QTY<>0 order by INVENMST.GCode"
               rs3.Open sql, conn, adOpenStatic, adLockReadOnly
               If rs3.RecordCount > 0 Then
                   Set DR_5.DataSource = rs3
                   DR_5.Sections("section2").Controls("Lab_Dt").Caption = Date
                   DR_5.Show
               Else
                  MsgBox "沒有盤差資料，可供列印！"
                  Exit Sub
               End If
     Case Is = "print2"
                If rs3.State Then rs3.Close
                sql = "SELECT INVENMST.GCode as GlobalCode,INVENMST.Location,INVENMST.LAST_QTY,INVENMST.INVE_QTY ,MATERMST.LCode as LocalCode,MATERMST.Description As Description from INVENMST,MATERMST where  MATERMST.GCode=INVENMST.GCode  order by INVENMST.GCode"
               rs3.Open sql, conn, adOpenStatic, adLockReadOnly
               If rs3.RecordCount > 0 Then
                   Set DR_6.DataSource = rs3
                   DR_6.Sections("section2").Controls("Lab_Dt").Caption = Date
                   DR_6.Show
               Else
                  MsgBox "沒有盤點資料，可供列印！"
                  Exit Sub
               End If
    Case Is = "exit"
             Unload Me
End Select

Exit Sub

INVT_Error_Log:
         
End Sub


Private Sub Data_Sort()

   If rs.State Then rs.Close
   
      sql = "SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%'"
      Debug.Print sql
  
      rs.Open sql, conn, adOpenDynamic, adLockPessimistic
  
      If rs.RecordCount <> 0 Then rs.MoveFirst
      
      While Not rs.EOF
      
'         For i = 0 To rs.Fields.Count - 1
'            Debug.Print i; rs.Fields(i)
'         Next i
         
         Select Case rs.Fields(4)
            Case "進貨":
               sql = "Update STOCKMST Set IN_QTY=IN_QTY+ " & Val(rs.Fields(2)) & " , REAL_QTY=LAST_QTY + (IN_QTY+ " & Val(rs.Fields(2)) & ") - OUT_QTY  where gcode='" & rs.Fields(0) & "' AND LOCATION='" & rs.Fields(1) & "' "
'              Sql = "Update STOCKMST Set IN_QTY=IN_QTY+ " &       Val(FLD(4)) & " , REAL_QTY=LAST_QTY + IN_QTY - (IN_QTY+ " &       Val(FLD(4)) & ")  where gcode='" &       FLD(2) & "' AND LOCATION='" &       FLD(1) & "' "
               Debug.Print sql
               conn.Execute sql, RAC
               If RAC = 0 Then
                  sql = "INSERT INTO STOCKMST(GCODE,LOCATION,IN_QTY,CREATE_DATE,USER_ID) " & _
                        "VALUES ('" & rs.Fields(0) & "' ,'" & rs.Fields(1) & "','" & Val(rs.Fields(2)) & "','" & rs.Fields(3) & "','" & rs.Fields(5) & "')"
                  conn.Execute sql
               End If
                  sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                  conn.Execute sql, RAC
                  rs.Delete
            
            
            Case "出貨":
                sql = "Update STOCKMST Set OUT_QTY = OUT_QTY+" & Val(rs.Fields(2)) & ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " & Val(rs.Fields(2)) & ") where GCODE='" & rs.Fields(0) & "'  and LOCATION = '" & rs.Fields(1) & "' "
               'Sql = "Update STOCKMST Set OUT_QTY = OUT_QTY+" & Val(FLD(4))       & ", REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY + " & Val(FLD(4))       & ") where GCODE='" & FLD(2)       & "'  and LOCATION = '" & FLD(1) & "' "
               conn.Execute sql, RAC

               If RAC <> 0 Then
                  sql = "Update MATERMST Set Stock_QTY=Stock_QTY - " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                 'Sql = "Update MATERMST Set Stock_QTY=Stock_QTY - " & Val(FLD(4))       & " Where gcode='" & FLD(2) & "'"
                  conn.Execute sql, RAC
                  rs.Delete
               End If
            
            Case "退貨":
               sql = "Update STOCKMST Set OUT_QTY=OUT_QTY - " & Val(rs.Fields(2)) & " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " & Val(rs.Fields(2)) & ")  where gcode='" & rs.Fields(0) & "' AND LOCATION='" & rs.Fields(1) & "' "
              'Sql = "Update STOCKMST Set OUT_QTY=OUT_QTY - " & Val(FLD(4)) & " , REAL_QTY=LAST_QTY + IN_QTY - (OUT_QTY - " & Val(FLD(4)) & ")  where gcode='" & FLD(2) & "' AND LOCATION='" & FLD(1) & "' "
               conn.Execute sql, RAC
               If RAC <> 0 Then
                  sql = "Update MATERMST Set Stock_QTY=Stock_QTY + " & Val(rs.Fields(2)) & " Where gcode='" & rs.Fields(0) & "'"
                 'Sql = "Update MATERMST Set Stock_QTY=Stock_QTY+ " & Val(FLD(4))        & " Where gcode='" & FLD(2) & "'"
                  conn.Execute sql, RAC
                  rs.Delete
               End If
            
         End Select
        
         rs.MoveNext
      
      Wend
   
    
End Sub

Private Sub Data_Clear()

   If rs.State Then rs.Close
   
      sql = "SELECT *  FROM ERRDATA WHERE  STATUS LIKE '%'"
      Debug.Print sql
  
      rs.Open sql, conn, adOpenDynamic, adLockPessimistic
  
      If rs.RecordCount <> 0 Then rs.MoveFirst
      
      While Not rs.EOF
         rs.Delete
         rs.MoveNext
      Wend
End Sub

Private Sub LoadState()
   '================================================================================================
     Dim Fso As New FileSystemObject
     Dim FileData As TextStream
     
     Set FileData = Fso.OpenTextFile(App.Path & "\" & "StateLog.Log", ForReading, True)
        MDIMain.Tag = FileData.ReadLine
     FileData.Close
     Set FileData = Nothing
     Set Fso = Nothing
   '================================================================================================
End Sub

Private Sub WriteState()
   '================================================================================================
     Dim Fso As New FileSystemObject
     Dim FileData As TextStream
     
     Set FileData = Fso.OpenTextFile(App.Path & "\" & "StateLog.Log", ForWriting, True)
        FileData.WriteLine MDIMain.Tag
     FileData.Close
     Set FileData = Nothing
     Set Fso = Nothing
   '================================================================================================
End Sub
