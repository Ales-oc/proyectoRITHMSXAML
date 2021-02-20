Imports System.Data.SqlClient
Imports System.Data

Public Class TratamientoBD
    Public Function abrirConexion(ByVal cadenaConexion As String)
        Dim conexion As SqlConnection = New SqlConnection(cadenaConexion)
        conexion.Open()
        Return conexion
    End Function

    Public Sub cerrarConexion(ByVal conexion As SqlConnection)
        If Not IsNothing(conexion) Then
            conexion.Close()
        End If
    End Sub

    Public Function comprobarRegistroExistente(ByVal cadenaConexion As String, ByVal selectQuery As String)
        Dim conexion As SqlConnection = New SqlConnection(cadenaConexion)
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(selectQuery, conexion)
        Dim dt As DataTable = New DataTable

        adapter.Fill(dt)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function obtenerDatos(ByVal cadenaConexion As String, ByVal selectQuery As String)
        Dim conexion As SqlConnection = New SqlConnection(cadenaConexion)
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(selectQuery, conexion)
        Dim ds As DataSet = New DataSet

        adapter.Fill(ds)
        Return ds
    End Function
End Class
