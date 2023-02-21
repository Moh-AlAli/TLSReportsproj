<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cdgdshipbyagent.aspx.vb" Inherits="CATS.ReportsProj.cdgdshipbyagent" %>

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
      <asp:Label ID="Label1" CssClass="lblt"   runat="server" Text="Daily OUTBound by agent"></asp:Label>
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
    <asp:Panel ID="panpar"  runat  ="server"  Height ="260px"  >
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
     <td >
    <asp:Label ID="lblcomp" runat="server" CssClass ="lbl" Text="Company"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtcomp"  CssClass="txtbox"  runat="server"></asp:TextBox>
    </td>
    <td>
    <asp:ImageButton ID="Imgsc" ImageUrl ="~/img/search.png"     runat="server" />
    </td>
    </tr>
    <tr>
   
    <td>
    <asp:Label ID="lbl" CssClass ="lbl"  runat="server" Text="B\L"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtbl" CssClass="txtbox" runat="server"></asp:TextBox>
    </td>
    <td>
     <asp:ImageButton ID="Imgsb" ImageUrl ="~/img/search.png" runat="server" />
    </td>
    </tr>
    </table>
     </td>
    </tr> 
       <tr>
    <td>
    <table>
    <tr>
    <td>
        <asp:Label ID="lblpv" CssClass ="lbl" runat="server" Text="Format Report"></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
    <table>
    <tr>
    
    <td>
     <asp:RadioButton ID="rbexc"   CssClass ="lbl" Text ="Excel" GroupName ="prev" runat="server" />
    </td>
    <td class="rttab">
    </td>
    </td>
    </tr>
    <tr>
    
    <td>
     <asp:RadioButton ID="rbcr" CssClass ="lbl"  Text ="PDF" GroupName ="prev" runat="server" />
    </td>
    </tr>
    </table>
   </td> 
   </tr>
    <tr>
    
    <td  >
    <table>
    <tr>
    <td class="tdimg" >
    </td>
    <td>
    <asp:ImageButton ID="Imgvrpt" ImageUrl ="~/img/vprt.png"  runat="server" />
    </td>
    <td>
    <asp:ImageButton ID="Imgclr" Width ="100px" Height ="32px"  ImageUrl ="~/img/clear.png" runat="server" />
    </td>
    </tr>
    </table>
    </td>      
    </tr>
    </table>
    </td>
    <td class ="td">
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
