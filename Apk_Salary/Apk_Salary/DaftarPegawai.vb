Imports System.Data.OleDb

Public Class DaftarPegawai
    Sub Tampilkan()
        cmd = New OleDbCommand("Select * from Pegawai", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ListBox1.Items.Add(rd.GetString(0) & vbTab & rd.GetString(1))
        Loop
    End Sub

    Private Sub DaftarPegawai_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Text = ""
        Call Koneksi()
        Call Tampilkan()
        ListBox1.Focus()
    End Sub


    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            cmd = New OleDbCommand("Select * from Pegawai where nama_Pegawai like '%" & TextBox1.Text & "%'", Conn)
            rd = cmd.ExecuteReader
            If rd.HasRows Then
                ListBox1.Items.Clear()
                Do While rd.Read
                    ListBox1.Items.Add(rd.GetString(0) & vbTab & rd.GetString(1))
                Loop
            Else
                MsgBox("Nama Pegawai tidak ditemukan")
                TextBox1.Focus()
            End If
        ElseIf e.KeyChar = Chr(27) Then
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class