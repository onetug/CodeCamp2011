<%@ Page AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.Home" Language="C#" %>
<%@ Import Namespace="CodeCamp.ASP.UI.Infrastructure" %>
<%@ Import Namespace="CodeCamp.ASP.Web.UI" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
	<title>Orlando Code Camp Home page</title>
	<link href="/Styles/Site.css" rel="stylesheet" type="text/css" />
	<link href="/Styles/NewsCarouselStyle.css" rel="stylesheet" type="text/css" />
	<link href="/Styles/SponsorCarouselStyle.css" rel="stylesheet" type="text/css" />

	<script type="text/javascript" src="/Scripts/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="/Scripts/jquery.jcarousel.min.js"></script>
	<script type="text/javascript" src="http://twitstat.us/twitstat.us-min.js"></script> 
	<script src="/Scripts/occ.home.aspx.js" type="text/javascript"></script>
	<script type="text/javascript">

		var _gaq = _gaq || [];
		_gaq.push(['_setAccount', 'UA-10607215-2']);
		_gaq.push(['_trackPageview']);

		(function () {
			var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
			ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
			var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
		})();

</script>
</head>
<body style="border: 0; padding: 0; background: #003d5e url(images/bg.gif) repeat-x; color: #333;">
	<form id="Form1" runat="server">
	<div class="page">
		<div class="title">
			<a href="<%=SeminoleStateCollegeUrl%>" target="_blank">
			<img src="Images/bing.png" alt="Map to Seminole State College" 
				style= "margin-top: -5px; margin-left: 700px; margin-right: 10px; margin-bottom: 0px; height: 37px; width: 52px; border: none; float:left;" />
			</a>
			<asp:Label ID="EventDateLabel" Font-Bold="true" ForeColor="Black" runat="server" style="padding-right:10px; float:right;">March 26, 2011<br />Seminole State College</asp:Label>
			<a style="text-align:left; font-weight:bold;font-size:16px; margin-left: 395px; margin-top: 70px; float:left; height: 24px; width: 370px;"><asp:Label ID="ErrorLabel" runat="server" ForeColor="OrangeRed" Width="370px" Visible="False"
					Text="Filler Text"/></a>
			
			<a style="margin-top: 15px;	margin-right: 10px; float: right; height: 80px; width: 169px;" href="RegisterView.aspx"></a>
			<asp:Panel ID="LoginPanel" runat="server" 
				Style="float:left;padding:0px;margin: 20px,10px,0px,10px;" Width="824px">
					<asp:Label ID="EmailLabel" runat="server" Text="Email: " Style="font-weight:bold; margin:5px,5px,5px,5px;padding-top:10px; padding-left:0px;" ForeColor="White"/>
					<asp:TextBox ID="email" runat="server" Width="220px" Style=" font-family:Verdana; font-size:12;" />
					<asp:Label ID="PasswordLabel" runat="server" Text="Password: " Style="font-weight:bold;margin:5px,5px,5px,5px;padding:10px" ForeColor="White"/>
					<asp:TextBox ID="pass" runat="server" Width="180px" TextMode="Password"/>
					<asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" Width="60px" Height="30px" 
						Style="font-weight:bold;font-size:12;margin:40px,20px,40px,0px;"/>
					<asp:Button ID="ResetPassword" runat="server" OnClick="ResetPassword_Click" 
						Text="Reset Password" Width="140px" Height="30px" 
						Style="font-weight:bold;font-size:12; margin-left:10px; margin:40px,20px,10px,10px;"/>
					<a href="<%=SeminoleStateCollegeUrl%>" target="_blank"> &nbsp;</a>
				</asp:Panel>   
				<a href="Images/OCC2011.Current.Agenda.pdf" target="_blank"
					onclick="window.open(this.href,'','width=1000,height=700');return false">
					<img src="Images/Agenda.png" alt="Current Code Camp Agenda" 
						 style="border-style:none; border-color:inherit; border-width:medium; 
								text-decoration:none;height:40px; width:112px;
								margin-top:-3px; margin-left:5px; margin-right:15px; margin-bottom:0px;"/>
				</a>
		  </div>
			<div id="MainArea" style=" background-color:#003D5E">

				<div id="Content" style="padding-left:0px; padding-right:5px; padding-bottom:10px;padding-top:0px;">
					<table>
						<tr>
							<td style=" vertical-align:top">
								<div style="height: 210px; margin-top: 10px; width: 215px;">
									<div id="Sponsors" style="padding-left:0px; padding-right:5px; padding-bottom:10px;padding-top:0px; height: 287px; margin-top: 0px;">
										<ul id="platinumSponsorCarousel" class="jcarousel-skin-tango">

													<%
													var imageUri = string.Empty;

													CodeCampUnityContainer.Container
														.Resolve<ICacheService>()
														.FindSponsors()
														.OrderBy(s => s.SponsorshipLevelId)
														.ToList()
														.ForEach(n =>
														{
															imageUri = n.Image;
														%>
															<li> <a href="<% Response.Write(n.Url); %>" target="_blank">
																	<img alt="" src="<%Response.Write(imageUri);%>" style="height: 140px; width: 140px" />
																</a>
															</li>
																<%
														}); 
														%>                                                                     																															 
										</ul>
									</div>
								</div>

								<div style="margin-top:0px; margin-left:-5px;">
									<a id="contribAnchor" 
										style="text-decoration: none;" 
										href="http://social.zune.net/redirect?type=phoneApp&id=fd103846-d34f-e011-854c-00237de2db9e" target="_blank"
										onclick="window.open(this.href,'','width=1000,height=700');return false">
										<img src="Images/otloveswp7.png" alt="" style="height:100px; width:198px" />
									</a>
								</div>

							<td align="left">
								<div id="SessionArea">                        
									<ul id="newsCarousel" class="newsCarousel-container-vertical" style="clear: both">
												<%
													var presentationItem = string.Empty;
																						
													CodeCampUnityContainer.Container
														.Resolve<ICacheService>()
														.FindSessions()
														.Where(s => s.SessionType == "Regular Session")
														.OrderBy(s => s.EventPresentation.Presentation.PresentationSpeakers.FirstOrDefault().Person.Name).ThenBy(s => s.Name)
														.ToList()
														.ForEach(n =>
																	 {
																		 presentationItem =
																			 string.Format("{0}<hr /><h2>{1}: {2}</h2><br/>{3}", 
																			 n.Track.Name,
																			 n.EventPresentation.Presentation.PresentationSpeakers.FirstOrDefault().Person.Name,
																			 n.EventPresentation.Presentation.Name,
																			 n.EventPresentation.Presentation.Description);
																	%>  
																	<li><%Response.Write(presentationItem);%></li>                 
																		<%
														});
														%>
									</ul>
								</div>
							</td>
						</tr>
						</table>
				</div>
				<table >
					<tr>
						<td>
							<div class="twitstatus_badge_container" id="twitstat_badge_350" 
								style=" width:450px; 
										margin-left:inherit; 
										border-color:Black; 
										border-width:5px; 
										padding-left:20px; 
										padding-right:20px; 
										padding-bottom:20px; 
										padding-top:20px;
										background-color:#ed8225;
										margin-bottom:25px;
										margin-top:-20px" >
							</div>
						</td>
						<td valign="top">
							<div id="Announcements" style=" margin-left:12px; width:439px;">                        
								<ul id="aCarousel" class="aCarousel-container-vertical">
									<li> <img alt="" src="Images/CodeCamp.gif" 
											style=" vertical-align:bottom; height:80px; width:212px"  />
										<br /> 
										Welcome to our 6th Annual Orlando Code Camp.
										<hr />
									</li>
										<%  CodeCampUnityContainer.Container
											.Resolve<ICacheService>()
											.FindAllAnnouncements()
											.ToList()
											.ForEach( n =>     
											{ var newsItem = string.Format("<b>{0}</b><br/>{1}", n.Title, n.Text);
									%>  
									<li><%Response.Write(newsItem);%></li>                 
										<%
											});
									%>  <%--                                  <%
										var cacheService = CodeCampUnityContainer.Container.Resolve<ICacheService>();
										
										var volunteerTaskCount = cacheService.FindSessions()
																			 .Where(s => s.Status != "Unfulfilled")
																			 .Where(s => s.SessionType == "Volunteer")
																			 .Count();
										if (volunteerTaskCount > 0)
										{
											cacheService.FindSessions()
														.ToList()
														.ForEach( n =>
																	  {
																		  var volunteerItem =
																			  string.Format(
																				  "<b>Volunteer Opportunity: {0}</b>{1}<b>Start Time:{2}:{3}</b>{1}<b>End Time:{4}:{5}</b>",
																				  n.Name,
																				  Environment.NewLine,
																				  n.StartTime.Hour,
																				  n.StartTime.Minute,
																				  n.EndTime.Hour,
																				  n.EndTime.Minute
																				  );
									%>
									<li> <b>We still have some volunteer opportunities available - please sign up!</b></li>                 
									<li> <%Response.Write(volunteerItem);%></li>                 
									<%
											});
									%>--%>
									<li> 
										<b>Lou of Orlando</b>
										<hr />
										We are welcoming Lou Tommasini to cater this year's Code Camp.  If you attended our "Day of Windows Phone 7 Developer Day", you'll remember Lou's food!
										<hr />
										<img alt="" src="Images/LouOfOrlando.png" 
											style="vertical-align:bottom; height:100px; width:300px"  />
										<img alt="" src="Images/Yum.jpg" 
											style="vertical-align:bottom; height:100px; width:120px"  />
										<br />
									   <hr />
									   You can find out more about Lou's offerings on <a href="http://www.facebook.com/pages/Lou-of-Orlando-Inc/184180644940949" target="_blank" style="text-decoration:underline;">Facebook</a>,
																					  <a href="http://www.thumbtack.com/louoforlando"  style="text-decoration:underline;" target="_blank">Thumbtack</a> has some of his recipes, and last but not least, 
																					  <a href="http://www.twitter.com/louoforlando" target="_blank" style="text-decoration:underline;" >Twitter</a>
									</li>
								</ul>
							</div>
							<div style="padding-left:5px; margin-top:-5px;">
								<a id="ContactUsLink" href="mailto:codecamp@onetug.org?subject=Questions about this years Orlando Code Camp">
									<img alt="" src="Images/ContactUs.png" style="height:255px; width:450px;"/>
								</a>
							</div>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</form>
	</body>
	</html>
