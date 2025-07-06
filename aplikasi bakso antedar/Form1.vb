Imports System.Data.Odbc
Public Class Form1

    Sub Terbuka()
        FormUtama.FileToolStripMenuItem.Enabled = True
        FormUtama.LogoutToolStripMenuItem.Enabled = True
        FormUtama.DataToolStripMenuItem.Enabled = True
        FormUtama.TransaksiToolStripMenuItem.Enabled = True
        FormUtama.LaporanToolStripMenuItem.Enabled = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Validasi jika nomor admin atau password kosong
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Nomor Admin dan Password Tidak Boleh Kosong!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Koneksi ke database
        Call Koneksi()

        ' Query untuk mengecek data admin
        Cmd = New OdbcCommand("SELECT * FROM tbl_admin WHERE noadmin='" & TextBox1.Text & "' AND passwordadmin='" & TextBox2.Text & "'", Conn)
        RD = Cmd.ExecuteReader()
        RD.Read()

        If RD.HasRows Then
            ' Jika login berhasil
            MsgBox("Login berhasil!", MsgBoxStyle.Information)
            FormUtama.STLabel2.Text = RD!noadmin
            FormUtama.STLabel4.Text = RD!namaadmin
            FormUtama.STLabel6.Text = RD!leveladmin
            If FormUtama.STLabel6.Text = "user" Then
                FormUtama.AdminToolStripMenuItem.Enabled = False
            End If
            ' Tampilkan Form1 dan tutup FormLogin
            Me.Hide()
            FormUtama.ShowDialog()
        Else
            ' Jika login gagal
            MsgBox("Nomor Admin atau Password salah!", MsgBoxStyle.Critical)
        End If
    End Sub

    ' Reset input pada form login
    Sub KondisiAwal()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
    End Sub

    ' Inisialisasi form saat pertama kali dimuat
    Private Sub FormLogin_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class
