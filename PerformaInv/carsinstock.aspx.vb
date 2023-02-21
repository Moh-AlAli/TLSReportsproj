Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class carsinstock
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Imgvrpt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgvrpt.Click
        Try
            Dim c As New clsreport
            Dim cid As Integer
            Dim pbl As String = ""
            Dim bl As String
            Dim pat9 As String = ""
            Dim at9 As String
            Dim fcomp As Int32
            Dim tcomp As Int32
            Dim type As String = ""
            If Txtat.Text = Nothing Then
                at9 = "%"
                pat9 = "9999"
            Else
                at9 = LTrim(RTrim(Txtat.Text))
                pat9 = LTrim(RTrim(Txtat.Text))
            End If

            If Txtbl.Text = Nothing Then
                bl = "%"
                pbl = "9999"
            Else
                bl = LTrim(RTrim(Txtbl.Text))
                pbl = LTrim(RTrim(Txtbl.Text))
            End If
            If Txtcomp.Text = Nothing Then
                fcomp = 0
                tcomp = 999999999
                cid = 9999
            Else

                Dim dc As DataSet = c.compname(Txtcomp.Text)
                cid = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                fcomp = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                tcomp = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()

            End If
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
            Dim ds As New DataSet
            If DDLDatetype.SelectedValue <> "0" Then
                If DDLDatetype.SelectedValue = "1" Then
                    type = "1"
                    ds = c.Carsinstock(fd, td, fcomp, tcomp, at9, bl)
                ElseIf DDLDatetype.SelectedValue = "2" Then
                    type = "2"
                    ds = c.Carsshipped(fd, td, fcomp, tcomp, at9, bl)
                End If
                '   ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("carsinstock.aspx").Replace("carsinstock.aspx", "xml\xcarsinstock.xsd"))

                If Txttd.Text = Nothing Then
                    td = "9999-12-31"
                Else
                    td = Date.Parse(Txttd.Text).ToString("yyyy-MM-dd")
                End If
                If fd <= td Then
                    rdoc.Load(HttpContext.Current.Server.MapPath("carsinstock.aspx").Replace("carsinstock.aspx", "reports\R8carsinstock.rpt"))
                    rdoc.SetDataSource(ds)

                    rdoc.SetParameterValue("comp", cid)
                    rdoc.SetParameterValue("bl", pbl)
                    rdoc.SetParameterValue("at9", pat9)
                    rdoc.SetParameterValue("fdate", fd)
                    rdoc.SetParameterValue("tdate", td)
                    rdoc.SetParameterValue("type", type)


                    If rbcr.Checked = True Or rbexc.Checked = True Then

                        If rbexc.Checked = True Then
                            rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Cars in-stock")
                        ElseIf rbcr.Checked = True Then
                            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Cars in-stock")
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
            Else
                Dim sb As New System.Text.StringBuilder
                Dim msg As String = "Error: Choose Date Type"
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
    Protected Sub Imgsc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsc.Click
        Try
            Session("fd") = Txtfd.Text
            Session("td") = Txttd.Text
            Session("Dtyp") = DDLDatetype.SelectedValue
            Response.Redirect("srchcomp.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgsb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsb.Click
        Try
            Session("fd") = Txtfd.Text
            Session("td") = Txttd.Text
            Session("Dtyp") = DDLDatetype.SelectedValue
            Response.Redirect("srchbl.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
   
    Protected Sub Imgsa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsa.Click
        Try
            Session("fd") = Txtfd.Text
            Session("td") = Txttd.Text
            Session("Dtyp") = DDLDatetype.SelectedValue
            Response.Redirect("srchiat9.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Protected Sub Imgclr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgclr.Click
        clear()
    End Sub
    Friend Sub clear()
        Txtbl.Text = ""
        Txtat.Text = ""
        Txtcomp.Text = ""
        Session("comp") = ""
        Session("bl") = ""
        Session("at9") = ""
        DDLDatetype.SelectedValue = "0"


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                Dim csion As String = Convert.ToString(Session("comp"))
                Txtcomp.Text = csion
                Dim bsion As String = Convert.ToString(Session("Bl"))
                Txtbl.Text = bsion
                Dim atsion As String = Convert.ToString(Session("at9"))
                Txtfd.Text = Session("fd")
                Txttd.Text = Session("td")
                DDLDatetype.SelectedValue = Session("Dtyp")
                Txtat.Text = atsion
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
End Class
