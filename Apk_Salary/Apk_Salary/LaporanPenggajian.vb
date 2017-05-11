Imports System.Data.OleDb

Public Class LaporanPenggajian
    Sub BulanTahun()
        da = New OleDbDataAdapter("Select distinct month(tanggal) as Bulan,year(tanggal) as Tahun from gaji", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "bulanan")
        DGV1.DataSource = (ds.Tables("Bulanan"))
        DGV1.ReadOnly = True
        DGV1.Columns(0).Width = 50
        DGV1.Columns(1).Width = 75
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Pilih dulu bulan dan tahun")
            Exit Sub
        Else
            ' Dim aRpt As New laporan_penggajian
            ' aRpt.SetDataSource(ds)
            Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan penggajian.rpt"
            Report.CrystalReportViewer1.SelectionFormula = "Month({gaji.Tanggal})=" & Val(TextBox1.Text) & " and Year({gaji.Tanggal})=" & Val(TextBox2.Text)
            Report.CrystalReportViewer1.Refresh()
            Report.CrystalReportViewer1.RefreshReport()
            Report.Show()

        End If
    End Sub

    Sub Tampilkan()
        da = New OleDbDataAdapter("Select pegawai.NIP,Nama_Pegawai,Tanggal,Nomor_Slip from gaji,pegawai where pegawai.nip=gaji.nip", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Pegawai")
        DGV.DataSource = (ds.Tables("Pegawai"))
        DGV.ReadOnly = True
        DGV.Columns(0).Width = 50
        DGV.Columns(2).Width = 75
        DGV.Columns(3).Width = 100
    End Sub
    Private Sub LaporanPenggajian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call BulanTahun()
        Call Tampilkan()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan gaji per nip.rpt"
        Report.CrystalReportViewer1.SelectionFormula = "{gaji.nip}='" & TextBox3.Text & "' and {gaji.nomor_slip}='" & TextBox6.Text & "'"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()
    End Sub

    Private Sub DGV_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV.RowEnter
        TextBox3.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        TextBox4.Text = DGV.Rows(e.RowIndex).Cells(1).Value
        TextBox5.Text = DGV.Rows(e.RowIndex).Cells(2).Value
        TextBox6.Text = DGV.Rows(e.RowIndex).Cells(3).Value
    End Sub

    Private Sub DGV1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV1.RowEnter
        TextBox1.Text = DGV1.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DGV1.Rows(e.RowIndex).Cells(1).Value
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub
End Class