Imports System.Data.OleDb

Public Class Golongan
    Sub Kosongkan()
        textbox1.Text = ""
        textbox2.Text = ""
        textbox3.Text = ""
        textbox4.Text = ""
        textbox5.Text = ""
        textbox6.Text = ""
        textbox1.Focus()
    End Sub

    Sub DataBaru()
        textbox2.Text = ""
        textbox3.Text = ""
        textbox4.Text = ""
        textbox5.Text = ""
        textbox6.Text = ""
        textbox6.Focus()
    End Sub

    Sub Tampilkan()
        da = New OleDbDataAdapter("Select * from Golongan", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Golongan")
        DGV.DataSource = (ds.Tables("Golongan"))
        DGV.ReadOnly = True
    End Sub

    Private Sub textbox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            cmd = New OleDbCommand("select * from Golongan where Golongan='" & textbox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                TextBox2.Text = rd.Item(1)
                TextBox3.Text = rd.Item(2)
                TextBox4.Text = rd.Item(3)
                TextBox5.Text = rd.Item(4)
                TextBox6.Text = rd.Item(5)
                textbox2.Focus()
            Else
                Call DataBaru()
                textbox2.Focus()
            End If
        End If
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub textbox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub textbox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then textbox4.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub textbox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then textbox5.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub textbox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then textbox6.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub textbox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then CmdSimpan.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub CmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSimpan.Click
        If textbox1.Text = "" Or textbox2.Text = "" Or textbox3.Text = "" Or textbox4.Text = "" Or textbox5.Text = "" Or textbox6.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            cmd = New OleDbCommand("Select * from Golongan where Golongan='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                Dim sqltambah As String = "Insert into Golongan(Golongan,TJ_Suami_Istri,TJ_Anak,Uang_Makan,uang_Lembur,TJ_Kehadiran) values " & _
                "('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
                cmd = New OleDbCommand(sqltambah, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
                MsgBox("Data Di Simpan")
            Else
                Dim sqledit As String = "Update Golongan set " & _
                "TJ_Suami_Istri='" & TextBox2.Text & "', " & _
                "TJ_Anak='" & TextBox3.Text & "', " & _
                "Uang_Makan='" & TextBox4.Text & "', " & _
                "uang_Lembur='" & TextBox5.Text & "', " & _
                "TJ_Kehadiran='" & TextBox6.Text & "' " & _
                "where Golongan='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(sqledit, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
                MsgBox("Data berhasil di Edit")
            End If
        End If


    End Sub

    Private Sub CmdBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdBatal.Click
        Call Kosongkan()
    End Sub

    Private Sub CmdTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdTutup.Click
        Me.Close()
    End Sub

    Private Sub CmdHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdHapus.Click
        If TextBox1.Text = "" Then
            MsgBox("Isi kode Golongan terlebih dahulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Hapus Data..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                cmd = New OleDbCommand("Delete * from Golongan where Golongan='" & TextBox1.Text & "'", Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
            Else
                Call Kosongkan()
               
            End If
        End If
    End Sub

    Private Sub Golongan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call Tampilkan()
    End Sub
End Class