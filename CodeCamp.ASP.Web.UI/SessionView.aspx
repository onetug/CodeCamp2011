<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SessionView.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.SessionView" %>
<%@ Import Namespace="CodeCamp.ASP.UI.Infrastructure" %>
<%@ Import Namespace="CodeCamp.ASP.Web.UI" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Presentation Depot</title>
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
			<a style=" margin-top: 0px; margin-left: 0px; height: 100px; width: 386px; float: left" href="Home.aspx" ></a>

            <asp:Label runat="server" ID="UploadStatusLabel" ForeColor="White" 
                style="margin-top:150px; margin-left:3px; margin-right:5px; padding-right:5px; float:right;" 
                Width="449px" />
            <asp:Label Text="Presenter Content Depot" runat="server" Font-Bold="True" 
                ForeColor="White" Font-Size="Large" 
                style="margin-top:10px; margin-left:0px; padding-right:40px; float:left;" 
                Width="227px"/>

        </div>
        <div>
<%--                <ol style=" font-size:medium;  color:White">
                    <li>Find your session in the list.</li>
                    <li>Click the Browse button to locate your compressed (.zip) content, </br> slides and/or source code.</li>
                    <li>Then click Upload to store them.</li>
                </ol>--%>
                

		    <asp:Panel ID="Panel1" runat="server">
			    <asp:GridView ID="SessionUploadGridView" runat="server" 
				    AutoGenerateColumns="False" OnRowCommand="SessionUploadGridView_RowCommand" 
				    DataKeyNames="Id" DataSourceID="OccRiaService" Width="945px" 
                    AllowSorting="true"
				    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    AllowPaging="True" 
                    emptydatatext="No data available." Font-Size="Medium">
				    <AlternatingRowStyle BackColor="#CCCCCC" ForeColor="#284775" />
				    <Columns>
					    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
						    SortExpression="Id" Visible="False" />
					    <asp:BoundField DataField="EventPresentation_Id" 
						    HeaderText="EventPresentation_Id" SortExpression="EventPresentation_Id" 
						    Visible="False" />
					
                        <asp:BoundField DataField="Name" 
                                        HeaderText="Session Name" 
                                        ItemStyle-Wrap="true" 
                                        SortExpression="Name"
                                        HeaderStyle-HorizontalAlign="Left" >

                        <ItemStyle Width="475px" Wrap="True" />
                        </asp:BoundField>

                        <%--SortExpression="Person.Name" --%>
                        <asp:TemplateField HeaderText="Speaker" HeaderStyle-HorizontalAlign="Left">                  
                          <ItemTemplate>
                            <%# GetPresenterName(Container.DataItem) %>
                          </ItemTemplate>
                        </asp:TemplateField>


			<%--		    <asp:BoundField DataField="Description" HeaderText="Description" 
						    SortExpression="Description" Visible="False" />
					    <asp:BoundField DataField="TrackId" HeaderText="TrackId" 
						    SortExpression="TrackId" Visible="False" />
					    <asp:BoundField DataField="StartTime" HeaderText="StartTime" 
						    SortExpression="StartTime" Visible="False" />
					    <asp:BoundField DataField="EndTime" HeaderText="EndTime" 
						    SortExpression="EndTime" Visible="False" />
					    <asp:BoundField DataField="SessionType" HeaderText="SessionType" 
						    SortExpression="SessionType" Visible="False" />
					    <asp:BoundField DataField="RoomId" HeaderText="RoomId" SortExpression="RoomId" 
						    Visible="False" />
					    <asp:BoundField DataField="MaxCapacity" HeaderText="MaxCapacity" 
						    SortExpression="MaxCapacity" Visible="False" />
					    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" 
						    Visible="False" />--%>
					    <asp:TemplateField HeaderText="">
					        <ItemTemplate>
						        <asp:FileUpload ID="SessionUpload" runat="server" ToolTip="Please use zipped files for the upload." />
						        <asp:Button ID="bt_upload" runat="server" EnableViewState="False" 
							        Text="Upload" CommandName="Upload"/>
                                <asp:Label runat="server" ID="UploadStatusLabel" />
					        </ItemTemplate>
				    </asp:TemplateField>
				    </Columns>
				    <EditRowStyle BackColor="#999999" />
				    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
				    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
				    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
				    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
				    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
				    <SortedAscendingCellStyle BackColor="#E9E7E2" />
				    <SortedAscendingHeaderStyle BackColor="#506C8C" />
				    <SortedDescendingCellStyle BackColor="#FFFDF8" />
				    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                         <pagersettings mode="Numeric" 
                          position="Bottom"           
                          pagebuttoncount="12"/>

			    </asp:GridView>
			    <asp:DomainDataSource ID="OccRiaService" runat="server"
				    DomainServiceTypeName="CodeCamp.RIA.Data.Web.CodeCampDomainService" 
				    QueryName="GetPresenterSessionsByEvent">
				    <QueryParameters>
					    <asp:Parameter DefaultValue="2" Name="eventId" Type="Int32" />
				    </QueryParameters>
			    </asp:DomainDataSource>
			    <br />
		    </asp:Panel>
	    </div>
    </div>	


</form>
</body>
</html>