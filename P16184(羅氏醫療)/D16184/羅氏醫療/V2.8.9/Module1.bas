Attribute VB_Name = "Module1"
Public USER_ID As String
Public rs As New ADODB.Recordset
Public rs1 As New ADODB.Recordset
Public rs2 As New ADODB.Recordset
Public rs3 As New ADODB.Recordset
Public rs4 As New ADODB.Recordset
Public rsTmp As New ADODB.Recordset

Public rsprt As New ADODB.Recordset
'Public FrmMateriel_Q_rsprt As New ADODB.Recordset


Public conn As New ADODB.Connection
'Public Report As New CR�w�O�C�L
Public YesNo As Boolean
Public NoMove As Boolean 'Requery�ɬO�_����GridToField
Public First1 As Boolean '�O�_�Ĥ@���i�Jform
Public SysPath As String  'getch �t�θ��|
Public FileName As String 'FormDir �Ǧ^���ɦW
Public FilePath As String 'FormDir �Ǧ^�����|
Public times As Integer
Public Sub OpenConn()
If conn.State Then Exit Sub
MDB = SysPath & "RGSN.MDB"
connstr = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
          "Data Source= '" & MDB & "'"
conn.CursorLocation = adUseClient
conn.Open connstr
End Sub
Public Sub OpenLCn()
If connL.State Then connL.Close
connstr = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
           "Data source=LREGAL.mdb"
connL.CursorLocation = adUseServer
connL.Open connstr
End Sub

Public Function Todayf()
a = Val(Year(Date)) - 1911
b = Month(Date)
c = Day(Date)
a = Format(a, "00")
b = Format(b, "00")
c = Format(c, "00")
Todayf = a & b & c
End Function
Public Sub FormsAttribute()
  Forms(1).Top = 0
  Forms(1).Left = 0
  Forms(1).Height = 8300
  Forms(1).Width = 11940
  'Forms(1).ShowMsg.ZOrder 0
  
End Sub
Public Sub CloseAll()
  If Forms.Count() > 1 Then Unload Forms(1)
End Sub
