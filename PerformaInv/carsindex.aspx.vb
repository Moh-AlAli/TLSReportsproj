Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class carsindex
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
    

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Page_unLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        rdoc.Close()
        rdoc.Dispose()
        GC.Collect()
        GC.WaitForFullGCComplete()
        GC.Collect()
    End Sub
    Protected Sub Imgvrpt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgvrpt.Click
        Try
            Dim c As New clsreport

            rdoc.Load(HttpContext.Current.Server.MapPath("carsindex.aspx").Replace("carsindex.aspx", "reports\R7carsindex.rpt"))
            Dim ds As New DataSet
            ds = c.Carsindex(Request.QueryString("rid"))
            rdoc.SetDataSource(ds)
            If rbcr.Checked = True Or rbexc.Checked = True Then

                If rbexc.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "ExportedToreport")
                ElseIf rbcr.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedToreport")
                End If

            Else
                Response.Write("Choose formate report")
            End If
            ds.Dispose()
            rdoc.Close()
            rdoc.Dispose()
            GC.Collect()
            GC.WaitForFullGCComplete()
            GC.Collect()
        Catch ex As Exception
            Response.Write(ex.Message)
            If rdoc Is Nothing Then
                rdoc.Close()
                rdoc.Dispose()
                GC.Collect()
                GC.WaitForFullGCComplete()
                GC.Collect()
            End If
        End Try
    End Sub
End Class