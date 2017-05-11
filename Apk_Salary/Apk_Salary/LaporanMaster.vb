Imports System.Data.OleDb

Public Class LaporanMaster
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        System.Windows.Forms.Help.ShowHelp(Me, "mengubah posisi database dalam crystal report.doc")
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan golongan.rpt"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan jabatan.rpt"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan potongan.rpt"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan pegawai.rpt"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan petugas.rpt"
        Report.CrystalReportViewer1.Refresh()
        Report.CrystalReportViewer1.RefreshReport()
        Report.Show()
    End Sub
End Class