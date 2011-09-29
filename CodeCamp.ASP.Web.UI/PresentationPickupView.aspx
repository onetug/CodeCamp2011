<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PresentationPickupView.aspx.cs" Inherits="CodeCamp.ASP.Web.UI.PresentationPickupView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Presentation Pickup</title>
	<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body style="border: 0; padding: 0; background: #003d5e url(images/bg.gif) repeat-x; color: #333;">
   <form id="form1" runat="server">
	<div class="resetPage">
		<div class="titleNoRegister">
            <asp:Label ID="Label1" Text="Presentation Downloads" runat="server" Font-Bold="True" 
                ForeColor="White" Font-Size="Large"
                style="margin-top:140px; margin-left:0px; padding-left:0px; padding-right:10px; float:left; " 
                Width="223px"/>

        </div>
        <div>          

		    <asp:Panel ID="Panel1" runat="server" >
			    <asp:GridView ID="SessionDownloadGridView" runat="server" 
				    AutoGenerateColumns="False" OnRowCommand="SessionDownloadGridView_RowCommand" 
				    DataKeyNames="Id" DataSourceID="OccRiaService" Width="940px" 
                    AllowSorting="true"
				    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    AllowPaging="True" 
                    emptydatatext="No data available." Font-Size="Medium" 
                    style="margin-right: 0px; margin-left:10px;">
				    <AlternatingRowStyle BackColor="#CCCCCC" ForeColor="#284775" />
				    <Columns>
					    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
						    SortExpression="Id" Visible="False" />
					    <asp:BoundField DataField="EventPresentation_Id" 
						    HeaderText="EventPresentation_Id" SortExpression="EventPresentation_Id" 
						    Visible="False" />

                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Presentation"
                                ItemStyle-Wrap="true" SortExpression="Name">
                            <ItemTemplate>                                
                                <asp:HyperLink 
                                Text='<%# ((CodeCamp.RIA.Data.Web.Session)Container.DataItem).Name%>'
                                runat="server" 
                                NavigateUrl="<%# GetPresentionNavigationLink(Container.DataItem) %>" Target="_blank"/>
                            </ItemTemplate>
                        </asp:TemplateField>
					
                        <asp:TemplateField HeaderText="Speaker" HeaderStyle-HorizontalAlign="Left">                  
                          <ItemTemplate>
                            <%# GetPresenterName(Container.DataItem) %>
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
				    QueryName="GetExistingPresentionsByEvent">
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
