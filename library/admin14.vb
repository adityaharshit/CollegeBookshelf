Imports System.Data.SqlClient
Public Class admin14
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select name, Department, Branch from Stu_detail where reg_num='" + TextBox1.Text + "'"
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.Read Then
                TextBox2.Text = dr.Item(0)
                TextBox3.Text = dr.Item(1)
                TextBox4.Text = dr.Item(2)
                Button2.Enabled = True
                TextBox1.Enabled = False
            Else
                MessageBox.Show("Student data doesn't exist")
            End If

        Else
            MessageBox.Show("please enter student register name")


        End If
        con.Close()
    End Sub

    Private Sub admin14_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        If String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
            MessageBox.Show("Please enter all the field")
        Else
            Try
                cmd.CommandText = "update Stu_detail set name='" + TextBox2.Text + "' where reg_num='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Stu_detail set Department='" + TextBox3.Text + "' where reg_num='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "update Stu_detail set Branch='" + TextBox4.Text + "' where reg_num='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data Updated Successfully.")
                Clear()
            Catch ex As Exception
                MessageBox.Show("Sorry! Something went wrong.")
            End Try
        End If
        con.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        admin1.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        con.Open()
        If Not String.IsNullOrWhiteSpace(TextBox1.Text) Then
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select name, Department, Branch from Stu_detail where reg_num='" + TextBox1.Text + "'"
            Dim dr As SqlDataReader
            dr = cmd.ExecuteReader
            If dr.Read Then
                dr.Close()
                cmd.CommandText = "delete from Stu_detail where reg_num='" + TextBox1.Text + "'"
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data deleted")
                Clear()
            Else
                MessageBox.Show("Student data doesn't exist")
            End If
        Else
            MessageBox.Show("Please enter Register number")
        End If
        con.Close()
    End Sub
    Private Sub Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox1.Enabled = True
        Button2.Enabled = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Clear()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim stdnm As Integer
        If String.IsNullOrWhiteSpace(TextBox2.Text) Then
            namelabel.Show()
            namelabel.Text = "This field can not be blank"
            Button2.Enabled = False
        ElseIf Integer.TryParse(TextBox2.Text, stdnm) Then
            namelabel.Show()
            namelabel.Text = "This field accepts String value"
            Button2.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button2.Enabled = False
            Else
                Button2.Enabled = True
            End If
            namelabel.Hide()
        End If
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim stdnm As Integer
        If String.IsNullOrWhiteSpace(TextBox3.Text) Then
            deptlabel.Show()
            deptlabel.Text = "This field can not be blank"
            Button2.Enabled = False
        ElseIf Integer.TryParse(TextBox3.Text, stdnm) Then
            deptlabel.Show()
            deptlabel.Text = "This field accepts String value"
            Button2.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button2.Enabled = False
            Else
                Button2.Enabled = True
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
            Button2.Enabled = False
        ElseIf Integer.TryParse(TextBox4.Text, stdnm) Then
            branchlabel.Show()
            branchlabel.Text = "This field accepts String value"
            Button2.Enabled = False
        Else
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Or String.IsNullOrWhiteSpace(TextBox3.Text) Or String.IsNullOrWhiteSpace(TextBox4.Text) Then
                Button2.Enabled = False
            Else
                Button2.Enabled = True
            End If
            branchlabel.Hide()
            'Button1.Enabled = True
        End If
    End Sub
End Class