Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class invsumbyallservices
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then

                pancomp.Visible = False
                Imgbx.Visible = False
                rbcr.Checked = True
            End If
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

    Protected Sub Impprv_Click(sender As Object, e As ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport

            Dim fd As DateTime
            Dim td As DateTime

            Dim fcom As String



            If Txtfd.Text <> Nothing Then
                fd = Txtfd.Text
            Else
                fd = "1900-01-01"
            End If
            If Txttd.Text <> Nothing Then
                td = Txttd.Text
            Else
                td = "9999-12-31"
            End If

            If Txtcom.Text = Nothing Then
                fcom = "%%"
            Else
                Dim dc As DataSet = c.company(Txtcom.Text)
                fcom = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
            End If


            If fd <= td Then
                Dim ds As New DataSet
                ds = c.sumbyservlodofflod(fd, td, fcom)

                ' ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("invsumbyser.aspx").Replace("invsumbyser.aspx", "xml\xinvssumserv.xml"))

                rdoc.Load(HttpContext.Current.Server.MapPath("invsumbyser.aspx").Replace("invsumbyser.aspx", "reports\invoicesSummaryDetails.rpt"))
                rdoc.SetDataSource(ds)




                If rbcr.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Invoices Summary by Services")
                ElseIf rbexc.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Invoices Summary by Services")
                End If
                ds.Dispose()
            Else
                Dim sb As New System.Text.StringBuilder
                Dim msg As String = "Error: From date greater than to date"
                sb.Append("<script type='text/javascript'>")
                sb.Append("window.onload=function(){")
                sb.Append("alert('")
                sb.Append(msg)
                sb.Append("')};")
                sb.Append("</script>")
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

            End If

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
    Protected Sub Imgbcls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgbx.Click
        pancomp.Visible = False
        Imgbx.Visible = False
    End Sub

    Protected Sub Imgscm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgscm.Click
        pancomp.Visible = True
        Imgbx.Visible = True
        Dim c As New clsreport

        Dim dt As New DataSet
        dt = c.company(LTrim(RTrim(Txtcom.Text)))
        Gvcomp.DataSource = dt
        Gvcomp.DataBind()
    End Sub

    Protected Sub Lkname_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim lk As LinkButton = DirectCast(sender, LinkButton)
            Dim rowcus As GridViewRow = DirectCast(lk.Parent.Parent, GridViewRow)
            Dim rincus As Integer = rowcus.RowIndex

            Dim lkname As LinkButton = CType(Gvcomp.Rows(rincus).FindControl("lkname"), LinkButton)
            Txtcom.Text = lkname.Text
            pancomp.Visible = False
            Imgbx.Visible = False

        Catch ex As Exception

        End Try
    End Sub

End Class