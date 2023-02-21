Public Partial Class srchblaspx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Bsbl_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bsbl.Click
        Try
            Dim c As New clsreport
            
            gvbl.Visible = True
            pangv.Visible = True

            Dim ds As DataSet = c.blname(Txtsbl.Text)
            gvbl.DataSource = ds
            gvbl.DataBind()
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub lkblclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkbl As LinkButton = DirectCast(gvbl.Rows(ind).FindControl("lkbl"), LinkButton)
            Session("bl") = lkbl.Text
            Response.Redirect("carsinstock.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("carsinstock.aspx")
    End Sub
End Class