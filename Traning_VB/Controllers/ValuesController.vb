Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Xml
Imports System.Xml.Serialization

Public Class ValuesController
    Inherits ApiController

    Dim orderRepo = New OrderMainRepository()

    Public Sub New()

    End Sub

    ' POST api/values
    Public Sub PostValue(<FromBody()> ByVal value As String)

    End Sub

    ' PUT api/values/5
    Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE api/values/5
    Public Sub DeleteValue(ByVal id As Integer)

    End Sub

    ' Route: api/values/{Id}
    ' HTTP Method: GET
    <HttpGet>
    <Route("api/getall/{Id}")>
    Public Async Function GetAll(<FromUri()> Id As Integer) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim orders = Await orderRepo.GetAll(Id)
        Dim serializer As New XmlSerializer(GetType(List(Of OrderMain)))
        ' Using Memory stream to convert object to xml
        Using stream As New MemoryStream()
            serializer.Serialize(stream, orders)
            Dim content As Byte() = stream.ToArray()
            Dim response As New HttpResponseMessage(HttpStatusCode.OK)
            response.Content = New ByteArrayContent(content)
            response.Content.Headers.ContentType = New Net.Http.Headers.MediaTypeHeaderValue("application/xml")

            Return response
        End Using

    End Function

    <HttpPost>
    <Route("api/convert")>
    Public Async Function Convert(<FromBody> xmlData As XmlDocument) As Threading.Tasks.Task(Of IHttpActionResult)

        Return Ok(xmlData)
    End Function
End Class
