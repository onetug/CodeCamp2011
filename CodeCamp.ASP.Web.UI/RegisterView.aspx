<%@ Page Title="Register" Language="C#"  AutoEventWireup="true" CodeBehind="RegisterView.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.RegisterView" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Orlando Code Camp 2011</title>
    <%--<style type="text/css">

        .displayMargins
        {
            padding-left:5px;
        }

        .inputDisplay
        {
           font-family:Segoe UI Semibold;    
           padding-left:5px;
        }
        
        .labelDisplay
        {
          color:White;
          padding-left:5px;
          font-family:Segoe UI Semibold;
        }
    
        </style>--%>
    <link href="~/Styles/Site.css" rel="Stylesheet" type="text/css" />  
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>  
    <script type="text/javascript">
        window.onload = function () {
            $("#EmailAddress1").focus();
        }
    </script>

</head>
<body style="background: #003d5e url(images/bg.gif) repeat-x; color: #333;font-size:14px;">    

    <form id="Form1" runat="server">
        <div class ="page">
            <div class="titleNoRegister">
            <a style=" margin-top: 0px; margin-left: 0px; height: 173px; width: 386px; float: left" 
                    href="Home.aspx" ></a>
                <asp:Label ID="EventDateLabel" Font-Bold="true" ForeColor="Black" runat="server" style="padding-right:10px; float: right">March 26, 2011 <br /> Seminole State College</asp:Label>
                <a href="http://www.bing.com/maps/explore/?org=aj&FORM=Z9LH9#5003/0.6002=bid:YN183x162104803:adj:0&1.6002=q:Seminole+State+College,+100+Weldon+BLVD,+Sanford,+FL:nelat:28.7808006872274:nelong:-81.2382995709229:swlat:28.7080173127726:swlong:-81.3727104290772:nosp:0:adj:0:notr:0:pg:1/5872/style=auto&lat=28.74445&lon=-81.305654&z=16&pid=5874">
                <img src="Images/bing.png" alt="Map to Seminole State College" 
                    style= "margin-top: -5px; float:none; margin-right:10px; margin-bottom: 0px; height: 37px; width: 52px; border: none;" /></a>
            </div>
        <div style="background-color: #b6b7bc; padding-top: 10px; padding-bottom: 10px;float: middle">
            <%--<table style="margin-left:auto; margin-right:auto;"cellspacing="5" align="center"> 
                <tr >
                    <td class="labelDisplay">
                        <asp:Label ID="EmailLabel" Text="Email Address" 
                                   runat="server" AssociatedControlID="EmailAddress1" /> 
                        <asp:Label ID="Asterisk1" runat="server" Text="*" ForeColor="Red" />
                    </td>
                    <td class="labelDisplay">
                        <asp:Label ID="ConfirmEmailLabel" Text="Confirm Email" 
                                   runat="server" AssociatedControlID="EmailAddress2"/>
                        <asp:Label ID="Asterisk2" runat="server" Text="*" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="inputDisplay" >
                        <asp:TextBox ID="EmailAddress1" runat="server" Width="300" TabIndex="1" />
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorEmail1" runat="server"
                                                    ControlToValidate="EmailAddress1"
                                                    ErrorMessage="Email Address is required."
                                                    ForeColor="Red"
                                                     SetFocusOnError="True" Text="*"
                                                     InitialValue="" Display="Static"/>
                        <asp:CompareValidator ID="CompareValidatorEmail1" runat="server"
                                              ControlToCompare="EmailAddress2" ErrorMessage="Email Addresses must match."
                                              ForeColor="Red"  ControlToValidate="EmailAddress1"
                                              Text="*" InitialValue="" Display="Static"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail1" runat="server" 
                                                        ControlToValidate="EmailAddress1" ErrorMessage="Invalid Email Address" 
                                                        ValidationExpression="([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)"
                                                         Display="Static" ForeColor="Red" Text="*" />
                        <!-- "^(([\w-]+\.)+[\w-]+|([a-zA-Z0-9_.-]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$" -->
                    </td>
                    <td class="inputDisplay">
                        <asp:TextBox ID="EmailAddress2" runat="server" Width="300" TabIndex="2" />
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorEmail2" runat="server"
                                                    ControlToValidate="EmailAddress2"
                                                    ErrorMessage="Email Address is required."
                                                    ForeColor="Red" SetFocusOnError="True" Text="*"
                                                    InitialValue="" Display="Static"/>
                        <asp:CompareValidator ID="CompareFieldValidatorEmail2" runat="server"
                                              ControlToCompare="EmailAddress1" ErrorMessage="Email Addresses must match."
                                              ForeColor="Red"  ControlToValidate="EmailAddress2" Text="*"
                                              InitialValue="" Display="Static"
                                              SetFocusOnError="True"/>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail2" runat="server" 
                                                        ControlToValidate="EmailAddress2" ErrorMessage="Invalid Email Address" 
                                                        ValidationExpression="([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)"
                                                         Display="Static" ForeColor="Red" Text="*" />
                                                         <!-- "(?<name>\S+)@(?<domain>\S+)" -->
                                                         <!-- "([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)" -->
                    </td>
                </tr>
                <tr>
                    <td class="labelDisplay">
                        <asp:Label ID="FirstNameLabel" Text="First Name" runat="server" AssociatedControlID="FirstName" />
                        <asp:Label ID="Asterisk3" runat="server" Text="*" ForeColor="Red" />
                    </td>
                    <td class="labelDisplay">
                        <asp:Label ID="LastNameLabel" Text="Last Name" runat="server" AssociatedControlID="LastName" />
                        <asp:Label ID="Asterisk4" runat="server" Text="*" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="inputDisplay">
                        <asp:TextBox ID="FirstName" runat="server" Width="300" TabIndex="3" />
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorFirstName" runat="server"
                            ControlToValidate="FirstName"
                            ErrorMessage="First Name is required."
                            ForeColor="Red" Font-Size="X-Small" 
                            SetFocusOnError="True" Text="*"
                            InitialValue="" Display="Dynamic"/>
                    </td>
                    <td class="inputDisplay">
                        <asp:TextBox ID="LastName" runat="server" Width="300" TabIndex="4" />
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorLastName" runat="server"
                            ControlToValidate="LastName"
                            ErrorMessage="Last Name is required."
                            ForeColor="Red" SetFocusOnError="True" Text="*"
                            InitialValue="" Display="Dynamic"/>
                    </td>
                </tr>
                <tr >
                    <td class="labelDisplay"><asp:Label ID="TitleLabel" Text="Title" runat="server" AssociatedControlID="JobTitle" /></td>
                    <td class="labelDisplay"><asp:Label ID="WebSiteLabel" Text="Web Site" runat="server" AssociatedControlID="WebSite" /></td>
                </tr>
                <tr>
                    <td class="inputDisplay"><asp:TextBox ID="JobTitle" runat="server" Width="300" TabIndex="5" /></td>
                    <td class="iunputDisplay"><asp:TextBox ID="WebSite" runat="server" Width="300" TabIndex="6" /></td>
                </tr>

                <tr>
                    <td class="labelDisplay">
                        <asp:Label ID="Password1Label" Text="Password" runat="server" AssociatedControlID="Password1" />
                        <asp:Label ID="Asterisk5" runat="server" Text="*" ForeColor="Red" />
                    </td>
                    <td class="labelDisplay">
                        <asp:Label ID="Password2Label" Text="Confirm Password" runat="server" AssociatedControlID="Password2" />
                        <asp:Label ID="Asterisk6" runat="server" Text="*" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td class="inputDisplay">
                        <asp:TextBox ID="Password1" runat="server" Width="300" TextMode="Password" TabIndex="9" />
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorPassword1" runat="server"
                                                    ControlToValidate="Password1"
                                                    ErrorMessage="Password (1) is required."
                                                    ForeColor="Red" SetFocusOnError="True" Text="*"
                                                    InitialValue="" Display="Static"/>
                        <asp:CompareValidator ID="CompareValidatorPassword1" runat="server"
                                              ControlToCompare="Password2" ErrorMessage="Passwords must match."
                                              ForeColor="Red" ControlToValidate="Password1" Font-Size="X-Small"
                                              Text="*"
                                              InitialValue="" Display="Static"/>
                    </td>

                    <td class="inputDisplay">
                        <asp:TextBox ID="Password2" runat="server" Width="300" TextMode="Password" TabIndex="10"/>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorPassword2" runat="server"
                                            ControlToValidate="Password2"
                                            ErrorMessage="Password (2) is required."
                                            ForeColor="Red" SetFocusOnError="True" InitialValue="" Display="Static" Text="*"/>
                        <asp:CompareValidator ID="CompareValidatorPassword2" runat="server"
                                              ControlToCompare="Password1" ErrorMessage="Passwords must match."
                                              ForeColor="Red" ControlToValidate="Password2" Text="*"
                                              SetFocusOnError="True" InitialValue="" Display="Static"/>
                    </td>
                </tr>
                <tr>
                    <td class="labelDisplay"><asp:Label ID="TwitterLabel" Text="Twitter Tag" runat="server" AssociatedControlID="Twitter" Font-Names="Segoe UI Semibold" /></td>
                    <td class="labelDisplay"><asp:Label ID="BlogLabel" Text="Blog" runat="server" AssociatedControlID="Blog" /></td>
                </tr>
                <tr>
                    <td class="inputDisplay"><asp:TextBox ID="Twitter" runat="server" Width="300" TabIndex="11" /></td>
                    <td class="inputDisplay"><asp:TextBox ID="Blog" runat="server" Width="300" TabIndex="12" /></td>
                </tr>
                <tr>
                    <td class="labelDisplay" >
                        <asp:label ID="ImageUrlLabel" Text="Image Url" runat="server" Width="300" />
                    </td>
                    <td class="labelDisplay" >
                       <asp:Panel ID="RegisterPanel" runat="server" Visible="false">
                            <asp:Label ID="LabelRegistered" runat="server" ForeColor="DarkGreen" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="inputDisplay">
                        <asp:TextBox ID="ImageUrl" runat="server" Width="300px" TabIndex="13" />
                    </td>
                    <td class="labelDisplay">
                        <asp:Panel ID="ErrorPanel" runat="server" Visible="false">
                            <asp:Label ID="LabelError" runat="server" ForeColor="Red" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="labelDisplay"><asp:Label ID="BioLabel" Text="Bio" runat="server" AssociatedControlID="Bio" /></td>
                </tr>
                <tr>
                    <td colspan="2" class="inputDisplay">
                        <asp:TextBox ID="Bio" runat="server" Width="630px" Height="80px" 
                            TextMode="MultiLine" TabIndex="14" Font-Names="Segoe UI Semibold"/></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" class="inputDisplay">
                        <asp:Button ID="Save" Text="Register" runat="server" CausesValidation="true" UseSubmitBehavior="true" 
                            Width="80" onclick="Save_Click" TabIndex="14" Font-Names="Segoe UI Semibold"/>
                    </td>
                </tr>
                <tr Height="0">
                    <td colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" 
                                            DisplayMode="BulletList" ForeColor="Red" HeaderText="You need to correct the following:" 
                                            ShowMessageBox="True" ShowSummary="true" />
                    </td>
                </tr>
            </table> --%>       		
        Registration is now closed, please come back again next year!!

        </div>
        </div>
    </form>
    </body>
    </html>

