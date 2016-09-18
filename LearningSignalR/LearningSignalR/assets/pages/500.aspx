<%@ Page Language="C#" %>
<%
    this.Response.StatusCode = 500;
    this.Response.ContentType = "text/html; charset=utf-8";
    this.Response.WriteFile(this.MapPath("~/assets/pages/500.html"));
%>