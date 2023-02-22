<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dcunstuffed.aspx.vb" Inherits="CATS.ReportsProj.dcunstuffed" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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


    <style type="text/css">
        .auto-style1 {
            width: 400px;
        }
    </style>
</head>
<body>
    <form id="frmdcun" runat="server">

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
       <asp:Label ID="Label1" CssClass="lblt"   runat="server" Text="DC Unstuffed"></asp:Label>
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

   <table >
 
   <tr>
   <td>
       <table   >
    <tr>
 <td style="width:100px;" >
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
    <asp:Label ID="Lbltd" CssClass ="lbl" runat="server" Text="To Date"></asp:Label>
    </td>
    <td >
     <asp:TextBox ID="Txttd" Width="120px" Height="20px"  runat="server"></asp:TextBox>
    </td>
  
    </tr>
    </table>
    </td>
      
    </tr>

         <tr>
             <td><table><tr>
                   <td class="untdp" >
    <asp:Label ID="lblcomp" runat="server" CssClass ="lbl" Text="Company"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtcomp" CssClass="txtbox" runat="server"></asp:TextBox>
    </td>
    <td>
    <asp:ImageButton ID="Imgsc" ImageUrl ="~/img/search.png"     runat="server" />
    </td>

                        </tr>
                 <tr>
                     <td class="untdp"></td>
                     <td style="position :absolute;">
                         <table>
                             <tr>
                                 <td class="auto-style1">
                                     <asp:ImageButton ID="Imgbx" runat="server" Height="20px" ImageUrl="~/imgs/close-icon-29.png" Visible="false" style="width: 18px" />
                                     <asp:Panel ID="pancomp" runat="server" Height="100px" ScrollBars="Vertical" Visible="false">
                                         <asp:GridView ID="Gvcomp" runat="server" AutoGenerateColumns="False" ForeColor="#333333" CellPadding="4" GridLines="None">
                                             <RowStyle BackColor="#EFF3FB" />
                                             <Columns>
                                                 <asp:TemplateField Visible="False">
                                                     <ItemTemplate>
                                                         <asp:Label ID="Label6" runat="server" Text='<%# Bind("comp_companyid")%>' Width="300px"></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Agent">
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="Lkname" runat="server" onclick="Lkname_Click" Text='<%# bind("comp_name") %>' Width="300px"></asp:LinkButton>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                               
                                             </Columns>
                                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                             <EditRowStyle Font-Size="11pt" BackColor="#2461BF" />
                                             <AlternatingRowStyle BackColor="White" />
                                             <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                             <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                             <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                             <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                         </asp:GridView>
                                     </asp:Panel>
                                 </td>
                             </tr>
                             <tr>
                                 <td class="auto-style1"></td>
                             </tr>
                         </table>
                     </td>

                 </tr>

                 </table></td>
   
    </tr>
    
    </table>
    </td>
    </tr>
    <tr>
    <td>
    <table>
    <tr>
    <td class="untdp">
        <asp:Label ID="Lblat" CssClass ="lbl" runat="server" Text="AT9"></asp:Label>
    </td>
        <td>
            <asp:TextBox ID="Txtat" runat="server" CssClass="txtbox"></asp:TextBox>
        </td>
        <td>
            <asp:ImageButton ID="Imgsa" runat="server" ImageUrl="~/img/search.png" style="height: 23px" />
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
                               <asp:Label ID="lblpv" runat="server" CssClass="lbl" Text="Format Report"></asp:Label>
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <table>
                                   <tr>
                                       <td>
                                           <asp:RadioButton ID="rbexc" runat="server" CssClass="lbl" GroupName="prev" Text="Excel" />
                                       </td>
                                       <td class="rttab"></td>
                                   </tr>
                               </table>
                           </td>
                       </tr>
                       <tr>
                           <td>
                               <asp:RadioButton ID="rbcr" runat="server" CssClass="lbl" GroupName="prev" Text="PDF" />
                           </td>
                       </tr>
                   </table>
               </td>
           </tr>
           <tr>
               <td>
                   <table>
                       <tr>
                           <td class="tdimg"></td>
                           <td>
                               <asp:ImageButton ID="Impprv" runat="server" ImageUrl="~/img/vprt.png" style="height: 34px" />
                           </td>
                           <td>
                               <asp:ImageButton ID="Imgclr" runat="server" Height="32px" ImageUrl="~/img/clear.png" Width="100px" />
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
        </td>
        </tr>
        </table>
    
     
    </form>
</body>
</html>
