Attribute VB_Name = "ring_mou"
'Option Base 1

Public PrintId As Long
Public Ret As Long
Public DataByte() As Byte
Public Ringout As String
Public id As Integer
Public AutoId As Boolean
Public Font_Name As String
Public Font_Bold As Boolean


Private Type NOTIFYICONDATA
    cbSize As Long
    hWnd As Long
    uId As Long
    uFlags As Long
    ucallbackMessage As Long
    hIcon As Long
    szTip As String * 64
End Type

Private Const NIM_ADD = &H0
Private Const NIM_MODIFY = &H1
Private Const NIM_DELETE = &H2
Private Const WM_MOUSEMOVE = &H200
Private Const NIF_MESSAGE = &H1
Private Const NIF_ICON = &H2
Private Const NIF_TIP = &H4

Private Declare Function Shell_NotifyIcon Lib "shell32" Alias "Shell_NotifyIconA" (ByVal dwMessage As Long, pnid As NOTIFYICONDATA) As Boolean
Public Declare Sub PatBlt Lib "gdi32" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long, ByVal nWidth As Long, ByVal nHeight As Long, ByVal dwRop As Long)
Public Declare Function GetPixel Lib "gdi32" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long) As Long
Public Declare Sub openprinter Lib "winspool.drv" Alias "OpenPrinterA" (ByVal pPrinterName As String, phPrinter As Long, pDefault As PRINTER_DEFAULTS)
Public Declare Sub EndDocPrinter Lib "winspool.drv" (ByVal hPrinter As Long)
Public Declare Sub StartDocPrinter Lib "winspool.drv" Alias "StartDocPrinterA" (ByVal hPrinter As Long, ByVal Level As Long, pDocInfo As DOC_INFO_2)
Public Declare Sub ClosePrinter Lib "winspool.drv" (ByVal hPrinter As Long)
Public Declare Sub WritePrinter Lib "winspool.drv" (ByVal hPrinter As Long, ByRef pBuf As Any, ByVal cdBuf As Long, pcWritten As Long)
Public Declare Function OSWinHelp% Lib "user32" Alias "WinHelpA" (ByVal hWnd&, ByVal HelpFile$, ByVal wCommand%, dwData As Any)
Declare Function CreateBitmap Lib "gdi32" (ByVal nWidth As Long, ByVal nHeight As Long, ByVal nPlanes As Long, ByVal nBitCount As Long, lpBits As Any) As Long
Declare Function CreateCompatibleBitmap Lib "gdi32" (ByVal hdc As Long, ByVal nWidth As Long, ByVal nHeight As Long) As Long
Declare Function CreateCompatibleDC Lib "gdi32" (ByVal hdc As Long) As Long
Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, lpInitData As DEVMODE) As Long
Declare Function CreateFont Lib "gdi32" Alias "CreateFontA" (ByVal H As Long, ByVal w As Long, ByVal E As Long, ByVal O As Long, ByVal w As Long, ByVal I As Long, ByVal u As Long, ByVal S As Long, ByVal c As Long, ByVal OP As Long, ByVal CP As Long, ByVal Q As Long, ByVal PAF As Long, ByVal F As String) As Long
Declare Function CreateFontIndirect Lib "gdi32" Alias "CreateFontIndirectA" (lpLogFont As LOGFONT) As Long
Declare Function GetBitmapDimensionEx Lib "gdi32" (ByVal hBitmap As Long, lpDimension As Size) As Long
Declare Function GetDC Lib "user32" (ByVal hWnd As Long) As Long
Declare Function GetDCOrgEx Lib "gdi32" (ByVal hdc As Long, lpPoint As POINTAPI) As Long
Declare Function GetDIBits Lib "gdi32" (ByVal aHDC As Long, ByVal hBitmap As Long, ByVal nStartScan As Long, ByVal nNumScans As Long, lpBits As Any, lpBI As BITMAPINFO, ByVal wUsage As Long) As Long
Declare Function GetObject Lib "gdi32" Alias "GetObjectA" (ByVal hObject As Long, ByVal nCount As Long, lpObject As Any) As Long
Declare Function DeleteDC Lib "gdi32" (ByVal hdc As Long) As Long
Declare Function DeleteObject Lib "gdi32" (ByVal hObject As Long) As Long
Declare Sub DrawText Lib "user32" Alias "DrawTextA" (ByVal hdc As Long, ByVal lpStr As String, ByVal nCount As Long, lpRect As RECT, ByVal wFormat As Long)
Declare Sub DrawTextEx Lib "user32" Alias "DrawTextExA" (ByVal hdc As Long, ByVal lpsz As String, ByVal n As Long, lpRect As RECT, ByVal un As Long, lpDrawTextParams As Any)
Declare Sub FloodFill Lib "gdi32" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long, ByVal crColor As Long)
Declare Function GetBitmapBits Lib "gdi32" (ByVal hBitmap As Long, ByVal dwCount As Long, lpBits As Any) As Long
Declare Sub GetTextExtentPoint32 Lib "gdi32" Alias "GetTextExtentPoint32A" (ByVal hdc As Long, ByVal lpsz As String, ByVal cbString As Long, lpSize As Size)
Declare Sub LoadBitmap Lib "user32" Alias "LoadBitmapA" (ByVal hInstance As Long, ByVal lpBitmapName As String)
Declare Sub Rectangle Lib "gdi32" (ByVal hdc As Long, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long)
Declare Function ReleaseDC Lib "user32" (ByVal hWnd As Long, ByVal hdc As Long) As Long
Declare Sub RtlMoveMemory Lib "KERNEL32" (lpvDest As Any, lpvSource As Any, ByVal cbCopy As Long)
Declare Sub SelectObject Lib "gdi32" (ByVal hdc As Long, ByVal hObject As Long)
Declare Sub SetBkColor Lib "gdi32" (ByVal hdc As Long, ByVal crColor As Long)
Declare Sub SetTextColor Lib "gdi32" (ByVal hdc As Long, ByVal crColor As Long)
Declare Function SetWorldTransform Lib "gdi32" (ByVal hdc As Long, lpXform As XFORM) As Long
Declare Sub StretchBlt Lib "gdi32" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long, ByVal nWidth As Long, ByVal nHeight As Long, ByVal hSrcDC As Long, ByVal xSrc As Long, ByVal ySrc As Long, ByVal nSrcWidth As Long, ByVal nSrcHeight As Long, ByVal dwRop As Long)
Declare Function SetPixelV Lib "gdi32" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long, ByVal crColor As Long) As Long
Declare Function TextOut Lib "gdi32" Alias "TextOutA" (ByVal hdc As Long, ByVal x As Long, ByVal y As Long, ByVal lpString As String, ByVal nCount As Long) As Long
Public Declare Function SetBitmapDimensionEx Lib "gdi32" (ByVal hbm As Long, ByVal nX As Long, ByVal nY As Long, lpSize As Size) As Long
Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Long, ByVal hwndCallback As Long) As Long
Declare Function sndPlaySound Lib "winmm.dll" Alias "sndPlaySoundA" (ByVal lpszSoundName As String, ByVal uFlags As Long) As Long
Declare Function sndPlaySoundL Lib "winmm.dll" Alias "sndPlaySoundA" (ByVal NUL As Long, ByVal uFlags As Long) As Long
Declare Function SetPolyFillMode Lib "gdi32" (ByVal hdc As Long, ByVal nPolyFillMode As Long) As Long
Declare Function RoundRect Lib "gdi32" (ByVal hdc As Long, ByVal X1 As Long, ByVal Y1 As Long, ByVal X2 As Long, ByVal Y2 As Long, ByVal X3 As Long, ByVal Y3 As Long) As Long
Declare Function Polygon Lib "gdi32" (ByVal hdc As Long, lpPoint As tPoint, ByVal nCount As Long) As Long
Declare Function BitBlt Lib "gdi32" (ByVal hDestDC As Long, ByVal x As Long, ByVal y As Long, ByVal nWidth As Long, ByVal nHeight As Long, ByVal hSrcDC As Long, ByVal xSrc As Long, ByVal ySrc As Long, ByVal dwRop As Long) As Long
Declare Function IntersectRect Lib "user32" (lpDestRect As tRect, lpSrc1Rect As tRect, lpSrc2Rect As tRect) As Long
Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Long, ByVal uParam As Long, ByVal lpvParam As Any, ByVal fuWinIni As Long) As Long
Public Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As Long, ByVal nIndex As Long) As Long

Public Declare Function GetGlyphOutline Lib "gdi32" Alias "GetGlyphOutlineA" (ByVal hdc As Long, ByVal uChar As Long, ByVal fuFormat As Long, lpgm As GLYPHMETRICS, ByVal cbBuffer As Long, lpBuffer As Any, lpmat2 As MAT2) As Long
Declare Function GetWorldTransform Lib "gdi32" (ByVal hdc As Long, lpXform As XFORM) As Long
Declare Function PlgBlt Lib "gdi32" (ByVal hdcDest As Long, lpPoint As POINTAPI, ByVal hdcSrc As Long, ByVal nXSrc As Long, ByVal nYSrc As Long, ByVal nWidth As Long, ByVal nHeight As Long, ByVal hbmMask As Long, ByVal xMask As Long, ByVal yMask As Long) As Long


Public Type R_xy
        Rx As Double
        Ry As Double
End Type


Public Type POINTAPI
        x As Long
        y As Long
End Type

Public Type GLYPHMETRICS
        gmBlackBoxX As Long
        gmBlackBoxY As Long
        gmptGlyphOrigin As POINTAPI
        gmCellIncX As Integer
        gmCellIncY As Integer
End Type

Public Type FIXED
        fract As Integer
        Value As Integer
End Type

Public Type MAT2
        eM11 As FIXED
        eM12 As FIXED
        eM21 As FIXED
        eM22 As FIXED
End Type


Public Type XFORM
        eM11 As Double
        eM12 As Double
        eM21 As Double
        eM22 As Double
        eDx As Double
        eDy As Double
End Type

Public Printdef As PRINTER_DEFAULTS
Public DOC_Inf_Set As DOC_INFO_2

Public Const LF_FACESIZE = 32
Public Const DT_BOTTOM = &H8
Public Const DT_CALCRECT = &H400
Public Const DT_CENTER = &H1
Public Const DT_CHARSTREAM = 4
Public Const DT_DISPFILE = 6
Public Const DT_EXPANDTABS = &H40
Public Const DT_EXTERNALLEADING = &H200
Public Const DT_INTERNAL = &H1000
Public Const DT_LEFT = &H0
Public Const DT_METAFILE = 5
Public Const DT_NOCLIP = &H100
Public Const DT_NOPREFIX = &H800
Public Const DT_PLOTTER = 0
Public Const DT_RASCAMERA = 3
Public Const DT_RASDISPLAY = 1
Public Const DT_RASPRINTER = 2
Public Const DT_RIGHT = &H2
Public Const DT_SINGLELINE = &H20
Public Const DT_TABSTOP = &H80
Public Const DT_TOP = &H0
Public Const DT_VCENTER = &H4
Public Const DT_WORDBREAK = &H10
Public Const PROOF_QUALITY = 2
Public Const ANSI_VAR_FONT = 12
Public Const CCHDEVICENAME = 32
Public Const CCHFORMNAME = 32

Type LOGFONT
        lfHeight As Long
        lfWidth As Long
        lfEscapement As Long
        lfOrientation As Long
        lfWeight As Long
        lfItalic As Byte
        lfUnderline As Byte
        lfStrikeOut As Byte
        lfCharSet As Byte
        lfOutPrecision As Byte
        lfClipPrecision As Byte
        lfQuality As Byte
        lfPitchAndFamily As Byte
        lfFaceName(0 To LF_FACESIZE - 1) As Byte
End Type

Type Size
        cx As Long
        cy As Long
End Type

Type BITMAPINFOHEADER
        biSize As Long
        biWidth As Long
        biHeight As Long
        biPlanes As Integer
        biBitCount As Integer
        biCompression As Long
        biSizeImage As Long
        biXPelsPerMeter As Long
        biYPelsPerMeter As Long
        biClrUsed As Long
        biClrImportant As Long
End Type

Type RGBQUAD
        rgbBlue As Byte
        rgbGreen As Byte
        rgbRed As Byte
        rgbReserved As Byte
End Type

Type BITMAPINFO
        bmiHeader As BITMAPINFOHEADER
        bmiColors As RGBQUAD
End Type

Type BITMAP
        bmType As Long
        bmWidth As Long
        bmHeight As Long
        bmWidthBytes As Long
        bmPlanes As Integer
        bmBitsPixel As Integer
        bmBits As Long
End Type

Type RECT
        Left As Long
        Top As Long
        Right As Long
        Bottom As Long
End Type

Type DRAWTEXTPARAMS
    cbSize As Long
    iTabLength As Long
    iLeftMargin As Long
    iRightMargin As Long
    uiLengthDrawn As Long
End Type


Type DEVMODE
        dmDeviceName As String * CCHDEVICENAME
        dmSpecVersion As Integer
        dmDriverVersion As Integer
        dmSize As Integer
        dmDriverExtra As Integer
        dmFields As Long
        dmOrientation As Integer
        dmPaperSize As Integer
        dmPaperLength As Integer
        dmPaperWidth As Integer
        dmScale As Integer
        dmCopies As Integer
        dmDefaultSource As Integer
        dmPrintQuality As Integer
        dmColor As Integer
        dmDuplex As Integer
        dmYResolution As Integer
        dmTTOption As Integer
        dmCollate As Integer
        dmFormName As String * CCHFORMNAME
        dmUnusedPadding As Integer
        dmBitsPerPel As Integer
        dmPelsWidth As Long
        dmPelsHeight As Long
        dmDisplayFlags As Long
        dmDisplayFrequency As Long
End Type


Type BitmapFileHeader
  bfType As String * 2
  bfSize As Long
  bfReserved1 As Integer
  bfReserved2 As Integer
  bfOffBits As Long
End Type

Type tPoint
  x As Long
  y As Long
End Type

Type tDot
  x As Integer
  y As Integer
End Type

Type tRect
  Left As Long
  Top As Long
  Right As Long
  Bottom As Long
  Width As Long
  Height As Long
End Type

Type DOC_INFO_2
        pDocName As String
        pOutputFile As String
        pDatatype As String
        dwMode As Long
        JobId As Long
End Type

Type DOC_INFO_1
        pDocName As String
        pOutputFile As String
        pDatatype As String
End Type


Type PRINTER_DEFAULTS
        pDatatype As String
        pDevMode As DEVMODE
        DesiredAccess As Long
End Type
' SystemParametersInfo 函式常數
Global Const SPI_GETKEYBOARDSPEED = 10
Global Const SPI_SETKEYBOARDSPEED = 11
Global Const SPI_GETKEYBOARDDELAY = 22
Global Const SPI_SETKEYBOARDDELAY = 23

' Polygon 常數
Global Const ALTERNATE = 1
Global Const WINDING = 2

' sndPlaySound 函式常數
Global Const SND_SYNC = &H0
Global Const SND_ASYNC = &H1
Global Const SND_NODEFAULT = &H2
Global Const SND_MEMORY = &H4
Global Const SND_LOOP = &H8
Global Const SND_NOSTOP = &H10

' 鍵盤掃描碼常數
Global Const KEY_UP = 38
Global Const KEY_DOWN = 40
Global Const KEY_LEFT = 37
Global Const KEY_RIGHT = 39
Global Const KEY_SPACE = 32

' VB ScaleMode 常數
Global Const PIXEL = 3
Global Const PI = 3.141592657
Global Const SRCCOPY = &HCC0020     'PUT
Global Const SRCAND = &H8800C6      'AND
Global Const SRCPAINT = &HEE0086    'OR
Global Const SRCINVERT = &H660046   'XOR
Public Const DSTINVERT = &H550009    'inver
Public Const WHITENESS = &HFF0062
Public Const BLACKNESS = &H42
Public Const Wd = 11.811023622
'取得鍵盤延遲
Public Function GetKeyboardDelay() As Integer
  Dim v_delay As Long
  Call SystemParametersInfo(SPI_GETKEYBOARDDELAY, 0, v_delay, 0)
  GetKeyboardDelay = v_delay
End Function


'取得兩數中較小值
Function Min(ByVal v_a As Variant, ByVal v_b As Variant) As Variant
  Min = IIf(v_a < v_b, v_a, v_b)
End Function

'取得兩數中較大值
Function Max(ByVal v_a As Variant, ByVal v_b As Variant) As Variant
  Max = IIf(v_a > v_b, v_a, v_b)
End Function

'交換兩個變數
Sub Swap(v_a As Variant, v_b As Variant)
  Dim c As Variant
  c = v_a
  v_a = v_b
  v_b = c
End Sub

'關閉音樂
Sub sndStopSound()
  Call sndPlaySoundL(0, 0)
End Sub

'簡易 MCI 函式
Function mciSendCmdString(ByVal v_command As String) As Long
  mciSendCmdString = mciSendString(v_command, 0&, 0, 0)
End Function

Sub MoveMem(Des() As Byte, Sour As String)
RtlMoveMemory Des(0), ByVal CStr(Sour), LenB(StrConv(Sour, vbFromUnicode)) + 1
End Sub

Public Function Over8(x As Long, Optional y As Long = 8)
   Over8 = IIf(Int(x / y) * y = x, x, (Int(x / y) + 1) * y)
End Function




Private Sub MsComm1_OnComm()
  '  MSComm1.Break = True
'   Do
 '     DoEvents
  ' Loop Until MSComm1.CommEvent = 0
   'MSComm1.Break = False

   Select Case MSComm1.CommEvent
   ' 錯誤
      Case comEventBreak
              MsgBox " 收到一個中斷"
      Case comEventFrame
              MsgBox "訊框錯誤"
      Case comEventOverrun
              MsgBox "資料遺失"
      Case comEventRxOver
             MsgBox "暫存區溢位"
      Case comEventRxParity
             MsgBox "同位檢查錯誤"
      Case comEventTxFull
             MsgBox "傳送暫存區滿"
      Case comEventDCB
             MsgBox "取得 DCB] 時發生無法預期的錯誤"
   ' 事件
      Case comEvCD
             MsgBox "CD 線的狀態發生變化"
      Case comEvCTS
'              If Me.ctss Then
'              MSComm1.Break = True
'              Me.ctss = False
'              Else
'              MSComm1.Break = False
'              Me.ctss = True
 '             End If
              
              MsgBox "CTS 線的狀態發生變化"
      Case comEvDSR
              MsgBox "DSR 線的狀態發生變化"
      Case comEvRing
              MsgBox "Ring Indicator 變化"
      Case comEvReceive
              MsgBox "收到最小接收字元數  "
      Case comEvSend
              MsgBox "傳輸暫存區有最小傳輸字元數個字元"
     Case comEvEOF
              MsgBox "輸入資料流中發現 EOF 字元"
     Case Else
              MsgBox MSComm1.CommEvent
   End Select
End Sub

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function SendTo(Datastr) As Variant
      Select Case TypeName(Datastr)
             Case "String"
                      DataByte = StrConv(Datastr, vbFromUnicode)
             Case "Byte()"
                      DataByte = Datastr
              Case Else
                      MsgBox TypeName(Datastr)
        End Select
     
     If Left(Port, 3) = "COM" Then
          MSComm1.Output = DataByte
    Else
         WritePrinter PrintId, DataByte(LBound(DataByte)), UBound(DataByte) - LBound(DataByte) + 1, Ret
    End If
  
End Function



Public Function StartPrint(Optional DeviceName As String, Optional jobname As String = " ", Optional FileName As String) As Boolean

DOC_Inf_Set.dwMode = 0
DOC_Inf_Set.pDocName = jobname
If FileName <> "" Then DOC_Inf_Set.pOutputFile = FileName
  If Left(Port, 3) = "COM" Then
         MSComm1.CommPort = Val(Mid(Port, 4, 1))
         MSComm1.PortOpen = True
         MSComm1.InputMode = comInputModeBinary
      StartPrint = IIf(MSComm1.PortOpen, True, False)
   Else
       openprinter IIf(DeviceName = "", Printer.DeviceName, DeviceName), PrintId, Printdef
         StartDocPrinter PrintId, 1, DOC_Inf_Set
         StartPrint = IIf(PrintId > 0, True, False)
   End If
End Function
'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function EndPrint() As Variant
     If Left(Port, 3) = "COM" Then
         MSComm1.PortOpen = False
     Else
         EndDocPrinter PrintId
         ClosePrinter PrintId
     End If
id = 0
End Function


Public Function FMT(Width As Integer, Length As Integer, Optional RESERVED As Boolean) As String
SendTo vbCrLf + vbCrLf + "FMT(1," + CStr(Width) + "," + CStr(Length) + "," + CStr(RESERVED * -2) + ",0,1)ACL()"
id = 1
End Function
'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function LMG(Margin As Integer) As Variant
If Margin > 0 And Margin < 108 Then SendTo vbCrLf + "LMG(" + CStr(Margin) + ")"
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function TMG(Margin As Integer) As Variant
If Margin > 0 And Margin < 999 Then SendTo vbCrLf + "TMG(" + CStr(Margin) + ")"
End Function


'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function BOX(X1 As Single, Y1 As Single, X2 As Single, Y2 As Single, Pattern As Integer, Width As Integer, Spattern As Integer) As Integer
'pattern 1 to 2
'spattern 0 to 1
SendTo vbCrLf + "BOX(" + CStr(id) + "," + CStr(X1) + "," + CStr(Y1) + "," + CStr(X2) + "," + CStr(Y2) + "," + CStr(Pattern) + "," + CStr(Width) + "," + CStr(Spattern) + ")"
SendTo "DAT(" + CStr(id) + "," + "BOX)"
 id = id + 1
BOX = id
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function LIN(X1 As Single, Y1 As Single, X2 As Single, Y2 As Single, Pattern As Integer, Width As Integer) As Integer
'pattern 1 to 2
'width 1 to 15
SendTo vbCrLf + "LIN(" + CStr(id) + "," + CStr(X1) + "," + CStr(Y1) + "," + CStr(X2) + "," + CStr(Y2) + "," + CStr(Pattern) + "," + CStr(Width) + ")"
SendTo "DAT(" + CStr(id) + "," + "LIN)"
 id = id + 1
LIN = id
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function CFL(x As Single, y As Single, FONTS As Integer, XR As Single, YR As Single, DAT As String) As Variant
If DAT = "" Then Exit Function
'If Me.id >= 70 Or DAT = "" Then MsgBox "CFL'S ID >70"
SendTo vbCrLf + "CFL(" + CStr(id) + "," + CStr(x) + "," + CStr(y) + "," + CStr(FONTS) + "," + CStr(XR) + "," + CStr(YR) + ")"
SendTo "DAT(" + CStr(id) + "," + DAT + ")"
 id = id + 1
CFL = id
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function Prt(page As Integer) As Variant
SendTo vbCrLf + "PRT(" + CStr(page) + ",1,1)"
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function BFL(x As Single, y As Single, Bdensity As Integer, B_type As Integer, B_height As Single, DAT As String, Optional n As Integer = 3, Optional w As Integer = 6) As Integer
If DAT = "" Then Exit Function
If Bdensity = 0 Then SendTo "BDN(" + CStr(n) + "," + CStr(w) + ")"
SendTo vbCrLf + "BFL(" + CStr(id) + "," + CStr(x) + "," + CStr(y) + "," + CStr(Bdensity) + "," + CStr(B_type) + "," + CStr(B_height) + ")" + "DAT(" + CStr(id) + "," + DAT + ")" + vbCrLf
 id = id + 1
BFL = id
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14

Public Function mids(Datacut, Start As Integer, Leng As Integer, Optional rtype As String = "S") As Variant
Dim Dataa() As Byte
ReDim datab(1 To Leng) As Byte
Select Case TypeName(Datacut)
          Case "String"
                  Dataa = StrConv(Datacut, vbFromUnicode)
          Case "BYTE()"
                  Dataa = Datacut
         Case Else
                 MsgBox TypeName(Datacut)
End Select
     For a = 1 To Leng
             datab(a) = Dataa(Start + a - 2)
     Next
If rtype = "S" Then mids = StrConv(datab, vbUnicode) Else mids = datab
End Function




Public Function COL(reverse As Boolean) As Variant
If reverse Then SendTo "COL(1)" Else SendTo "COL(0)"
End Function


Public Function PIT(dot As Integer) As Variant
SendTo "PIT(" + CStr(dot) + "d)"
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function DMD(overlapp As Boolean) As Variant
If overlapp Then SendTo "DMD(1)" Else SendTo "DMD(0)"
End Function

Function getBim(xpos As Single, ypos As Single, txtbmp As String, fHeight As Double, fWidth As Double, Optional RESERVED As Boolean = True, Optional ByVal dree As Double, Optional ByVal MidPos As Double) As Integer
Dim FontDc As Long
Dim RECTset As RECT
Dim Sizeset As Size
Dim Fontset As LOGFONT
Dim xpoint(4) As POINTAPI
If Trim(txtbmp) = "" Then Exit Function
bmptxtlen = LenB(StrConv(Trim(txtbmp), vbFromUnicode))
Fontset.lfCharSet = 1
Fontset.lfEscapement = 0

If Font_Bold Then Fontset.lfWeight = 700 Else Fontset.lfWeight = 400
Fontset.lfItalic = 0
Fontset.lfStrikeOut = 0
MoveMem Fontset.lfFaceName, Font_Name
'MoveMem Fontset.lfFaceName, "華康中明體"


RECTset.Left = 0: RECTset.Right = 0: RECTset.Bottom = 0: RECTset.Top = 0
Sizeset.cx = 0: Sizeset.cy = 0
Fontset.lfHeight = Int(fHeight * Wd)
Fontset.lfWidth = Int(IIf(fWidth > 1000, (fWidth - 1000) * Wd / 2, fWidth / bmptxtlen * Wd))
'hmemdc = Picture1.hwnd
hmemdc = GetDC(0)
FontDc = CreateCompatibleDC(hmemdc)
hFont = CreateFontIndirect(Fontset)
SelectObject FontDc, hFont
GetTextExtentPoint32 FontDc, txtbmp, bmptxtlen, Sizeset
realx = Over8(Sizeset.cx, 32): realy = Sizeset.cy
RECTset.Right = Sizeset.cx: RECTset.Bottom = Sizeset.cy

ibmp = CreateCompatibleBitmap(FontDc, realx, Sizeset.cy)
SelectObject FontDc, ibmp
PatBlt FontDc, 0, 0, realx, Sizeset.cy, IIf(RESERVED, WHITENESS, BLACKNESS)
DrawText FontDc, txtbmp, bmptxtlen, RECTset, 0
If RESERVED Then StretchBlt FontDc, 0, 0, realx, Sizeset.cy, FontDc, 0, 0, realx, Sizeset.cy, DSTINVERT

Dim gm As GLYPHMETRICS
Dim m2 As MAT2

'r = GetGlyphOutline(FontDc, "f", 1, gm, 0, 0, m2)


sida = dree * PI / 180
If dree > 0 Then
  xpoint(1).x = Sizeset.cy * Sin(sida)
  xpoint(1).y = Sizeset.cy * (1 - Cos(sida))
  xpoint(2).x = xpoint(1).x + Sizeset.cx * Cos(sida)
  xpoint(2).y = xpoint(1).y + Sizeset.cx * Sin(sida)
  xpoint(3).x = Cos(sida)
  xpoint(3).y = Sizeset.cy
  xpoint(4).x = xpoint(3).x + Sizeset.cx * Cos(sida)
  xpoint(4).y = xpoint(3).y + Sizeset.cx * Sin(sida)
  realx = Over8(Max(Abs(xpoint(2).x - xpoint(3).x), Abs(xpoint(1).x - xpoint(4).x)), 32)
  realy = Max(Abs(xpoint(2).y - xpoint(3).y), Abs(xpoint(1).y - xpoint(4).y))
 REctdc = CreateCompatibleDC(hmemdc)
 ibmp = CreateCompatibleBitmap(REctdc, realx, realy)
 SelectObject REctdc, ibmp
 PatBlt FontDc, 0, 0, Sizeset.cx, Sizeset.cy, IIf(RESERVED, WHITENESS, BLACKNESS)
 DrawText FontDc, txtbmp, bmptxtlen, RECTset, 0
 If RESERVED Then StretchBlt FontDc, 0, 0, Sizeset.cx, Sizeset.cy, FontDc, 0, 0, Sizeset.cx, Sizeset.cy, DSTINVERT
Select Case dree
           Case 0 To 89.4
            xpoint(1).y = xpoint(1).y - Sizeset.cy * (1 - Cos(sida))
            xpoint(2).y = xpoint(2).y - Sizeset.cy * (1 - Cos(sida))
            xpoint(3).y = xpoint(3).y - Sizeset.cy * (1 - Cos(sida))
            xpoint(4).y = xpoint(4).y - Sizeset.cy * (1 - Cos(sida))
           Case 90 To 179.9
            xpoint(1).y = xpoint(1).y - Sizeset.cy
            xpoint(2).y = xpoint(2).y - Sizeset.cy
            xpoint(3).y = xpoint(3).y - Sizeset.cy
            xpoint(4).y = xpoint(4).y - Sizeset.cy
            xpoint(1).x = xpoint(1).x - Sizeset.cx * Cos(sida)
            xpoint(2).x = xpoint(2).x - Sizeset.cx * Cos(sida)
            xpoint(3).x = xpoint(3).x - Sizeset.cx * Cos(sida)
            xpoint(4).x = xpoint(4).x - Sizeset.cx * Cos(sida)
           Case 180 To 270
            xpoint(1).y = xpoint(1).y - (Sizeset.cy + Sizeset.cx * Sin(sida))
            xpoint(2).y = xpoint(2).y - (Sizeset.cy + Sizeset.cx * Sin(sida))
            xpoint(3).y = xpoint(3).y - (Sizeset.cy + Sizeset.cx * Sin(sida))
            xpoint(4).y = xpoint(4).y - (Sizeset.cy + Sizeset.cx * Sin(sida))
            xpoint(1).x = xpoint(1).x - (Sizeset.cy * Sin(sida) + Sizeset.cx * Cos(sida))
            xpoint(2).x = xpoint(2).x - (Sizeset.cy * Sin(sida) + Sizeset.cx * Cos(sida))
            xpoint(3).x = xpoint(3).x - (Sizeset.cy * Sin(sida) + Sizeset.cx * Cos(sida))
            xpoint(4).x = xpoint(4).x - (Sizeset.cy * Sin(sida) + Sizeset.cx * Cos(sida))
          Case 270 To 360
            xpoint(1).x = xpoint(1).x - (Sizeset.cy * Sin(sida))
            xpoint(2).x = xpoint(2).x - (Sizeset.cy * Sin(sida))
            xpoint(3).x = xpoint(3).x - (Sizeset.cy * Sin(sida))
            xpoint(4).x = xpoint(4).x - (Sizeset.cy * Sin(sida))
             xpoint(1).y = xpoint(1).y + (Sizeset.cy * (Cos(sida) - 1) - Sizeset.cx * Sin(sida))
             xpoint(2).y = xpoint(2).y + (Sizeset.cy * (Cos(sida) - 1) - Sizeset.cx * Sin(sida))
             xpoint(3).y = xpoint(3).y + (Sizeset.cy * (Cos(sida) - 1) - Sizeset.cx * Sin(sida))
             xpoint(4).y = xpoint(4).y + (Sizeset.cy * (Cos(sida) - 1) - Sizeset.cx * Sin(sida))
 End Select

PlgBlt REctdc, xpoint(1), FontDc, 0, 0, Sizeset.cx, Sizeset.cy, 0, 0, 0
StretchBlt FontDc, 0, 0, realx, Sizeset.cy, REctdc, 0, 0, Sizeset.cx, Sizeset.cy, DSTINVERT
DeleteDC REctdc

End If

ReDim BMPDATA(1 To realx * realy / 8) As Byte
GetBitmapBits ibmp, realx * realy / 8, BMPDATA(LBound(BMPDATA))



If (MidPos * Wd - realx) > 0 Then
   SendTo vbCrLf + "BIM(" + CStr(id) + "," + Trim(CStr(Int((MidPos * Wd - realx) / 2))) + "d," + Trim(CStr(ypos)) + "," + Trim(CStr(realx / 8)) + "," + Trim(CStr(realy)) + ")"
Else
   SendTo vbCrLf + "BIM(" + CStr(id) + "," + Trim(CStr(xpos)) + "," + Trim(CStr(ypos)) + "," + Trim(CStr(realx / 8)) + "," + Trim(CStr(realy)) + ")"
End If
'a = Len(BMPDATA)
SendTo BMPDATA
 id = id + 1
ReleaseDC 0, hmemdc
DeleteDC FontDc
DeleteDC hmemdc
DeleteObject FontDc
DeleteObject hFont
DeleteObject ibmp
getBim = id
End Function

'警告! 切勿移除或修改以下的註解行!
'MemberInfo=14
Public Function bim(pos_x As Integer, pos_y As Integer, FName As String, Optional rate_x As Integer = 0, Optional rate_y As Integer = 0, Optional ByVal dree As Double, Optional RESERVED As Boolean)
Dim bm As BITMAPINFO
Dim fhead(1 To 14) As Byte
Dim bmdat()  As Byte
Dim hdc As Long
Dim hdc1 As Long
Dim ttt(1 To 4) As Byte
Dim xpoint(4) As POINTAPI

fno = FreeFile
If Dir(FName) = "" Or FileLen(FName) < 66 Then Exit Function
  Open FName For Binary As fno
  Get fno, , fhead
  If Left(StrConv(fhead, vbUnicode), 2) <> "BM" Then Close fno: Exit Function
  Get fno, , bm
  Get fno, , ttt
  ReDim bmdat(1 To bm.bmiHeader.biSizeImage) As Byte
  Get fno, , bmdat
  Close fno

realx = Over8(bm.bmiHeader.biWidth, 32)

hdc = CreateCompatibleDC(0)
hdc1 = CreateCompatibleDC(0)

ibmp = CreateBitmap(realx, bm.bmiHeader.biHeight, bm.bmiHeader.biPlanes, bm.bmiHeader.biBitCount, bmdat(LBound(bmdat)))
ibmp1 = CreateCompatibleBitmap(hdc1, realx, bm.bmiHeader.biHeight)
SelectObject hdc, ibmp
SelectObject hdc1, ibmp1





sida = dree * PI / 180
If dree > 0 Then
  xpoint(1).x = bm.bmiHeader.biHeight * Sin(sida)
  xpoint(1).y = bm.bmiHeader.biHeight * (1 - Cos(sida))
  xpoint(2).x = xpoint(1).x + bm.bmiHeader.biWidth * Cos(sida)
  xpoint(2).y = xpoint(1).y + bm.bmiHeader.biWidth * Sin(sida)
  xpoint(3).x = Cos(sida)
  xpoint(3).y = bm.bmiHeader.biHeight
  xpoint(4).x = xpoint(3).x + bm.bmiHeader.biWidth * Cos(sida)
  xpoint(4).y = xpoint(3).y + bm.bmiHeader.biWidth * Sin(sida)


  realx = Over8(Max(Abs(xpoint(2).x - xpoint(3).x), Abs(xpoint(1).x - xpoint(4).x)), 32)
  realy = Max(Abs(xpoint(2).y - xpoint(3).y), Abs(xpoint(1).y - xpoint(4).y))
  
  StretchBlt hdc1, 0, 0, realx, bm.bmiHeader.biHeight, hdc, 0, 0, realx, bm.bmiHeader.biHeight, SRCCOPY
  StretchBlt hdc, 0, bm.bmiHeader.biHeight, realx, -bm.bmiHeader.biHeight, hdc1, 0, 0, realx, bm.bmiHeader.biHeight, SRCCOPY

 REctdc = CreateCompatibleDC(hdc)
 ibmp = CreateCompatibleBitmap(REctdc, realx, realy)
 SelectObject REctdc, ibmp
  
 PatBlt REctdc, 0, 0, realx, realy, IIf(RESERVED, WHITENESS, BLACKNESS)
 
 'If RESERVED Then StretchBlt RectDc, 0, 0, bm.bmiHeader.biWidth, bm.bmiHeader.biHeight, FontDc, 0, 0, Sizeset.cx, Sizeset.cy, DSTINVERT
Select Case dree
           Case 0 To 89.4
            xpoint(1).y = xpoint(1).y - bm.bmiHeader.biHeight * (1 - Cos(sida))
            xpoint(2).y = xpoint(2).y - bm.bmiHeader.biHeight * (1 - Cos(sida))
            xpoint(3).y = xpoint(3).y - bm.bmiHeader.biHeight * (1 - Cos(sida))
            xpoint(4).y = xpoint(4).y - bm.bmiHeader.biHeight * (1 - Cos(sida))
           Case 90 To 179.9
            xpoint(1).y = xpoint(1).y - bm.bmiHeader.biHeight
            xpoint(2).y = xpoint(2).y - bm.bmiHeader.biHeight
            xpoint(3).y = xpoint(3).y - bm.bmiHeader.biHeight
            xpoint(4).y = xpoint(4).y - bm.bmiHeader.biHeight
            xpoint(1).x = xpoint(1).x - bm.bmiHeader.biWidth * Cos(sida)
            xpoint(2).x = xpoint(2).x - bm.bmiHeader.biWidth * Cos(sida)
            xpoint(3).x = xpoint(3).x - bm.bmiHeader.biWidth * Cos(sida)
            xpoint(4).x = xpoint(4).x - bm.bmiHeader.biWidth * Cos(sida)
           Case 180 To 270
            xpoint(1).y = xpoint(1).y - (bm.bmiHeader.biHeight + bm.bmiHeader.biWidth * Sin(sida))
            xpoint(2).y = xpoint(2).y - (bm.bmiHeader.biHeight + bm.bmiHeader.biWidth * Sin(sida))
            xpoint(3).y = xpoint(3).y - (bm.bmiHeader.biHeight + bm.bmiHeader.biWidth * Sin(sida))
            xpoint(4).y = xpoint(4).y - (bm.bmiHeader.biHeight + bm.bmiHeader.biWidth * Sin(sida))
            xpoint(1).x = xpoint(1).x - (bm.bmiHeader.biHeight * Sin(sida) + bm.bmiHeader.biWidth * Cos(sida))
            xpoint(2).x = xpoint(2).x - (bm.bmiHeader.biHeight * Sin(sida) + bm.bmiHeader.biWidth * Cos(sida))
            xpoint(3).x = xpoint(3).x - (bm.bmiHeader.biHeight * Sin(sida) + bm.bmiHeader.biWidth * Cos(sida))
            xpoint(4).x = xpoint(4).x - (bm.bmiHeader.biHeight * Sin(sida) + bm.bmiHeader.biWidth * Cos(sida))
          Case 270 To 360
            xpoint(1).x = xpoint(1).x - (bm.bmiHeader.biHeight * Sin(sida))
            xpoint(2).x = xpoint(2).x - (bm.bmiHeader.biHeight * Sin(sida))
            xpoint(3).x = xpoint(3).x - (bm.bmiHeader.biHeight * Sin(sida))
            xpoint(4).x = xpoint(4).x - (bm.bmiHeader.biHeight * Sin(sida))
             xpoint(1).y = xpoint(1).y + (bm.bmiHeader.biHeight * (Cos(sida) - 1) - bm.bmiHeader.biWidth * Sin(sida))
             xpoint(2).y = xpoint(2).y + (bm.bmiHeader.biHeight * (Cos(sida) - 1) - bm.bmiHeader.biWidth * Sin(sida))
             xpoint(3).y = xpoint(3).y + (bm.bmiHeader.biHeight * (Cos(sida) - 1) - bm.bmiHeader.biWidth * Sin(sida))
             xpoint(4).y = xpoint(4).y + (bm.bmiHeader.biHeight * (Cos(sida) - 1) - bm.bmiHeader.biWidth * Sin(sida))
 End Select

PlgBlt hdc1, xpoint(1), REctdc, 0, 0, bm.bmiHeader.biWidth, bm.bmiHeader.biHeight, 0, 0, 0
StretchBlt hdc, 0, 0, realx, bm.bmiHeader.biHeight, hdc1, 0, 0, bm.bmiHeader.biWidth, bm.bmiHeader.biHeight, DSTINVERT
DeleteDC REctdc
End If


If realx <> bm.bmiHeader.biWidth Then
PatBlt hdc, bm.bmiHeader.biWidth, 0, realx, bm.bmiHeader.biHeight, WHITENESS
PatBlt hdc1, bm.bmiHeader.biWidth, 0, realx, bm.bmiHeader.biHeight, WHITENESS
End If


If rate_t = 0 And rate_t = 0 Then
StretchBlt hdc, 0, 0, realx, bm.bmiHeader.biHeight, hdc, 0, 0, realx, bm.bmiHeader.biHeight, DSTINVERT
Else
 StretchBlt hdc, 0, 0, realx, bm.bmiHeader.biHeight, hdc, 0, 0, realx, bm.bmiHeader.biHeight, DSTINVERT
End If

GetBitmapBits ibmp, bm.bmiHeader.biHeight * realx / 8, bmdat(LBound(bmdat))

DeleteDC hdc1: DeleteDC hdc: DeleteObject ibmp1: DeleteObject ibmp
SendTo "BIM(" + CStr(id) + "," + CStr(pos_x) + "," + CStr(pos_y) + "," + CStr(Over8(bm.bmiHeader.biWidth, 32) / 8) + "," + CStr(bm.bmiHeader.biHeight) + ")"
SendTo bmdat

id = id + 1
bim = id
End Function


Public Function Word_Cnt(ByVal Wd As String, wd_width As Double) As Double
  For I = 1 To Len(Wd)
    If Asc(Mid$(Wd, I, 1)) >= 0 And Asc(Mid$(Wd, I, 1)) <= 127 Then
      cnt = cnt + 1
    Else
      cnt = cnt + 2
    End If
  Next
  Word_Cnt = Val(cnt * wd_width)
End Function




