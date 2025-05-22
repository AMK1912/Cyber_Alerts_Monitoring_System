<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >
  <script language="javascript" type="text/javascript">
    <!--     
    window.history.forward(1);
    -->
  </script>
<head runat="server">
    <title>Sakhi </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" /> 
    <style type="text/css">
html 
{
    display: table;
    margin: auto;
    height:1200px;
}
    body
{
    margin: 0px;
	padding:0px;
	height:1200px;
	text-align:center;
	border: 0px solid gray;
	
}
.tblbdr{ 
padding-left:0px;
padding-top:5px;
padding-bottom:5px;
border: 1px solid  #999999;
border-radius: 7px;
border-collapse: separate;
}
 
.mySlides {display:none;}

 #marqueecontainer{
position: relative;
width: 300px; /*marquee width */
height: 1200px; /*marquee height */
/*background-color: white;
overflow: hidden;*/
border: 0px solid orange;
padding: 2px;
padding-left: 4px;
}
 #marqueecontainer1{
position: relative;
width: 300px; /*marquee width */
height: 1200px; /*marquee height */
/*background-color: white;
overflow: hidden;*/
border: 0px solid orange;
padding: 2px;
padding-left: 0px;
}
        .auto-style1 {
            height: 1px;
            width: 229px;
        }
        .auto-style2 {
            padding-left: 0px;
            padding-top: 5px;
            padding-bottom: 5px;
            border: 1px solid #999999;
            border-radius: 7px;
            border-collapse: separate;
            height: 30px;
            width: 229px;
        }
        .auto-style3 {
            height: 3px;
            width: 229px;
        }
        .auto-style4 {
            width: 326px;
        }
        .auto-style5 {
            height: 1px;
        }
    </style>  
<script type="text/javascript"> 
        function toUpper(txt) {
            document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
            return true;
        }   
</script> 
</head>
<body  style="background-image: url(images/bgn3.jpg); "      >
      <form id="form1" runat="server">
       <asp:ScriptManager runat="server" ID="Scrptmain"> </asp:ScriptManager>
      <div style="text-align: center;  " >
      <table style="">
      <tr>
      <td    valign="top"  >
      <div class="marqueecontainer">
        <marquee  height="1000px" direction="up"  scrolldelay="200" ><asp:Image id="Image18" runat="server" ImageUrl="~/user_img/x1.jpg" Width="300px" Height="210px"></asp:Image> <br />
<asp:Image id="Image19" runat="server" Width="300px" ImageUrl="~/user_img/x2.jpg" Height="210px"></asp:Image><br />
 <asp:Image id="Image20" ImageUrl="~/user_img/x3.jpg" runat="server" Width="300px" Height="210px"></asp:Image><br />
 <asp:Image id="Image21" runat="server" ImageUrl="~/user_img/x4.jpg" Width="300px" Height="210px"></asp:Image> <br />
<asp:Image id="Image22" runat="server" Width="300px" ImageUrl="~/user_img/x5.jpg" Height="210px"></asp:Image><br />
 <!--<asp:Image id="Image23" ImageUrl="~/user_img/x6.png" runat="server" Width="250px" Height="250px"></asp:Image><br />
 <asp:Image id="Image24" runat="server" ImageUrl="~/user_img/x7.jpg" Width="250px" Height="250px"></asp:Image> <br />
<asp:Image id="Image25" runat="server" Width="250px" ImageUrl="~/user_img/x8.jpg" Height="250px"></asp:Image><br />
 <asp:Image id="Image26" ImageUrl="~/user_img/x9.jpg" runat="server" Width="250px" Height="250px"></asp:Image><br />
  <asp:Image id="Image27" ImageUrl="~/user_img/x10.jpg" runat="server" Width="250px" Height="250px"></asp:Image>
  -->
  </marq
      </div></td>
      <td valign="top" style="width: 1003px;  ">
        <table width="1000px" border="0"   cellpadding="0" cellspacing="0" style="background-color:White; margin:0px; margin-top:0px; border:none 0px; padding:0px;  "  >
            
               <tr  style="background-color:White;" >
               <td colspan="3" valign="top" style="text-align: left">
                <table>
                <tr>
                <td align="left" rowspan="2">
                        &nbsp;</td>
                <td align="right" style="width: 900px; text-align: center; height: 65px;">
                    <br />
                </td>
               <td style="height: 65px">
                   &nbsp;
               </td>
                </tr>
                 
                </table>
               </td>
                </tr>
                
                <tr >
                <td colspan="3" style="font-stretch:wider; " class="auto-style5">
                  </td>
                </tr>
                 <tr  >
      
    <td style="height: 35px; " colspan="3" bgcolor="Navy"></td>
            </tr>
              <tr   >
              <td style="height: 35px;  " colspan="3"  >
                  </td>
            </tr>
             <tr   >
               <td  style="font-stretch:wider; height: 29px;" valign="top" colspan="3" >
               <table style="width: 993px">
               <tr valign="middle" style="font-stretch:wider; height: 29px;">
               <td colspan="2">
               
               </td>
               <td>
                    <table  style="background-color: Whitesmoke;"  >
                   
                      <tr>
                            <td colspan="2" style="  text-align:justify; " valign="top" class="auto-style4">
                           
                            
                     </td>
                    <td>
                    <table style="border:1px solid gray" >
                    <tr>
                    <td colspan="2" style="height:40px">
                        Login</td>
                    </tr>
                    <tr>
                     <td style="height: 30px; text-align: right;"  >
                     <asp:Label ID="Label23" runat="server" Text="SAIL PNO :" Font-Bold="False" Font-Names="Arial" Font-Size="12px" ForeColor="Navy"></asp:Label> &nbsp;
                    </td>
                       <td align="left" style="font-weight: bold; font-size: 12px; color: navy; font-family: Arial;  height: 30px;">
                       <asp:TextBox ID="txtusername" runat="server" onkeyup="return toUpper(this.id)"  Width="133px" MaxLength="7" TabIndex="1" AutoCompleteType="Disabled" oncopy="return false" onpaste="return false"></asp:TextBox>
                      </td>
                    </tr>
                    <tr>
                     <td style="height: 30px; text-align: right;"  >
                      <asp:Label ID="Label24" runat="server" Text="PASSWORD :" Font-Bold="False" Font-Names="Arial" Font-Size="12px" ForeColor="Navy"></asp:Label> &nbsp; 
                    </td> 
                    <td align="left" style="font-weight: bold; font-size: 12px; color: navy; font-family: Arial;  ">
                    <asp:TextBox ID="txtpassword" runat="server" Width="132px" TextMode="Password" TabIndex="2" MaxLength="20" OnTextChanged="btnlogin_Click" AutoCompleteType="Disabled" oncopy="return false" onpaste="return false"></asp:TextBox>
                   &nbsp;&nbsp;</td>
                    </tr>
                        <tr>
                            <td style="font-size: 12px; color: navy; font-family: Arial;" colspan="2" valign="top">
                           <asp:UpdatePanel ID="UP1" runat="server">
                                 <ContentTemplate>
                                     <table>
                                         <tr>
                                             <td style="width: 100px; " valign="top" align="right">
                                                 &nbsp;
                                             </td>
                                             <td style="width: 100px;" valign="top">
                                                 </td>
                                             <td  valign="top">
                                                 &nbsp;</td>
                                         </tr>
                                     </table>
                                 </ContentTemplate>
                             </asp:UpdatePanel>  </td>
                        </tr>
                        <tr>
                        <td colspan="2" valign="top">
                          <table>
                             <tr>
                             <td style="font-size: 12px; color: navy; font-family: Arial;                             ">
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             </tr>
                             </table>
                        </td>
                        </tr>
                    <tr>
                     <td style="height: 25px" >
                            </td>
                            <td align="left" style="font-weight: bold; font-size: 14px;   color: navy;
                                font-family: Arial">
                    <asp:Button ID="btnlogin" runat="server" BorderStyle="Outset" Font-Bold="False" Font-Names="Arial"
                        Font-Size="14px" ForeColor="DarkBlue" OnClick="btnlogin_Click"
                        Text="Sign In" Width="72px" TabIndex="3" Height="25px" /></td>
                    </tr>
                     <tr>
                        <td colspan="2"  align="center">
                        <asp:Label ID="message" runat="server" Text="Password for Executives: same as EPMS<br />Password for Non-executives: same as HRMS" Font-Italic="true" Width="329px" Font-Names="Arial" Font-Size="12px" ForeColor="Brown" Font-Bold="True" Height="28px"></asp:Label></td>
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
           
           
          <!--  <tr>
                <td colspan="3" style="height: 1px; text-align: left">
                  </td>
            </tr>-->
            <tr  >
                    <td colspan="3" style="width: 386px; height: 10px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman"
                        Font-Size="Small" ForeColor="Maroon" Text=" "  Width="257px"></asp:Label>
                    </td>
                </tr>
                <tr >
                    <td colspan="3" style="font-weight: bold; font-size: 14pt; width: 386px; color: red; font-style: italic; height: 14px" align="center"  >
                        &nbsp;</td>
                </tr>
          
  
    <script>
        var myIndex = 0;
        carousel();

        function carousel() {
            var i;
            var x = document.getElementsByClassName("mySlides");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            myIndex++;
            if (myIndex > x.length) { myIndex = 1 }
            x[myIndex - 1].style.display = "block";
            setTimeout(carousel, 2000); // Change image every 2 seconds
        }
    </script>
           
           </td>
            <td   align="center" valign="top" style="padding-left:10px;border:1px solid gray;">
           <table>
           <tr>
           <td class="auto-style1">
           
           </td>
           </tr>
           <tr align="center">
           <td class="auto-style2" style="background-color:DarkBlue; "   >
                 &nbsp;</td>
            
           </tr>
           <tr>
           <td class="auto-style3">
           
           </td>
           </tr>
           <tr  align="center">
            <td class="auto-style2" style="background-color:Indigo; " >
                 &nbsp;</td>
           </tr>
            <tr>
           <td class="auto-style3">
           
           </td>
           </tr>
           <tr  align="center"> 
            <td class="auto-style2" style="background-color:DarkBlue; " >
                  &nbsp;</td>
 
           </tr>
            <tr>
           <td class="auto-style3">
           
           </td>
           </tr>
           <tr  align="center"> 
            <td class="auto-style2" style="background-color:Indigo; " >
                  &nbsp;</td>
 
           </tr>
           </table>
           </td>
           <td>
               &nbsp;</td>
           </tr>
           <tr>
           <td  colspan="3" align="center" style="height: 121px"  >
           <table>
           <tr>
         
           <td valign="middle">
            &nbsp;  &nbsp;&nbsp;   &nbsp;&nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;    
            &nbsp;  &nbsp;&nbsp;    &nbsp;  &nbsp;&nbsp;    &nbsp;  &nbsp;&nbsp;     
            &nbsp;  &nbsp;&nbsp;    &nbsp;  &nbsp;&nbsp;    &nbsp;  &nbsp;&nbsp;     
           </td>
           </tr>
         
           </table>
          </td>
          
           </tr>  
           
        <tr  >
        <td colspan="3" align="center" style="height: 5px">
                        <asp:Label ID="Labely1" runat="server"  Font-Bold="True" Font-Names="Times New Roman" Text="Contact Us : For any suggestions/ issues contact us at sakhi.portal@sail.in"     Font-Size="14px" ForeColor="Maroon" Height="21px"
                Width="940px"></asp:Label>
          &nbsp;&nbsp;
        </td>
        </tr>  
            <tr  >
      
    <td style="height: 35px; " colspan="3" bgcolor="Navy">&nbsp;</td>
            </tr>
              
             </table>
      </td>
      </tr>
      </table>
         
        </div>
        
         
    </form>
</body>
</html>
