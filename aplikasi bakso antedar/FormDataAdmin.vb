Imports System.Data.Odbc
Public Class FormDataAdmin

    Sub Terbuka()
        FormUtama.FileToolStripMenuItem.Enabled = True
        FormUtama.DataToolStripMenuItem.Enabled = True
        FormUtama.TransaksiToolStripMenuItem.Enabled = True
        FormUtama.LaporanToolStripMenuItem.Enabled = True

    End Sub

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        ComboBox1.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True

        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

        Call Koneksi()
        Da = New OdbcDataAdapter("Select noadmin, namaadmin, leveladmin from tbl_admin", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_admin")
        DataGridView1.DataSource = Ds.Tables("tbl_admin")
        DataGridView1.ReadOnly = True
    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox4.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.Items.Add("ADMIN")
        ComboBox1.Items.Add("USER")
    End Sub

    Private Sub FormDataAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
        If FormUtama.STLabel6.Text.ToLower() = "user" Then
            MsgBox("Anda tidak memiliki akses ke halaman ini!", MsgBoxStyle.Exclamation, "Akses Ditolak")
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
            Call NomorOtomatis()
            TextBox1.Enabled = False
            TextBox2.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim InputData As String = "insert into tbl_admin value('" & TextBox1.Text & "','" & TextBox2.Text & "', '" & TextBox4.Text & "', '" & ComboBox1.Text & "')"
                Cmd = New OdbcCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim UpdateData As String = "Update tbl_admin set namaadmin='" & TextBox2.Text & "',passwordadmin= '" & TextBox4.Text & "',leveladmin= '" & ComboBox1.Text & "'where noadmin= '" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(UpdateData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Update Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OdbcCommand("Select * From tbl_admin where noadmin='" & TextBox1.Text & "'", Conn)
            RD = Cmd.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Kode Admin Tidak Ada")
            Else
                TextBox1.Text = RD.Item("noadmin")
                TextBox2.Text = RD.Item("namaadmin")
                TextBox4.Text = RD.Item("passwordadmin")
                ComboBox1.Text = RD.Item("leveladmin")

            End If
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button3.Text = "Delete"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim HapusData As String = "Delete from tbl_admin where noadmin='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OdbcCommand("Select * from tbl_admin where noadmin in (select max(noadmin) from tbl_admin)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        RD = Cmd.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "admin" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 3) + 1
            UrutanKode = "admin" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    
End Class