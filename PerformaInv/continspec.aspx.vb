Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class continspec
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rbcr.Checked = True
    End Sub
    Protected Sub Page_unLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        rdoc.Close()
        rdoc.Dispose()
        GC.Collect()
        GC.WaitForFullGCComplete()
        GC.Collect()
    End Sub
    Protected Sub Imgclr_Click(sender As Object, e As ImageClickEventArgs) Handles Imgclr.Click
        Txtfd.Text = Nothing
        txttd.Text = Nothing
    End Sub

    Protected Sub Impprv_Click(sender As Object, e As ImageClickEventArgs) Handles Impprv.Click
        Try
            Lblmsg.Visible = False
            Lblmsg.Text = ""
            Dim c As New clsreport
            Dim ds As New DataSet
            If Txtfd.Text = Nothing Then
                Txtfd.Text = "1900-01-01"
            End If
            If txttd.Text = Nothing Then
                txttd.Text = "9999-12-31"
            End If
            ds = c.continsp(Txtfd.Text, txttd.Text)
            '  ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("continspec.aspx").Replace("continspec.aspx", "xml\xdinsp.xsd"))

            rdoc.Load(HttpContext.Current.Server.MapPath("continspec.aspx").Replace("continspec.aspx", "reports\Dailyinspection.rpt"))
            rdoc.SetDataSource(ds)

            rdoc.SetParameterValue("fromdate", Txtfd.Text)
            rdoc.SetParameterValue("todate", txttd.Text)
            If rbexc.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Daily Inspection")
            ElseIf rbcr.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Daily Inspection")
            End If

            ds.Dispose()
            rdoc.Dispose()
            rdoc.Close()
            GC.Collect()
            GC.WaitForFullGCComplete()
            GC.Collect()
        Catch ex As Exception
            Lblmsg.Text = ex.Message
            Lblmsg.Visible = True
            If rdoc Is Nothing Then
                rdoc.Dispose()
                rdoc.Close()
                GC.Collect()
                GC.WaitForFullGCComplete()
                GC.Collect()
            End If
        End Try
    End Sub
End Class