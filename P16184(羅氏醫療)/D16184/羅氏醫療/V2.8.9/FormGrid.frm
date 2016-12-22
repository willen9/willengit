VERSION 5.00
Object = "{CDE57A40-8B86-11D0-B3C6-00A0C90AEA82}#1.0#0"; "MSDATGRD.OCX"
Begin VB.Form FormGrid 
   BackColor       =   &H00C0FFC0&
   BorderStyle     =   4  'Fixed ToolWindow
   ClientHeight    =   3825
   ClientLeft      =   15
   ClientTop       =   15
   ClientWidth     =   4530
   ControlBox      =   0   'False
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3825
   ScaleWidth      =   4530
   ShowInTaskbar   =   0   'False
   StartUpPosition =   1  'CenterOwner
   Begin VB.Frame Frame1 
      BackColor       =   &H00C0FFC0&
      Height          =   1095
      Left            =   0
      TabIndex        =   1
      Top             =   2650
      Width           =   4455
      Begin VB.CommandButton CmdYes 
         BackColor       =   &H00FFFFC0&
         Caption         =   "碓定"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   10.5
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   700
         Left            =   600
         Picture         =   "FormGrid.frx":0000
         Style           =   1  'Graphical
         TabIndex        =   3
         Tag             =   "2"
         Top             =   240
         Width           =   1215
      End
      Begin VB.CommandButton CmdNo 
         BackColor       =   &H00FFFFC0&
         Caption         =   "取消"
         BeginProperty Font 
            Name            =   "標楷體"
            Size            =   10.5
            Charset         =   136
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   700
         Left            =   2640
         Picture         =   "FormGrid.frx":08CA
         Style           =   1  'Graphical
         TabIndex        =   2
         Tag             =   "2"
         Top             =   240
         Width           =   1215
      End
      Begin VB.PictureBox Picture1 
         BackColor       =   &H0080C0FF&
         Height          =   1000
         Left            =   0
         ScaleHeight     =   945
         ScaleWidth      =   4395
         TabIndex        =   4
         Top             =   90
         Width           =   4455
         Begin VB.Line Line2 
            BorderColor     =   &H008080FF&
            BorderWidth     =   3
            X1              =   4500
            X2              =   4500
            Y1              =   0
            Y2              =   1200
         End
      End
   End
   Begin MSDataGridLib.DataGrid DataGrid1 
      Height          =   2535
      Left            =   0
      TabIndex        =   0
      Top             =   120
      Width           =   4455
      _ExtentX        =   7858
      _ExtentY        =   4471
      _Version        =   393216
      AllowUpdate     =   0   'False
      BackColor       =   12632319
      ForeColor       =   16711680
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
   Begin VB.Line Line3 
      BorderColor     =   &H8000000C&
      BorderWidth     =   3
      X1              =   4440
      X2              =   4440
      Y1              =   2745
      Y2              =   3745
   End
   Begin VB.Line Line1 
      BorderColor     =   &H8000000C&
      BorderWidth     =   3
      X1              =   0
      X2              =   4440
      Y1              =   3750
      Y2              =   3750
   End
End
Attribute VB_Name = "FormGrid"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub CmdNo_Click()
YesNo = False
Unload Me
End Sub

Private Sub CmdYes_Click()
YesNo = True
Unload Me
End Sub

Private Sub DataGrid1_DblClick()
YesNo = True
Unload Me
End Sub

