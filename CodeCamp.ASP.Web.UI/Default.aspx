<%@ Page Title="Orlando Code Camp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CodeCamp.ASP.Web.UI._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
    #twitstat_badge_361
    {
        /*height: 284px;
        margin-left: 633px;
        margin-top: 55px;
        width: 275px;*/
    }
    </style>
    <script type="text/javascript" src="http://twitstat.us/twitstat.us-min.js"></script>
    <script type="text/javascript">
        twitstat.badge.init({
            badge_container: "twitstat_badge_361",
            title: "Twitter Search",
            keywords: "#OrlandoCC",
            max: 10,
            border_color: "#434343",
            header_background: "#434343",
            header_font_color: "#ffffff",
            content_background_color: "#e1e1e1",
            content_font_color: "#333333",
            link_color: "#307ace",
            width: 370
        });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" Style="width:auto; height:auto">
        <div id="MainArea">
            <div class="leftCol" id="Sponsors">
                <div id="DiamondAndPlatinum" style="height:200px; border-bottom: 3px solid Black;">
                    <img src="Images/Sponsors/microsoft.gif" alt="Diamond and Platinum Sponsors" />
                </div>
                <div id="RemainingSponsors" style="margin: 0px; width: 200px; height: 100px; padding: 0px;">
                    <img src="Images/Sponsors/SeminoleStateCollege.jpg" alt="Sponsors" style="margin:0px; width: 200px; height: 100px;" />
                </div>
            </div>
            <div id="Content" style="float:left; padding:10px,0px,0px,0px; width:700px;background-color: White; border-left: 1px solid Black; border-bottom: 1px solid Black; min-height: 500px;">
                 <h2>
                    We&#39;re busy getting the new site ready. Please keep checking back!
                 </h2>
                 <div class="twitstatus_badge_container" id="twitstat_badge_361" style="float:left; margin-left:370px;border-color:Orange; border-width:5px;"></div>
            </div>
            

        </div>


</asp:Content>
