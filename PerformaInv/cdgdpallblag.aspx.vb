Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class cdgdpallblag
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Imgvrpt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgvrpt.Click
        Try

            Dim c As New clsreport

            Dim cid As Integer
            Dim bl As String




            If Txtbl.Text = Nothing Then
                bl = "9999"
            Else
                bl = LTrim(RTrim(Txtbl.Text))
            End If
            If Txtcomp.Text = Nothing Then

                cid = 9999
            Else
                Dim dc As DataSet = c.gpcomp(Txtcomp.Text)
                cid = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
            End If
            Dim ds As New DataSet
            ds = c.goodspallet()

            rdoc.Load(HttpContext.Current.Server.MapPath("cdgdpallblag.aspx").Replace("cdgdpallblag.aspx", "reports\R15gdpallblag.rpt"))
            rdoc.SetDataSource(ds)

            rdoc.SetParameterValue("comp", cid)
            rdoc.SetParameterValue("bl", bl)


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
        End Try
    End Sub
    Protected Sub Page_unLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        rdoc.Close()
        rdoc.Dispose()
        GC.Collect()
        GC.WaitForFullGCComplete()
        GC.Collect()
    End Sub
    Protected Sub Imgsc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsc.Click
        Try
            Response.Redirect("gcomp.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Protected Sub Imgsb_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgsb.Click
        Try

            Response.Redirect("gbl.aspx")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub Imgclr_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgclr.Click
        clear()
    End Sub
    Friend Sub clear()
        Txtbl.Text = ""
        Txtcomp.Text = ""
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack() Then
                Dim csion As String = Convert.ToString(Session("gcomp"))
                Txtcomp.Text = csion
                Dim bsion As String = Convert.ToString(Session("gBl"))
                Txtbl.Text = bsion

            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class