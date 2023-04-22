Imports System.Data.SqlClient
Public Class admin13
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand

    Private Sub admin13_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Register number")
        ComboBox1.Items.Add("Name")
        ComboBox1.Items.Add("Department")
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        Dim dt As New DataTable()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        Try
            If ComboBox1.SelectedIndex = "0" Then
                cmd.CommandText = "select * from Stu_detail where reg_num='" + TextBox1.Text + "'"
            ElseIf ComboBox1.SelectedIndex = "1" Then
                cmd.CommandText = "select * from Stu_detail where name LIKE '%" + TextBox1.Text + "%'"
            ElseIf ComboBox1.SelectedIndex = "2" Then
                cmd.CommandText = "select * from Stu_detail where Department like '%" + TextBox1.Text + "%'"
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
            MessageBox.Show("Sorry! Something went wrong")
        End Try
        con.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        admin1.Show()
        Me.Close()
    End Sub
End Class