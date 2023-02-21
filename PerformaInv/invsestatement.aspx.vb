Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Class invsestatement
    Inherits System.Web.UI.Page
    private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
        If Not Page.IsPostBack() Then
            pancomp.Visible = False
            Imgbx.Visible = False

            Dim c As New clsreport
            Dim dtype As New DataTable
            dtype = c.invtype()
            DDLtype.Items.Add(New ListItem("--ALL--", 0))
                DDLtype.Items.Add(New ListItem("All invoices excluding Inspection", 1))
            For i = 0 To dtype.Rows.Count - 1 Step 1
                Dim itemtxt As String = dtype.Rows(i).Item("capt_us").ToString()
                Dim itemval As String = dtype.Rows(i).Item("capt_code").ToString()
                DDLtype.Items.Add(New ListItem(itemtxt, itemval))
            Next
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

    Protected Sub Impprv_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Impprv.Click
        Try
            Dim c As New clsreport
           
            Dim fd As Date
            Dim td As Date
            
            Dim fcom As Integer
            Dim tcom As Integer 


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
        
            If Txtcom.Text = Nothing Then
                fcom=0
                tcom = 99999999
            Else
                Dim dc As DataSet = c.company(Txtcom.Text)
                fcom = dc.Tables(0).Rows(0).Item("comp_companyid").ToString()
                tcom=fcom
            End If
            Dim condtype As String
            If DDLtype.SelectedValue = "0" Then
                condtype = ""
            ElseIf DDLtype.SelectedValue = "1" Then
                condtype = " and lcpi_type not in ('InspectionYard')"
            Else
                condtype = " and lcpi_type in ('" & DDLtype.SelectedValue & "' )"
            End If
            If fd <= td Then
                Dim ds As New DataSet
                ds = c.invstate(fd, td, fcom, tcom, condtype)

                ' ds.WriteXmlSchema(HttpContext.Current.Server.MapPath("invsestatement.aspx").Replace("invsestatement.aspx", "xml\xinvstate.xml"))

                rdoc.Load(HttpContext.Current.Server.MapPath("invsestatement.aspx").Replace("invsestatement.aspx", "reports\R18invstatement.rpt"))
                rdoc.SetDataSource(ds)
                rdoc.SetParameterValue("fromdate", fd)
                rdoc.SetParameterValue("todate", td)
                'rdoc.SetParameterValue("comp", tcom)



                If rbcr.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Invoices Statement")
                ElseIf rbexc.Checked = True Then
                    rdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, True, "Invoices Statement")
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
            Txtcom .Text = lkname.Text
            pancomp.Visible = False
            Imgbx.Visible = False

        Catch ex As Exception
           
        End Try
    End Sub
    
End Class