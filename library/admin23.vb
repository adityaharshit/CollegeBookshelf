Imports System.Data.SqlClient
Public Class admin23
    Dim Con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub admin23_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("By Title")
        ComboBox1.Items.Add("By Author")
        ComboBox1.Items.Add("By Publisher")
        ComboBox1.Items.Add("By Subject")
        ComboBox1.Items.Add("By Department")
        ComboBox1.Items.Add("By ISBN No.")
        ComboBox1.Items.Add("By Accession No.")
        Con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If Con.State = ConnectionState.Open Then
            Con.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Con.Open()
        Dim dt As New DataTable()
        cmd = Con.CreateCommand()
        cmd.CommandType = CommandType.Text
        Try
            If ComboBox1.SelectedIndex = "0" Then
                cmd.CommandText = "select * from Book_detail where Title LIKE '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "1" Then
                cmd.CommandText = "select * from Book_detail where Author LIKE '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "2" Then
                cmd.CommandText = "select * from Book_detail where Publisher like '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "3" Then
                cmd.CommandText = "select * from Book_detail where Subject LIKE '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "4" Then
                cmd.CommandText = "select * from Book_detail where Department LIKE '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "5" Then
                cmd.CommandText = "select * from Book_detail where ISBN = '" + TextBox1.Text + "'"
            ElseIf ComboBox1.SelectedIndex = "6" Then
                cmd.CommandText = "select * from Book_detail where Accn_no = '" + TextBox1.Text + "'"
            End If
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If Not dr.Read Then
                MessageBox.Show("No data found")
                DataGridView1.DataSource = Nothing
                DataGridView1.Refresh()
                dr.Close()
            Else
                dr.Close()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                DataGridView1.DataSource = dt
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry something went wrong!")
        End Try
        Con.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        admin2.Show()
        Me.Close()
    End Sub
End Class