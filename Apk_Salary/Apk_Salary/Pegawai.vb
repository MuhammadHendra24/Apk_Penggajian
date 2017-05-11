Imports System.Data.OleDb

Public Class Pegawai
    Sub Kosongkan()
        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        TextBox3.Text = ""
        TextBox1.Focus()
    End Sub

    Sub DataBaru()
        TextBox2.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        TextBox3.Text = ""
        TextBox3.Text = ""
        TextBox2.Focus()
    End Sub

    Sub TabelPegawai()
        da = New OleDbDataAdapter("Select * from Pegawai", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Pegawai")
        DGV.DataSource = (ds.Tables("Pegawai"))
        DGV.ReadOnly = True
    End Sub

    Sub TampilJabatan()
        cmd = New OleDbCommand("select distinct kode_jabatan from jabatan", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(0))
        Loop
    End Sub

    Sub TampilGolongan()
        cmd = New OleDbCommand("select distinct Golongan from golongan", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox2.Items.Add(rd.Item(0))
        Loop
    End Sub
    Sub TampilDepartement()
        cmd = New OleDbCommand("select distinct Departement from Pegawai", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox4.Items.Add(rd.Item(0))
        Loop
    End Sub

    Sub TampilStatus()
        cmd = New OleDbCommand("select distinct Status from Pegawai", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox3.Items.Add(rd.Item(0))
        Loop
    End Sub

    Sub TampilTabel()
        Call TabelPegawai()

    End Sub
    Sub TampilPilihan()
        Call TampilJabatan()
        Call TampilGolongan()
        Call TampilDepartement()
        Call TampilStatus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            cmd = New OleDbCommand("select * from Pegawai where NIP='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                TextBox2.Text = rd.Item(1)
                ComboBox1.Text = rd.Item(2)
                ComboBox2.Text = rd.Item(3)
                ComboBox3.Text = rd.Item(4)
                TextBox3.Text = rd.Item(5)
                ComboBox4.Text = rd.Item(6)
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
            ComboBox1.Focus()
        End If
    End Sub

    Private Sub Combobox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then Combobox2.Focus()
        'If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub
    Private Sub Combobox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
        'If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub Combobox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then Combobox3.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub Combobox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
        'If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then CmdSimpan.Focus()
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub CmdSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSimpan.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or Combobox1.Text = "" Or Combobox2.Text = "" Or Combobox3.Text = "" Then
            MsgBox("Data Belum Lengkap")
            Exit Sub
        Else
            cmd = New OleDbCommand("Select * from Pegawai where NIP='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                Dim sqltambah As String = "Insert into Pegawai(NIP,Nama_Pegawai,Kode_Jabatan,Golongan,Status,Jumlah_Anak,Departement) values " & _
                "('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "','" & TextBox3.Text & "','" & ComboBox4.Text & "')"
                cmd = New OleDbCommand(sqltambah, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call TabelPegawai()
            Else
                Dim sqledit As String = "Update Pegawai set " & _
                "Nama_Pegawai='" & TextBox2.Text & "', " & _
                "Kode_Jabatan='" & ComboBox1.Text & "', " & _
                "Golongan='" & ComboBox2.Text & "', " & _
                "Status='" & ComboBox3.Text & "', " & _
                "Jumlah_Anak='" & TextBox3.Text & "' " & _
                "Departement='" & ComboBox4.Text & "' " & _
                "where NIP='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(sqledit, Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call TabelPegawai()
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
            MsgBox("Isi kode Pegawai terlebih dahulu")
            TextBox1.Focus()
            Exit Sub
        Else
            If MessageBox.Show("Yakin akan dihapus..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                cmd = New OleDbCommand("Delete * from Pegawai where NIP='" & TextBox1.Text & "'", Conn)
                cmd.ExecuteNonQuery()
                Call Kosongkan()
                Call TabelPegawai()
            Else
                Call Kosongkan()
            End If
        End If
    End Sub

    Private Sub Pegawai_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilTabel()
        Call TampilPilihan()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Jabatan.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Golongan.Show()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class