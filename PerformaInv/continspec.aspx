<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="continspec.aspx.vb" Inherits="CATS.ReportsProj.continspec" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href ="css/parform.css"  rel="Stylesheet" type ="text/css"  />
      <link href ="css/ui-lightness/jquery-ui-1.9.2.custom.min.css"rel="Stylesheet" type="text/css"  />
           <link href ="css/ui-lightness/jquery-ui-timepicker-addon.css" rel="Stylesheet" type="text/css"  />
           <script src="jq/jquery-1.8.3.js"  ></script>
           <script src="jq/jquery-ui-1.9.2.custom.js"   ></script>
           <script src="jq/jquery-ui-1.9.2.custom.min.js"   ></script>
    <link href="js/jquery-ui.structure.min.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.structure.min.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="js/jquery-ui.min.css" rel="stylesheet" type="text/css" />
 

 <script>

     $(function () {
         $("#Txtfd").datepicker(
             {
                 dateFormat: "yy-mm-dd"

             }
         );
         $("#Txttd").datepicker(
             {
                 dateFormat: "yy-mm-dd"

             }
         );
     }
     );
 </script>

               </head>
<body>
    <form id="frminsp" runat="server">

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
       <asp:Label ID="Label1" CssClass="lblt"   runat="server" Text="Daily Inspection"></asp:Label>
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
   
    </td>
    <td>
    <asp:Label ID="Lbltd" CssClass ="lbl" runat="server" Text="To Date"></asp:Label>
    </td>
    <td >
     <asp:TextBox ID="txttd" Width="120px" Height="20px"  runat="server"></asp:TextBox>
    </td>
    <td>
 
 
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
