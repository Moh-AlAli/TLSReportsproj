<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="carsunstuffed.aspx.vb" Inherits="CATS.ReportsProj.carsunstuffed" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="css/parform.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    </div>
    <div>
    <table class="fultab" >
      <tr>
      <td>
      
      </td>
      <td  >
      <table>
      <tr>
      <td  >
      </td>
      <td class="tdtit"   >
      </td>
      <td>
      <asp:Label ID="Lblmsg" Visible="false"  runat="server" Text=""></asp:Label>
      </td>
      </tr>
      </table>
      
      </td>
      <td  class="tddiv">
      </td>
      <td>
      </td>
      </tr>
      <tr>
      <td>
      </td>
      <td   >
      <table>
      <tr>
      <td  >
     
      </td>
      <td class="tdtit"   >
      </td>
      <td>
       <asp:Label ID="Label1" CssClass="lblt"   runat="server" Text="CARS Unstuffed"></asp:Label>
      </td>
      </tr>
      </table>
       
      </td>
      <td  class="tddiv">
      </td>
      <td class="tddiv" >
      </td>
     
      </tr>
      </table> 
      </div>
    <table >
    <tr>
    <td  >
    <asp:Panel ID="panpar"  runat  ="server"  Height ="240px" Width="625px"  >
   <table >
 
   <tr>
   <td>
       <table   >
    <tr>
 <td class="td" >
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
     <td class="untdp" >
    <asp:Label ID="lblfd"  CssClass ="lbl"  runat="server" Text="From Date"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtfd"  Width="120px" Height="20px" runat="server"></asp:TextBox>
    </td>
    <td>
     <asp:ImageButton ID="Imgfd"   ImageUrl ="~/img/calendar.jpg" Width="35px" Height ="24px" runat="server" />
     <asp:CalendarExtender ID="CalendarExtender1" TargetControlID ="txtfd" PopupButtonID="Imgfd" Format ="yyyy/MM/dd"  runat="server"></asp:CalendarExtender>
    </td>
    <td>
    <asp:Label ID="Lbltd" CssClass ="lbl" runat="server" Text="To date"></asp:Label>
    </td>
    <td >
     <asp:TextBox ID="txttd" Width="120px" Height="20px"  runat="server"></asp:TextBox>
    </td>
    <td>
    <asp:ImageButton ID="Imgtd" ImageUrl ="~/img/calendar.jpg" Width="34px" Height ="21px" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID ="txttd" PopupButtonID="Imgtd" Format ="yyyy/MM/dd"  runat="server"></asp:CalendarExtender>
    </td>
    </tr>
    </table>
    </td>
      
    </tr>
    <tr>
    <td>
    <table>
    <tr>
     <td class="untdp" >
    <asp:Label ID="Lblat"  CssClass ="lbl" runat="server" Text="AT9"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtat" CssClass ="txtbox"  runat="server"></asp:TextBox>
       
    </td>
    <td>
    <asp:ImageButton ID="Imgsa" ImageUrl ="~/img/search.png"    runat="server" style="height: 23px" />
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
    <asp:ImageButton ID="Impprv" ImageUrl ="~/img/vprt.png"  runat="server" />
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
