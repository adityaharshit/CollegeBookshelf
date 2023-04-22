Imports System.Data.SqlClient
Public Class admin7
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Private Sub admin7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\library\library\library\Database1.mdf;Integrated Security=True"
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        Dim dr As SqlDataReader
        Dim count As Integer = 0
        Try
            'read book data
            cmd.CommandText = "select count(*) from Book_detail"
            dr = cmd.ExecuteReader
            If dr.Read Then
                count = dr.Item(0)
                dr.Close()
            End If
            Label1.Text = "Total available books : " + count.ToString
            'read student data
            cmd.CommandText = "select count(*) from Stu_detail"
            dr = cmd.ExecuteReader
            If dr.Read Then
                count = dr.Item(0)
                dr.Close()
            End If
            Label2.Text = "Total registered students : " + count.ToString
            'read total issued books
            cmd.CommandText = "select count(*) from Issued_books"
            dr = cmd.ExecuteReader
            If dr.Read Then
                count = dr.Item(0)
                dr.Close()
            End If
            Label3.Text = "Currently issued books : " + count.ToString
            'read books issued today
            Dim dat As Date = Date.Today
            dat = dat.ToString("dd/MM/yyyy")
            Dim mydate
            cmd.CommandText = "select Issue_date from Issued_books"
            dr = cmd.ExecuteReader
            count = 0
            While dr.Read
                mydate = dr.Item(0)
                If mydate = dat Then
                    count += 1
                End If

            End While
            dr.Close()

            Label4.Text = "Books issued today : " + count.ToString

            'Books returned today
            cmd.CommandText = "select Return_date from history"
            dr = cmd.ExecuteReader
            count = 0
            While dr.Read
                mydate = dr.Item(0)
                If mydate = dat Then
                    count += 1
                End If

            End While
            dr.Close()

            Label5.Text = "Books returned today : " + count.ToString

            'Total reserved books
            cmd.CommandText = "select count(*) from Reserve"
            dr = cmd.ExecuteReader
            If dr.Read Then
                count = dr.Item(0)
                dr.Close()
            End If
            Label6.Text = "Total reserved books : " + count.ToString

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        con.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        admin.Show()
        Me.Close()
    End Sub
End Class