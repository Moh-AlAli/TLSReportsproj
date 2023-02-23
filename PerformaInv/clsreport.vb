Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Public Class clsreport
    Friend Function createdes(ByVal key As String) As TripleDES
        Dim md5 As MD5 = New MD5CryptoServiceProvider()
        Dim des As TripleDES = New TripleDESCryptoServiceProvider()
        des.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key))
        des.IV = New Byte(des.BlockSize \ 8 - 1) {}
        Return des
    End Function
    Friend Function Decryption(ByVal cyphertext As String, ByVal key As String) As String
        Dim b As Byte() = Convert.FromBase64String(cyphertext)
        Dim des As TripleDES = createdes(key)
        Dim ct As ICryptoTransform = des.CreateDecryptor()
        Dim output As Byte() = ct.TransformFinalBlock(b, 0, b.Length)
        Return Encoding.Unicode.GetString(output)
    End Function
    Friend Function Readconnectionstring() As String

        Dim secretkey As String = "Fhghqwjehqwlegtoit123mnk12%&4#"
        Dim path As String = HttpContext.Current.Server.MapPath("invoice.aspx").Replace("invoice.aspx", "srctxt\srccon.txt")
        Dim sr As New StreamReader(path)

        Dim Server As String = sr.ReadLine()
        Dim DataBase As String = sr.ReadLine()
        Dim UserId As String = sr.ReadLine()
        Dim Password As String = sr.ReadLine()


        Dim ds As String = Decryption(Server, secretkey)
        Dim db As String = Decryption(DataBase, secretkey)
        Dim uid As String = Decryption(UserId, secretkey)
        Dim pass As String = Decryption(Password, secretkey)

        Dim cons As String = "Data Source =" & ds & "; DataBase =" & db & "; User Id =" & uid & "; Password =" & pass & ";"


        Return cons
    End Function

    Friend Function company(ByVal name As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select comp_companyid,comp_name from company where comp_deleted is null and comp_name like N'%" & name & "%' "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function contract(ByVal name As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select cont_contractid,cont_name from contract where cont_deleted is null and cont_name like N'%" & name & "%' "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function empcon(ByVal did As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select i.vein_driver,i.Vein_Name ,o.book_Name,cz.capt_us as size   " &
                            "  from VehiclesIn i inner join CDShipped h on h.cdsh_CDShippedID =i.vein_CDShippedid  " &
                            "  inner join Booking o on o.book_BookingID=h.cdsh_Bookingid " &
                            "  left outer join Custom_Captions cz on h.cdsh_size =cz.Capt_Code  " &
                            "  where (h.cdsh_size is null or cz.Capt_Family='lcch_volum') and h.cdsh_Deleted is null  and i.Vein_Deleted is null and h.cdsh_CDShippedID = " & did
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function pinv(ByVal pid As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " SELECT comp_companyid ,comp_name,Comp_IdCust,coalesce(lcpi_ContInspid,0) as ContInspid " & _
                            " ,Lcpi_Name,lcpi_date,lcpi_totalinv,lcpi_customdeclare,Lcpi_LCLPerformaInvID " & _
                            " ,lcpi_Type,lcpi_cnee,lcpi_BL" & _
                            " ,Addr_Address1,Addr_Address2,Addr_Address3,Addr_Address4,Addr_Address5" & _
                            " ,CDin_Name ,cdin_customdecl, convert(nvarchar, Capt_US) as Capt_US ," & _
                            " Lcid_Name, lcid_code, lcid_days, lcid_cbm" & _
                            " ,SUM(lcid_Amount) as amount,COUNT(Lcid_LCLPerformaInvDtID) as cntdet" & _
                            " FROM LCLPerformaInv inner join LCLPerformaInvDt on Lcpi_LCLPerformaInvID = lcid_LCLPerformaInvid  inner join Company on lcpi_companyid = Comp_CompanyId" & _
                            " left outer join cdindex on lcpi_cdindexid =CDin_CDIndexID left outer join Address on Addr_AddressId=Comp_PrimaryAddressId " & _
                            " inner join Custom_Captions on Capt_Code=lcid_code " & _
                            " WHERE Capt_Family='lcid_code' and" & _
                            " (Lcpi_Deleted IS NULL) AND (Lcid_Deleted IS NULL) AND  " & _
                            " lcpi_lclperformainvid = '" & pid & "' " & _
                            " group by comp_companyid ,comp_name,Comp_IdCust,lcpi_ContInspid " & _
                            " ,Lcpi_Name,lcpi_date,lcpi_totalinv,lcpi_customdeclare,Lcpi_LCLPerformaInvID,lcpi_lclcontainerdetailid,Lcpi_Status " & _
                            " ,lcpi_accpacno,lcpi_Type,lcpi_cnee,lcpi_BL" & _
                            " ,Addr_Address1,Addr_Address2,Addr_Address3,Addr_Address4,Addr_Address5 " & _
                            " ,CDin_Name ,cdin_customdecl, convert(nvarchar, Capt_US) " & _
                            " ,Lcid_Name, lcid_code, lcid_days, lcid_cbm "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function expcon(ByVal did As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " SELECT o.veou_plateno,o.veou_drivername,s.cdsh_Name,s.cdsh_sealNo,cs.Capt_US as size " & _
                            " FROM   CDShipped s left outer join Vehicleout o ON s.cdsh_CDShippedID=o.veou_CDShippedId LEFT OUTER JOIN Custom_Captions cs ON s.cdsh_size=cs.Capt_Code " & _
                            " WHERE  (s.cdsh_size is null  OR cs.Capt_Family='lcch_volum') and s.cdsh_type='Container' and o.Veou_Deleted is null  and s.cdsh_CDShippedID= " & did

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function unstuffedload(ByVal did As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " SELECT n.vein_driver,n.Vein_Name,d.Cddt_CDIndexDetailID, d.cddt_seal, d.Cddt_Name, cs.Capt_US as size " & _
                            " FROM   CDIndexDetail d LEFT OUTER JOIN  VehiclesIn n ON d.Cddt_CDIndexDetailID=n.vein_cdindexdetailid LEFT OUTER JOIN  Custom_Captions cs ON d.cddt_size=cs.Capt_Code" & _
                            " WHERE  n.Vein_Deleted IS  NULL  AND (d.cddt_size  IS  NULL  OR cs.Capt_Family=N'lcch_volum')  AND d.Cddt_Deleted IS  NULL  " & _
                            " AND    d.Cddt_CDIndexDetailID= '" & did & "'" & _
                            " ORDER BY d.Cddt_CDIndexDetailID "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function unstuffedreturn(ByVal did As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT  o.veou_plateno, o.veou_drivername, d.Cddt_CDIndexDetailID, d.Cddt_Name, cs.Capt_US as size " & _
                             "  FROM   CDIndexDetail d LEFT OUTER JOIN Vehicleout o ON d.Cddt_CDIndexDetailID=o.veou_cdindexdetailid LEFT OUTER JOIN  Custom_Captions cs ON d.cddt_size=cs.Capt_Code " & _
                             " WHERE (cs.Capt_Family IS  NULL  OR cs.Capt_Family=N'lcch_volum')  AND o.Veou_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL " & _
                             " and d.Cddt_CDIndexDetailID= '" & did & "'" & _
                             " ORDER BY d.Cddt_CDIndexDetailID"


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function Cdindex(ByVal cid As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT i.CDin_CDIndexID,c.comp_name,n.cont_name, i.cdin_container, i.cdin_customdecl, i.cdin_containerno_20, i.cdin_shipname, n.cont_Name, i.cdin_decdate, i.cdin_containerno_40, d.cddt_serial, d.Cddt_Name, d.cddt_seal, d.cddt_wt, d.cddt_cbm, i.cdin_Content, d.cddt_nopackages, cc.Capt_US as cargocond, cz.Capt_US as size " & _
                            "  FROM   CDIndexDetail d INNER JOIN  CDIndex i ON d.cddt_cdindexid=i.CDin_CDIndexID LEFT OUTER JOIN  Custom_Captions cz ON d.cddt_size=cz.Capt_Code LEFT OUTER JOIN  Custom_Captions cc ON d.cddt_cargocondition=cc.Capt_Code INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId LEFT OUTER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID " & _
                            "  WHERE  i.CDin_Deleted IS  NULL  AND (d.cddt_size IS  NULL  OR cz.Capt_Family=N'lcch_volum') AND (d.cddt_cargocondition  IS  NULL  OR cc.Capt_Family=N'lcch_cargocondition')  AND d.Cddt_Deleted IS  NULL  " & _
                            "  AND i.CDin_CDIndexID='" & cid & "'" & _
                            "  ORDER BY i.CDin_CDIndexID, d.cddt_serial"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function Carsindex(ByVal rid As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT c.Comp_Name, r.Caid_CarsIndexID, r.Caid_Name, r.caid_Arrivaldate, r.caid_carsno, n.cacn_Name, r.caid_vesselno, r.caid_line, d.cidt_pol, d.cidt_bl, d.cidt_sno, d.cidt_cnee, d.cidt_qty, d.cidt_WT, d.cidt_CBM, d.cidt_trans, d.cidt_finaldestination,cp.Capt_US as property, cs.Capt_US as stage " & _
                            "  FROM   CarsIndex r INNER JOIN  CarsIndexDetail d ON r.Caid_CarsIndexID=d.cidt_carsindexid INNER JOIN  Company c ON r.caid_companyid=c.Comp_CompanyId LEFT OUTER JOIN  Custom_Captions cs ON r.caid_stage=cs.Capt_Code LEFT OUTER JOIN  CarsContract n ON r.caid_CarsContractid=n.cacn_CarsContractID LEFT OUTER JOIN Custom_Captions cp ON d.cidt_Carsproperty=cp.Capt_Code" & _
                            "  WHERE  r.Caid_Deleted IS  NULL and c.comp_deleted is null  AND (d.cidt_Carsproperty  IS  NULL  OR cp.Capt_Family=N'cidt_Carsproperty') AND d.Cidt_Deleted IS  NULL  AND (r.caid_stage IS  NULL  OR cs.Capt_Family=N'caid_stage') " & _
                            "  AND r.Caid_CarsIndexID='" & rid & "'" & _
                            "  ORDER BY r.Caid_CarsIndexID"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function Carsinstock(ByVal fd As String, ByVal td As String, ByVal fcomp As Int32, ByVal tcomp As Int32, ByVal at9 As String, ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select caid_carsindexid,Cidt_CarsIndexDetailID,comp_companyid,caid_name as vessel,comp_name,cacn_Name as CARSContract,cidt_bl as bl,caid_AT9 as AT9,cidt_sno as customno,coalesce(cidt_WT,0) as wt,coalesce(cidt_cbm,0) as cbm,convert(datetime,left(convert(nvarchar,caid_unstuffingdate,120),10),120) as unstuffingdate,convert(datetime,left(convert(nvarchar,cidt_shippeddate,120),10),120) as shippeddate,coalesce(cidt_carsnoremaining,0) as remaincars,coalesce(cidt_totalshippedcars,0) as shippedcars,coalesce(cidt_actualqty,0) as Noofcars from CarsIndex ,CarsIndexDetail ,Company ,CarsContract " & _
                            " where Caid_CarsIndexID = cidt_carsindexid " & _
                            " and Caid_Deleted is null and Cidt_Deleted is null " & _
                            " and caid_companyid =comp_companyid  and caid_CarsContractid =cacn_CarsContractID and coalesce(convert(nvarchar,left(convert(nvarchar,caid_unstuffingdate,120),10),120),'1900-01-01') between '" & fd & "' and '" & td & "'" & _
                            " and Comp_CompanyId between " & fcomp & " and  " & tcomp & " and ltrim(rtrim(coalesce(caid_AT9,''))) like ltrim(rtrim('" & at9 & "')) and ltrim(rtrim(coalesce(cidt_bl,''))) like ltrim(rtrim('" & bl & "'))" & _
                            " order by Comp_CompanyId,cacn_carscontractid,Caid_CarsIndexID ,cidt_carsindexdetailid "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = Sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function Carsshipped(ByVal fd As String, ByVal td As String, ByVal fcomp As Int32, ByVal tcomp As Int32, ByVal at9 As String, ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select caid_carsindexid,Cidt_CarsIndexDetailID,comp_companyid,caid_name as vessel,comp_name,cacn_Name as CARSContract,cidt_bl as bl,caid_AT9 as AT9,cidt_sno as customno,coalesce(cidt_WT,0) as wt,coalesce(cidt_cbm,0) as cbm,convert(datetime,left(convert(nvarchar,caid_unstuffingdate,120),10),120) as unstuffingdate,convert(datetime,left(convert(nvarchar,crsh_shippeddate,120),10),120) as shippeddate,coalesce(cidt_carsnoremaining,0) as remaincars,coalesce(cidt_totalshippedcars,0) as shippedcars,coalesce(cidt_actualqty,0) as Noofcars " & _
                            " from CarsIndex ,CarsIndexDetail ,Company ,CarsContract ,CarsShippment " & _
                            " where Caid_CarsIndexID = cidt_carsindexid and Cidt_CarsIndexDetailID =crsh_CarsIndexDetailid" & _
                            " and Caid_Deleted is null and Cidt_Deleted is null " & _
                            "  and caid_companyid =comp_companyid  and caid_CarsContractid =cacn_CarsContractID and coalesce(convert(nvarchar,left(convert(nvarchar,crsh_shippeddate,120),10),120),'1900-01-01') between '" & fd & "' and '" & td & "'" & _
                             " and Comp_CompanyId between " & fcomp & " and  " & tcomp & " and ltrim(rtrim(coalesce(caid_AT9,''))) like ltrim(rtrim('" & at9 & "')) and ltrim(rtrim(coalesce(cidt_bl,''))) like ltrim(rtrim('" & bl & "'))" & _
                            " order by Comp_CompanyId,cacn_carscontractid,Caid_CarsIndexID ,cidt_carsindexdetailid "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function compname(ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT  Comp_CompanyId,comp_name " & _
                            "  FROM     Company " & _
                            "  WHERE  Comp_Deleted is null and  Comp_Name like '" & comp & "%'" & _
                            "  ORDER by comp_name "


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function blname(ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT cidt_carsindexdetailid,cidt_bl" & _
                            "  FROM   Carsindexdetail " & _
                            "  WHERE  cidt_deleted is null and coalesce(Cidt_bl,'') like '" & bl & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function at9(ByVal at As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT Caid_CarsIndexid,Caid_AT9 " & _
                            "  FROM   CARSINDEX  " & _
                            "  WHERE caid_deleted is null and coalesce(Caid_AT9,'') like '" & at & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function unat9(ByVal at As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   select distinct caid_carsindexid,caid_AT9 from CarsIndex i,CarsIndexDetail d,Company c" & _
                            "  where Caid_CarsIndexID =cidt_carsindexid  and c.Comp_CompanyId=caid_companyid and caid_deleted is null and Cidt_Deleted is null " & _
                            "  and i.Caid_AT9 like '" & at & "%' "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function carsunstuf() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select Caid_CarsIndexID ,r.caid_AT9,d.Cidt_CarsIndexDetailID,c.Comp_Name,r.caid_unstuffingdate, r.Caid_Name, r.caid_carsno , r.caid_vesselno, d.cidt_bl,d.cidt_sno,d.cidt_qty, d.cidt_WT, d.cidt_CBM,d.cidt_cusomdeclaredate,d.cidt_customdeclare,(select Capt_US from Custom_Captions where d.cidt_Carsproperty=Capt_Code and Capt_Family='cidt_Carsproperty' )as prop " & _
                            " from   CarsIndex r,CarsIndexDetail d,Company c " & _
                            " where r.Caid_CarsIndexID=d.cidt_carsindexid and r.caid_companyid=c.Comp_CompanyId  and r.Caid_Deleted is null and d.Cidt_Deleted is null and c.comp_deleted is null"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function dcunstuf(ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT  d.cddt_unstuffingdate, d.Cddt_Name,d.cddt_cbm, d.cddt_wt, d.cddt_nopackages, i.cdin_container, c.Comp_Name, d.cddt_size, n.cont_RevenueAccount, i.cdin_AT9, d.cddt_type, d.cddt_cargocondition, cz.Capt_US as size " & _
                            "  FROM   CDIndexDetail d INNER JOIN  CDIndex i ON d.cddt_cdindexid=i.CDin_CDIndexID left outer join Custom_Captions  cz ON d.cddt_size=cz.Capt_Code INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId INNER JOIN Contract n ON i.cdin_Contractid=n.cont_ContractID " & _
                            "  WHERE  i.CDin_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL  AND  (d.cddt_size IS  NULL  OR cz.Capt_Family=N'lcch_volum') AND n.cont_RevenueAccount=N'DC' and comp_name like '" & comp & "'"


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function dunat9(ByVal at As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT  distinct i.cdin_cdindexid, i.Cdin_AT9" & _
                            "  FROM   CDIndexDetail d INNER JOIN  CDIndex i ON d.cddt_cdindexid=i.CDin_CDIndexID INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId INNER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID " & _
                            "  WHERE  i.Cdin_AT9 like '" & at & "%' and i.CDin_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL   AND n.cont_RevenueAccount=N'DC'"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function dinstock() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "SELECT c.comp_companyid,c.comp_name,n.cont_ContractID, i.CDin_CDIndexID, d.Cddt_CDIndexDetailID, g.good_GoodsID, n.cont_companyid, n.cont_Name, " & _
                      " i.CDin_Name, i.cdin_AT9, d.Cddt_Name, g.good_Name, g.good_cbmremain, g.good_remainpackage, " & _
                      " g.good_remainwt, g.good_cbmshipped, g.good_packageshipped, g.good_WTshipped, i.cdin_Remainpackage, g.good_Package, " & _
                      " g.good_cbm, g.good_wt, d.cddt_unstuffingdate,(select top 1 gdsh_date from GoodsShipped where gdsh_Goodsid =g.good_goodsid and gdsh_Deleted is null order by gdsh_date desc) as shipdate " & _
                      " FROM dbo.Contract n INNER JOIN " & _
                      "  CDIndex i ON n.cont_ContractID = i.cdin_Contractid " & _
                      "  INNER JOIN  Company c ON n.cont_companyid=c.Comp_CompanyId  INNER JOIN " & _
                      " CDIndexDetail d ON i.CDin_CDIndexID = d.cddt_cdindexid INNER JOIN " & _
                      " dbo.Goods g ON d.Cddt_CDIndexDetailID = g.good_cdindexdetailid " & _
                      " WHERE (i.CDin_Deleted Is NULL) And (d.Cddt_Deleted Is NULL) And (g.good_Deleted Is NULL) And (n.cont_Deleted Is NULL) And " & _
                      " (n.cont_RevenueAccount = 'DC') AND (g.good_cbmremain > 0) "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function dcompname(ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT distinct c.Comp_CompanyId,c.Comp_Name  " & _
                            "  FROM   vdcinstock s INNER JOIN  Company c ON s.cont_companyid=c.Comp_CompanyId " & _
                            "  where  c.Comp_Deleted is null and Comp_Name like '" & comp & "%'" & _
                            "  ORDER by c.comp_name "


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function dblname(ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " SELECT distinct s.CDin_CDIndexID,s.CDin_Name" & _
                            "  FROM    vdcinstock s INNER JOIN  Company c ON s.cont_companyid=c.Comp_CompanyId  " & _
                            "  where s.Cdin_Name like '" & bl & "%'" & _
                            "  ORDER BY s.CDin_Name "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function dat9(ByVal at As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  SELECT  distinct s.CDin_CDIndexID,s.cdin_AT9" & _
                            "  FROM    vdcinstock s INNER JOIN  Company c ON s.cont_companyid=c.Comp_CompanyId " & _
                            "  where s.Cdin_AT9 like '" & at & "%'" & _
                            "  ORDER BY s.CDin_AT9 "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function goodspallet() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select i.cdin_cdindexid,i.CDin_Name,i.cdin_consignee,i.cdin_customdecl,p.Gdpt_Name,l.itlo_name ,g.good_Name,d.cddt_unstuffingdate,c.comp_name,c.comp_companyid " & _
                            "  from cdindex i inner join GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID " & _
                            "  inner join ItemLocation l on p.gdpt_ItemLocationID=l.Itlo_ItemLocationID inner join Goods g on i.cdin_cdindexid=g.good_cdindexid " & _
                            "  inner join CDIndexDetail d on i.CDin_CDIndexID =d.cddt_cdindexid inner join Company c on i.cdin_company =c.comp_companyid " & _
                            " where p.gdpt_Active='Y' and  i.cdin_deleted is null and d.cddt_deleted is null and l.itlo_deleted is null and p.gdpt_deleted is null and c.comp_deleted is null and g.good_deleted is null"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gscomp(ByVal name As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select distinct c.comp_companyid,c.comp_name " & _
                            "  from CDShipped s inner join GoodsShipped gs on s.cdsh_CDShippedID =gs.gdsh_CDShippedID  inner join Goods g on gs.gdsh_Goodsid =g.good_goodsid  " & _
                            "  inner join cdindex i on i.CDin_CDIndexID=g.good_cdindexid inner join  GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID  " & _
                            "  inner join Contract n on s.cdsh_Contractid =n.cont_ContractID  inner join Company c on n.cont_companyid  =c.comp_companyid " & _
                            "  where  i.cdin_deleted is null  and p.gdpt_deleted is null and n.cont_deleted is null and gs.gdsh_deleted is null " & _
                            "  and c.comp_name like '" & name & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gsbl(ByVal bl As String, ByVal cid As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select distinct i.cdin_cdindexid,i.cdin_name " & _
                            "  from CDShipped s inner join GoodsShipped gs on s.cdsh_CDShippedID =gs.gdsh_CDShippedID  inner join Goods g on gs.gdsh_Goodsid =g.good_goodsid  " & _
                            "  inner join cdindex i on i.CDin_CDIndexID=g.good_cdindexid inner join  GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID  " & _
                            "  inner join Contract n on s.cdsh_Contractid =n.cont_ContractID  inner join Company c on n.cont_companyid  =c.comp_companyid " & _
                            "  where  i.cdin_deleted is null and p.gdpt_deleted is null and n.cont_deleted is null and gs.gdsh_deleted is null " & _
                            "  and i.cdin_name like '" & bl & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gpcomp(ByVal name As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select distinct c.comp_name,c.comp_companyid " & _
                            "  from cdindex i inner join GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID " & _
                            "  inner join ItemLocation l on p.gdpt_ItemLocationID=l.Itlo_ItemLocationID inner join Goods g on i.cdin_cdindexid=g.good_cdindexid " & _
                            "  inner join CDIndexDetail d on i.CDin_CDIndexID =d.cddt_cdindexid inner join Company c on i.cdin_company =c.comp_companyid " & _
                            "  where p.gdpt_Active='Y' and c.comp_deleted is null and c.comp_name like '" & name & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gpbl(ByVal bl As String, ByVal cid As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select distinct i.cdin_cdindexid,i.CDin_Name,c.comp_companyid " & _
                            "  from cdindex i inner join GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID " & _
                            "  inner join ItemLocation l on p.gdpt_ItemLocationID=l.Itlo_ItemLocationID inner join Goods g on i.cdin_cdindexid=g.good_cdindexid " & _
                            "  inner join CDIndexDetail d on i.CDin_CDIndexID =d.cddt_cdindexid inner join Company c on i.cdin_company =c.comp_companyid " & _
                            "   where p.gdpt_Active='Y' and i.cdin_deleted is null and c.comp_companyid='" & cid & "' and i.cdin_name like '" & bl & "%'"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function cdblagent() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   SELECT  i.CDin_CDIndexID, d.cddt_serial,d.cddt_size, d.Cddt_Name, d.cddt_seal, d.cddt_wt, d.cddt_cbm, d.cddt_unstuffingdate, i.CDin_Name, i.cdin_customdecl, i.cdin_decdate, n.cont_Name, d.Cddt_CDIndexDetailID, c.Comp_CompanyId, c.Comp_Name, d.cddt_nopackages, n.cont_RevenueAccount, i.cdin_posted, d.cddt_type, i.cdin_remainCBM, i.cdin_remainwt, p.gdsh_date, p.gdsh_cbm, p.gdsh_wt, cz.Capt_US as size,cddt_cargocondition " & _
                            "   FROM    CDIndex i INNER JOIN CDIndexDetail d ON i.CDin_CDIndexID=d.cddt_cdindexid INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId INNER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID LEFT OUTER JOIN  GoodsShipped p ON i.CDin_CDIndexID=p.gdsh_cdindexid LEFT OUTER JOIN Custom_Captions cz ON d.cddt_size=cz.Capt_Code " & _
                            "   WHERE   i.CDin_Deleted IS  NULL  AND (d.cddt_size IS  NULL  OR cz.Capt_Family=N'lcch_volum') AND d.Cddt_Deleted IS  NULL  AND  n.cont_RevenueAccount=N'CrossDocking'" & _
                            "   ORDER BY c.Comp_CompanyId, i.CDin_CDIndexID, d.Cddt_CDIndexDetailID, d.cddt_serial "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function cdcompname(ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   SELECT  distinct c.Comp_CompanyId, c.Comp_Name" & _
                            "   FROM    CDIndex i INNER JOIN CDIndexDetail d ON i.CDin_CDIndexID=d.cddt_cdindexid INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId LEFT OUTER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID LEFT OUTER JOIN  GoodsShipped p ON i.CDin_CDIndexID=p.gdsh_cdindexid " & _
                            "   WHERE   i.CDin_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL  AND  n.cont_RevenueAccount=N'CrossDocking' and c.comp_name like '" & comp & "%'" & _
                            "   ORDER BY  c.Comp_Name"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function cdcud(ByVal cud As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   SELECT  distinct i.CDin_CDIndexID,i.cdin_customdecl" & _
                            "   FROM    CDIndex i INNER JOIN CDIndexDetail d ON i.CDin_CDIndexID=d.cddt_cdindexid INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId LEFT OUTER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID LEFT OUTER JOIN  GoodsShipped p ON i.CDin_CDIndexID=p.gdsh_cdindexid " & _
                            "   WHERE   i.CDin_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL  AND  n.cont_RevenueAccount=N'CrossDocking' and c.comp_name like '" & cud & "%'" & _
                            "   ORDER BY i.cdin_customdecl "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function cdunat9(ByVal at9 As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   SELECT  distinct  i.cdin_cdindexid,i.cdin_AT9" & _
        "  FROM CDIndexDetail d INNER JOIN  CDIndex i ON d.cddt_cdindexid=i.CDin_CDIndexID  INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId INNER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID" & _
        "  WHERE i.CDin_Deleted Is NULL And d.Cddt_Deleted Is NULL And n.cont_RevenueAccount = N'CrossDocking' " & _
        "  and i.cdin_AT9 like '" & at9 & "%'"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function cdunstuff() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "   SELECT  d.cddt_unstuffingdate, d.Cddt_Name, d.cddt_cbm, d.cddt_wt, d.cddt_nopackages, i.cdin_container,i.cdin_consignee ,c.Comp_Name, d.cddt_size, n.cont_RevenueAccount, i.cdin_AT9, d.cddt_type, d.cddt_cargocondition, cz.Capt_US as size" & _
                            "   FROM    CDIndexDetail d INNER JOIN  CDIndex i ON d.cddt_cdindexid=i.CDin_CDIndexID LEFT OUTER JOIN Custom_Captions cz ON d.cddt_size=cz.Capt_Code INNER JOIN  Company c ON i.cdin_company=c.Comp_CompanyId INNER JOIN  Contract n ON i.cdin_Contractid=n.cont_ContractID " & _
                            "   WHERE  i.CDin_Deleted IS  NULL  AND d.Cddt_Deleted IS  NULL   AND (d.cddt_size IS  NULL  OR cz.Capt_Family=N'lcch_volum') AND n.cont_RevenueAccount=N'CrossDocking'"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gdpship() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "  select s.cdsh_CDShippedID,i.cdin_cdindexid,i.cdin_consignee,p.Gdpt_GoodsPalletID ,i.CDin_Name,i.cdin_customdecl,s.cdsh_name, gs.gdsh_Date ,p.Gdpt_Name,l.itlo_name,g.good_name,c.comp_name,c.comp_companyid,coalesce(gdsh_wt,0) as wt,coalesce(gdsh_cbm,0) as cbm,coalesce(gdsh_Packages,0) as pkg  " & _
                            "  from CDShipped s inner join GoodsShipped gs on s.cdsh_CDShippedID =gs.gdsh_CDShippedID  inner join Goods g on gs.gdsh_Goodsid =g.good_goodsid  " & _
                            "  inner join cdindex i on i.CDin_CDIndexID=g.good_cdindexid inner join  GoodsPallet p on i.CDin_CDIndexID =p.gdpt_CDIndexID left outer join ItemLocation l on p.gdpt_ItemLocationID=l.Itlo_ItemLocationID " & _
                            "  inner join Contract n on s.cdsh_Contractid =n.cont_ContractID  inner join Company c on n.cont_companyid  =c.comp_companyid " & _
                            "  where  i.cdin_deleted is null and l.itlo_deleted is null and p.gdpt_deleted is null and n.cont_deleted is null and gs.gdsh_deleted is null "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function gdpshipbyagent(ByVal goods As String, ByVal fdate As String, ByVal tdate As String, ByVal fcomp As Int64, ByVal tcomp As Int64, ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select s.cdsh_CDShippedID,i.cdin_cdindexid,i.cdin_consignee,i.CDin_Name,i.cdin_customdecl,s.cdsh_name, gs.gdsh_Date ,g.good_name,c.comp_name,c.comp_companyid  ,coalesce(gdsh_wt,0) as wt,coalesce(gdsh_cbm,0) as cbm,coalesce(gdsh_Packages,0) as pkg    " & _
                            " from CDShipped s , GoodsShipped gs, Goods g ,cdindex i ,Contract n , Company c " & _
                            " where s.cdsh_CDShippedID =gs.gdsh_CDShippedID and  gs.gdsh_Goodsid =g.good_goodsid and gdsh_cdindexid=cdin_cdindexid and s.cdsh_Contractid =n.cont_ContractID  and n.cont_companyid  =c.comp_companyid and good_name like '%" & goods & "%' and convert(datetime,left(convert(nvarchar,gdsh_date,120),10)) between '" & fdate & "' and '" & tdate & "' " & _
                            " and comp_companyid between " & fcomp & " and  " & tcomp & " and cdin_name like '%" & bl & "%' "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function invstate(ByVal fd As String, ByVal td As String, ByVal fcomp As Int64, ByVal tcomp As Integer, ByVal condtype As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select comp_companyid,comp_name,lcpi_BL,lcpi_customdeclare,lcpi_name,lcpi_totalinv,convert(datetime,left(convert(nvarchar,lcpi_date,120),10),120) as indate  from LCLPerformaInv,company  where lcpi_companyid=Comp_CompanyId and lcpi_companyid  between  " & fcomp & " and " & tcomp & " " & _
                            " and lcpi_status in('Draft','Posted') and lcpi_deleted is null  and convert(datetime,left(convert(nvarchar,lcpi_date,120),10),120) between '" & fd & "' and '" & td & "' " & condtype & " "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function sercomp(ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select comp_companyid,comp_name  from company where Comp_deleted is null and comp_name like '%" & comp & "%' "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function continsp(ByVal fd As String, ByVal td As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select cnis_ContInspID ,cnis_name ,(select capt_us from custom_captions where capt_code=i.cnis_contsize and capt_family='lcch_volum'  )  as contsize " & _
                            " ,cnis_plateno ,cnis_Customdeclaration ,cnis_customer ,convert(datetime,left(convert(nvarchar,cnis_InspectionStartDate,120),10),120) as inspdate  " & _
                            " from ContInsp i " & _
                            " where cnis_Deleted is null and convert(datetime,left(convert(nvarchar,cnis_InspectionStartDate,120),10),120) between '" & fd & "' and '" & td & "'"

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function invinsp(ByVal cinsid) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "Select cnis_ContInspID,cnis_customer ,cnis_cnee  from ContInsp  where cnis_ContInspID =" & cinsid
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function cdmonthly(ByVal fd As String, ByVal td As String, ByVal fcom As Int64, ByVal tcom As Int64, ByVal bl As String, ByVal goods As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select cdin_cdindexid,cdin_name,convert(datetime,left(convert(nvarchar,cddt_unstuffingdate,120),10),120) as DateofStorage,cdin_shipname,cddt_cdindexdetailid,cddt_name,coalesce(good_Package,0) as qty , good_name,cdin_shipper as Shipper,cdin_po as PO  " & _
       " from Cdindex, cdindexdetail, goods " & _
       " where cdin_cdindexid = cddt_cdindexid And " & _
       " good_cdindexid = cdin_cdindexid And Cddt_CDIndexDetailID = good_cdindexdetailid " & _
       " and good_deleted is null and cdin_deleted is null " & _
       " and cddt_deleted is null " & _
       " and convert(datetime,left(convert(nvarchar,cddt_unstuffingdate,120),10),120) between '" & fd & "' and '" & td & "' " & _
       " and  cdin_name like N'%" & bl & "%' and cdin_company between " & fcom & " and " & tcom & " and Ltrim(rtrim(good_name)) like N'%" & Trim(goods) & "%' and good_remainpackage >0  "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function serbl(ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select cdin_cdindexid,cdin_name from cdindex where cdin_deleted is null   " & _
                            " and  cdin_name like N'%" & bl & "%' "

        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function invtype() As DataTable
        Dim cmd As New SqlCommand
        Dim sql As String = "select Capt_Code,capt_us from Custom_Captions " & _
                            " where Capt_Family='itlo_type' " & _
                            " and Capt_Deleted is null    "


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataTable
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function containercode(ByVal did As Integer) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " Select cddt_cdindexdetailid,cddt_name,cddt_code,(select cdin_name from cdindex where cdin_cdindexid=cddt_cdindexid) as BL from cdindexdetail " & _
                            " where cddt_deleted Is null And cddt_cdindexdetailid = " & did


        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function goods(ByVal name As String, ByVal comp As String, ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select distinct rtrim(ltrim(good_name)) as good_name from goods,cdindex,company where good_cdindexid=cdin_cdindexid and cdin_company =comp_companyid and good_name like N'" & name & "' and cdin_name like N'" & bl & "' and comp_name like N'" & comp & "' and good_deleted is null"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = Sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function goodsshippment(ByVal name As String, ByVal comp As String, ByVal bl As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select distinct rtrim(ltrim(good_name)) as good_name from GoodsShipped ,cdshipped,contract,company,goods,cdindex where good_GoodsID =gdsh_Goodsid and  cdsh_CDShippedID =gdsh_cdshippedid and gdsh_cdindexid =cdin_cdindexid and cdsh_contractid=cont_contractid and cont_companyid=comp_companyid and good_name like N'" & name & "' and cdin_name like N'" & bl & "' and comp_name like N'" & comp & "' and good_deleted is null and gdsh_deleted is null"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function revenues(ByVal fd As String, ByVal td As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = " select lcpi_Type , coalesce(sum(lcpi_totalinv),0) from LCLPerformaInv where lcpi_deleted is null and  Lcpi_Status='Posted'" & _
                            " and lcpi_posteddate between '" & fd & "' and '" & td & "'" & _
                            " group by lcpi_Type  "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function
    Friend Function sumbyservlodofflod(ByVal fd As DateTime, ByVal td As DateTime, ByVal comp As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select Lcpi_LCLPerformaInvID,Lcid_LCLPerformaInvDtID,cdin_startunstuffing,comp_companyid,comp_name,cdin_name,cdin_customdecl,convert(datetime,left(convert(nvarchar,lcpi_valuedate,120),10)) as invdate,lcpi_accpacno,(select capt_us from Custom_Captions where capt_code=lclperformainvdt.lcid_code and Capt_Family='lcid_code') as servicedesc,coalesce(lcid_Amount,0) as amnt,Lcid_Name,lcid_note      " &
                            " from LCLPerformaInv ,lclperformainvdt,Company,cdindex " &
                            " where Lcpi_LCLPerformaInvID =lcid_LCLPerformaInvid and Comp_CompanyId=lcpi_companyid and cdin_cdindexid=lcpi_cdindexid " &
                            " and Lcpi_Status in('Posted','Draft') and Lcpi_Deleted is null " &
                            " and Lcid_Deleted is null and lcid_code in('001','012') and coalesce(lcid_Amount,0)<>0" &
                            " and lcpi_companyid like '" & comp & "' and convert(datetime,left(convert(nvarchar,lcpi_valuedate,120),10)) between '" & fd & "' and '" & td & "'" &
                            " order by lcid_code  "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function sumbyserv(ByVal fd As DateTime, ByVal td As DateTime, ByVal comp As String, ByVal servc As String) As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "select Lcpi_LCLPerformaInvID,Lcid_LCLPerformaInvDtID,cdin_startunstuffing,comp_companyid,comp_name,cdin_name,cdin_customdecl,convert(datetime,left(convert(nvarchar,lcpi_valuedate,120),10)) as invdate,lcpi_accpacno,(select capt_us from Custom_Captions where capt_code=lclperformainvdt.lcid_code and Capt_Family='lcid_code') as servicedesc,coalesce(lcid_Amount,0) as amnt,Lcid_Name,lcid_note      " &
                            " from LCLPerformaInv ,lclperformainvdt,Company,cdindex " &
                            " where Lcpi_LCLPerformaInvID =lcid_LCLPerformaInvid and Comp_CompanyId=lcpi_companyid and cdin_cdindexid=lcpi_cdindexid " &
                            " and Lcpi_Status in('Posted','Draft') and Lcpi_Deleted is null " &
                            " and Lcid_Deleted is null  and coalesce(lcid_Amount,0)<>0" &
                            " and lcpi_companyid like '" & comp & "' and convert(datetime,left(convert(nvarchar,lcpi_valuedate,120),10)) between '" & fd & "' and '" & td & "' and ltrim(rtrim(coalesce(lcid_code,''))) like '" & Trim(servc) & "'" &
                            " order by lcid_code  "
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

    Friend Function invservices() As DataSet
        Dim cmd As New SqlCommand
        Dim sql As String = "Select  capt_code,capt_us from custom_captions where capt_family='lcid_code' and capt_deleted is null order by convert(nvarchar,Capt_US)"
        Dim da As New SqlDataAdapter
        Dim con As New SqlConnection(Readconnectionstring())
        With cmd
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = con
            .CommandTimeout = 0
        End With
        Dim dt As New DataSet
        da.SelectCommand = cmd
        da.Fill(dt)
        con.Close()
        con.Dispose()
        Return dt
    End Function

End Class
