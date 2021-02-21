Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class registro

    Dim strCon As String = System.Configuration.ConfigurationSettings.AppSettings("strCon")
    Dim conexion As SqlConnection
    Dim tBD As TratamientoBD = New TratamientoBD
    Dim terminosOk As Boolean = False

    Private Sub TextBlock1_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles TextBlock1.MouseLeftButtonDown
        Dim saveDialogWriter As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog

        Dim fichero As File = crearArchivoEula()

        saveDialogWriter.Title = "Descargar EULA de RITHMS™"
        saveDialogWriter.FileName = "RITHMS_EULA.txt"
        saveDialogWriter.Filter = "Archivo de texto (*.txt)|*.txt"
        saveDialogWriter.OpenFile()
        saveDialogWriter.ShowDialog()
    End Sub

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnInit.Click

        Try
            insertarUsuario()
        Catch sqle As SqlException
            MsgBox("Error de SQL")
        Catch ex As Exception
            MsgBox("Error Fatal de la aplicación, vuelva a iniciarla", vbCritical)
        Finally
            tBD.cerrarConexion(conexion)
        End Try
        tBD.cerrarConexion(conexion)

    End Sub

    Private Sub insertarUsuario()
        If comprobarDatos() Then
            conexion = tBD.abrirConexion(strCon)
            'insertarEnTabla()
            MsgBox("True")
        End If
    End Sub

    Private Function comprobarDatos() As Boolean
        Dim datosOk As Boolean = False

        If Not UserTextBox.Text = String.Empty And Not CorreoTextBox.Text = String.Empty And Not PasswordBox.Password = String.Empty Then
            If Calendar1.SelectedDate < Date.Today Then
                If CorreoTextBox.Text.Contains("@") Then
                    If Not tBD.comprobarRegistroExistente(strCon, "SELECT * FROM Usuarios WHERE nickname='" + UserTextBox.Text + "'") Then
                        If Not tBD.comprobarRegistroExistente(strCon, "SELECT * FROM Usuarios WHERE correo='" + CorreoTextBox.Text + "'") Then
                            If terminosOk Then
                                datosOk = True
                            Else
                                ErrorTextBlock.Text = "Debe aceptar los términos y condiciones de uso."
                            End If
                        Else
                            ErrorTextBlock.Text = "El correo que ha especificado ya está en uso."
                        End If
                    Else
                        ErrorTextBlock.Text = "El usuario que ha especificado ya existe."
                    End If
                Else
                    ErrorTextBlock.Text = "Introduzca un correo válido."
                End If
            Else
                ErrorTextBlock.Text = "Introduzca una fecha de nacimiento válida."
            End If
        Else
            ErrorTextBlock.Text = "Debes rellenar los campos 'Nombre de usuario', 'Correo', y 'Contraseña' obligatoriamente."
        End If

        Return datosOk
    End Function

    Private Sub TermsCheckBox_Checked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TermsCheckBox.Checked
        terminosOk = True
    End Sub

    Private Sub TermsCheckBox_Unchecked(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TermsCheckBox.Unchecked
        terminosOk = False
    End Sub

    Private Function crearArchivoEula() As File
        Throw New NotImplementedException
    End Function


End Class
