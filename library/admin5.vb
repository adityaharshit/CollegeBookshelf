Imports System.Data.SqlClient
Public Class admin5
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dt As Date
    Private Sub admin5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        Dim dr As SqlDataReader
        Dim diff As Integer
        Dim fine As Double
        Dim isdt, rtdt As Date
        Dim bktitle, stdname As String
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        'checking for details in issued_books table
        cmd.CommandText = "select Book_title,Accn_no,Student_name,Student_regno,Issue_date from Issued_books where Accn_no = '" + TextBox1.Text + "' and Student_regno='" + TextBox2.Text + "' "
        dr = cmd.ExecuteReader
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Then
            MessageBox.Show("Field can't be left blank")
        Else
            Try
                If dr.Read Then

                    isdt = Date.Parse(dr.Item(4))
                    TextBox4.Text = isdt.ToString("yyyy-MM-dd")
                    rtdt = Date.Parse(TextBox3.Text)
                    bktitle = dr.Item(0)
                    stdname = dr.Item(2)

                    dr.Close()



                    'Calculate fine
                    diff = DateDiff(DateInterval.Day, isdt, rtdt)
                    'MessageBox.Show(diff)
                    If diff > 10 Then
                        fine = ((diff - 10) * 10)
                        If MessageBox.Show("Fine on the book is Rs. " + fine.ToString + ". Collect now?", "Fine", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                            Dim collectamt As Integer = 0
                            While True
                                Dim inputtext As String = InputBox("Enter the fine to be collected. Total fine to be collected is:" + fine.ToString)
                                'If InputBox= Windows.Forms.DialogResult
                                If Not String.IsNullOrWhiteSpace(inputtext) Then
                                    If Integer.TryParse(inputtext, collectamt) Then
                                        If collectamt > fine Then
                                            MessageBox.Show("Fine to be collected is greater than the fine for the student")
                                        Else
                                            fine = fine - collectamt
                                            cmd.CommandText = "update Stu_detail set fine = '" + fine.ToString + "' where reg_num='" + TextBox2.Text + "'"
                                            cmd.ExecuteNonQuery()
                                            MessageBox.Show("Fine has been collected")
                                            collectamt = 0
                                            Exit While
                                        End If
                                    Else
                                        MessageBox.Show("Wrong input")
                                    End If

                                Else
                                    Exit While
                                End If

                            End While
                        End If
                    Else
                        fine = 0
                    End If


                    TextBox5.Text = fine
                    'inserting into history table
                    cmd.CommandText = "insert into history(Accn_num,Book_title,Student_name,Student_regno,Issued_date,Return_date,Fine) values('" + TextBox1.Text + "','" + bktitle + "','" + stdname + "','" + TextBox2.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + TextBox5.Text + "')"
                    cmd.ExecuteNonQuery()
                    'updating total_copies value
                    cmd.CommandText = "update Book_detail set Total_Copies = 1 where Accn_no = '" + TextBox1.Text + "'"
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "update Book_detail set Last_issued_by='" + TextBox2.Text + "' where Accn_no = '" + TextBox1.Text + "'"
                    cmd.ExecuteNonQuery()
                    'updating token value
                    cmd.CommandText = "update Stu_detail set token = 1 where reg_num = '" + TextBox2.Text + "'"
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "update Stu_detail set Last_issued_book='" + TextBox1.Text + "' where reg_num = '" + TextBox2.Text + "'"
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "update Stu_detail set Fine= Fine + '" + TextBox5.Text + "' where reg_num = '" + TextBox2.Text + "'"
                    cmd.ExecuteNonQuery()
                    'delete data from isued_books table
                    cmd.CommandText = "delete from Issued_books where Accn_no='" + TextBox1.Text + "'"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Book returned.")
                    clear()
                Else
                    MessageBox.Show("Wrong input")
                End If
            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong")
            End Try

        End If

        con.Close()
    End Sub
    Private Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        admin.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        con.Open()
        Dim regnum As String
        Dim fineamt, collectamt As Integer
        collectamt = 0
        Dim inputtext As String
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        regnum = InputBox("Enter the student register number : ")
        If Not String.IsNullOrWhiteSpace(regnum) Then
            cmd.CommandText = "Select reg_num, Fine from Stu_detail where reg_num= '" + regnum + "'"
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            Try
                If dr.Read Then
                    If dr.Item(1) = 0 Then
                        MessageBox.Show("Student has no fine")
                    Else
                        fineamt = dr.Item(1)
                        dr.Close()

                        While True
                            inputtext = InputBox("Enter the fine to be collected. Total fine to be collected is:" + fineamt.ToString)
                            If Not String.IsNullOrEmpty(inputtext) Then
                                If Integer.TryParse(inputtext, collectamt) Then
                                    If collectamt > fineamt Then
                                        MessageBox.Show("Fine to be collected is greater than the fine for the student")
                                    Else
                                        fineamt = fineamt - collectamt
                                        cmd.CommandText = "update Stu_detail set fine = '" + fineamt.ToString + "' where reg_num='" + regnum + "'"
                                        cmd.ExecuteNonQuery()
                                        MessageBox.Show("Fine has been collected")
                                        Exit While
                                    End If
                                Else
                                    MessageBox.Show("Wrong input")
                                End If
                            Else
                                Exit While
                            End If
                        End While
                    End If
                Else
                    MessageBox.Show("student data doesn't exist")
                End If
            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong.")
            End Try

        End If

        con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        admin.Show()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        TextBox3.Text = Date.Today.ToString("yyyy-MM-dd")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        Label6.Show()
        Dim rtdt As Date
        Dim accn, diff, fine As Integer
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            If Integer.TryParse(TextBox1.Text, accn) Then
                cmd.CommandText = "select Student_regno,Issue_date from Issued_books where Accn_no = '" + TextBox1.Text + "'"
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                If dr.Read Then
                    TextBox2.Text = dr.Item(0)
                    dt = Date.Parse(dr.Item(1))
                    TextBox4.Text = dt.ToString("yyyy-MM-dd")
                    'for calculating fine
                    rtdt = Date.Parse(TextBox3.Text)
                    diff = DateDiff(DateInterval.Day, dt, rtdt)
                    If diff > 10 Then
                        fine = ((diff - 10) * 10)
                    Else
                        fine = 0
                    End If
                    TextBox5.Text = fine
                    'calculated
                    Label6.Hide()
                Else
                    Label6.Text = "Book not issued by anyone"
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                End If
                dr.Close()
            Else
                Label6.Text = "Invalid input"
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
            End If

        Else
            Label6.Hide()

        End If
        con.Close()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim rtdt As Date
        Dim diff, fine As Integer
        If Not String.IsNullOrWhiteSpace(TextBox3.Text) Then
            If Date.TryParse(TextBox3.Text, rtdt) Then
                Label7.Hide()
                diff = DateDiff(DateInterval.Day, dt, rtdt)
                If diff > 10 Then
                    fine = (diff - 10) * 10
                    Button1.Enabled = True
                    Label7.Hide()
                ElseIf diff < 0 Then
                    Label7.Show()
                    Button1.Enabled = False
                    Label7.Text = "Return date can not be before issue date"
                Else
                    Button1.Enabled = True
                    fine = 0
                    Label7.Hide()
                End If
                TextBox5.Text = fine
            Else
                Label7.Show()
                Label7.Text = "Invalid Date format(YYYY-MM-DD)"
            End If
        Else
            Label7.Show()
            Label7.Text = "This field can't be left blank"
        End If


    End Sub
End Class