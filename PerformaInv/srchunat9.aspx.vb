Partial Public Class srchunat9
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lkatclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkat As LinkButton = DirectCast(gvat.Rows(ind).FindControl("lkat"), LinkButton)
            Session("at") = lkat.Text
            Response.Redirect("carsunstuffed.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Bsat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bsat.Click
        Try
            Dim c As New clsreport
            pangv.Visible = True
            gvat.Visible = True
            Dim ds As DataSet = c.unat9(Txtsat.Text)
            gvat.DataSource = ds
            gvat.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("carsunstuffed.aspx")
    End Sub
End Class