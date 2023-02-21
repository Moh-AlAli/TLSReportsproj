Public Partial Class searchduat
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lkatclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkat As LinkButton = DirectCast(gvat.Rows(ind).FindControl("lkat"), LinkButton)
            Session("dat") = lkat.Text
            Response.Redirect("dcunstuffed.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Bsat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bsat.Click
        Try
            Dim c As New clsreport
            pangv.Visible = True
            gvat.Visible = True
            Dim ds As DataSet = c.dunat9(Txtsat.Text)
            gvat.DataSource = ds
            gvat.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("dcunstuffed.aspx")
    End Sub
End Class