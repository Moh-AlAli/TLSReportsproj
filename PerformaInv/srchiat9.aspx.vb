Public Partial Class srchiat9
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Bsat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bsat.Click
        Try
            Dim c As New clsreport
            gvat.Visible = True
            pangv.Visible = True
            Dim ds As DataSet = c.at9(Txtsat.Text)
            gvat.DataSource = ds
            gvat.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Protected Sub lkatclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkat As LinkButton = DirectCast(gvat.Rows(ind).FindControl("lkat"), LinkButton)
            Session("at9") = lkat.Text
            Response.Redirect("carsinstock.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("carsinstock.aspx")
    End Sub
End Class