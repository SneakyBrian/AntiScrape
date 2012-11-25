<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AntiScrape.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        This is the AntiScrape Demo Page
    </h2>
    <p>
        AntiScrape is an IIS ASP.NET Http Module to help in the fight against website scrapers!
    </p>
    <p>
        To learn more about AntiScrape visit <a href="https://github.com/SneakyBrian/AntiScrape" title="AntiScrape on Github">AntiScrape on Github</a>.
    </p>
    
    <h3>
        Here is the list of currently detected scrapers:
    </h3>

    <table>
        <thead>
            <tr>
                <th>Partial IP</th>
                <th>User Agent</th>
                <th>Timestamp</th>
            </tr>
        </thead>
        <tbody>
        <%  foreach (var item in new AntiScrape.DataStorage.SQLDataStorage().GetScrapers())
            {
                Response.Write(string.Format("<tr><td>...{0}</td><td>{1}</td><td>{2}</td></tr>", item.IP.Substring(item.IP.Length / 2, item.IP.Length / 2), item.UserAgent, item.Timestamp)); 
            } %>
        </tbody>
    </table>
        
</asp:Content>
