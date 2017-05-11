Imports System.Data.OleDb

Public Class Penggajian
    Sub Tampilkan()
        da = New OleDbDataAdapter("Select Kode_Potongan as Kode,nama_Potongan as [Nama Potongan]from Potongan where left(kode_Potongan,1)='1'", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Potongan")
        DGV.DataSource = (ds.Tables("Potongan"))
        DGV.Columns.Add("Jumlah", "Jumlah")
        DGV.Columns(0).ReadOnly = True
        DGV.Columns(1).ReadOnly = True
        DGV.Columns(0).Width = 40
        DGV.Columns(1).Width = 125
    End Sub

    Sub NomorOtomatis()

        cmd = New OleDbCommand("select max(nomor_slip) from gaji", Conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            TextBox1.Text = Format(Now, "yyMMdd") + "0001"
        Else
            If Microsoft.VisualBasic.Left(rd.GetString(0), 6) <> Format(Now, "yyMMdd") Then
                TextBox1.Text = Format(Now, "yyMMdd") + "0001"
            Else
                TextBox1.Text = rd.GetString(0) + 1
            End If
        End If

    End Sub

    Private Sub Penggajian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call NomorOtomatis()
        TextBox2.Text = Today
        Call Tampilkan()
    End Sub

    Sub BersihkaN()
        TextBox16.Clear()
        TextBox17.Clear()
        TextBox18.Clear()
        TextBox19.Clear()
        TextBox20.Clear()
        TextBox21.Clear()
        TextBox22.Clear()
        TextBox23.Clear()
    End Sub

    Sub BersihkanGrid()
        DGV.Columns.Clear()
        Call Tampilkan()
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox3.Text = UCase(TextBox3.Text)
            Call BersihkaN()
            Call BersihkanGrid()
            cmd = New OleDbCommand("select * from gaji where nip='" & TextBox3.Text & "' and month(tanggal) ='" & Month(TextBox2.Text) & "' and year(tanggal) = '" & Year(TextBox2.Text) & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                MsgBox("Nip " & TextBox3.Text & " bulan ini telah gajian")
                Call Batalkan()
                Exit Sub
            End If

            cmd = New OleDbCommand("select * from pegawai where nip='" & TextBox3.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox4.Text = rd.Item(1)
                TextBox5.Text = rd.Item(2)
                TextBox6.Text = rd.Item(3)
                TextBox7.Text = rd.Item(4) 'status
                TextBox8.Text = rd.Item(5)
                cmd = New OleDbCommand("select * from jabatan where kode_jabatan='" & TextBox5.Text & "'", Conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows Then
                    TextBox5.Text = rd.Item(1)
                    TextBox9.Text = rd.Item(2)
                    TextBox10.Text = rd.Item(3)
                End If

                cmd = New OleDbCommand("select * from golongan where golongan='" & TextBox6.Text & "'", Conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows Then
                    If TextBox7.Text = "MENIKAH" Then
                        TextBox11.Text = rd.Item(1)
                        TextBox12.Text = rd.Item(2)
                        TextBox13.Text = rd.Item(3)
                        TextBox14.Text = rd.Item(4)
                        TextBox15.Text = rd.Item(5)
                        TextBox23.Text = Val(TextBox12.Text) * Val(TextBox8.Text)
                    Else
                        TextBox11.Text = 0
                        TextBox12.Text = 0
                        TextBox13.Text = rd.Item(3)
                        TextBox14.Text = rd.Item(4)
                        TextBox15.Text = rd.Item(5)
                        TextBox23.Text = 0
                    End If
                    TextBox19.Focus()
                End If
            Else
                MsgBox("NIP tidak terdaftar")
                DaftarPegawai.Show()
            End If
        End If
    End Sub

    Private Sub TextBox19_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox19.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox21.Text = Val(TextBox13.Text) * Val(TextBox19.Text)
            TextBox16.Text = Val(TextBox9.Text) + Val(TextBox10.Text) + Val(TextBox11.Text) + Val(TextBox23.Text) + Val(TextBox21.Text) + Val(TextBox22.Text) + Val(TextBox15.Text)
            TextBox20.Focus()
        End If
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub


    Private Sub TextBox20_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox20.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox22.Text = Val(TextBox14.Text) * Val(TextBox20.Text)
            TextBox16.Text = Val(TextBox9.Text) + Val(TextBox10.Text) + Val(TextBox11.Text) + Val(TextBox23.Text) + Val(TextBox21.Text) + Val(TextBox22.Text) + Val(TextBox15.Text)
            DGV.Focus()
        End If
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Private Sub DGV_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.CellEndEdit
        If e.ColumnIndex = 2 Then
            Call TotalPotongan()
            TextBox18.Text = Val(TextBox16.Text) - Val(TextBox17.Text)
        End If

    End Sub

    Sub TotalPotongan()
        Dim Hitung As Integer = 0
        For I As Integer = 0 To DGV.Rows.Count - 1
            Hitung = Hitung + Val(DGV.Rows(I).Cells(2).Value)
            TextBox17.Text = Hitung
        Next
    End Sub

    Private Sub DGV_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DGV.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled() = True
    End Sub

    Sub Batalkan()
        TextBox3.Clear() : TextBox14.Clear()
        TextBox4.Clear() : TextBox15.Clear()
        TextBox5.Clear() : TextBox16.Clear()
        TextBox6.Clear() : TextBox17.Clear()
        TextBox7.Clear() : TextBox18.Clear()
        TextBox8.Clear() : TextBox19.Clear()
        TextBox9.Clear() : TextBox20.Clear()
        TextBox10.Clear() : TextBox21.Clear()
        TextBox11.Clear() : TextBox22.Clear()
        TextBox12.Clear() : TextBox23.Clear()
        TextBox13.Clear()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call Batalkan()
        Call BersihkanGrid()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call Batalkan()
        Me.Close()
        'End
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox3.Text = "" Or TextBox19.Text = "" Or TextBox20.Text = "" Or TextBox16.Text = "" Or TextBox17.Text = "" Or TextBox18.Text = "" Then
            MsgBox("Data belum lengkap")
            Exit Sub
        Else
            Dim simpan As String = "insert into gaji(nomor_Slip,tanggal,pendapatan,potongan,gaji_bersih,nip,kode_petugas) values " & _
            "('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox16.Text & "','" & TextBox17.Text & "','" & TextBox18.Text & "','" & TextBox3.Text & "','" & MenuUtama.Panel1.Text & "')"
            cmd = New OleDbCommand(simpan, Conn)
            cmd.ExecuteNonQuery()

            For baris As Integer = 0 To DGV.Rows.Count - 2
                'simpan ke tabel detail
                Dim SimpanDetail As String = "Insert into detailGaji(Nomor_slip,kode_potongan,jumlah) values " & _
                "('" & TextBox1.Text & "','" & DGV.Rows(baris).Cells(0).Value & "','" & DGV.Rows(baris).Cells(2).Value & "')"
                cmd = New OleDbCommand(SimpanDetail, Conn)
                cmd.ExecuteNonQuery()
            Next baris

            Call Batalkan()
            Call BersihkanGrid()
            Call NomorOtomatis()
            TextBox3.Focus()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class