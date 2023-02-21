<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="gbl.aspx.vb" Inherits="CATS.ReportsProj.gbl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
 <link href="css/parform.css" rel="Stylesheet" type="text/css"   />
</head>
<body>
<form id="frmdsb" runat="server">
   <table class ="fultab" >
   <tr>
   <td>
   <table class ="fultab" >
   <tr>
   <td>
   <table>
   <tr>
   <td class="tdsearch" >
   </td>
   <td class="tdst">
   </td>
   <td >
   <asp:Label ID="Lblsbl" CssClass ="lblt" runat="server" Text="SearchB\L"></asp:Label>
   </td>
   </tr>
   </table>
   </td>
   
   </tr>
   <tr>
   <td align="ceneter">
   <table   >
   <tr>
   <td class="tdts"   >
   </td>
   <td >
   <asp:Label ID="lblbl" CssClass="lbl"   runat="server" Text="B\L"></asp:Label>
   </td>
   <td >
   <asp:TextBox ID="Txtsbl" CssClass ="txtbox"  runat="server"></asp:TextBox>
   </td>
   </tr>
   <tr>
   <td>
   </td>
   <td>
   </td>
   <td align="center">
   <table>
   <tr>
   <td>
     <asp:Button ID="Bsbl" runat="server" Text="Search" />
   </td>
  <td>
  <asp:Button ID="Bcls" runat ="server" Text="Close" />
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
 
   <tr>
   <td>
   <table class ="fultab">
   <tr>
   <td class="tdsearch" >
   
   </td>
   <td >
    <asp:Panel Id="pangv"  Visible="false" runat ="server" Width="350px" Height ="200px"  ScrollBars ="Vertical"  >
   <table class="tdgv"  >
   <tr>
   <td  >
    <asp:GridView ID="gvbl" Visible ="false"  Width ="320px"   runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false"  > 
           <ItemTemplate >
               <asp:LinkButton ID="Lkid" Text ='<%#bind("cdin_CDindexID")%>' runat="server" ></asp:LinkButton>
           </ItemTemplate> 
            </asp:TemplateField>
           <asp:TemplateField HeaderText="B\L">
           <ItemTemplate >
               <asp:LinkButton ID="Lkbl" 
                   Text ='<%#bind("cdin_name")%>' runat="server" onclick="lkblclick"></asp:LinkButton>
           </ItemTemplate> 
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
   </td>
   </tr>
   </table>
    </asp:Panel>
   </td>
   
  </tr>
    
    </table>
    
   </td>
   </tr>
   </table>
</form>
</body> 
</html> 