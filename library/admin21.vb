Imports System.Data.SqlClient
Public Class admin21
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub admin21_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
            MessageBox.Show("Please fill all the details!")
        Else
            Try
                cmd.CommandText = "select * from Book_detail where Accn_no='" + accn.Text + "'"
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MessageBox.Show("Data already exists!")
                    dr.Close()
                Else
                    dr.Close()
                    cmd.CommandText = "insert into Book_detail(ISBN,Title, Author, Accn_no,Publisher,Subject,Department,Language,Edition,Total_Copies) values('" + isbn.Text + "','" + title.Text + "','" + author.Text + "','" + accn.Text + "','" + publisher.Text + "','" + subject.Text + "','" + department.Text + "','" + language.Text + "','" + edition.Text + "','" + count.Text + "' )"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Data inserted successfully")
                    Clear()
                End If

            Catch ex As Exception
                MessageBox.Show("Something went wrong!")
            End Try
        End If
        con.Close()
    End Sub

    Private Sub Clear()
        accn.Text = ""
        count.Text = ""
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
        admin2.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        admin2.Close()
        Me.Close()
    End Sub

    Private Sub accn_TextChanged(sender As Object, e As EventArgs) Handles accn.TextChanged
        Dim accnno As Integer
        If Integer.TryParse(accn.Text, accnno) Then
            Label11.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        ElseIf String.IsNullOrWhiteSpace(accn.Text) Then
            Label11.Show()
            Label11.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label11.Show()
            Label11.Text = "This field only accepts 10 values"
            Button1.Enabled = False

        End If
    End Sub

    Private Sub isbn_TextChanged(sender As Object, e As EventArgs) Handles isbn.TextChanged
        Dim isbnno As Double
        If Double.TryParse(isbn.Text, isbnno) Then
            Label12.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        ElseIf String.IsNullOrWhiteSpace(isbn.Text) Then
            Label12.Show()
            Label12.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label12.Show()
            Label12.Text = "This field only accepts integer values"
            Button1.Enabled = False
        End If
    End Sub

    Private Sub count_TextChanged(sender As Object, e As EventArgs) Handles count.TextChanged
        Dim cnt As Integer
        If Integer.TryParse(count.Text, cnt) Then
            Label13.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        ElseIf String.IsNullOrWhiteSpace(count.Text) Then
            Label13.Show()
            Label13.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label13.Show()
            Label13.Text = "This field only accepts integer values"
            Button1.Enabled = False
        End If
    End Sub

    Private Sub department_TextChanged(sender As Object, e As EventArgs) Handles department.TextChanged
        Dim dept As Integer
        If Integer.TryParse(department.Text, dept) Then
            Label14.Show()
            Label14.Text = "This field only accepts character values"
            Button1.Enabled = False
        ElseIf String.IsNullOrWhiteSpace(department.Text) Then
            Label14.Show()
            Label14.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label14.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub author_TextChanged(sender As Object, e As EventArgs) Handles author.TextChanged
        Dim dept As Integer
        If Integer.TryParse(author.Text, dept) Then
            Label15.Show()
            Label15.Text = "This field only accepts character values"
            Button1.Enabled = False
        ElseIf String.IsNullOrWhiteSpace(author.Text) Then
            Label15.Show()
            Label15.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label15.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub publisher_TextChanged(sender As Object, e As EventArgs) Handles publisher.TextChanged
        Dim dept As Integer
        If Integer.TryParse(publisher.Text, dept) Then
            Label16.Show()
            Label16.Text = "This field only accepts character values"
            Button1.Enabled = False
        ElseIf String.IsNullOrWhiteSpace(publisher.Text) Then
            Label16.Show()
            Label16.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label16.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub subject_TextChanged(sender As Object, e As EventArgs) Handles subject.TextChanged
        Dim dept As Integer
        If Integer.TryParse(subject.Text, dept) Then
            Label17.Show()
            Label17.Text = "This field only accepts character values"
            Button1.Enabled = False
        ElseIf String.IsNullOrWhiteSpace(subject.Text) Then
            Label17.Show()
            Label17.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label17.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub language_TextChanged(sender As Object, e As EventArgs) Handles language.TextChanged
        Dim dept As Integer
        If Integer.TryParse(author.Text, dept) Then
            Label18.Show()
            Label18.Text = "This field only accepts character values"
            Button1.Enabled = False
        ElseIf String.IsNullOrWhiteSpace(language.Text) Then
            Label18.Show()
            Label18.Text = "This field can't be left blank"
            Button1.Enabled = False
        Else
            Label18.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub title_TextChanged(sender As Object, e As EventArgs) Handles title.TextChanged
        If String.IsNullOrEmpty(title.Text) Then
            Label19.Show()
            Label19.Text = "This field can't be left blank"
        Else
            Label19.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub edition_TextChanged(sender As Object, e As EventArgs) Handles edition.TextChanged
        If String.IsNullOrEmpty(edition.Text) Then
            Label20.Show()
            Label20.Text = "This field can't be left blank"
        Else
            Label20.Hide()
            If String.IsNullOrWhiteSpace(accn.Text) Or String.IsNullOrWhiteSpace(title.Text) Or String.IsNullOrWhiteSpace(author.Text) Or String.IsNullOrWhiteSpace(publisher.Text) Or String.IsNullOrWhiteSpace(department.Text) Or String.IsNullOrWhiteSpace(subject.Text) Or String.IsNullOrWhiteSpace(edition.Text) Or String.IsNullOrWhiteSpace(language.Text) Or String.IsNullOrWhiteSpace(isbn.Text) Or String.IsNullOrWhiteSpace(count.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
        End If
    End Sub
End Class