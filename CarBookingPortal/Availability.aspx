<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Availability.aspx.cs" Inherits="CarBookingPortal.Availability" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Enter from time :
        <asp:TextBox ID="txtBoxFromTime" runat="server"></asp:TextBox>
    </p>
    <p>
        Enter to Time :
        <asp:TextBox ID="txtBoxToTime" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnCheck" runat="server" OnClick="btnCheck_Click" Text="Check" />
        <asp:Button ID="btnBook" runat="server" OnClick="btnBook_Click" Text="Book" Visible="False" />
    </p>
    <p>
        &nbsp;</p>
    <asp:Panel ID="pnlBookingDetails" runat="server" Visible="False">
        Purpose :
        <asp:TextBox ID="txtBoxPurpose" runat="server" Height="56px" MaxLength="200" TextMode="MultiLine" Width="215px"></asp:TextBox>
        <br />
    </asp:Panel>
</asp:Content>
