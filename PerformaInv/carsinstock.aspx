<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="carsinstock.aspx.vb" Inherits="CATS.ReportsProj.carsinstock" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cars in-stock</title>
    <link href="css/parform.css"  rel="Stylesheet" type="text/css"  /> 
 <link href ="css/ui-lightness/jquery-ui-1.9.2.custom.min.css"rel="Stylesheet" type="text/css"  />
           <link href ="css/ui-lightness/jquery-ui-timepicker-addon.css" rel="Stylesheet" type="text/css"  />
           <script src="jq/jquery-1.8.3.js"  ></script>
           <script src="jq/jquery-ui-1.9.2.custom.js"   ></script>
           <script src="jq/jquery-ui-1.9.2.custom.min.js"   ></script>
           <script  src="jq/jquery-ui-timepicker-addon.js"  ></script>
    <link href="js/jquery-ui.structure.min.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.min.css" rel="stylesheet" type="text/css" />
 
           <script>
               $(function() {
               $("#Txtfd").datepicker(
               { dateFormat : "yy-mm-dd"
         
           }
               );
               $("#Txttd").datepicker(
               { dateFormat: "yy-mm-dd",
                
                 
               }
               );
        
               }
           );
               </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table class="fultab" >
      <tr>
      <td class="tddiv" >
      
      </td>
      <td  align="center">
      <asp:Label ID="Label1" CssClass="lblt"   runat="server" Text="CARS In-stock & Shipped"></asp:Label>
      </td>
      <td  class="tddiv">
      </td>
      <td>
      </td>
      </tr>
      </table> 
    </div>
    <table  style="height :400px;">
    <tr>
    <td class="tdcl" >
    </td>
    <td class="rttab" >
    <asp:Panel ID="panpar"  runat  ="server"  Height ="320px"  >
   <table >
   <tr>
   <td>
    <table   >
    <tr>
    
    <td>
    <table class ="tbr">
 <tr>
     <td>
     <table>
     <tr>
     <td style="width:90px">
     <asp:label ID="lbldttyp"  CssClass ="lbl"  runat="server"  Text ="Date Type"></asp:label> 
     </td>
         <td><asp:DropDownList ID="DDLDatetype" Width="360px" runat="server" >
             <asp:ListItem Selected="True" Value="0">--None--</asp:ListItem>
             <asp:ListItem Value="1">Un-stuffingDate</asp:ListItem>
             <asp:ListItem Value="2">Shipped Date</asp:ListItem>
             </asp:DropDownList></td>
     </tr>
     </table>
     </td>

 </tr>
     <tr>
         <td>
<table>
    <tr>
  <td style="width:90px">
    <asp:Label ID="Label2" CssClass ="lbl" runat="server" Text="From Date"></asp:Label>
    </td>
    <td class="auto-style1">
    <asp:TextBox ID="Txtfd" Width="140px" Height="20px"   runat="server"></asp:TextBox>
    </td>
    <td class="style1"> <asp:Label ID="Label3" CssClass ="lbl" runat="server" Text="To Date"></asp:Label></td>
    <td>
     <asp:TextBox ID="Txttd" Width="140px" Height="20px"  runat="server"></asp:TextBox>
    </td>
    </tr>
</table>
         </td>
  
    </tr>
          <tr>
     <td >
     <table>
     <tr>
     <td style="width:90px">
    <asp:Label ID="lblcomp" runat="server" CssClass ="lbl" Text="Company"></asp:Label>
    </td>
    <td class="auto-style1">
    <asp:TextBox ID="Txtcomp"  CssClass="txtbox"  runat="server"></asp:TextBox>
    </td>
    <td>
    <asp:ImageButton ID="Imgsc" ImageUrl ="~/img/search.png"     runat="server" />
    </td>
    </tr>
    <tr>
   
    <td style="width:90px">
    <asp:Label ID="lbl" CssClass ="lbl"  runat="server" Text="B\L"></asp:Label>
    </td>
    <td class="auto-style1">
    <asp:TextBox ID="Txtbl" CssClass="txtbox" runat="server"></asp:TextBox>
    </td>
    <td>
     <asp:ImageButton ID="Imgsb" ImageUrl ="~/img/search.png" runat="server" />
    </td>
    </tr>
    <tr>
    <td style="width:90px">
    <asp:Label ID="Lblat" CssClass ="lbl" runat="server" Text="AT9"></asp:Label>
    </td>
    <td class="auto-style1">
    <asp:TextBox ID="Txtat" CssClass="txtbox" runat="server"></asp:TextBox>
    </td>
    <td>
    <asp:ImageButton ID="Imgsa" ImageUrl ="~/img/search.png"   runat="server" />
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
    </table>
     </td>
    </tr> 
    <tr>
    <td >
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
    
     
    </tr>
    </table>
        </asp:Panel>
        
    </td>
    </tr>
    </table>
  
   
</form>
</body>
</html>
