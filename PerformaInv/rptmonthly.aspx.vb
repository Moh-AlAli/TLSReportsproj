Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class rptmonthly
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                rbcr.Checked = True
            End If
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

    Protected Sub Imgclr_Click(sender As Object, e As ImageClickEventArgs) Handles Imgclr.Click
        Txtfd.Text = ""
        Txttd.Text = ""
        Txtcom.Text = ""
        Txtbl.Text = ""
        rbcr.Checked = True
    End Sub

    Protected Sub Impprv_Click(sender As Object, e As ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport
            Dim ds As New DataSet
            Dim fd As String
            Dim td As String
            Dim fcom As Int64
            Dim tcom As Int64
            Dim bl As String
            Dim goods As String
            If Txtfd.Text = Nothing Then
                fd = "1900-01-01"
            Else
                fd = Date.Parse(Txtfd.Text).ToString("yyyy-MM-dd")
            End If
            If Txttd.Text = Nothing Then
                td = "9999-12-31"
            Else
                td = Date.Parse(Txttd.Text).ToString("yyyy-MM-dd")
            End If
            If Txtcom.Text = Nothing Then
                fcom = 0
                tcom = 999999999999
            Else
                Dim dc As DataSet = c.company(Txtcom.Text)
                fcom = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                tcom = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
            End If
            If Txtbl.Text = Nothing Then
                bl = ""
            Else
                bl = Txtbl.Text
            End If

            If Txtgoods.Text = Nothing Then
                goods = ""
            Else
                goods = Txtgoods.Text
            End If

            ds = c.cdmonthly(fd, td, fcom, tcom, bl, goods)
            ' ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("rptmonthly.aspx").Replace("rptmonthly.aspx", "xml\xcdmonth.xsd"))
            rdoc.Load(HttpContext.Current.Server.MapPath("rptmonthly.aspx").Replace("rptmonthly.aspx", "reports\monthlyform.rpt"))
            rdoc.SetDataSource(ds)
            rdoc.SetParameterValue("fromdate", Txtfd.Text)
            rdoc.SetParameterValue("todate", Txttd.Text)
            rdoc.SetParameterValue("comp", Txtcom.Text)
            rdoc.SetParameterValue("bl", Txtbl.Text)
            If rbcr.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Stock Report")

            ElseIf rbexc.Checked = True Then
                rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Stock Report")

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
                rdoc.Close()
                rdoc.Dispose()
                GC.Collect()
                GC.WaitForFullGCComplete()
                GC.Collect()
            End If
        End Try
    End Sub

    Protected Sub Imgscm_Click(sender As Object, e As ImageClickEventArgs) Handles Imgscm.Click
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
    Protected Sub Imgbcls_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Imgbx.Click
        pancomp.Visible = False
        Imgbx.Visible = False
    End Sub
    Protected Sub Lkbl_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try

            Dim lk As LinkButton = DirectCast(sender, LinkButton)
            Dim rowcus As GridViewRow = DirectCast(lk.Parent.Parent, GridViewRow)
            Dim rincus As Integer = rowcus.RowIndex

            Dim lkbl As LinkButton = CType(Gvbl.Rows(rincus).FindControl("lkbl"), LinkButton)
            Txtbl.Text = lkbl.Text
            panbl.Visible = False
            Imgbblx.Visible = False

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Imgbblx_Click(sender As Object, e As ImageClickEventArgs) Handles Imgbblx.Click
        panbl.Visible = False
        Imgbblx.Visible = False
    End Sub
    Protected Sub Imgsbl_Click(sender As Object, e As ImageClickEventArgs) Handles Imgsbl.Click
        panbl.Visible = True
        Imgbblx.Visible = True
        Dim c As New clsreport

        Dim dt As New DataSet
        dt = c.serbl(LTrim(RTrim(Txtbl.Text)))
        Gvbl.DataSource = dt
        Gvbl.DataBind()
    End Sub

    Protected Sub Imgsergds_Click(sender As Object, e As ImageClickEventArgs) Handles Imgsergds.Click
        Try
            Dim ds As New DataSet
            Dim c As New clsreport
            Dim goods As String = ""
            Dim bl As String = ""
            Dim comp As String = ""
            If Txtgoods.Text = Nothing Then
                goods = "%%"
            Else
                goods = "%" & Txtgoods.Text & "%"
            End If
            If Txtbl.Text = Nothing Then
                bl = "%%"
            Else
                bl = "%" & Txtbl.Text & "%"
            End If
            If Txtcom.Text = Nothing Then
                comp = "%%"
            Else
                comp = "%" & Txtcom.Text & "%"
            End If
            ds = c.goods(goods, comp, bl)
            Gvgoods.DataSource = ds
            Gvgoods.DataBind()
            pangoods.Visible = True
            Imgclsgoods.Visible = True
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Imgclsgoods_Click(sender As Object, e As ImageClickEventArgs) Handles Imgclsgoods.Click
        pangoods.Visible = False
        Imgclsgoods.Visible = False
    End Sub
    

    Protected Sub Lkgood_Click(sender As Object, e As EventArgs)
        Try

            Dim lk As LinkButton = DirectCast(sender, LinkButton)
            Dim rowcus As GridViewRow = DirectCast(lk.Parent.Parent, GridViewRow)
            Dim rincus As Integer = rowcus.RowIndex

            Dim lkgood As LinkButton = CType(Gvgoods.Rows(rincus).FindControl("lkgood"), LinkButton)
            Txtgoods.Text = lkgood.Text
            pangoods.Visible = False
            Imgclsgoods.Visible = False

        Catch ex As Exception

        End Try
    End Sub
End Class