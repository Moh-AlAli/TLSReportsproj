Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class cdunstuffed
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack() Then
                Dim IpassAstringfrompage1 As String = Convert.ToString(Session("cuat"))
                Txtat.Text = IpassAstringfrompage1
                Txtfd.Text = Session("cufd")
                txttd.Text = Session("cutd")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgsa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsa.Click
        Try
            Session("cufd") = Txtfd.Text
            Session("cutd") = txttd.Text
            Response.Redirect("srchcdat9.aspx")
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
    End Sub
    Protected Sub Impprv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport
            Dim fd As Date
            Dim td As Date
            Dim at9 As String
            Lblmsg.Visible = False
   
            rdoc.Load(HttpContext.Current.Server.MapPath("cdunstuffed.aspx").Replace("cdunstuffed.aspx", "reports\R14cdunstuff.rpt"))
            rdoc.SetDataSource(c.cdunstuff())
            If Txtfd.Text <> Nothing Then
                fd = Txtfd.Text
            Else
                fd = "1900-01-01"
            End If
            If txttd.Text <> Nothing Then
                td = txttd.Text
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
    Protected Sub Page_unLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        rdoc.Close()
        rdoc.Dispose()
        GC.Collect()
        GC.WaitForFullGCComplete()
        GC.Collect()
    End Sub
 
End Class