VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Begin VB.Form Form2 
   Caption         =   "Form2"
   ClientHeight    =   7290
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   11640
   LinkTopic       =   "Form2"
   ScaleHeight     =   7290
   ScaleWidth      =   11640
   Begin VB.CommandButton STO_Cmd1 
      Caption         =   "刪除 STOCKMST GCode 重複資料"
      Height          =   735
      Left            =   9840
      TabIndex        =   3
      Top             =   3840
      Width           =   1575
   End
   Begin VB.CommandButton STO_Cmd2 
      Caption         =   "列出 STOCKMST GCode 重複資料"
      Height          =   735
      Left            =   7800
      TabIndex        =   2
      Top             =   3840
      Width           =   1815
   End
   Begin VB.CommandButton INV_Cmd2 
      Caption         =   "列出 INVENMST GCode 重複資料"
      Height          =   735
      Left            =   7800
      TabIndex        =   1
      Top             =   1560
      Width           =   1815
   End
   Begin VB.CommandButton INV_Cmd1 
      Caption         =   "刪除 INVENMST GCode 重複資料"
      Height          =   735
      Left            =   9840
      TabIndex        =   0
      Top             =   1560
      Width           =   1575
   End
   Begin MSDataGridLib.DataGrid DataGrid1 
      Height          =   1215
      Left            =   120
      TabIndex        =   4
      Top             =   240
      Width           =   11415
      _ExtentX        =   20135
      _ExtentY        =   2143
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
   Begin MSDataGridLib.DataGrid DataGrid2 
      Height          =   1215
      Left            =   120
      TabIndex        =   5
      Top             =   2520
      Width           =   11415
      _ExtentX        =   20135
      _ExtentY        =   2143
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
End
Attribute VB_Name = "Form2"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub INV_Cmd1_Click()

   If rs1.State Then rs1.Close
   sql = "SELECT * FROM INVENMST ORDER BY GCode"
   rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
   Set DataGrid1.DataSource = rs1
   
   rs1.MoveFirst
      
   While rs1.EOF <> True
   
   If rs2.State Then rs2.Close
   sql = "SELECT * FROM INVENMST Where GCode = '" & rs1.Fields("Gcode") & "' ORDER BY GCode"
   rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
   If rs2.RecordCount > 1 Then

''   ===========================================================================================
'   Dim Fso As New FileSystemObject
'   Dim FileData As TextStream
'
'   Set FileData = Fso.OpenTextFile(App.Path & "\MY.Log", ForAppending, True)
'
''   FileData.WriteLine "----------------------------------------------------------------------------"
''   FileData.WriteLine Now & "盤點轉結錯誤的資料為"
'   FileData.WriteLine rs2.Fields("GCode") & vbTab & _
'                      rs2.Fields("Location") & vbTab & _
'                      rs2.Fields("LAST_QTY") & vbTab & _
'                      rs2.Fields("INVE_QTY") & vbTab & _
'                      rs2.Fields("Real_QTY") & vbTab & _
'                      rs2.Fields("ADJUST_QTY") & vbTab & _
'                      rs2.Fields("INVE_Date")
''   FileData.WriteLine "----------------------------------------------------------------------------"
'   FileData.Close
'   Set FileData = Nothing
'   Set Fso = Nothing
'   '================================================================================================
   rs2.MoveFirst
   rs2.MoveNext
   rs2.Delete
   rs2.Update

   End If
   
   rs1.MoveNext
   Wend
End Sub

Private Sub INV_Cmd2_Click()

   If rs1.State Then rs1.Close
   sql = "SELECT * FROM INVENMST ORDER BY GCode"
   rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
   Set DataGrid1.DataSource = rs1
   
   rs1.MoveFirst
   
   
   While rs1.EOF <> True
   
   If rs2.State Then rs2.Close
   sql = "SELECT * FROM INVENMST Where GCode = '" & rs1.Fields("Gcode") & "' and Location = '" & rs1.Fields("Location") & "' ORDER BY GCode"
   rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
   If rs2.RecordCount > 1 Then

'   ===========================================================================================
   Dim Fso As New FileSystemObject
   Dim FileData As TextStream

   Set FileData = Fso.OpenTextFile(App.Path & "\MY_INVENMST.Log", ForAppending, True)

'   FileData.WriteLine "----------------------------------------------------------------------------"
'   FileData.WriteLine Now & "盤點轉結錯誤的資料為"
   FileData.WriteLine rs2.Fields("GCode") & vbTab & _
                      rs2.Fields("Location") & vbTab & _
                      rs2.Fields("LAST_QTY") & vbTab & _
                      rs2.Fields("INVE_QTY") & vbTab & _
                      rs2.Fields("Real_QTY") & vbTab & _
                      rs2.Fields("ADJUST_QTY") & vbTab & _
                      rs2.Fields("INVE_Date")
'   FileData.WriteLine "----------------------------------------------------------------------------"
   FileData.Close
   Set FileData = Nothing
   Set Fso = Nothing
   '================================================================================================
'   rs2.MoveFirst
'   rs2.MoveNext
'   rs2.Delete
'   rs2.Update

   End If
   
   rs1.MoveNext
   Wend
End Sub

Private Sub STO_Cmd2_Click()


   If rs1.State Then rs1.Close
   sql = "SELECT * FROM STOCKMST ORDER BY GCode"
   rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
   Set DataGrid2.DataSource = rs1
   
   rs1.MoveFirst
   
   
   While rs1.EOF <> True
   
   If rs2.State Then rs2.Close
   sql = "SELECT * FROM STOCKMST Where GCode = '" & rs1.Fields("Gcode") & "' and LOCATION = '" & rs1.Fields("LOCATION") & "' ORDER BY GCode"
   rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
   If rs2.RecordCount > 1 Then

'   ===========================================================================================
   Dim Fso As New FileSystemObject
   Dim FileData As TextStream

   Set FileData = Fso.OpenTextFile(App.Path & "\MY_STOCKMST.Log", ForAppending, True)

'   FileData.WriteLine "----------------------------------------------------------------------------"
'   FileData.WriteLine Now & "盤點轉結錯誤的資料為"
   FileData.WriteLine rs2.Fields("GCode") & vbTab & _
                      rs2.Fields("Location") & vbTab & _
                      rs2.Fields("LAST_QTY") & vbTab & _
                      rs2.Fields("Real_QTY") & vbTab & _
                      rs2.Fields("IN_QTY") & vbTab & _
                      rs2.Fields("OUT_QTY") & vbTab & _
                      rs2.Fields("USER_ID") & vbTab & _
                      rs2.Fields("CREATE_DATE")
'   FileData.WriteLine "----------------------------------------------------------------------------"
   FileData.Close
   Set FileData = Nothing
   Set Fso = Nothing
   '================================================================================================
'   rs2.MoveFirst
'   rs2.MoveNext
'   rs2.Delete
'   rs2.Update

   End If
   
   rs1.MoveNext
   Wend
End Sub

Private Sub STO_Cmd1_Click()

   If rs1.State Then rs1.Close
   sql = "SELECT * FROM STOCKMST ORDER BY GCode"
   rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
   Set DataGrid2.DataSource = rs1
   
   rs1.MoveFirst
   
   
   While rs1.EOF <> True
   
   If rs2.State Then rs2.Close
   sql = "SELECT * FROM STOCKMST Where GCode = '" & rs1.Fields("Gcode") & "' and LOCATION = '" & rs1.Fields("LOCATION") & "' ORDER BY GCode"
   rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
   If rs2.RecordCount > 1 Then

''   ===========================================================================================
'   Dim Fso As New FileSystemObject
'   Dim FileData As TextStream
'
'   Set FileData = Fso.OpenTextFile(App.Path & "\MY.Log", ForAppending, True)
'
''   FileData.WriteLine "----------------------------------------------------------------------------"
''   FileData.WriteLine Now & "盤點轉結錯誤的資料為"
'   FileData.WriteLine rs2.Fields("GCode") & vbTab & _
'                      rs2.Fields("Location") & vbTab & _
'                      rs2.Fields("LAST_QTY") & vbTab & _
'                      rs2.Fields("Real_QTY") & vbTab & _
'                      rs2.Fields("IN_QTY") & vbTab & _
'                      rs2.Fields("OUT_QTY") & vbTab & _
'                      rs2.Fields("USER_ID") & vbTab & _
'                      rs2.Fields("CREATE_DATE")
''   FileData.WriteLine "----------------------------------------------------------------------------"
'   FileData.Close
'   Set FileData = Nothing
'   Set Fso = Nothing
'   '================================================================================================
   rs2.MoveFirst
   rs2.MoveNext
   rs2.Delete
   rs2.Update

   End If
   
   rs1.MoveNext
   Wend
End Sub

Private Sub Form_Load()
   If conn.State Then conn.Close
   MDB = SysPath & "RGSN.MDB"
   connstr = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
             "Data Source= '" & MDB & "'"
   conn.CursorLocation = adUseClient
   conn.Open connstr
End Sub


