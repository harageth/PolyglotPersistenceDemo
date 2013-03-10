<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PolyglotDemo.Website.WebForm1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Polyglot Persistence</title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="virtualPath" runat="server" Text="/" ></asp:Label>
        <div>
                <asp:FileUpload ID="uploadFileToDatabase" Enabled="true" runat="server" Visible="true" />
                <asp:Button ID="uploadFile" Text="Upload File" runat="server" Enabled="true" OnClick="UploadFile_Click" />
        </div>
    </div>

        <ul>
            <asp:ListView ID="folders" runat="server" >
                <ItemTemplate>
                    <li> 
                        <asp:LinkButton ID="folder" runat="server" OnClick="ChangeCWD_Click" Text='<%# DataBinder.Eval(Container.DataItem, "folderName")%>'> </asp:LinkButton>
                        
                    </li>
                                        
                </ItemTemplate>

            </asp:ListView>
            <br>
            <asp:ListView ID="files" runat="server" >
                
                <ItemTemplate>
                    <asp:LinkButton ID="files" runat="server" OnClick="DownloadFile" Text="<%# Container.DataItem %>" ></asp:LinkButton> <br />
                </ItemTemplate>

            </asp:ListView>

        </ul>

        <asp:Button ID="createFolder" Text="Create Folder" runat="server" OnClick="CreateFolder" />

    </form>


    <script src="/javascript/anchorChangeCWD.js" lang="javascript" type="text/javascript"></script>

</body>
</html>
