Imports System.Data.SqlClient
Imports System.Data

Class MainWindow
    Dim mplayer As MediaPlayer

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPlay.Click
        mplayer = New MediaPlayer
        mplayer.Open(New Uri("C:/Users/aleru/Desktop/Actividades_a_entregar/PoroyectoFinalDI/ProyectoFinal/Amarillo.mp3", UriKind.Relative))
        mplayer.Play()
    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TextBox1.GotFocus
        Me.PasswordBox.Focus()
    End Sub

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnInit.Click

        If Me.UserTextBox.Text.ToString = "" Or Me.PasswordBox.Password.ToString = "" Then
            Me.ErrorTextBlock.Text = "Debe esecificar un usuario y contraseña para iniciar sesión"
        Else
            comprobarUsuario()
        End If

    End Sub

    Private Sub comprobarUsuario()
        Dim tBD As TratamientoBD = New TratamientoBD
        Dim strCon As String = System.Configuration.ConfigurationSettings.AppSettings("strCon")
        Dim conexion As SqlConnection = New SqlConnection(strCon)
        Dim ds As DataSet = New DataSet
        Dim campoAComparar As String = elegirUsuarioCorreo()

        If tBD.comprobarRegistroExistente(strCon, "SELECT * FROM Usuarios WHERE " + campoAComparar + "='" + UserTextBox.Text + "'") Then
            ds = tBD.obtenerDatos(strCon, "SELECT CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('password', password)) FROM Usuarios WHERE " + campoAComparar + "='" + UserTextBox.Text + "'")

            If PasswordBox.Password = ds.Tables(0).Rows.Item(0).Item(0) Then
                Dim homePage As New HomePage
                homePage.nombreUsr = UserTextBox.Text
                Me.Close()
                homePage.ShowDialog()
            Else
                Me.ErrorTextBlock.Text = "El usuario o contraseña especificado no existe"
            End If
        Else
            Me.ErrorTextBlock.Text = "El usuario o contraseña especificado no existe"
        End If

    End Sub

    Private Function elegirUsuarioCorreo() As String
        If UserTextBox.Text.Contains("@") Then
            Return "correo"
        Else
            Return "nickname"
        End If
    End Function

    Private Sub btnRegist_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnRegist.Click
        Dim registroForm As New registro
        registroForm.ShowDialog()
    End Sub
End Class
