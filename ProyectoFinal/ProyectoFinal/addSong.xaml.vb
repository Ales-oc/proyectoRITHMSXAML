Imports System.IO
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data

Public Class addSong

    Dim strCon As String = System.Configuration.ConfigurationSettings.AppSettings("strCon")
    Dim conexion As SqlConnection
    Dim tBD As TratamientoBD = New TratamientoBD
    Dim openDialogWriter As Microsoft.Win32.OpenFileDialog

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnOpen.Click
        openDialogWriter = New Microsoft.Win32.OpenFileDialog
        openDialogWriter.Filter = "Archivo de audio (*.mp3)|*.mp3"

        If openDialogWriter.ShowDialog() = True Then
            rutaTextBox.Text = openDialogWriter.FileName
        End If
    End Sub

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnInit.Click

        Try
            insertarCancion()
        
        Finally
            tBD.cerrarConexion(conexion)
        End Try
        tBD.cerrarConexion(conexion)
    End Sub

    Private Sub insertarCancion()
        If comprobarDatos() Then
            conexion = tBD.abrirConexion(strCon)
            insertarEnTabla()
            MsgBox("Correcto")
            'Me.Close()
        End If
    End Sub

    Private Function comprobarDatos() As Boolean
        Dim datosOk As Boolean = False

        If Not tituloTextBox.Text = String.Empty And Not autorTextBox.Text = String.Empty And Not generoTextBox.Text = String.Empty Then
            If Not tBD.comprobarRegistroExistente(strCon, "SELECT * FROM Canciones WHERE titulo='" + tituloTextBox.Text + "'") Then
                If Not rutaTextBox.Text = String.Empty Then
                    datosOk = True
                Else
                    ErrorTextBlock.Text = "Debe añadir el archivo con la canción."
                End If
            Else
                ErrorTextBlock.Text = "El título que ha especificado ya existe."
            End If
        Else
            ErrorTextBlock.Text = "Debes rellenar los campos 'Título', 'Autor', y 'Genero' obligatoriamente."
        End If

        Return datosOk
    End Function

    Private Sub insertarEnTabla()

        Dim cmd As SqlCommand = New SqlCommand

        cmd.Connection = conexion
        cmd.CommandText = "INSERT INTO Canciones (autor, titulo, genero, [archivo]) VALUES (@autor, @titulo, @genero, @archivo)"
        With cmd.Parameters
            .AddWithValue("autor", autorTextBox.Text)
            .AddWithValue("titulo", tituloTextBox.Text)
            .AddWithValue("genero", generoTextBox.Text)
            .AddWithValue("archivo", SqlDbType.VarBinary).Value = File.ReadAllBytes(rutaTextBox.Text)
        End With
        cmd.ExecuteNonQuery()
    End Sub

End Class
