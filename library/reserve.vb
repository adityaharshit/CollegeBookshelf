Imports System.Data.SqlClient
Public Class reserve
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        admin4.Show()
    End Sub

    Private Sub reserve_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand()
        Dim today_date, rsv_date As Date
        Dim diff As Integer
        Dim reg, acc As String
        today_date = Date.Today
        cmd.CommandText = "select Student_regno, Reserved_upto,Accn_no from Reserve"
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        While dr.Read
            rsv_date = Date.Parse(dr.Item(1))
            diff = DateDiff(DateInterval.Day, today_date, rsv_date)
            If diff < 0 Then
                reg = dr.Item(0)
                acc = dr.Item(2)
                del(reg, acc)

            End If

        End While
        dr.Close()
        'displaying data
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Reserve"
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()
    End Sub
    Private Sub del(regnum As String, accno As String)
        Dim con1 As New SqlConnection
        Dim cmd1 As New SqlCommand
        con1.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        con1.Open()
        cmd1 = con1.CreateCommand
        cmd1.CommandType = CommandType.Text
        cmd1.CommandText = "update Stu_detail set rsv_token=1 where reg_num='" + regnum + "'"
        cmd1.ExecuteNonQuery()
        cmd1.CommandText = "Update Book_detail set rsv_cnt=1 where Accn_no='" + accno + "'"
        cmd1.ExecuteNonQuery()
        cmd1.CommandText = "delete from Reserve where Student_regno='" + regnum + "'"
        cmd1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        Dim title, name As String
        Dim isdt As Date
        Dim dr As SqlDataReader
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Then
            MessageBox.Show("Please enter all the details")
        Else

            Try
                cmd.CommandText = "select * from Issued_books where Accn_no='" + TextBox1.Text + "' and Student_regno='" + TextBox2.Text + "'"
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MessageBox.Show("Book can't be reserved because it has been issued by the same student")
                    dr.Close()
                Else
                    dr.Close()
                    cmd.CommandText = "select Accn_no,Total_Copies,rsv_cnt,Title from Book_detail where Accn_no='" + TextBox1.Text + "'"
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr.Item(1) = 0 And dr.Item(2) = 1 Then
                            title = dr.Item(3)
                            dr.Close()
                            'checking for student details
                            cmd.CommandText = "select reg_num,Token,rsv_token,name from Stu_detail where reg_num='" + TextBox2.Text + "'"
                            dr = cmd.ExecuteReader
                            If dr.Read Then
                                If dr.Item(2) = 1 Then
                                    name = dr.Item(3)
                                    dr.Close()
                                    'extracting issue date
                                    cmd.CommandText = "select Issue_date from Issued_books where Accn_no = '" + TextBox1.Text + "'"
                                    dr = cmd.ExecuteReader
                                    If dr.Read Then
                                        isdt = Date.Parse(dr.Item(0))
                                        dr.Close()
                                    Else
                                        MessageBox.Show("Data not found")
                                    End If
                                    isdt = isdt.AddDays(11)
                                    'inserting into reserve table
                                    cmd.CommandText = "insert into reserve(Accn_no,Book_Title,Student_regno,Student_name,Reserved_upto) values('" + TextBox1.Text + "','" + title + "','" + TextBox2.Text + "','" + name + "','" + isdt.ToString("yyyy-MM-dd") + "')"
                                    cmd.ExecuteNonQuery()
                                    'updating student details table
                                    cmd.CommandText = "Update Stu_detail set rsv_token=0 where reg_num='" + TextBox2.Text + "'"
                                    cmd.ExecuteNonQuery()
                                    'updating book details table
                                    cmd.CommandText = "Update Book_detail set rsv_cnt=0 where Accn_no='" + TextBox1.Text + "'"
                                    cmd.ExecuteNonQuery()
                                    MessageBox.Show(title + " has been reserved by " + name)
                                    TextBox1.Text = ""
                                    TextBox2.Text = ""
                                    Button4.PerformClick()
                                Else
                                    MessageBox.Show("Student can't reserve a book")
                                End If
                            Else
                                MessageBox.Show("Student Data doesn't exist")
                                dr.Close()
                            End If

                        Else
                            MessageBox.Show("Book can't be reserved")
                        End If

                    Else
                        MessageBox.Show("Book Data doesn't exist")

                    End If
                End If


            Catch ex As Exception
                MessageBox.Show("Something went wrong!")
            End Try
        End If
        con.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Reserve"
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
        con.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select Accn_no, Student_regno from reserve where Student_regno='" + TextBox2.Text + "'"
        Dim dr As SqlDataReader
        dr = cmd.ExecuteReader
        Try
            If dr.Read Then
                dr.Close()
                cmd.CommandText = "update Stu_detail set rsv_token=1 where reg_num='" + TextBox2.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "Update Book_detail set rsv_cnt=1 where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "delete from reserve where Student_regno='" + TextBox2.Text + "'"
                cmd.ExecuteNonQuery()
                MessageBox.Show("The reservation has been cancelled")
                TextBox1.Text = ""
                TextBox2.Text = ""
                Button4.PerformClick()
            Else
                MessageBox.Show("Book is currently not reserved")
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry! Something went wrong.")
        End Try
        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Open()
        Dim title, name As String
        Dim dr As SqlDataReader
        Dim isdt As Date
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select Accn_no,Total_Copies from Book_detail where Accn_no = '" + TextBox1.Text + "'"
        dr = cmd.ExecuteReader
        Try
            If dr.Read Then
                If dr.Item(1) = 1 Then
                    dr.Close()
                    cmd.CommandText = "select reg_num,token from Stu_detail where reg_num = '" + TextBox2.Text + "'"
                    dr = cmd.ExecuteReader
                    If dr.Read Then
                        If dr.Item(1) = 1 Then
                            dr.Close()
                            cmd.CommandText = "select * from reserve where student_regno='" + TextBox2.Text + "'"
                            dr = cmd.ExecuteReader
                            If dr.Read Then
                                title = dr.Item(1)
                                name = dr.Item(3)
                                isdt = Date.Today
                                dr.Close()
                                cmd.CommandText = "update Stu_detail set rsv_token=1 where reg_num='" + TextBox2.Text + "'"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "update Stu_detail set token=0 where reg_num='" + TextBox2.Text + "'"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "Update Book_detail set rsv_cnt=1 where Accn_no='" + TextBox1.Text + "'"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "Update Book_detail set Total_Copies=0 where Accn_no='" + TextBox1.Text + "'"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "insert into Issued_books(Book_title,Accn_no,Student_name,Student_regno,Issue_date) values('" + title + "','" + TextBox1.Text + "','" + name + "','" + TextBox2.Text + "','" + isdt.ToString("yyyy-MM-dd") + "')"
                                cmd.ExecuteNonQuery()
                                cmd.CommandText = "delete from reserve where Student_regno='" + TextBox2.Text + "'"
                                cmd.ExecuteNonQuery()
                                MessageBox.Show("Book has been issued")
                                TextBox1.Text = ""
                                TextBox2.Text = ""
                                Button4.PerformClick()
                            Else
                                MessageBox.Show("Book not reserved")
                            End If
                        Else
                            MessageBox.Show("Student has already issued another book")
                        End If
                    Else
                        MessageBox.Show("Student data doesn't exist")
                    End If


                Else
                    MessageBox.Show("Book currently issued by someone else")
                End If
            Else
                MessageBox.Show("Book data doesn't exist")
            End If
        Catch ex As Exception
            MessageBox.Show("Sorry! Something went wrong.")
        End Try
        con.Close()
    End Sub
    Private Sub TextBox1_keyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        Dim num As String = "1234567890"
        If Not (Asc(e.KeyChar) = 8) Then
            If Not num.Contains(e.KeyChar.ToString) Then
                MessageBox.Show("please enter a number")
                e.KeyChar = ChrW(0)
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Select Student_regno from Reserve where Accn_no='" + TextBox1.Text + "'"
        Dim dr As SqlDataReader
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            Try
                dr = cmd.ExecuteReader
                If dr.Read Then
                    TextBox2.Text = dr.Item(0)
                    dr.Close()
                Else
                    TextBox2.Text = ""
                End If
            Catch ex As Exception

            End Try
        Else
            TextBox2.Text = ""
        End If
        con.Close()
    End Sub
End Class