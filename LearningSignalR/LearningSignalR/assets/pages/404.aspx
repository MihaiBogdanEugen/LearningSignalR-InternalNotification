<%@ Page Language="C#" %>
<%
    this.Response.StatusCode = 404;
    this.Response.ContentType = "text/html; charset=utf-8";
    this.Response.WriteFile(this.MapPath("~/assets/pages/404.html"));
%>