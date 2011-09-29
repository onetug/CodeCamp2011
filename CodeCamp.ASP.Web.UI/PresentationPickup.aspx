<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PresentationPickup.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.PresentationPickup" %>
<%@ Import Namespace="CodeCamp.RIA.Data.Web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Presentation Depot</title>
	<link href="Styles/Site.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .grid
        {
            color: #333333;
        }

        .grid-row
        {
            background-color: #F7F6F3;
        }

        .grid-row-alternating
        {
            background-color: #D1DDF1;
        }

        .grid-selected-row
        {
            color: #333333;
            background-color: #D1DDF1;
            font-weight: bold;
        }

        .grid-header, .grid-footer
        {
            color: White;
            background-color: #507CD1;
            font-weight: bold;
        }

        .grid-pager
        {
            color: White;
            background-color: #2461BF;
            text-align: center;
        }

        .grid-row-edit
        {
            background-color: #2461BF;
        }
    </style>
</head>
<body style="border: 0; padding: 0; background: #003d5e url(images/bg.gif) repeat-x; color: #333;">
   <form id="form2" runat="server">
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
            <asp:Label ID="Label1" Text="Presenter Content Depot" runat="server" Font-Bold="True" 
                ForeColor="White" Font-Size="Large" 
                style="margin-top:10px; margin-left:0px; padding-right:40px; float:left;" 
                Width="227px"/>

        </div>
        <div>
		    <asp:Panel ID="Panel1" runat="server">
			    
			    <br />
			    <asp:GridView ID="SessionPickupGridView" runat="server" 
				    AutoGenerateColumns="False" 
				    DataKeyNames="Id" DataSourceID="OccRiaService" Width="945px" 
				    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    AllowPaging="True" 
                    emptydatatext="No data available." AllowSorting="True">
				    <AlternatingRowStyle BackColor="#CCCCCC" ForeColor="#284775" />
				    <Columns>
						
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            ItemStyle-Wrap="true" SortExpression="Description"  >

                        <ItemStyle Wrap="True" />
                        </asp:BoundField>

                        <asp:BoundField DataField="StartTime" 
                                        HeaderText="StartTime" 
                                        ItemStyle-HorizontalAlign="Right" 
                                        HeaderStyle-HorizontalAlign="Center"
                                        DataFormatString="{0:h:mm:tt}"
                                        SortExpression="StartTime">

                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        </asp:BoundField>

                        <asp:TemplateField  HeaderText="Track Name" SortExpression="TrackId">
                            <ItemTemplate>
                                <%# Eval("Track.Name") %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="145px" />
                            <ItemStyle HorizontalAlign="Left" />
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

                         <pagersettings mode="NumericFirstLast"
                              position="Bottom"           
                              pagebuttoncount="12"/>

                           <pagerstyle backcolor="SteelBlue"
                              height="30px"
                              verticalalign="Bottom"
                              horizontalalign="Center"/>

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