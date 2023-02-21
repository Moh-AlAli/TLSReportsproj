Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Data.SqlClient
Partial Public Class dcunstuffed
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                Dim pasp As String = Convert.ToString(Session("dat"))
                Txtat.Text = pasp
                Txtfd.Text = Session("dfd")
                txttd.Text = Session("dtd")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgsa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsa.Click
        Try
            Session("dfd") = Txtfd.Text
            Session("dtd") = txttd.Text
            Response.Redirect("searchduat.aspx")
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
        Session("dfd") = ""
        Session("dtd") = ""
        Session("dat") = ""
    End Sub
    Protected Sub Impprv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport

            Dim fd As Date
            Dim td As Date
            Dim at9 As String
            Lblmsg.Visible = False
            Dim comp As String = "%%"
            If Txtcomp.Text <> Nothing Then
                comp = Txtcomp.Text
            End If
            Dim dun As New DataSet
            dun = c.dcunstuf(comp)
            rdoc.Load(HttpContext.Current.Server.MapPath("dcunstuffed.aspx").Replace("dcunstuffed.aspx", "reports\R10DCunstuff.rpt"))
            rdoc.SetDataSource(dun)
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
                        rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "DCUnstuffed")
                    ElseIf rbcr.Checked = True Then
                        rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "DCunstuffed")
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
            dun.Dispose()
            rdoc.Close()
            rdoc.Dispose()
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

    Protected Sub Lkname_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lk As LinkButton = DirectCast(sender, LinkButton)
            Dim rowcus As GridViewRow = DirectCast(lk.Parent.Parent, GridViewRow)
            Dim rincus As Integer = rowcus.RowIndex

            Dim lkname As LinkButton = CType(Gvcomp.Rows(rincus).FindControl("lkname"), LinkButton)
            txtcomp.Text = lkname.Text
            pancomp.Visible = False
            Imgbx.Visible = False

        Catch ex As Exception
          
        End Try
    End Sub

    

    Protected Sub Imgsc_Click(sender As Object, e As ImageClickEventArgs) Handles Imgsc.Click
        Try

            pancomp.Visible = True
            Imgbx.Visible = True
            Dim c As New clsreport
            Dim dcomp As New DataSet
            dcomp = c.company(Txtcomp.Text)
            Gvcomp.DataSource = dcomp
            Gvcomp.DataBind()
            dcomp.Dispose()
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
    Protected Sub Imgbx_Click(sender As Object, e As ImageClickEventArgs) Handles Imgbx.Click

        Imgbx.Visible = False
        pancomp.Visible = False

    End Sub
End Class