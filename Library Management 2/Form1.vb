Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim i As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=WIZKID;Initial Catalog=LibraryDB;Integrated Security=True;TrustServerCertificate=True"
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()
        Disp_Data()

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "Insert into Books values('" + txtTitle.Text + "','" + txtAuthor.Text + "','" + txtYearPublished.Text + "','" + txtGenre.Text + "')"
        cmd.ExecuteNonQuery()
        MsgBox("Record Inserted Succesfully")
        Disp_Data()
    End Sub
    Public Sub Disp_Data()
        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Books"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        dgvBooks.DataSource = dt



    End Sub

    Private Sub dgvBooks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellContentClick

    End Sub

    Private Sub dgvBooks_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellClick
        Try
            If con.State = ConnectionState.Open Then
                con.Close()

            End If
            con.Open()
            i = Convert.ToInt32(dgvBooks.SelectedCells.Item(0).Value.ToString())
            cmd = con.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Books where id=" & i & ""
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                txtTitle.Text = dr.GetString(1).ToString()
                txtAuthor.Text = dr.GetString(2).ToString()
                txtYearPublished.Text = dr.GetInt32(3).ToString()
                txtGenre.Text = dr.GetString(4).ToString()
            End While
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "update Books set Title='" + txtTitle.Text + "',Author='" + txtAuthor.Text + "',YearPublished='" + txtYearPublished.Text + "',Genre='" + txtGenre.Text + "'where id=" & i & ""
        cmd.ExecuteNonQuery()

        Disp_Data()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox("Only Type In Title when using delete")
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Books where Title='" + txtTitle.Text + "'"
        cmd.ExecuteNonQuery()
        Disp_Data()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If con.State = ConnectionState.Open Then
            con.Close()

        End If
        con.Open()

        cmd = con.CreateCommand
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from Books"
        cmd.ExecuteNonQuery()
        Disp_Data()
    End Sub


End Class
