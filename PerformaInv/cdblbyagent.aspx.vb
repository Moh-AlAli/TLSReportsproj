Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class cdblbyagent
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack() Then
                Dim sesscom As String = Convert.ToString(Session("ccom"))
                Txtcom.Text = sesscom
                Dim sesscud As String = Convert.ToString(Session("cud"))
                Txtcud.Text = sesscud
                Txtfd.Text = Session("cfd")
                txttd.Text = Session("ctd")
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgclr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgclr.Click
        clear()
    End Sub
    Friend Sub clear()
        Txtfd.Text = ""
        Txtcom.Text = ""
        Txtcud.Text = ""
        txttd.Text = ""
        Lblmsg.Text = ""
    End Sub
    Protected Sub Impprv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport

            Dim fd As Date
            Dim td As Date
            Dim cud As String
            Dim com As Integer
            Lblmsg.Visible = False
            If rbcr.Checked = True Or rbexc.Checked = True Then

                If rbcr.Checked = True Then
                    Dim ds As New DataSet
                    ds = c.cdblagent()
                    ' ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("cdblbyagent.aspx").Replace("cdblbyagent.aspx", "xml\xcdbyag.xsd"))
                    rdoc.Load(HttpContext.Current.Server.MapPath("cdblbyagent.aspx").Replace("cdblbyagent.aspx", "reports\R13cdblbyagentpdf.rpt"))
                    rdoc.SetDataSource(ds)
                    If Txtfd.Text <> Nothing Then
                        fd = Txtfd.Text
                    Else
                        fd = "1900-01-01"
                    End If
                    If txttd.Text <> Nothing Then
                        td = Txttd.Text
                    Else
                        td = "9999-12-31"
                    End If
                    If Txtcud.Text <> Nothing Then
                        cud = RTrim(LTrim(Txtcud.Text))
                    Else
                        cud = "9999"
                    End If
                    If Txtcom.Text = Nothing Then

                        com = 9999
                    Else
                        Dim dc As DataSet = c.cdcompname(Txtcom.Text)
                        com = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                    End If
                    If fd <= td Then
                        rdoc.SetParameterValue("fromdate", fd)
                        rdoc.SetParameterValue("todate", td)
                        rdoc.SetParameterValue("agent", com)
                        rdoc.SetParameterValue("custdec", cud)
                        ds.Dispose()
                        
                        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Cross Docking B\L By Agent")

                   
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
            ElseIf rbexc.Checked = True Then
                    Dim ds As New DataSet
                    ds = c.cdblagent()
                    'ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("cdblbyagent.aspx").Replace("cdblbyagent.aspx", "xml\xcdbyag.xsd"))
                    rdoc.Load(HttpContext.Current.Server.MapPath("cdblbyagent.aspx").Replace("cdblbyagent.aspx", "reports\R13cdblbyagent.rpt"))
                    rdoc.SetDataSource(ds)
                If Txtfd.Text <> Nothing Then
                        fd = Txtfd.Text
                Else
                        fd = "1900-01-01"
                End If
                If txttd.Text <> Nothing Then
                        td = Txttd.Text
                Else
                        td = "9999-12-31"
                End If
                If Txtcud.Text <> Nothing Then
                    cud = RTrim(LTrim(Txtcud.Text))
                Else
                    cud = "9999"
                End If
                If Txtcom.Text = Nothing Then

                    com = 9999
                Else
                    Dim dc As DataSet = c.cdcompname(Txtcom.Text)
                    com = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                End If
                If fd <= td Then
                        rdoc.SetParameterValue("fromdate", fd)
                        rdoc.SetParameterValue("todate", td)
                        rdoc.SetParameterValue("agent", com)
                        rdoc.SetParameterValue("custdec", cud)



                        rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Cross Docking B\L By Agent")

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

            End If
            ElseIf rbcr.Checked = False And rbexc.Checked = False Then
                Response.Write("Choose formate report")
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
    Protected Sub Imgscm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgscm.Click
        Try
            Session("cfd") = Txtfd.Text
            Session("ctd") = txttd.Text
            Response.Redirect("srchcdcom.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Imgbsd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgbsd.Click
        Try
            Session("cfd") = Txtfd.Text
            Session("ctd") = txttd.Text
            Response.Redirect("srchdecl.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class