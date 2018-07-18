Option Explicit On
Imports System.Runtime.InteropServices
Public Class Form1
    Declare Sub mouse_event Lib "user32.dll" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4
    Public Const VK_F1 As Integer = &H70
    Public Const VK_F2 As Integer = &H71
    Public Const VK_F3 As Integer = &H72
    Public Const WM_HOTKEY As Integer = &H312
    Private CPos As Point
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Call RegisterHotKey(Me.Handle, 1, Nothing, VK_F1)
        Call RegisterHotKey(Me.Handle, 2, Nothing, VK_F2)
        Call RegisterHotKey(Me.Handle, 3, Nothing, VK_F3)
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Call UnregisterHotKey(Me.Handle, 1)
        Call UnregisterHotKey(Me.Handle, 2)
        Call UnregisterHotKey(Me.Handle, 3)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_HOTKEY Then
            Dim id As IntPtr = m.WParam
            Select Case (id.ToString)
                Case "1"
                    CPos = MousePosition
                Case "2"
                    Timer1.Start()
                Case "3"
                    Timer1.Stop()
            End Select
        End If
        MyBase.WndProc(m)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call mouse_event(MOUSEEVENTF_LEFTDOWN, CPos.X, CPos.Y, 0, 0)
        Call mouse_event(MOUSEEVENTF_LEFTUP, CPos.X, CPos.Y, 0, 0)
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Me.Close()
    End Sub
End Class

