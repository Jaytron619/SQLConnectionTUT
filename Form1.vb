Imports System.Data
Imports System.Data.SqlClient
Public Class Form1
    Dim Sqlcon As New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\JAYprojects\AppDesign\SQLConnectionTUT\SQLtutorial.mdf;Integrated Security=True")

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If txtID.Text = "" And txtName.Text = "" And txtAge.Text = "" Then
            MessageBox.Show("Fill in the spaces provided", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Sqlcon.Open()
            Dim com As SqlCommand = Sqlcon.CreateCommand
            com.CommandText = " INSERT INTO info VALUES (" & txtID.Text & ", '" & txtName.Text & "'," & txtAge.Text & ")"
            If com.ExecuteNonQuery() > 0 Then
                MessageBox.Show("Insertion was Successful")
            Else
                MessageBox.Show("Insertion was Unsuccessful")
            End If
            Sqlcon.Close()
        End If

        UpdateGrid2()
        autoSearch()
        UpdateCombo()
        Clear()

    End Sub

    Private Sub Clear()
        txtID.Clear()
        txtAge.Clear()
        txtName.Clear()

    End Sub


    Private Sub UpdateGrid2()

        Dim qry As String = "SELECT * FROM info "
        Sqlcon.Open()

        Dim ad As New SqlDataAdapter(qry, Sqlcon)
        Dim ds As New DataTable()
        ad.Fill(ds)
        DataGridView2.DataSource = ds

        Sqlcon.Close()

    End Sub

    Private Sub UpdateCombo()
        cmbID.Items.Clear()

        Sqlcon.Open()
        Dim com As SqlCommand = Sqlcon.CreateCommand()
        com.CommandText = " SELECT * FROM info "
        Dim SQLReader As SqlDataReader = com.ExecuteReader()

        While SQLReader.Read()
            cmbID.Items.Add(SQLReader.Item("ID"))

        End While
        Sqlcon.Close()

    End Sub

    Private Sub cmbID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbID.SelectedIndexChanged
        Sqlcon.Open()
        Dim com As SqlCommand = Sqlcon.CreateCommand()
        com.CommandText = " SELECT * FROM info WHERE ID=" & cmbID.SelectedItem
        Dim SQLReader As SqlDataReader = com.ExecuteReader()

        SQLReader.Read()
        txtName2.Text = SQLReader.Item("Name")
        txtAge2.Text = SQLReader.Item("Age")
        Sqlcon.Close()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If cmbID.Text = "" Then
            MessageBox.Show(" Select an ID and edit the information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Sqlcon.Open()
            Dim com As SqlCommand = Sqlcon.CreateCommand()
            com.CommandText = " UPDATE  info SET Name='" & txtName2.Text & "', Age=" & txtAge2.Text & " WHERE ID=" & cmbID.SelectedItem

            If com.ExecuteNonQuery() > 0 Then
                MessageBox.Show("Record was edited")
            Else
                MessageBox.Show("Error occured whiles editing!")
            End If
        End If
        Sqlcon.Close()
        autoSearch()
        UpdateGrid2()
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If cmbID.Text = "" Then
            MessageBox.Show(" Select an ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Sqlcon.Open()
            Dim com As SqlCommand = Sqlcon.CreateCommand()
            com.CommandText = " DELETE FROM info WHERE ID=" & cmbID.SelectedItem
            com.ExecuteNonQuery()
            Sqlcon.Close()
        End If

        UpdateGrid2()
        UpdateCombo()
        autoSearch()
        cmbID.Text = String.Empty
        txtName2.Clear()
        txtAge2.Clear()
    End Sub

    Private Sub autoSearch()
        Dim cmd As New SqlCommand("select name from info", Sqlcon)
        Sqlcon.Open()

        Dim dr As SqlDataReader = cmd.ExecuteReader()
        Dim myColl As New AutoCompleteStringCollection()
        While dr.Read()
            myColl.Add(dr.GetString(0))
        End While
        txtSearch.AutoCompleteCustomSource = myColl
        Sqlcon.Close()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load, txtSearch.TextChanged

        autoSearch()
        UpdateCombo()
        UpdateGrid2()
    End Sub

    Private Sub txtAutoFilter_TextChanged(sender As Object, e As EventArgs) Handles txtAutoFilter.TextChanged

        If cmbColums.Text = "Name" Then

            Dim qry As String = "SELECT * FROM info where Name like '" + txtAutoFilter.Text.ToString() + "%'"
            Sqlcon.Open()
            Dim ad As New SqlDataAdapter(qry, Sqlcon)
            Dim ds As New DataTable()
            ad.Fill(ds)
            DataGridView2.DataSource = ds
            Sqlcon.Close()

        ElseIf cmbColums.Text = "ID" Then

            Dim qry As String = "SELECT * FROM info where ID like '" + txtAutoFilter.Text.ToString() + "%'"
            Sqlcon.Open()
            Dim ad As New SqlDataAdapter(qry, Sqlcon)
            Dim ds As New DataTable()
            ad.Fill(ds)
            DataGridView2.DataSource = ds
            Sqlcon.Close()

        ElseIf cmbColums.Text = "Age" Then

            Dim qry As String = "SELECT * FROM info where Age like '" + txtAutoFilter.Text.ToString() + "%'"
            Sqlcon.Open()
            Dim ad As New SqlDataAdapter(qry, Sqlcon)
            Dim ds As New DataTable()
            ad.Fill(ds)
            DataGridView2.DataSource = ds
            Sqlcon.Close()
        Else
            Dim qry As String = "SELECT * FROM info where Name like '" + txtAutoFilter.Text.ToString() + "%'"
            Sqlcon.Open()
            Dim ad As New SqlDataAdapter(qry, Sqlcon)
            Dim ds As New DataTable()
            ad.Fill(ds)
            DataGridView2.DataSource = ds
            Sqlcon.Close()

        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim qry As String = "SELECT * FROM info where Name like '" + txtSearch.Text + "%'"
            Sqlcon.Open()
            Dim ad As New SqlDataAdapter(qry, Sqlcon)
            Dim ds As New DataTable()
            ad.Fill(ds)
            DataGridView2.DataSource = ds
            Sqlcon.Close()

        End If
    End Sub
End Class
