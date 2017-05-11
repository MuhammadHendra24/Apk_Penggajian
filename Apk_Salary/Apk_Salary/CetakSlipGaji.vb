Imports System.Data.OleDb

Public Class CetakSlipGaji
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Or TextBox1.Text = "" Then
            MsgBox("Pilih dulu bulan, tahun dan isi NIP")
            Exit Sub
        Else
            Report.CrystalReportViewer1.ReportSource = "D:\Skripsi\Apk_Salary\Apk_Salary\laporan gaji per nip.rpt"
            Report.CrystalReportViewer1.SelectionFormula = "{gaji.nip}='" & TextBox1.Text & "' and Month({gaji.Tanggal})=" & Val(ComboBox1.Text) & " and Year({gaji.Tanggal})=" & Val(ComboBox2.Text)
            Report.CrystalReportViewer1.Refresh()
            Report.CrystalReportViewer1.RefreshReport()
            Report.Show()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            TextBox1.Text = UCase(TextBox1.Text)
            cmd = New OleDbCommand("select * from pegawai where nip='" & UCase(TextBox1.Text) & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows Then
                TextBox2.Text = rd.Item(1)
                ComboBox1.Focus()
            Else
                MsgBox("NIP tidak terdaftar")
                DaftarPegawai.Show()
            End If
        End If
    End Sub

    Private Sub CetakSlipGaji_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 1 To 12
            ComboBox1.Items.Add(i)
        Next

        For q As Integer = 2016 To 2017
            ComboBox2.Items.Add(q)
        Next
    End Sub
End Class