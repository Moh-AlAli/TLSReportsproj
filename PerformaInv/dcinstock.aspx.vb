Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class dcinstock
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                Dim csion As String = Convert.ToString(Session("dcomp"))
                Txtcomp.Text = csion
                Dim bsion As String = Convert.ToString(Session("dBl"))
                Txtbl.Text = bsion
                Dim atsion As String = Convert.ToString(Session("dat9"))
                Txtat.Text = atsion
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
    Protected Sub Imgvrpt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgvrpt.Click
        Try
            Dim c As New clsreport
                       Dim cid As Integer
            Dim bl As String
            Dim at9 As String

            If Txtat.Text = Nothing Then
                at9 = "9999"
            Else
                at9 = LTrim(RTrim(Txtat.Text))
            End If

            If Txtbl.Text = Nothing Then
                bl = "9999"
            Else
                bl = LTrim(RTrim(Txtbl.Text))
            End If
            If Txtcomp.Text = Nothing Then

                cid = 9999
            Else
                Dim dc As DataSet = c.dcompname(Txtcomp.Text)
                cid = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
            End If
            Dim ds As New DataSet
            ds = c.dinstock()
            '  ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("dcinstock.aspx").Replace("dcinstock.aspx", "xml\xdcstock.xsd"))
            rdoc.Load(HttpContext.Current.Server.MapPath("dcinstock.aspx").Replace("dcinstock.aspx", "reports\R11vdcinstock.rpt"))
            rdoc.SetDataSource(ds)

            rdoc.SetParameterValue("comp", cid)
            rdoc.SetParameterValue("bl", bl)
            rdoc.SetParameterValue("at9", at9)
            If rbcr.Checked = True Or rbexc.Checked = True Then
                If rbexc.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "DC in-stock")
                ElseIf rbcr.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "DC in-stock")
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

    Protected Sub Imgsc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsc.Click
        Try
            Response.Redirect("searchdccomp.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Protected Sub Imgsb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsb.Click
        Try

            Response.Redirect("searchdcbl.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgsa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsa.Click
        Try
            Response.Redirect("searchdiat.aspx")
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
        Session("dcomp") = ""
        Session("dbl") = ""
        Session("dat9") = ""
    End Sub
End Class