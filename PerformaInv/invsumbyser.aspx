<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="invsumbyser.aspx.vb" Inherits="CATS.ReportsProj.invsumbyser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoices summary by Service</title>
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
        <script src="js/modal.js" type="text/javascript"></script>
    <script src="js/bootstrap.js"></script>
    <link href="css/bootstrap.css" rel="stylesheet" />

 

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
    <form id="form1"   runat="server">
        <table class="fultab" >
      <tr>
      <td class="tdtit" >
           <asp:Label ID="Lblmsg" Visible="false"  runat="server" Text=""></asp:Label>
      </td>
     
     
      </tr>
      <tr>
     
      <td>
      <table>
      <tr>

      <td style="width:160px;"   >
      </td>
      <td  >
       <asp:Label ID="Label1" CssClass="lblt" style="font-size :14pt;"   runat="server" Text="Invoices Summary By Services"></asp:Label>
      </td>
      </tr>
      </table>
       
      </td>
      <td class="tddiv" >
      </td>
     
      </tr>
      </table> 
   
    <table >
    <tr>
 <td style="width:100px;"></td>
    <td  >
    <asp:Panel ID="panpar"  runat  ="server"  Height ="300px" Width="625px"  >
   <table >
 
   <tr >
   <td style="width:20px;">
       <table   >
    <tr>
 <td class="td" >
 </td>
    <td class="auto-style1">
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
    <td style="width:40px;">
    
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
    <td>
    <table>
    <tr>
        
               <td class="untdp" >
    <asp:Label ID="Lblcom"  CssClass ="lbl" runat="server" Text="Company"></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="Txtcom" CssClass ="txtbox"  runat="server"></asp:TextBox>
       
    </td>
    <td>
    <asp:ImageButton ID="Imgscm" ImageUrl ="~/img/search.png"    runat="server" 
            style="height: 23px" />
    </td>
        
     
    </tr>
        <tr>
<td class="untdp"></td>
        </tr>
     </table> 
      </td>
          </tr>
     <tr>
  
     <td style="position :absolute;" >
       
       <table>
       <tr>
       
       <td  style="width:450px; text-align:right;"> <asp:ImageButton ID="Imgbx" ImageUrl ="~/img/close-icon-29.png" Height ="20px" Visible ="false"   runat="server" />
                <asp:Panel ID="pancomp" runat ="server" Height ="100px" Width ="450px" 
               Visible ="false" ScrollBars ="Vertical"  >
                    <asp:GridView ID="Gvcomp" runat="server" BorderColor="#999999" ForeColor="#333333" 
                    AutoGenerateColumns="False" Width ="430px">
                        <RowStyle BackColor="#F7F6F3"  ForeColor="#333333" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" Width="300px" runat="server" 
                                  Text='<%#Bind("comp_companyid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Lkname" Width="300px"  runat="server" onclick="Lkname_Click" 
                                  Text='<%#Bind("comp_name") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Size ="8pt" BackColor="#B3B3B3" Font-Bold="True" 
                            ForeColor="White" />
                        <EditRowStyle Font-Size="11pt" />
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                    </asp:GridView>
           </asp:Panel>
                  </td>
       </tr>
       </table> 
        </td>
    </tr>
        <tr>
           <td>
               <table>
                   <tr>
                              <td  class="untdp" ><asp:Label ID="Label2"  CssClass ="lbl" runat="server" Text="Service"></asp:Label></td>
            <td>
            <asp:DropDownList ID="DDLserv" runat="server"></asp:DropDownList>
                </td>
                   </tr>
               </table>

           </td>
               <%-- </tr>
            </table>
                </td>--%>
           
        </tr>
    </table>
    </td>
   
    
    </tr>
           
         <tr>
    <td>
    <table>
 
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
        </tr> 
        </table> 
        
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
