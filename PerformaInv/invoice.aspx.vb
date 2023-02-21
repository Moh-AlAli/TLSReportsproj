Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Partial Public Class pinvrpt
    Inherits System.Web.UI.Page
    Private rdoc As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim c As New clsreport
            rdoc.Load(HttpContext.Current.Server.MapPath("invoice.aspx").Replace("invoice.aspx", "reports\R1performainvoice.rpt"))

            Dim dinv As New DataSet
            dinv = c.pinv(Request.QueryString("pid"))
            '    dinv.WriteXmlSchema(HttpContext.Current.Server.MapPath("invoice.aspx").Replace("invoice.aspx", "xml\xinv.xsd"))
            Dim coninspid As Integer = dinv.Tables(0).Rows(0).Item("continspid").ToString()
            Dim dinsp As New DataSet
            dinsp = c.invinsp(coninspid)

            Dim sec As Section
            Dim secs As Sections
            Dim rob As ReportObject
            Dim robs As ReportObjects
            Dim subrpobj As SubreportObject
            Dim subrp As ReportDocument
            secs = rdoc.ReportDefinition.Sections
            For Each sec In secs
                robs = sec.ReportObjects
                For Each rob In robs
                    If rob.Kind = ReportObjectKind.SubreportObject Then
                        subrpobj = CType(rob, SubreportObject)
                        subrp = subrpobj.OpenSubreport(subrpobj.SubreportName)
                        If subrp.Name = "insp" Then
                            subrp.SetDataSource(dinsp)
                        End If
                    End If

                Next
            Next

            rdoc.SetDataSource(dinv)

            rdoc.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, "Invoice")

            dinv.Dispose()
            dinsp.Dispose()
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