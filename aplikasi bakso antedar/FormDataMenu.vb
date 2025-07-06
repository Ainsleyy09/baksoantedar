Imports System.Data.Odbc
Public Class FormDataMenu

    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox4.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox4.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = "False"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True

        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"

        Call Koneksi()
        Da = New OdbcDataAdapter("Select * from tbl_menu", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_menu")
        DataGridView1.DataSource = Ds.Tables("tbl_menu")
        DataGridView1.ReadOnly = True
    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox4.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        Call MunculSatuan()
    End Sub

    Sub MunculSatuan()
        Call Koneksi()
        Cmd = New OdbcCommand("select distinct satuanmenu from tbl_menu", Conn)
        RD = Cmd.ExecuteReader
        ComboBox1.Items.Clear()
        Do While RD.Read
            ComboBox1.Items.Add(RD.Item("satuanmenu"))
        Loop
    End Sub

    Private Sub FormDataBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim InputData As String = "insert into tbl_menu value('" & TextBox1.Text & "','" & TextBox2.Text & "', '" & TextBox4.Text & "', '" & TextBox3.Text & "', '" & ComboBox1.Text & "')"
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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim UpdateData As String = "Update tbl_menu set namamenu='" & TextBox2.Text & "',hargamenu= '" & TextBox4.Text & "',stokmenu= '" & TextBox3.Text & "', satuanmenu= '" & ComboBox1.Text & "' where nomenu= '" & TextBox1.Text & "'"
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
            Cmd = New OdbcCommand("Select * From tbl_menu where nomenu='" & TextBox1.Text & "'", Conn)
            RD = Cmd.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Nomor Menu Tidak Ada")
            Else
                TextBox1.Text = RD.Item("nomenu")
                TextBox2.Text = RD.Item("namamenu")
                TextBox4.Text = RD.Item("hargamenu")
                TextBox3.Text = RD.Item("stokmenu")
                ComboBox1.Text = RD.Item("satuanmenu")

            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

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
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Silahkan isi semua field")
            Else
                Call Koneksi()
                Dim HapusData As String = "Delete from tbl_menu where nomenu='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Hapus Data Berhasil")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OdbcCommand("Select * from tbl_menu where nomenu in (select max(nomenu) from tbl_menu)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        RD = Cmd.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "MNB" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 3) + 1
            UrutanKode = "MNB" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub
End Class