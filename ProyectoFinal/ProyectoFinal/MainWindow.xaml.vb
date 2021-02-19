Class MainWindow 
    Dim mplayer As MediaPlayer

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPlay.Click
        mplayer = New MediaPlayer
        mplayer.Open(New Uri("C:/Users/aleru/Desktop/Actividades_a_entregar/PoroyectoFinalDI/ProyectoFinal/Amarillo.mp3", UriKind.Relative))
        mplayer.Play()
    End Sub

    Private Sub PasswordBox_PasswordChanged(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)

    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles TextBox1.GotFocus
        Me.PasswordBox.Focus()
    End Sub
End Class
