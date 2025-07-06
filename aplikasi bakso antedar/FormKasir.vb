Imports System.Data.Odbc
Public Class FormKasir

    Dim tglMysql As String
    Sub KondisiAwal()
        lbltanggal.Text = Today
        lbladmin.Text = FormUtama.STLabel4.Text
        lblkembali.Text = ""
        TextBox2.Text = ""
        lblnamabrng.Text = ""
        lblharga.Text = ""
        TextBox3.Text = ""
        TextBox3.Enabled = False
        lblitem.Text = ""
        Call NomorOtomatis()
        Call BuatKolom()
        Label9.Text = "0"
        TextBox1.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        lbljam.Text = TimeOfDay
    End Sub

    Private Sub FormKasir_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Sub NomorOtomatis()
        Call Koneksi()
        Cmd = New OdbcCommand("Select * from tbl_transaksi where notrans in (select max(notrans) from tbl_transaksi)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        RD = Cmd.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "T" + Format(Now, "yyMMdd") + "001"
        Else
            Dim lastNotrans As String = RD.GetString(0).Substring(8) ' Mengambil 3 digit terakhir (misalnya 001, 002, dst.)
            Hitung = Long.Parse(lastNotrans) + 1
            UrutanKode = "T" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung.ToString(), 3) ' Format dengan padding nol
        End If
        lblnojual.Text = UrutanKode

    End Sub

    Sub BuatKolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode")
        DataGridView1.Columns.Add("Nama", "Nama Menu")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Jumlah", "Jumlah")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            Cmd = New OdbcCommand("Select * From tbl_menu where nomenu='" & TextBox2.Text & "'", Conn)
            RD = Cmd.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Kode Menu Tidak Ada")
            Else
                TextBox2.Text = RD.Item("nomenu")
                lblnamabrng.Text = RD.Item("namamenu")
                lblharga.Text = RD.Item("hargamenu")
                lbljumlah.Text = RD.Item("stokmenu")
                TextBox3.Enabled = True

            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If lblnamabrng.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Silahkan Masukan Nomor Menu dan Tekan Enter!")
        Else
            If Val(lbljumlah.Text) < Val(TextBox3.Text) Then
                MsgBox("Stok Kurang!")
            Else
                DataGridView1.Rows.Add(New String() {
               TextBox2.Text,
               lblnamabrng.Text,
               lblharga.Text,
               TextBox3.Text,
               Val(lblharga.Text) * Val(TextBox3.Text)
            })
                Call RumusSubtotal()
                TextBox2.Text = ""
                lblnamabrng.Text = ""
                lblharga.Text = ""
                TextBox3.Text = ""
                Call RumusCariItem()
            End If
        End If


    End Sub

    Sub RumusSubtotal()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitung = hitung + DataGridView1.Rows(i).Cells(4).Value
            Label9.Text = hitung
        Next
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(Label9.Text) Then
                MsgBox("Pembayaran Kurang!")
            ElseIf Val(TextBox1.Text) = Val(Label9.Text) Then
                lblkembali.Text = 0
            ElseIf Val(TextBox1.Text) > Val(Label9.Text) Then
                lblkembali.Text = Val(TextBox1.Text) - Val(Label9.Text)
                Button1.Focus()
            End If
        End If
    End Sub

    Sub RumusCariItem()
        Dim hitungItem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitungItem = hitungItem + DataGridView1.Rows(i).Cells(3).Value
            lblitem.Text = hitungItem
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If lblkembali.Text = "" Or Label9.Text = "" Then
            MsgBox("Transaksi Tidak Ada, Silahkan Lakukan Transaksi Terlebih Dahulu!")
        Else
            tglMysql = Format(Today, "yyy-MM-dd")
            Dim SimpanTransaksi As String = "Insert into tbl_transaksi values ('" & lblnojual.Text & "', '" & tglMysql & "', '" & lbljam.Text & "', '" & lblitem.Text & "', '" & Label9.Text & "', '" & TextBox1.Text & "', '" & lblkembali.Text & "',  '" & FormUtama.STLabel2.Text & "')"
            Cmd = New OdbcCommand(SimpanTransaksi, Conn)
            Cmd.ExecuteNonQuery()

            For baris As Integer = 0 To DataGridView1.Rows.Count - 2
                Dim SimpanDetail As String = "Insert into tbl_detailtransaksi values ('" & lblnojual.Text & "','" & DataGridView1.Rows(baris).Cells(0).Value & "', '" & DataGridView1.Rows(baris).Cells(1).Value & "', '" & DataGridView1.Rows(baris).Cells(2).Value & "', '" & DataGridView1.Rows(baris).Cells(3).Value & "', '" & DataGridView1.Rows(baris).Cells(4).Value & "')"
                Cmd = New OdbcCommand(SimpanDetail, Conn)
                Cmd.ExecuteNonQuery()

                Cmd = New OdbcCommand("Select * from tbl_menu where nomenu= '" & DataGridView1.Rows(baris).Cells(0).Value & "'", Conn)
                RD = Cmd.ExecuteReader
                RD.Read()
                Dim kurangiStok As String = "Update tbl_menu set stokmenu = '" & RD.Item("stokMenu") - DataGridView1.Rows(baris).Cells(3).Value & "' where nomenu= '" & DataGridView1.Rows(baris).Cells(0).Value & "'"
                Cmd = New OdbcCommand(kurangiStok, Conn)
                Cmd.ExecuteNonQuery()
            Next

            If MessageBox.Show("Apakah ingin cetak nota...?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                AxCrystalReport1.SelectionFormula = "totext({tbl_transaksi.notrans})='" & lblnojual.Text & "'"
                AxCrystalReport1.ReportFileName = "notajual.rpt"
                AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
                AxCrystalReport1.RetrieveDataFiles()
                AxCrystalReport1.Action = 1
                Call KondisiAwal()

                Call KondisiAwal()
                MsgBox("Transaksi Telah Berhasil Disimpan")
            Else
            End If

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub
End Class