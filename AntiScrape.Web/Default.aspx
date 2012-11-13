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
                <th>IP Address</th>
                <th>User Agent</th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var item in AntiScrape.InMemoryStore.InMemoryStorage.GetScrapers())
            {
                Response.Write(string.Format("<tr><td>{0}.xxx.xxx.xxx</td><td>{1}</td></tr>", item.Item1.Substring(0, item.Item1.IndexOf('.')), item.Item2)); 
            } %>
        </tbody>
    </table>
        
</asp:Content>
