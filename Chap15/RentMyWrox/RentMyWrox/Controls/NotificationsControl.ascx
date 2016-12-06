<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationsControl.ascx.cs" Inherits="RentMyWrox.Controls.NotificationsControl" %>
<asp:ScriptManager Id="smNotifications" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="upNotifications" runat="server">
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="hfNumberToSkip" />
        <asp:Label runat="server" ID="NotificationTitle" CssClass="NotificationTitle" />
        <asp:Label runat="server" ID="NotificationDetail" CssClass="NotificationDetail" />
        <div class="paginationline">
            <span class="leftside">
                <asp:LinkButton ID="lbPrevious" Text="<<" runat="server" 
                      ToolTip="Previous Item" OnClick="Previous_Click" />
            </span>
            <span class="rightside">
                <asp:LinkButton ID="lbNext" Text=">>" runat="server" 
                        ToolTip="Next Item" OnClick="Next_Click" />
            </span>
        </div>
        <asp:Timer runat="server" ID="tmrNotifications" Interval="5000" OnTick="Notifications_Tick" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="uprogNotifications" DisplayAfter="500" runat="server" AssociatedUpdatePanelID="upNotifications">
    <ProgressTemplate>
        <div class="progressnotification">
            Updating...
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>


        