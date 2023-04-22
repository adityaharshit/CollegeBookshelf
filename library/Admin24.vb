Imports System.Data.SqlClient
Public Class Admin24
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        admin2.Show()
        Me.Close()
    End Sub

    Private Sub Admin24_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please Enter Accession number")
        Else
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select Title,Publisher,Author,Department,Subject,Edition,Language,ISBN from Book_detail where Accn_no='" + TextBox1.Text + "'"
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.Read Then
                title.Text = dr.Item(0)
                publisher.Text = dr.Item(1)
                author.Text = dr.Item(2)
                department.Text = dr.Item(3)
                subject.Text = dr.Item(4)
                edition.Text = dr.Item(5)
                language.Text = dr.Item(6)
                isbn.Text = dr.Item(7)
                dr.Close()
                TextBox1.Enabled = False
                Button2.Enabled = True
            Else
                MessageBox.Show("No Data Found")
            End If
        End If
        con.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Open()
        cmd.CommandType = CommandType.Text
        If String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Then
            MessageBox.Show("A field can't be left blank")
        Else
            Try
                cmd.CommandText = "update Book_detail set Title='" + title.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Author='" + author.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "update Book_detail set ISBN='" + isbn.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Department='" + department.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Publisher='" + publisher.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Subject='" + subject.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Language='" + language.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Book_detail set Edition='" + edition.Text + "' where Accn_no='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                MessageBox.Show("Record Updated")
                clear()
            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong")
            End Try


        End If
        con.Close()
    End Sub
    Private Sub clear()
        TextBox1.Text = ""
        TextBox1.Enabled = True
        Button2.Enabled = False
        title.Text = ""
        author.Text = ""
        publisher.Text = ""
        subject.Text = ""
        department.Text = ""
        language.Text = ""
        edition.Text = ""
        isbn.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            MessageBox.Show("Please Enter Book Accession Number")
        Else
            cmd.CommandText = "delete from Book_detail where Accn_no='" + TextBox1.Text + "'"
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data deleted")
            clear()
        End If
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
End Class