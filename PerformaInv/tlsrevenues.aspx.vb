Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Public Class tlsrevenues
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                rbcr.Checked = True

            End If
                    Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Imgvrpt_Click(sender As Object, e As ImageClickEventArgs) Handles Imgvrpt.Click
        Try
            Dim ds As New DataSet
            Dim c As New clsreport
            Dim fd As String = ""
            Dim td As String = ""
            If Txtfd.Text = Nothing Then
                fd = "1900-01-01"
            Else
                fd = Txtfd.Text
            End If
            If Txttd.Text = Nothing Then
                td = "9999-12-31"
            Else
                td = Txttd.Text
            End If
            ds = c.revenues(fd, td)
            '    ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("tlsrevenues.aspx").Replace("tlsrevenues.aspx", "xml\xrev.xsd"))
            rdoc.Load(HttpContext.Current.Server.MapPath("tlsrevenues.aspx").Replace("tlsrevenues.aspx", "reports\revenues.rpt"))
            rdoc.SetDataSource(ds)

            If rbcr.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Revenues Report")
            ElseIf rbexc.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Revenues Report")
            End If

            ds.Dispose()
            rdoc.Dispose()
            rdoc.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
            If rdoc Is Nothing Then
                rdoc.Dispose()
                rdoc.Close()
            End If
        End Try
    End Sub
End Class