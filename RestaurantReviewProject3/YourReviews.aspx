<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YourReviews.aspx.cs" Inherits="RestaurantReviewProject3.YourReviews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css" />
    <title>Your Reviews</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Here are all your reviews!</h1>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" DataKeyNames="ReviewId" CssClass="gridview-font" Width="600px">
                <Columns>
                    <%--<asp:ImageField DataImageUrlField="IMGURL" HeaderText="Picture">
                        <ControlStyle Width="100px" Height="100px" />
                    </asp:ImageField>--%>
                    <asp:BoundField DataField="ReviewId" HeaderText="Id" InsertVisible="False" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="RestaurantName" HeaderText="Restaurant"/>
                    <asp:BoundField DataField="ReviewText" HeaderText="Review"/>
                    <asp:BoundField DataField="FoodQualityRating" HeaderText="Food Quality Rating"/>
                    <asp:BoundField DataField="PriceLevelRating" HeaderText="Price Level Rating"/>
                    <asp:BoundField DataField="ServiceRating" HeaderText="Service Rating"/>
                    <asp:BoundField DataField="AtmosphereRating" HeaderText="Atmosphere Rating"/>
                    <asp:BoundField DataField="AVGRating" HeaderText="Average Rating"/>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Edit Review" CommandName="MyButton1" CommandArgument='<%# Eval("ReviewID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" Text="Delete Review" CommandName="MyButton2" CommandArgument='<%# Eval("ReviewID") %>' />                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Rows="5" TextMode="MultiLine" Width="198px"></asp:TextBox>
            <br />
            <asp:Button ID="Button3" runat="server" Text="Make Edit" Visible="False" Width="74px" OnClick="Button3_Click" style="font-weight: 700" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Back Home" OnClick="Button1_Click" style="font-weight: 700" />
        </div>
    </form>
</body>
</html>