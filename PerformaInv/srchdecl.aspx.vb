Public Partial Class srchdecl
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Bscus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bscus.Click
        Try
            Dim c As New clsreport
            pangv.Visible = True
            gvcusd.Visible = True

            Dim ds As DataSet = c.cdcud(Txtcsd.Text)
            gvcusd.DataSource = ds
            gvcusd.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub lkcudclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkcud As LinkButton = DirectCast(gvcusd.Rows(ind).FindControl("Lkcud"), LinkButton)
            Session("cud") = lkcud.Text
            Response.Redirect("cdblbyagent.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("cdblbyagent.aspx")
    End Sub
End Class