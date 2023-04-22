Imports System.Data.SqlClient
Public Class admin4
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim bktitle, stdname As String
    Private Sub admin4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Hide()
        Label4.Hide()
        Label2.Hide()
        Label5.Hide()
        'Button1.Hide()
        Button6.Hide()
        TextBox2.Hide()
        TextBox3.Hide()
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        admin.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select Accn_no,Total_Copies,title,rsv_cnt from Book_detail where Accn_no = '" + TextBox1.Text + "'"
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        Try
            If dr.Read Then
                Dim cnt As Integer = Integer.Parse(dr.Item(1))
                bktitle = dr.Item(2)
                If cnt > 0 And dr.Item(3) = 1 Then
                    Label3.Show()
                    Label3.Text = "Book can be issued"
                    Label2.Show()
                    TextBox2.Show()
                    Button6.Show()
                    TextBox1.Enabled = False
                Else
                    Label3.Show()
                    Label3.Text = "Book can't be issued"
                End If
            Else
                MessageBox.Show("Book data not available")
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry! Something went wrong.")
        End Try
        con.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select reg_num,token,name from Stu_detail where reg_num = '" + TextBox2.Text + "'"
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        Try
            If dr.Read Then
                Dim cnt As Integer = Integer.Parse(dr.Item(1))
                stdname = dr.Item(2)
                If cnt > 0 Then
                    Label4.Show()
                    Label4.Text = "Student can issue the book"
                    Label5.Show()
                    TextBox3.Show()
                    Label6.Show()
                    Button1.Enabled = True
                    TextBox2.Enabled = False
                    TextBox3.Text = Date.Today.ToString("yyyy-MM-dd")
                Else
                    Label4.Show()
                    Label4.Text = "Student has already issued a book"
                End If
            Else
                MessageBox.Show("Student data not available")
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry! Something went wrong.")
        End Try
        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        reserve.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        admin.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text = "" Then
            MessageBox.Show("Please enter the date")
        Else
            Try
                con.Open()
                cmd = con.CreateCommand
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "insert into issued_books(Accn_no,Book_title, Student_regno,Student_name,Issue_date) values('" + TextBox1.Text + "','" + bktitle + "','" + TextBox2.Text + "','" + stdname + "','" + TextBox3.Text + "')"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Stu_detail set token = 0 where reg_num = '" + TextBox2.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Total_Copies = 0 where Accn_no = '" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                MessageBox.Show("Book issued.")
                clear()
                con.Close()
            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong")
            End Try
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        clear()
    End Sub

    Private Sub clear()
        TextBox1.Text = ""
        TextBox1.Enabled = True
        TextBox2.Text = ""
        TextBox2.Hide()
        TextBox2.Enabled = True
        Label3.Hide()
        TextBox3.Text = ""
        Label4.Hide()
        TextBox3.Hide()
        Button1.Enabled = False
        Button6.Hide()
        Label2.Hide()
        Label5.Hide()
        Label6.Hide()
    End Sub
End Class