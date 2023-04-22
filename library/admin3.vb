Imports System.Data.SqlClient
Public Class admin3
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Issued_books"
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()
    End Sub

    Private Sub admin3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        ComboBox1.Items.Add("By Accn_num")
        ComboBox1.Items.Add("By Student reg no")
        ComboBox1.Items.Add("By Book Title")
        ComboBox1.Items.Add("By Student name")
        ComboBox1.Items.Add("By Issue Date")
        ComboBox1.Items.Add("By Return Date")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        admin.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from history"
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        con.Open()
        Dim dt As New DataTable()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        Try
            If ComboBox1.SelectedIndex = 0 Then
                cmd.CommandText = "select * from history where Accn_num='" + TextBox1.Text + "'"
            ElseIf ComboBox1.SelectedIndex = 1 Then
                cmd.CommandText = "select * from history where Student_regno='" + TextBox1.Text + "'"
            ElseIf ComboBox1.SelectedIndex = 2 Then
                cmd.CommandText = "select * from history where Book_title Like '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = 3 Then
                cmd.CommandText = "select * from history where Student_name Like '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = 4 Then
                cmd.CommandText = "select * from history where Issued_date='" + TextBox1.Text + "'"
            ElseIf ComboBox1.SelectedIndex = 5 Then
                cmd.CommandText = "select * from history where Return_date='" + TextBox1.Text + "'"
            End If

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            DataGridView1.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Wrong Input")
        End Try
        con.Close()
    End Sub
End Class