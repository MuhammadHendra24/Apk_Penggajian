Imports System.Data.OleDb

Public Class GantiPasswordUser
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox1.Text = UCase(TextBox1.Text)
            cmd = New OleDbCommand("select * from petugas where Nama_Petugas='" & TextBox1.Text & "' and Kode_Petugas='" & MenuUtama.Panel1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox1.Enabled = False
                TextBox2.Enabled = True
                TextBox2.Focus()
            Else
                MsgBox("Anda tidak berhak mengganti password disini karena anda tidak sedang login")
                TextBox1.Focus()
            End If
        End If
    End Sub


    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            cmd = New OleDbCommand("select * from petugas where Nama_Petugas='" & MenuUtama.Panel2.Text & "' and Password_Petugas='" & TextBox2.Text & "' ", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox2.Enabled = False
                TextBox3.Enabled = True
                TextBox3.Focus()
            Else
                MsgBox("Password lama salah")
                TextBox2.Focus()
            End If
        End If

    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        TextBox3.MaxLength = 10
        TextBox3.PasswordChar = "X"
        If e.KeyChar = Chr(13) Then
            If Len(TextBox3.Text) < 6 Then
                MsgBox("Password minimal 6 karakter")
                Exit Sub
            End If
            If TextBox3.Text = "" Then
                MsgBox("Passwrod baru wajib diisi")
                TextBox3.Focus()
            Else
                TextBox4.Enabled = True
                TextBox4.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        TextBox3.MaxLength = 10
        TextBox4.PasswordChar = "X"
        If e.KeyChar = Chr(13) Then
            If TextBox4.Text <> TextBox3.Text Then
                MsgBox("Password konfirmasi salah, password tidak sama")
                TextBox4.Focus()
            Else
                If MessageBox.Show("Yakin password lama <" & TextBox2.Text & "> akan diganti dengan <" & TextBox3.Text & ">..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim ganti As String = "Update petugas set Password_Petugas='" & TextBox3.Text & "' where Nama_Petugas='" & TextBox1.Text & "' and Kode_Petugas='" & MenuUtama.Panel1.Text & "'"
                    cmd = New OleDbCommand(ganti, Conn)
                    cmd.ExecuteNonQuery()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    Me.Close()
                Else
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub GantiPasswordUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
End Class