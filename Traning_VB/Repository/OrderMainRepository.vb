Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class OrderMainRepository
    Implements IOrderMainRepository

    Private ReadOnly cs As String = ConfigurationManager.ConnectionStrings("EcoContext").ConnectionString

    Public Sub New()

    End Sub

    Public Async Function GetAll(customerID As Integer) As Task(Of IList(Of OrderMain)) Implements IOrderMainRepository.GetAll
        Dim employees As New List(Of OrderMain)()
        Using con As New SqlConnection(cs)
            Dim cmd As New SqlCommand("dbo.ecJCPFilterOrdersServicePiece", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add(New SqlParameter("@CustomerID", SqlDbType.Int) With {.Value = customerID})
            cmd.Parameters.Add(New SqlParameter("@ServiceType", SqlDbType.VarChar) With {.Value = ""})
            cmd.Parameters.Add(New SqlParameter("@PieceRef", SqlDbType.VarChar) With {.Value = ""})
            con.Open()
            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            While rdr.Read()
                Dim orderMain As New OrderMain() With {
                    .OrderID = Integer.Parse(rdr("OrderID").ToString()),
                    .CustomerID = Integer.Parse(rdr("CustomerID").ToString()),
                    .Service = rdr("Service").ToString()
                }
                employees.Add(orderMain)
            End While
        End Using
        Return employees
    End Function

End Class