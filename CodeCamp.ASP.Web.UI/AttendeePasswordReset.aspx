<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendeePasswordReset.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.AttendeePasswordReset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Attendeed Password Reset</title>
	<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="border: 0; padding: 0; background: #003d5e url(images/bg.gif) repeat-x; color: #333;">
   <form id="form1" runat="server">
	<div class="resetPage">
		<div class="titleNoRegister">
			<a href="<%=SeminoleStateCollegeUrl%>" target="_blank">
			<img src="Images/bing.png" alt="Map to Seminole State College" 
				style= "margin-top: -5px; margin-left: 700px; margin-right: 10px; margin-bottom: 0px; height: 37px; width: 52px; border: none; float:left;" />
			</a>
			<asp:Label ID="EventDateLabel" Font-Bold="true" ForeColor="Black" runat="server" style="padding-right:10px; float:right;">March 26, 2011<br />Seminole State College</asp:Label>
			<a style=" margin-top: 0px; margin-left: 0px; height: 100px; width: 386px; float: left" 
					href="Home.aspx" ></a>
		</div>
		<div class="resetMainContent">
			<h6>Please enter your email address login and click the <b>Reset</b> button.  
			<br />We will send you an email which contains a temporary password.</h6>
			<table style="margin-top: -15px; padding-left:4px;">
			<tr>
			<!--<td><input ID="tbParamB" type="text" style="margin-left:10px; padding-left:10px;"></td>-->
			<!-- input type="text" style="margin-left:10px; padding-left:10px; width:270px;" -->
			<!-- <input ID="Text1" type="text" style="margin-left:10px; padding-left:10px;"-->
			<!-- <button onclick="Done()" type="button" style="width: 80px">OK</button> -->
				<td style="color:Orange; font-weight: bolder;">Email Address:</td>
				<td><asp:Textbox ID="EmailAddress" runat="server" Width="280" Font-Names="Verdana" ForeColor="Black" Font-Bold="false" /></td>
				<td align="right">
					<asp:Button ID="Button1" Text="Reset" runat="server" OnClick="OkToResetPw_Click" 
						Font-Names="Tahoma,Verdana" Font-Size="Medium" Width="71px" />
				</td>
			</tr>    
			
					   
			</table>

			<asp:RegularExpressionValidator ID="EmailRegExValidator" runat="server" 
				ControlToValidate="EmailAddress" ForeColor="OrangeRed" Font-Names="Verdana"
				ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">The email address you entered isn&#39;t valid.</asp:RegularExpressionValidator>
			<br />
			<asp:Label ID="MissingAttendee" runat="server" ForeColor=Red Font-Names="Verdana"></asp:Label>
			<br />
			<%--<asp:CompareValidator ID="PassCompareValidator" runat="server" ForeColor=Red Font-Names="Verdana"
				ErrorMessage="Passwords do not match" ControlToCompare="Password" 
				ControlToValidate="ConfirmedPassword"></asp:CompareValidator>--%>
		</div>

	 </div>
	</form>
</body>
</html>
