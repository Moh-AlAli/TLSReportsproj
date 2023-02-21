Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class cddtcode
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim c As New clsreport
            Dim ds As New DataSet
            ds = c.containercode(Request.QueryString("did"))
            'ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("cddtcode.aspx").Replace("cddtcode.aspx", "xml\xcontcode.xsd"))

            rdoc.Load(HttpContext.Current.Server.MapPath("cddtcode.aspx").Replace("cddtcode.aspx", "reports\contcode.rpt"))

            rdoc.SetDataSource(ds)

            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Container Code")
            ds.Dispose()
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