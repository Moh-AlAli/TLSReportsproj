Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class carsunstuffed
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack() Then
                Dim IpassAstringfrompage1 As String = Convert.ToString(Session("at"))
                Txtat.Text = IpassAstringfrompage1
                Txtfd.Text = Session("fd")
                txttd.Text = Session("td")
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
    Protected Sub Imgsa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsa.Click
        Try
            Session("fd") = Txtfd.Text
            Session("td") = txttd.Text
            Response.Redirect("srchunat9.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgclr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgclr.Click
        clear()
    End Sub
    Friend Sub clear()
        Txtfd.Text = ""
        Txtat.Text = ""
        txttd.Text = ""
        Lblmsg.Text = ""
        Session("at") = ""
    End Sub
    Protected Sub Impprv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport

            Dim fd As Date
            Dim td As Date
            Dim at9 As String
            Lblmsg.Visible = False
            Dim ds As DataSet = c.carsunstuf()
            ' ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("carsunstuffed.aspx").Replace("carsunstuffed.aspx", "xml\carsunstuffed.xml"))
            rdoc.Load(HttpContext.Current.Server.MapPath("carsunstuffed.aspx").Replace("carsunstuffed.aspx", "reports\R9carsunstuff.rpt"))

            rdoc.SetDataSource(ds)
            If Txtfd.Text <> Nothing Then
                fd = Date.Parse(Txtfd.Text)
            Else
                fd = "1900-01-01"
            End If
            If txttd.Text <> Nothing Then
                td = Txttd.Text
            Else
                td = "9999-12-31"
            End If
            If Txtat.Text <> Nothing Then
                at9 = RTrim(LTrim(Txtat.Text))
            Else
                at9 = "9999"
            End If
            If fd <= td Then


                rdoc.SetParameterValue("fromdate", fd)
                rdoc.SetParameterValue("todate", td)
                rdoc.SetParameterValue("at9", at9)

                If rbcr.Checked = True Or rbexc.Checked = True Then
                    If rbexc.Checked = True Then
                        rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "ExportedToreport")
                    ElseIf rbcr.Checked = True Then
                        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "ExportedToreport")
                    End If
                Else
                    Response.Write("Choose formate report")
                End If

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

End Class