Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class unstuffedreturn
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim c As New clsreport
            Dim ds As New DataSet
            ds = c.unstuffedreturn(Request.QueryString("did"))
            rdoc.Load(HttpContext.Current.Server.MapPath("unstuffedreturn.aspx").Replace("unstuffedreturn.aspx", "reports\R5cdrtemty.rpt"))

            rdoc.SetDataSource(ds)

            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedToreport")
            ds.dispose()
            rdoc.Dispose()
            rdoc.Close()
            GC.Collect()
            GC.WaitForFullGCComplete()
            GC.Collect()
        Catch ex As Exception
            Response.Write(ex.Message)
            If rdoc Is Nothing Then
                rdoc.Dispose()
                rdoc.Close()
                GC.Collect()
                GC.WaitForFullGCComplete()
                GC.Collect()
            End If
        End Try
    End Sub
    Protected Sub Page_unLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        rdoc.Close()
        rdoc.Dispose()
        GC.Collect()
        GC.WaitForFullGCComplete()
        GC.Collect()
    End Sub

End Class