Imports System.Web.Http
Imports System.Web.Optimization
Imports System.Xml

Public Class WebApiApplication
    Inherits System.Web.HttpApplication


    Public Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        If Request.ContentType = "text/xml" Then
            Dim stream As New System.IO.StreamReader(Request.InputStream)
            Dim xmlData As String = stream.ReadToEnd()
            HttpContext.Current.Request.InputStream.Position = 0 ' Reset position to read again later
            Dim xmlDocument As New XmlDocument()
            xmlDocument.LoadXml(xmlData)

            ' Now you have the XML data in the "xmlDocument" variable, and you can use it as needed.
            ' For example, you can store it in a session variable or pass it to a controller for processing.
        End If
    End Sub
    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class
