Imports System.Data.SqlClient
Public Class admin11

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer

    Private Sub admin11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
            MessageBox.Show("Please fill all the details")
        Else
            Try
                cmd.CommandText = "Select * from Stu_detail where reg_num='" + TextBox1.Text + "'"
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader
                If dr.Read Then
                    MessageBox.Show("Data already exists.")
                    dr.Close()
                Else
                    dr.Close()
                    cmd.CommandText = "insert into Stu_detail(reg_num,name,department,Branch) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "')"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Data inserted successfully")
                    Clear()
                End If

            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong.")
            End Try
            Me.Close()
            admin1.Show()
        End If
        con.Close()
    End Sub
    Private Sub Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        admin1.Show()
        Me.Close()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim stdnm As Integer
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            namelabel.Show()
            namelabel.Text = "This field can not be blank"
            Button1.Enabled = False
        ElseIf Integer.TryParse(TextBox2.Text, stdnm) Then
            namelabel.Show()
            namelabel.Text = "This field accepts String value"
            Button1.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
            'Button1.Enabled = True
            namelabel.Hide()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim stdnm As Integer
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            deptlabel.Show()
            deptlabel.Text = "This field can not be blank"
            Button1.Enabled = False
        ElseIf Integer.TryParse(TextBox3.Text, stdnm) Then
            deptlabel.Show()
            deptlabel.Text = "This field accepts String value"
            Button1.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
            'Button1.Enabled = True
            deptlabel.Hide()
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Dim stdnm As Integer
        If String.IsNullOrWhiteSpace(TextBox4.Text) Then
            branchlabel.Show()
            branchlabel.Text = "This field can not be blank"
            Button1.Enabled = False
        ElseIf Integer.TryParse(TextBox4.Text, stdnm) Then
            branchlabel.Show()
            branchlabel.Text = "This field accepts String value"
            Button1.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button1.Enabled = False
            Else
                Button1.Enabled = True
            End If
            branchlabel.Hide()
            'Button1.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If String.IsNullOrWhiteSpace(TextBox1.Text) Then
            idlabel.Text = "This field can't be blank."
            Button1.Enabled = False
        Else
            idlabel.Hide()
        End If
        If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub
End Class