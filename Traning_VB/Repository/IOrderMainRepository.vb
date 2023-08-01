Imports System.Threading.Tasks

Public Interface IOrderMainRepository
    Function GetAll(customerID As Integer) As Task(Of IList(Of OrderMain))
End Interface
