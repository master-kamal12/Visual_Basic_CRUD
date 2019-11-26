Imports System.Data.Odbc
Public Class Form1
    Sub TampilGrid()
        bukakoneksi()

        DA = New OdbcDataAdapter("SELECT * FROM tbl_mahasiswa ", CONN)
        DS = New DataSet
        DA.Fill(DS, "tbl_mahasiswa")
        DataGridView1.DataSource = DS.Tables("tbl_mahasiswa")

        tutupkoneksi()
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        TampilGrid()
        Munculcombo()
    End Sub

    Sub KosongkanData()
        txtNIM.Text = ""
        txtNama.Text = ""
        txtAlamat.Text = ""
        txtTelepon.Text = ""
        cmbJurusan.Text = ""
    End Sub

    Sub Munculcombo()
        cmbJurusan.Items.Add("Ilmu Komputer")
        cmbJurusan.Items.Add("Kimia")
        cmbJurusan.Items.Add("Fisika")
        cmbJurusan.Items.Add("Matematika")
    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click
        If txtNIM.Text = "" Or txtNama.Text = "" Or txtAlamat.Text = "" Or txtTelepon.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            bukakoneksi()
            Dim simpan As String = "insert into tbl_mahasiswa values ('" & txtNIM.Text & "','" & txtNama.Text & "', '" & txtAlamat.Text & "', '" & txtTelepon.Text & "', '" & cmbJurusan.Text & "')"

            CMD = New OdbcCommand(simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input data berhasil")
            TampilGrid()
            KosongkanData()

            tutupkoneksi()
        End If
    End Sub

    Private Sub txtNIM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNIM.KeyPress
        txtNIM.MaxLength = 6
        If e.KeyChar = Chr(13) Then
            bukakoneksi()
            CMD = New OdbcCommand("Select * From tbl_mahasiswa where nim_mhs='" & txtNIM.Text & "'", CONN)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("NIM Tidak Ada Silahkan Coba Lagi!")
                txtNIM.Focus()
            Else
                txtNama.Text = RD.Item("nama_mhs")
                txtAlamat.Text = RD.Item("alamat_mhs")
                txtTelepon.Text = RD.Item("telepon_mhs")
                cmbJurusan.Text = RD.Item("jurusan_mhs")
                txtNama.Focus()
            End If
        End If
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnEdit.Click
        bukakoneksi()
        Dim edit As String = "update tbl_mahasiswa set
        nama_mhs='" & txtNama.Text & "',
        alamat_mhs='" & txtAlamat.Text & "',
        telepon_mhs='" & txtTelepon.Text & "',
        jurusan_mhs='" & cmbJurusan.Text & "' where nim_mhs='" & txtNIM.Text & "'"

        CMD = New OdbcCommand(edit, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data Berhasil di Update")
        TampilGrid()
        KosongkanData()
        tutupkoneksi()
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If txtNIM.Text = "" Then
            MsgBox("Silahkan pilih data yang akan dihapus dengan masukkan NIM dan Enter")
        Else
            If MessageBox.Show("Yakin akan dihapus ?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                bukakoneksi()
                Dim hapus As String = "delete From tbl_mahasiswa where nim_mhs='" & txtNIM.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                TampilGrid()
                KosongkanData()
                tutupkoneksi()
            End If
        End If
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        Me.Close()
    End Sub
End Class
