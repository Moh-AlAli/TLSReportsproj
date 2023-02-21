Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class unstuffedload
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
    

            Dim c As New clsreport
            Dim ds As New DataSet
            ds = c.unstuffedload(Request.QueryString("did"))
            rdoc.Load(HttpContext.Current.Server.MapPath("unstuffedload.aspx").Replace("unstuffedload.aspx", "reports\R4loadformbyCD.rpt"))
            rdoc.SetDataSource(ds)

            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedToreport")

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