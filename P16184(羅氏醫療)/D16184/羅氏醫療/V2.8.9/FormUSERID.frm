VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Begin VB.Form FrmUSERID 
   BackColor       =   &H80000004&
   BorderStyle     =   1  '��u�T�w
   Caption         =   "�ϥΪ̱b���]�w"
   ClientHeight    =   6855
   ClientLeft      =   210
   ClientTop       =   -30
   ClientWidth     =   7410
   ControlBox      =   0   'False
   KeyPreview      =   -1  'True
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MDIChild        =   -1  'True
   MinButton       =   0   'False
   ScaleHeight     =   6855
   ScaleWidth      =   7410
   Begin MSDataGridLib.DataGrid DataGrid2 
      Height          =   3012
      Left            =   240
      TabIndex        =   16
      TabStop         =   0   'False
      Top             =   2160
      Width           =   6852
      _ExtentX        =   12091
      _ExtentY        =   5318
      _Version        =   393216
      BackColor       =   16777215
      ForeColor       =   16711680
      HeadLines       =   1
      RowHeight       =   19
      BeginProperty HeadFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "�s�ө���"
         Size            =   12
         Charset         =   136
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "�s�ө���"
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
            ColumnWidth     =   8.5887e5
         EndProperty
         BeginProperty Column01 
            ColumnWidth     =   8.5887e5
         EndProperty
      EndProperty
   End
   Begin VB.Frame Frm2 
      BackColor       =   &H80000004&
      BorderStyle     =   0  '�S���ؽu
      Enabled         =   0   'False
      Height          =   2172
      Left            =   240
      TabIndex        =   11
      Top             =   240
      Width           =   6735
      Begin VB.TextBox TxtUSER_GROUP 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   1440
         MaxLength       =   10
         TabIndex        =   20
         Top             =   1440
         Width           =   1695
      End
      Begin VB.TextBox TxtStyle 
         Alignment       =   2  '�m�����
         Appearance      =   0  '����
         BackColor       =   &H80000004&
         BorderStyle     =   0  '�S���ؽu
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   14.25
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000C0&
         Height          =   375
         Left            =   5160
         TabIndex        =   18
         Text            =   "�s  ��"
         Top             =   0
         Width           =   1575
      End
      Begin VB.TextBox TxtPASSWORD1 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         IMEMode         =   3  '�Ȥ�
         Left            =   1440
         MaxLength       =   6
         PasswordChar    =   "*"
         TabIndex        =   3
         Top             =   1080
         Width           =   1215
      End
      Begin VB.TextBox TxtUSER_ID 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   1440
         MaxLength       =   10
         TabIndex        =   0
         Top             =   0
         Width           =   1815
      End
      Begin VB.TextBox TxtUSER_NAME 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         Left            =   1440
         MaxLength       =   20
         TabIndex        =   1
         Top             =   360
         Width           =   2295
      End
      Begin VB.TextBox TxtPASSWORD 
         BackColor       =   &H00E0E0E0&
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   285
         IMEMode         =   3  '�Ȥ�
         Left            =   1440
         MaxLength       =   6
         PasswordChar    =   "*"
         TabIndex        =   2
         Top             =   720
         Width           =   1215
      End
      Begin VB.ComboBox CobGROUP 
         Height          =   300
         Left            =   1440
         TabIndex        =   21
         Top             =   1440
         Width           =   1935
      End
      Begin VB.Label Label5 
         BackColor       =   &H80000004&
         Caption         =   "���ݸs��:"
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   372
         Left            =   360
         TabIndex        =   19
         Top             =   1440
         Width           =   1092
      End
      Begin VB.Label Label1 
         BackColor       =   &H80000004&
         Caption         =   "�P�{�K�X:"
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   360
         TabIndex        =   17
         Top             =   1080
         Width           =   1215
      End
      Begin VB.Label Label2 
         BackColor       =   &H80000004&
         Caption         =   "�ϥΪ̥N�X:"
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   14
         Top             =   0
         Width           =   1335
      End
      Begin VB.Label Label3 
         BackColor       =   &H80000004&
         Caption         =   "�W     ��:"
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   480
         TabIndex        =   13
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label4 
         BackColor       =   &H80000004&
         Caption         =   "�K     �X:"
         BeginProperty Font 
            Name            =   "�s�ө���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   480
         TabIndex        =   12
         Top             =   720
         Width           =   975
      End
   End
   Begin VB.Frame FrmCmd 
      BackColor       =   &H80000004&
      BorderStyle     =   0  '�S���ؽu
      Height          =   1335
      Left            =   240
      TabIndex        =   15
      Top             =   5160
      Width           =   6855
      Begin VB.CommandButton CmdQuery 
         BackColor       =   &H80000004&
         Caption         =   "�d��"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   2880
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   7
         Top             =   360
         Width           =   1095
      End
      Begin VB.CommandButton CmdNew 
         BackColor       =   &H80000004&
         Caption         =   "�s�W"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   240
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   4
         Top             =   360
         Width           =   1095
      End
      Begin VB.CommandButton CmdExit 
         BackColor       =   &H80000004&
         Caption         =   "���}"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   5520
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   10
         Top             =   360
         Width           =   1095
      End
      Begin VB.CommandButton CmdUpdate 
         BackColor       =   &H80000004&
         Caption         =   "�ק�"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   1560
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   6
         Top             =   360
         Width           =   1095
      End
      Begin VB.CommandButton CmdDel 
         BackColor       =   &H80000004&
         Caption         =   "�R��"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   4200
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   8
         Top             =   360
         Width           =   1095
      End
      Begin VB.CommandButton CmdYes 
         BackColor       =   &H80000004&
         Caption         =   "�P�w"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   1080
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   5
         Tag             =   "2"
         Top             =   360
         Visible         =   0   'False
         Width           =   1575
      End
      Begin VB.CommandButton CmdNo 
         BackColor       =   &H80000004&
         Caption         =   "����"
         BeginProperty Font 
            Name            =   "�з���"
            Size            =   12
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   735
         Left            =   4200
         Style           =   1  '�Ϥ��~�[
         TabIndex        =   9
         Tag             =   "2"
         Top             =   360
         Visible         =   0   'False
         Width           =   1575
      End
      Begin VB.Shape Shape5 
         BorderWidth     =   3
         Height          =   975
         Left            =   120
         Top             =   240
         Width           =   6615
      End
      Begin VB.Shape Shape6 
         BorderColor     =   &H80000005&
         BorderWidth     =   6
         Height          =   975
         Left            =   120
         Top             =   240
         Width           =   6615
      End
      Begin VB.Shape Shape7 
         BorderColor     =   &H00808080&
         BorderWidth     =   9
         Height          =   975
         Left            =   120
         Top             =   240
         Width           =   6615
      End
   End
   Begin VB.Shape Shape9 
      Height          =   6492
      Left            =   120
      Top             =   120
      Width           =   7092
   End
   Begin VB.Shape Shape10 
      BorderColor     =   &H80000005&
      BorderWidth     =   2
      Height          =   6492
      Left            =   120
      Top             =   120
      Width           =   7092
   End
   Begin VB.Shape Shape11 
      BorderColor     =   &H00C0C0C0&
      BorderWidth     =   5
      Height          =   6492
      Left            =   120
      Top             =   120
      Width           =   7092
   End
End
Attribute VB_Name = "FrmUSERID"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim rs As New ADODB.Recordset
Attribute rs.VB_VarHelpID = -1
Dim rsTmp As New ADODB.Recordset
Attribute rsTmp.VB_VarHelpID = -1
Dim flag As Integer  'flag=1 �s�W  flag=2�ק� flag=3�d��

Private Sub CobLEVEL_KeyPress(KeyAscii As Integer)
  KeyAscii = 0
End Sub


Private Sub CobGROUP_Click()
  TxtUSER_GROUP = CobGROUP.List(CobGROUP.ListIndex)
End Sub

Private Sub Form_Activate()
   If rs.State Then rs.Close
   If rsTmp.State Then rsTmp.Close
   DoEvents
   Sql = "SELECT USER_ID FROM USERMSD GROUP BY USER_ID "
   rsTmp.Open Sql, conn, adOpenStatic, adLockReadOnly
   While Not rsTmp.EOF
     CobGROUP.AddItem rsTmp.Fields(0)
     rsTmp.MoveNext
   Wend
   rsTmp.Close
   'rs.CursorLocation = adUseClient
   Sql = "select USER_ID AS �ϥΪ̥N��,USER_NAME AS �ϥΪ̦W��,USER_PASS AS �K�X,USER_GROUP from USERMST order by USER_ID"
   rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
   Call SETDG
   If rs.RecordCount = 0 Then Exit Sub
   Call GridToField
End Sub

Private Sub Form_Load()
   If App.PrevInstance Then End
   flag = 0
   Me.Top = 500
   Me.Left = 2000
   'Call OpenConn
End Sub

'========================================CmdControl=================================================
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�s�W~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdNew_Click()
   'If UCase(USER_ID) <> "ADMIN" Then
   '   MsgBox "�z�L�v���s�W���!", vbExclamation + vbYesNo, "�t�ΰT��"
   '   Exit Sub
   'End If
   If Me.MousePointer = 11 Then Exit Sub
   TxtStyle = "�s  �W"
   flag = 1
   Call UnLocked
   Call FieldClear
   TxtUSER_ID.BackColor = &H80000005
   TxtUSER_ID.Locked = False
   TxtUSER_ID.SetFocus
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�ק�~~~~~~~~~~~~~~~~~~~~~~~
Private Sub cmdUpdate_Click()
   If UCase(TxtUSER_ID) <> UCase(USER_ID) And UCase(USER_ID) <> "ADMIN" Then
      MsgBox "�z�L�v���ק惡�ϥΪ̤����!", vbExclamation + vbOKOnly, "�t�ΰT��"
      Exit Sub
   End If
   If Me.MousePointer = 11 Then Exit Sub
   If rs.RecordCount = 0 Then Exit Sub
   TxtStyle = "��  ��"
   flag = 2
   Call UnLocked
   TxtUSER_NAME.SetFocus
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�R��~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdDEL_Click()
   If rs.RecordCount = 0 Then Exit Sub
   If Me.MousePointer = 11 Then Exit Sub
   Me.MousePointer = 11
   If UCase(TxtUSER_ID) = "ADMIN" Then
      MsgBox "���ϥΪ̵L�k�R��!", vbExclamation + vbYesNo, "�t�ΰT��"
      Me.MousePointer = 4
      Exit Sub
   End If
   If MsgBox("�O�_�T�w�R��!!", vbExclamation + vbYesNo, "�t�ΰT��") = vbNo Then
      Me.MousePointer = 4
      Exit Sub
   End If
   On Error Resume Next
   rs.Delete
   Sql = "Delete from USERMSD where USER_ID='" & TxtUSER_ID & "'"
   conn.Execute Sql
   If rs.RecordCount = 0 Then
      Call FieldClear
      CmdUpDate.Enabled = False
      CmdQuery.Enabled = False
      CmdDel.Enabled = False
   End If
   Me.MousePointer = 4
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�d��~~~~~~~~~~~~~~~~~~~~~~~
Private Sub cmdQuery_Click()
   If Me.MousePointer = 11 Then Exit Sub
   If rs.RecordCount = 0 Then Exit Sub
   TxtStyle = "�d  ��"
   flag = 3
   Call UnLocked
   Call FieldClear
   CobLEVEL = ""
   TxtUSER_ID.BackColor = &H80000005
   TxtUSER_ID.Locked = False
   TxtUSER_ID.SetFocus
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�T�w~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdYes_Click()
   If Me.MousePointer = 11 Then Exit Sub
   If TxtUSER_ID = "" And flag <> 3 Then
      MsgBox "�п�J�ϥΪ̥N��!!", vbExclamation + vbOKOnly, "�t�ΰT��"
      TxtUSER_ID.SetFocus
      Exit Sub
   End If
   If TxtUSER_NAME = "" And flag <> 3 Then
      MsgBox "�п�J�ϥΪ̦W��!!", vbExclamation + vbOKOnly, "�t�ΰT��"
      TxtUSER_NAME.SetFocus
      Exit Sub
   End If
   If TxtPASSWORD <> TxtPASSWORD1 And flag <> 3 Then
      MsgBox "�K�X���~,�Э��s��J!!", vbExclamation + vbOKOnly, "�t�ΰT��"
      TxtPASSWORD1 = ""
      TxtPASSWORD.SetFocus
      TxtPASSWORD.SelStart = 0
      TxtPASSWORD.SelLength = Len(TxtPASSWORD)
      Exit Sub
   End If
   
   'On Error GoTo ERR1
   Err.Clear
   Select Case flag
       Case 1     '�s�W
          Sql = "INSERT INTO USERMST(USER_ID,USER_NAME,USER_PASS,USER_GROUP) VALUES ('" & TxtUSER_ID & "','" & TxtUSER_NAME & "','" & TxtPASSWORD & "','" & TxtUSER_GROUP & "' )"
          conn.Execute Sql
          If Len(TxtUSER_GROUP) = 0 Then
             Sql = "select * from PROGMREF order by PROG_ID"
             rsTmp.Open Sql, conn, adOpenDynamic, adLockPessimistic
             While Not rsTmp.EOF
               Sql = "INSERT INTO USERMSD(USER_ID,PROGRAM_ID,FLAG) VALUES ('" & TxtUSER_ID & "','" & rsTmp.Fields("PROG_ID") & "','0')"
               conn.Execute Sql
               rsTmp.MoveNext
             Wend
             rsTmp.Close
          End If
          rs.Requery: Call SETDG
          CmdUpDate.Enabled = True
          CmdQuery.Enabled = True
          CmdDel.Enabled = True
          Call FieldClear
          TxtUSER_ID.SetFocus
          Exit Sub
       Case 2     '�ק�
          iWait
          Call FieldToGrid
          i = 0
          On Error Resume Next
          Do While 1
             Err.Clear
             rs.Update
             If Err.Number <> 0 Then
                rs.CancelUpdate
                a = rs.Fields("num")
                NoMove = True
                rs.Requery
                SETDG
                rs.Find "num=" & a
                NoMove = False
                If rs.EOF Then
                   MsgBox "������Ƥw�Q�R��!!", vbExclamation + vbOKOnly, "�t�ΰT��"
                   rs.MoveFirst
                   Exit Do
                End If
                Call FieldToGrid
              Else
               Exit Do
             End If
          Loop
          If Len(TxtUSER_GROUP) = 0 Then
             Sql = "SELECT * FROM USERMSD WHERE USER_ID = '" & TxtUSER_ID & "'"
             rsTmp.Open Sql, conn, adOpenStatic, adLockReadOnly
             If rsTmp.RecordCount = 0 Then
                rsTmp.Close
                Sql = "select * from PROGMREF order by PROG_ID"
                rsTmp.Open Sql, conn, adOpenDynamic, adLockPessimistic
                While Not rsTmp.EOF
                  Sql = "INSERT INTO USERMSD(USER_ID,PROGRAM_ID,FLAG) VALUES ('" & TxtUSER_ID & "','" & rsTmp.Fields("PROG_ID") & "','0')"
                  conn.Execute Sql
                  rsTmp.MoveNext
                Wend
                rsTmp.Close
            End If
            rsTmp.Close
          End If
          Call eWait
       Case 3     '�d��
          If rs.State Then rs.Close
          If CobLEVEL = "" Then
             tmp = ""
            Else
             tmp = CobLEVEL.ListIndex
          End If
          Sql = "select USER_ID AS �ϥΪ̥N��,USER_NAME AS �ϥΪ̦W��,USER_PASS AS �K�X,USER_GROUP from USERMST where USER_ID LIKE '" & TxtUSER_ID & "%' AND USER_NAME LIKE '" & TxtUSER_NAME & "%' AND USER_PASS LIKE '" & TxtPASSWORD & "%' order by USER_ID"
          Call iWait
          rs.Open Sql, conn, adOpenKeyset, adLockPessimistic
          Call eWait
          If rs.RecordCount = 0 Then
             MsgBox "�d�L�ŦX���󤧸��!!", vbExclamation + vbOKOnly, "�t�ΰT��"
             rs.Close
             Exit Sub
          End If
          Call SETDG
          Call GridToField
          Call Locked
          'Call Locked1
          'CmdExitQuery.Visible = True
          Exit Sub
   End Select
   Call Locked
   Call GridToField
   Exit Sub

ERR1:
   Do While 1
      a = Err.Number
      If Err.Number <> 0 Then
         Select Case Err.Number
                'rs.CancelUpdate
             Case -2147217900, -2147217873
                MsgBox "�ϥΪ̥N������!!", vbExclamation + vbOKOnly, "�t�ΰT��"
                TxtUSER_ID.SetFocus
                TxtUSER_ID.SelStart = 0
                TxtUSER_ID.SelLength = Len(TxtUSER_ID)
                Err.Clear
                Exit Sub
              Case -2147217864
                a = a
              Case Else
                MsgBox Err.Number, vbExclamation + vbOKOnly, "�t�ΰT��"
                Err.Clear
         End Select
       Else
         Exit Do
      End If
   Loop
End Sub
'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~����~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdNo_Click()
   If Me.MousePointer = 11 Then Exit Sub
   Call FieldClear
   If flag = 1 Then
      iWait
      rs.Requery
      SETDG
      Call eWait
      'rs.MoveLast
   End If
   If flag = 3 Then
       iWait
       If rs.State Then rs.Close
       Sql = "select USER_ID AS �ϥΪ̥N��,USER_NAME AS �ϥΪ̦W��,USER_PASS AS �K�X,USER_GROUP from USERMST order by val(USER_ID)"
       rs.Open Sql, conn, adOpenDynamic, adLockPessimistic
       Call SETDG
       Call eWait
   End If
   Call GridToField
   Call Locked
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~�d�ߨ���~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExitQuery_Click()
   If Me.MousePointer = 11 Then Exit Sub
   rs.Close
   CmdExitQuery.Visible = False
   Sql = "select STATUS_ID AS �w�O,CLASS AS �x��,DESCRIPTION AS �x�컡�� from REFERENC"
   Call iWait
   rs.Open Sql, conn, adOpenKeyset, adLockPessimistic
   Call eWait
   DataGrid2.Row = 1
   Set DataGrid2.DataSource = rs
   Call CmdNo_Click
   rs.Close
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~���}~~~~~~~~~~~~~~~~~~~~~~~
Private Sub CmdExit_Click()
 ' FormExit
  Unload Me
End Sub

Private Sub Form_Unload(CANCEL As Integer)
   Set DataGrid2.DataSource = Nothing
   'conn.Close
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Sub~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
Public Sub Locked()
   TxtStyle = "�s  ��"
   CmdNew.Visible = True
   CmdUpDate.Visible = True
   CmdDel.Visible = True
   CmdQuery.Visible = True
   CmdEXIT.Visible = True
   Call Locked1
End Sub

Public Sub Locked1()
   CmdYes.Visible = False
   CmdNo.Visible = False
   Frm2.Enabled = False
   TxtUSER_ID.BackColor = &HE0E0E0
   TxtUSER_NAME.BackColor = &HE0E0E0
   TxtPASSWORD.BackColor = &HE0E0E0
   TxtPASSWORD1.BackColor = &HE0E0E0
   DataGrid2.Enabled = True
   flag = 0
End Sub

Public Sub UnLocked()
   CmdNew.Visible = False
   CmdUpDate.Visible = False
   CmdDel.Visible = False
   CmdQuery.Visible = False
   CmdEXIT.Visible = False
   CmdYes.Visible = True
   CmdNo.Visible = True
   Frm2.Enabled = True
   TxtUSER_NAME.BackColor = &H80000005
   TxtPASSWORD.BackColor = &H80000005
   TxtPASSWORD1.BackColor = &H80000005
   DataGrid2.Enabled = False
End Sub

Public Sub GridToField()
   Call FieldClear
   If rs.RecordCount = 0 Then Exit Sub
   TxtUSER_ID = "" & rs.Fields("�ϥΪ̥N��")
   TxtUSER_NAME = "" & rs.Fields("�ϥΪ̦W��")
   TxtPASSWORD = "" & rs.Fields("�K�X")
   TxtPASSWORD1 = "" & rs.Fields("�K�X")
   TxtUSER_GROUP = "" & rs.Fields("USER_GROUP")
End Sub

Public Sub FieldToGrid()
   rs.Fields("�ϥΪ̥N��") = TxtUSER_ID
   rs.Fields("�ϥΪ̦W��") = TxtUSER_NAME
   rs.Fields("�K�X") = TxtPASSWORD
   rs.Fields("USER_GROUP") = TxtUSER_GROUP
End Sub

Public Sub FieldClear()

   TxtUSER_ID = ""
   TxtUSER_NAME = ""
   TxtPASSWORD = ""
   TxtPASSWORD1 = ""
   TxtUSER_GROUP = ""
End Sub

Private Sub DataGrid2_RowColChange(LastRow As Variant, ByVal LastCol As Integer)
   If NoMove Then Exit Sub
   Call GridToField
End Sub

'~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`
Public Sub SETDG()
   Set DataGrid2.DataSource = rs
   DataGrid2.Columns(0).Width = 2000
   DataGrid2.Columns(1).Width = 3000
   DataGrid2.Columns(2).Width = 0
   DataGrid2.Columns(3).Width = 0
End Sub
Private Sub TxtPASSWORD_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{tab}"
End Sub

Private Sub TxtPASSWORD1_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{tab}"
End Sub

Private Sub TxtUSER_ID_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{tab}"
End Sub

Private Sub TxtUSER_NAME_KeyPress(KeyAscii As Integer)
   If KeyAscii = 13 Then SendKeys "{tab}"
End Sub
