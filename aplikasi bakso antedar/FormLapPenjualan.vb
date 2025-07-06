Public Class FormLapPenjualan

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AxCrystalReport1.SelectionFormula = "totext({tbl_transaksi.tgltrans})='" & DateTimePicker1.Value & "'"
        AxCrystalReport1.ReportFileName = "lapharian.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    Private Sub FormLapPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()

        ComboBox1.Items.Add("01 - JANUARI")
        ComboBox1.Items.Add("02 - FEBRUARI")
        ComboBox1.Items.Add("03 - MARET")
        ComboBox1.Items.Add("04 - APRIL")
        ComboBox1.Items.Add("05 - MEI")
        ComboBox1.Items.Add("06 - JUNI")
        ComboBox1.Items.Add("07 - JULI")
        ComboBox1.Items.Add("08 - AGUSTUS")
        ComboBox1.Items.Add("09 - SEPTEMBER")
        ComboBox1.Items.Add("10 - OKTOBER")
        ComboBox1.Items.Add("11 - NOVEMBER")
        ComboBox1.Items.Add("12 - DESEMBER")

        ComboBox2.Items.Clear()
        ComboBox2.Text = Date.Now.Year
        For i As Integer = 0 To 5
            ComboBox2.Items.Add(Date.Now.Year - i)


        Next
        'Label6.Text = "2024, 12, 12"
        'Label7.Text = "2024, 12, 18"

        Label6.Text = Format(DateTimePicker2.Value, "yyyy, MM, dd")
        Label7.Text = Format(DateTimePicker3.Value, "yyyy, MM, dd")

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Silahkan isi bulan atau tahunnya terlebih dahulu")
        Else
            AxCrystalReport1.SelectionFormula = "Month({tbl_transaksi.tgltrans})=" & Val(ComboBox1.Text) & " and year({tbl_transaksi.tgltrans})=" & Val(ComboBox2.Text)
        AxCrystalReport1.ReportFileName = "lapbulanan.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
            AxCrystalReport1.Action = 1
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Ambil tanggal awal dan akhir
        Dim tglAwal As String = "Date(" & Format(DateTimePicker2.Value, "yyyy,MM,dd") & ")"
        Dim tglAkhir As String = "Date(" & Format(DateTimePicker3.Value, "yyyy,MM,dd") & ")"

        ' Perbaikan SelectionFormula
        AxCrystalReport1.SelectionFormula = "{tbl_transaksi.tgltrans} in " & tglAwal & " to " & tglAkhir
        AxCrystalReport1.ReportFileName = "lapmingguan.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        Label6.Text = Format(DateTimePicker2.Value, "yyyy, MM, dd")
    End Sub

    Private Sub DateTimePicker3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker3.ValueChanged
        Label7.Text = Format(DateTimePicker3.Value, "yyyy, MM, dd")
    End Sub
End Class