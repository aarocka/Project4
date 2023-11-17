<!-- Kevin To -->
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MakeReview.aspx.cs" Inherits="RestaurantReviewProject3.MakeReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="StyleReviewerRepresentative.css"/>
    <title>Make Review</title>
    <style type="text/css">
        #rating {
            width: 45px;
        }

        #rating0 {
            width: 45px;
        }

        #rating1 {
            width: 45px;
        }

        #rating2 {
            width: 45px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body-center">
            <h1>Make Your Review Here!</h1>
            <label for="rating">
                <br />
                <strong>Food Quality Rating(1-5):</strong>
            </label>
            <input type="number" id="rating" name="rating" min="1" max="5" value="1" />
            <br />
            P<strong>rice Level Rating(1-5):</strong>
            <input type="number" id="rating0" name="rating0" min="1" max="5" value="1" /><br />
            <strong>Service Rating(1-5):</strong>
            <input type="number" id="rating1" name="rating1" min="1" max="5" value="1" /><br />
            <strong>Atmosphere Rating(1-5):</strong>
            <input type="number" id="rating2" name="rating2" min="1" max="5" value="1" /><br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Write your review below:" style="font-weight: 700"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Height="98px" Width="208px" Rows="5" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Add Review" OnClick="Button1_Click" Height="26px" style="font-weight: 700" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back Home" style="font-weight: 700" />
        </div>
    </form>
</body>
</html>