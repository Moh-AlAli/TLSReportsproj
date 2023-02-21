﻿
Partial Public Class srchcdcom
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Bsat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bsat.Click
        Try
            Dim c As New clsreport
            pangv.Visible = True
            gvcompsear.Visible = True

            Dim ds As DataSet = c.cdcompname(Txtcomp.Text)
            gvcompsear.DataSource = ds
            gvcompsear.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lkcomclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim t As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(t.Parent.Parent, GridViewRow)
            Dim ind As Integer = row.RowIndex

            Dim lkcomp As LinkButton = DirectCast(gvcompsear.Rows(ind).FindControl("lkcomp"), LinkButton)
            Session("ccom") = lkcomp.Text
            Response.Redirect("cdblbyagent.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Bcls_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Bcls.Click
        Response.Redirect("cdblbyagent.aspx")
    End Sub
End Class