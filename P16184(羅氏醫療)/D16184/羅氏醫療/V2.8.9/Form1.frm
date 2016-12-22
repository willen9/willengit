VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   7740
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   13125
   LinkTopic       =   "Form1"
   MDIChild        =   -1  'True
   ScaleHeight     =   7740
   ScaleWidth      =   13125
   Begin VB.CommandButton Command2 
      Caption         =   "Command2"
      Height          =   975
      Left            =   4560
      TabIndex        =   3
      Top             =   6000
      Width           =   2295
   End
   Begin MSDataGridLib.DataGrid DataGrid2 
      Height          =   2295
      Left            =   360
      TabIndex        =   2
      Top             =   3360
      Width           =   10935
      _ExtentX        =   19288
      _ExtentY        =   4048
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
   Begin VB.CommandButton Command1 
      Caption         =   "Command1"
      Height          =   855
      Left            =   8280
      TabIndex        =   1
      Top             =   5880
      Width           =   2175
   End
   Begin MSDataGridLib.DataGrid DataGrid1 
      Height          =   2895
      Left            =   480
      TabIndex        =   0
      Top             =   120
      Width           =   10695
      _ExtentX        =   18865
      _ExtentY        =   5106
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
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()

  If rs2.State Then rs2.Close
  While Not rs1.EOF
      sql = "SELECT * FROM INVENMST where GCode='" & rs1.Fields("GCode") & "' and Location='" & rs1.Fields("Location") & "'" & " ORDER BY GCode"
      If rs2.State Then rs2.Close
      rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
      If rs2.RecordCount > 1 Then
      
'      sql = "SELECT * FROM INVENMST where GCode='" & rs1.Fields("GCode") & "' and Location='" & rs1.Fields("Location") & "'" & " ORDER BY GCode"
'      If rs3.State Then rs3.Close
'      rs3.Open sql, conn, adOpenDynamic, adLockOptimistic
'
'      If rs3.RecordCount = 1 Then
'           If rs4.State Then rs4.Clone
'               sql = "SELECT * from temp "
'               rs4.Open sql, conn, adOpenStatic, adLockOptimistic
'               rs4.AddNew
'               rs4.Fields("GCode") = rs1.Fields("GCode")
'               rs4.Fields("Location") = rs1.Fields("Location")
'               rs4.Update
      
      
      Set DataGrid2.DataSource = rs2
         '================================================================================================
             Dim Fso As New FileSystemObject
             Dim FileData As TextStream

             Set FileData = Fso.OpenTextFile(App.Path & "\" & "ERR_INVT.Log", ForAppending, True)
                FileData.WriteLine rs2.Fields("GCode") & vbTab & _
                                  rs2.Fields("Location") & vbTab & _
                                  rs2.Fields("LAST_QTY") & vbTab & _
                                  rs2.Fields("INVE_QTY") & vbTab & _
                                  rs2.Fields("Real_QTY") & vbTab & _
                                  rs2.Fields("ADJUST_QTY") & vbTab & _
                                  rs2.Fields("INVE_Date")
                FileData.Close
             Set FileData = Nothing
             Set Fso = Nothing
           '================================================================================================
      End If

'      End If
'      Set DataGrid1.DataSource = rs2
     rs1.MoveNext
  Wend
End Sub


Private Sub Command2_Click()
  If rs1.State Then rs1.Close

  sql = "SELECT * FROM temp" 'where GCode='" & rs1.Fields("GlobalCode") & "' ORDER BY GCode"
  rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
  Set DataGrid1.DataSource = rs1
  rs1.MoveFirst
  

  While Not rs1.EOF
'  If rs2.State Then rs2.Close
'  sql = "SELECT * FROM temp where GCode='" & rs1.Fields("GCode") & "' and Location='" & rs1.Fields("Location") & "' ORDER BY GCode"
'  rs2.Open sql, conn, adOpenDynamic, adLockOptimistic
''  Set DataGrid1.DataSource = rs2
'  If rs2.RecordCount > 1 Then
'     rs1.Delete
'  End If

  rs1.Fields(2) = 0
  rs1.Fields(3) = 0
  rs1.Fields(4) = 0
  rs1.Fields(5) = 0

  rs1.MoveNext
  Wend

End Sub

Private Sub Form_Load()
  If rs1.State Then rs1.Close

  sql = "SELECT GCode,Location,LAST_QTY,INVE_QTY,REAL_QTY,ADJUST_QTY FROM INVENMST" 'where GCode='" & rs1.Fields("GlobalCode") & "' ORDER BY GCode"
  rs1.Open sql, conn, adOpenDynamic, adLockOptimistic
  Set DataGrid1.DataSource = rs1
  
  rs1.MoveFirst
  

  
'  If rs1.State Then rs1.Close
'
'  Sql = "SELECT GCode,Location,LAST_QTY,INVE_QTY,REAL_QTY,ADJUST_QTY FROM INVENMST" 'where GCode='" & rs1.Fields("GlobalCode") & "' ORDER BY GCode"
'  rs1.Open Sql, conn, adOpenDynamic, adLockOptimistic
'  Set DataGrid1.DataSource = rs1
'  If rs2.RecordCount > 2 Then
'
'  End If
'  If Not rs2.EOF Then
'     Set DataGrid1.DataSource = rs2
'  End If
End Sub
