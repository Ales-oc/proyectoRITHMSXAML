Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class registro

    Dim strCon As String = System.Configuration.ConfigurationSettings.AppSettings("strCon")
    Dim conexion As SqlConnection
    Dim tBD As TratamientoBD = New TratamientoBD
    Dim terminosOk As Boolean = False

    Private Sub btnInit_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnInit.Click

        Try
            insertarUsuario()
        Catch sqle As SqlException
            MsgBox("Error de SQL", vbExclamation)
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
            insertarEnTabla()
            My.Windows.HomePage.Show()
            My.Windows.MainWindow.Close()
            Me.Close()
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

    Private Sub insertarEnTabla()
        Dim fecha As String = Replace(Calendar1.SelectedDate, "/", "-")
        Dim insert As String = "INSERT INTO Usuarios (nickname, correo, password, fecha_nacimiento, Admin) VALUES ('" + UserTextBox.Text + "','" + CorreoTextBox.Text + "', ENCRYPTBYPASSPHRASE('password', '" + PasswordBox.Password + "'), '" + fecha + "', 0)"
        Dim comando As SqlCommand
        comando = New SqlCommand(insert, conexion)
        comando.ExecuteNonQuery()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub TextBlock1_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles TextBlock1.MouseLeftButtonDown
        Dim saveDialogWriter As Microsoft.Win32.SaveFileDialog = New Microsoft.Win32.SaveFileDialog

        saveDialogWriter.Title = "Descargar EULA de RITHMS™"
        saveDialogWriter.FileName = "RITHMS_EULA.txt"
        saveDialogWriter.Filter = "Archivo de texto (*.txt)|*.txt"

        If saveDialogWriter.ShowDialog() = True Then
            Dim sw As StreamWriter = File.CreateText(saveDialogWriter.FileName)
            sw.Write("Acuerdo de Licencia de Usuario Final" + vbLf + vbLf +
                    "El presente Acuerdo de Licencia de Usuario Final (EULA) constituye un contrato entre el usuario y RITHMStm con sede en España, Sevilla. En la presente Licencia se establecen las normas de uso del software y de toda la documentación asociada al mismo (por ejemplo los manuales, las cajas), ya sea en formato impreso o electrónico, descargada o disponible en línea, junto con los complementos y las mejoras que sustituyen o complementan el software y que no se distribuyan bajo una licencia independiente (en lo sucesivo el 'Software'). El Software se distribuye bajo licencia, no se vende. Al instalar o utilizar el Software el usuario confirma que ha leído el EULA y que acepta regirse por los términos del mismo. Si el usuario no está de acuerdo con estas condiciones, no podrá instalar y utilizar el Software." + vbLf +
                    "El Software está protegido por las leyes nacionales de derechos de autor, así como por otros tratados internacionales relativos a la protección de la propiedad intelectual." + vbLf + vbLf +
                    "Por el presente EULA, RITHMS le concede al usuario una licencia limitada y no exclusiva de instalación y uso de una copia del Software en un ordenador o una unidad (en función del tipo de sistema operativo para el que esté destinado el Software), únicamente para fines personales y no comerciales. El Software no se puede compartir ni instalar al mismo tiempo en varios ordenadores o varias unidades instaladas dentro de una red." + vbLf +
                    "RITHMStm es el licenciante del Software. El Usuario reconoce que no se le transfiere ningún derecho de propiedad intelectual, título o propiedad derivada del Software. RITHMStm se reserva todos los derechos (incluidos, aunque no de forma exclusiva, los derechos de autor, otros derechos de propiedad intelectual, por ejemplo las marcas comerciales) del Software, de cualquiera de sus partes, especialmente de las partes del Software que pudiesen considerarse obras independientes (por ejemplo, los títulos, los sonidos, las bandas sonoras, los gráficos, los personajes, las descripciones, los diálogos), así como de todo el material asociado. Todos los derechos no concedidos por el presente EULA quedan reservados a RITHMStm." + vbLf + vbLf +
                    "El usuario no podrá bajo ningún concepto:" + vbLf +
                    "(a) utilizar el Software con fines comerciales, es decir relacionado indirecta o directamente con ánimo de lucro;" + vbLf +
                    "(b) distribuir, sublicenciar, ceder, arrendar, transferir derechos del Software a terceros, excepto en los casos que se indican en el presente EULA;" + vbLf +
                    "(c) instalar o utilizar el Software en la red, otorgar acceso al Software en línea o instalar el Software en más de un ordenador al mismo tiempo;" + vbLf +
                    "(d) esquivar las protecciones técnicas utilizadas para proteger contra el uso ilícito o la copia del Software o de sus características;" + vbLf +
                    "(e) realizar ingeniería inversa, descompilar, desensamblar el Software o crear obras dependientes;" + vbLf +
                    "(f) eliminar o modificar marcas registradas, logotipos y otra información incluida en el Software." + vbLf +
                    "En caso de que el usuario incumpla con alguna de las disposiciones incluidas en la parte 'Licencia' o 'Restricciones', RITHMStm tendrá derecho de rescindir el EULA con un período de antelación de 14 días y/o de hacer uso de todos los derechos que le correspondan por la Ley aplicable. El EULA será rescindido con carácter inmediato si el usuario intenta esquivar o infringir alguno de los medios técnicos de protección contra copia o instalación ilegal del Software. En caso de rescisión del EULA, el usuario deberá desinstalar y eliminar permanentemente el Software, así como todas sus copias." + vbLf + vbLf +
                    "Este EULA está sujeto a la Ley de España.")

            sw.Flush()
            sw.Close()

        End If

    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Me.OnLostFocus()

    End Sub

    Private Sub OnLostFocus()
        Me.Focus()
    End Sub

End Class
