<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="PolyglotDemo.Website.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        To login to this system please enter the username "Guest" with the password "Guest"
        <br />
        This system is meant as a simple cloud file storage system.
        <br /> File size cannot exceed *insert file size limitation here*
        however that is an arbitrary file size limit based on the server infrastructure (or that limit cannot be exceeded due to the nature of the HTTP protocol). 
        <br />
        <asp:Label ID="Label1" Text="Username: " runat="server" ></asp:Label>
        <asp:TextBox id="username" runat="server" ></asp:TextBox>
        <br />
        <asp:Label ID="Label2" Text="Password: " runat="server" ></asp:Label>
        <asp:TextBox TextMode="Password" id="password" runat="server" ></asp:TextBox>
        <br />
        <asp:Button ID="submit" Text="Submit" runat="server" OnClick="submit_Click" />
    </div>
    </form>
</body>
</html>
