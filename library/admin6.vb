Imports System.Data.SqlClient
Public Class admin6
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub admin6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label1.Hide()
        Label2.Hide()
        TextBox1.Hide()
        TextBox2.Hide()
        Button1.Hide()
        Button2.Hide()

        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        Label1.Text = "Current Username"
        Label1.Show()
        Label2.Text = "New Username"
        Label2.Show()
        TextBox1.Show()
        TextBox2.Show()
        Button1.Show()
        Button2.Show()
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        Label1.Text = "Current Password"
        Label1.Show()
        Label2.Text = "New Password"
        Label2.Show()
        TextBox1.Show()
        TextBox2.Show()
        Button1.Show()
        Button2.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        If RadioButton1.Checked = True Then
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Field can't be left blank")
            Else
                cmd.CommandText = "select userid from admin_details where userid = '" + TextBox1.Text + "'"
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader()
                If dr.Read() Then
                    dr.Close()
                    'Dim cmd1 As New SqlCommand
                    cmd = con.CreateCommand
                    cmd.CommandType = CommandType.Text

                    cmd.CommandText = "Update admin_details set userid = '" + TextBox2.Text + "' where userid='" + TextBox1.Text + "'"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Userid changed successfully")

                Else
                    MessageBox.Show("Incorrect userid")
                    con.Close()
                End If
            End If

        ElseIf RadioButton2.Checked = True Then
            If String.IsNullOrWhiteSpace(TextBox1.Text) Or String.IsNullOrWhiteSpace(TextBox2.Text) Then
                MessageBox.Show("Field can't be left blank")
            Else
                cmd.CommandText = "select password from admin_details where password = '" + TextBox1.Text + "'"
                Dim dr As SqlDataReader
                dr = cmd.ExecuteReader()
                If dr.Read() Then
                    dr.Close()
                    'Dim cmd1 As New SqlCommand
                    cmd = con.CreateCommand
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "Update admin_details set password = '" + TextBox2.Text + "' where password='" + TextBox1.Text + "'"
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Password changed successfully")

                Else
                    MessageBox.Show("Incorrect current password")
                    con.Close()
                End If
            End If
        End If
        con.Close()
        admin.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        admin.Show()
        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        admin.Show()
        Me.Close()
    End Sub
End Class