Imports System.Drawing.Bitmap
Public Class MenuUtama
    Private Declare Function ShellExecute _
 Lib "shell32.dll" _
 Alias "ShellExecuteA" ( _
 ByVal hwnd As Long, _
 ByVal lpOperation As String, _
 ByVal lpFile As String, _
 ByVal lpParameters As String, _
 ByVal lpDirectory As String, _
 ByVal nShowCmd As Long) _
 As Long


    Dim Bergerak As Integer
    Dim Teks As String

    Private Sub GolonganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GolonganToolStripMenuItem.Click
        Golongan.Show()
    End Sub

    Private Sub JabatanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JabatanToolStripMenuItem.Click
        Jabatan.Show()
    End Sub

    Private Sub PotonganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PotonganToolStripMenuItem.Click
        Potongan.Show()
    End Sub

    Private Sub PegawaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PegawaiToolStripMenuItem.Click
        Pegawai.Show()
    End Sub

    Private Sub PetugasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PetugasToolStripMenuItem.Click
        Petugas.Show()
    End Sub

    Private Sub PenggajianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenggajianToolStripMenuItem.Click
        Penggajian.Show()
    End Sub

    Private Sub DataMasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataMasterToolStripMenuItem.Click
        LaporanMaster.Show()
    End Sub

    Private Sub DataPenggajianToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataPenggajianToolStripMenuItem.Click
        LaporanPenggajian.Show()
    End Sub

    Private Sub UtilityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UtilityToolStripMenuItem.Click

    End Sub

    Private Sub CetakSlipGajiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakSlipGajiToolStripMenuItem.Click
        CetakSlipGaji.Show()
    End Sub

    Private Sub GantiPasswordUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GantiPasswordUserToolStripMenuItem.Click
        GantiPasswordUser.Show()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeluarToolStripMenuItem.Click
        If MessageBox.Show("Tutup aplikasi..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Conn.Close()
            End
        End If
    End Sub

    Private Sub MenuUtama_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        'Me.Close()
        'End
    End Sub

    Private Sub MenuUtama_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(27) Then
            If MessageBox.Show("Tutup aplikasi..?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Conn.Close()
                End
            End If
        End If
    End Sub

    Private Sub MenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Teks = Me.Text
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Text = Bergerak
        Teks = Microsoft.VisualBasic.Right(Teks, Len(Teks) - 1) & Microsoft.VisualBasic.Left(Teks, 1)
        Me.Text = Teks

    End Sub

    Private Sub FileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileToolStripMenuItem.Click

    End Sub

    Private Sub ManualBookToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ManualBookToolStripMenuItem.Click
        System.Windows.Forms.Help.ShowHelp(Me, "Buku petunjuk.html")
    End Sub


    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub StatusStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles StatusStrip1.ItemClicked

    End Sub
End Class