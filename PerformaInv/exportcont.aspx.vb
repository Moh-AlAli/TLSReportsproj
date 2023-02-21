Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class exportcont
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ds As New DataSet
            Dim c As New clsreport
            ds = c.expcon(Request.QueryString("pid"))

            rdoc.Load(HttpContext.Current.Server.MapPath("exportcont.aspx").Replace("exportcont.aspx", "reports\R3exportcont.rpt"))
            rdoc.SetDataSource(ds)

            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedToreport")
            rdoc.Close()
            rdoc.Dispose()
            GC.Collect()
            GC.WaitForFullGCComplete()
            GC.Collect()
            ds.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)

            Response.Write(Request.QueryString("pid"))

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
