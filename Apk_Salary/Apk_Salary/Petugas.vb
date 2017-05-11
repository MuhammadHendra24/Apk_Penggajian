Imports System.Data.OleDb
Public Class Petugas

    Sub Kosongkan()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        combobox1.Text = ""
        TextBox1.Focus()
    End Sub

    Sub DataBaru()
        TextBox2.Text = ""
        TextBox3.Text = ""
        combobox1.Text = ""
        TextBox2.Focus()
    End Sub

    Sub Tampilkan()
        da = New OleDbDataAdapter("Select * from Petugas", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Petugas")
        DGV.DataSource = (ds.Tables("Petugas"))
        DGV.ReadOnly = True
    End Sub

    Sub TampilStatus()
        cmd = New OleDbCommand("select distinct status_petugas from petugas", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(0))
        Loop
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox1.Text = UCase(TextBox1.Text)
            cmd = New OleDbCommand("select * from Petugas where Kode_Petugas='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                TextBox2.Text = rd.Item(1)
                TextBox3.Text = rd.Item(2)
                ComboBox1.Text = rd.Item(3)
                TextBox2.Focus()
            Else
                Call DataBaru()
                TextBox2.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox2.Text = UCase(TextBox2.Text)
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox1.Focus()
    End Sub

    Private Sub combobox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            ComboBox1.Text = UCase(ComboBox1.Text)
            CmdSimpan.Focus()
        End If

    End Sub

    Private Sub CmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSimpan.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            cmd = New OleDbCommand("Select * from Petugas where Kode_Petugas='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                Dim sqltambah As String = "Insert into Petugas(kode_Petugas,Nama_Petugas,Password_Petugas,Status_Petugas) values " & _
                "('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "')"
                cmd = New OleDbCommand(sqltambah, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
                Call TampilStatus()
            Else
                Dim sqledit As String = "Update Petugas set " & _
                "Nama_Petugas='" & TextBox2.Text & "', " & _
                "Password_Petugas='" & TextBox3.Text & "', " & _
                "Status_Petugas='" & ComboBox1.Text & "' " & _
                "where Kode_Petugas='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(sqledit, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
                Call TampilStatus()
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
            MsgBox("Isi kode Petugas terlebih dahulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                cmd = New OleDbCommand("Delete * from Petugas where Kode_Petugas='" & TextBox1.Text & "'", Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call Tampilkan()
                Call TampilStatus()
            Else
                Call Kosongkan()
                Call TampilStatus()
            End If
        End If
    End Sub

    Private Sub Petugas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call Tampilkan()
        Call TampilStatus()
    End Sub

End Class