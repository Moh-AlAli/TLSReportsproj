<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cdgoodspallet.aspx.vb" Inherits="CATS.ReportsProj.CDgoodspallet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
<link href="css/parform.css"  rel="Stylesheet" type="text/css"  /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table class="fultab" >
      <tr>
      <td class="tddiv" >
      
      </td>
      <td  align="center">
      
      </td>
      <td  class="tddiv">
      </td>
      <td>
      </td>
      </tr>
      </table> 
    </div>
    <table >
    <tr>
    <td class="tdcl" >
    </td>
    <td class="rttab" >
    <asp:Panel ID="panpar"  runat  ="server"  Height ="170px"  >
   <table >
   <tr>
   <td>
    <table   >
    <tr>
    <td class="td">
    </td>
    <td>
    <table class ="tbr">
     <tr>
     <td>
     <table>
    
   
    <tr>
    <td>
    <table>
    <tr>
    <td class="tdtit">
    </td>
    <td>
     <asp:RadioButton ID="rbexc"   CssClass ="lbl" Text ="Excel" GroupName ="prev" runat="server" />
    </td>
    <td class="rttab">
    </td>
    </tr>
    <tr>
    <td></td>
    <td>
     <asp:RadioButton ID="rbcr" CssClass ="lbl"  Text ="PDF" GroupName ="prev" runat="server" />
    </td>
    </tr>
    </table>
       
    </td>
    </tr>
    </table>
     </td>
    </tr> 
    <tr>
    <td>
    <table>
     <tr>
     </td>
    <td  >
    <table>
    <tr>
    <td class="tdtit" >
    </td>
    <td>
   
        <asp:ImageButton ID="Imgvrpt" ImageUrl ="~/img/vprt.png"  runat="server" />
    </td>
    <td>
        &nbsp;</td>
    </tr>
    </table>
    </td>      
    </tr>
    </table>
    </td>
     
    </tr>
    </table>
    </td>
    </tr>
    </table> 
    </asp:Panel>
    </td>
    </tr>
    </table>
</form>
</body>
</html>
