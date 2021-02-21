Imports System.Data.SqlClient
Imports System.Data

Public Class HomePage
    Dim temp As String = My.Computer.FileSystem.SpecialDirectories.Temp
    Dim strCon As String = System.Configuration.ConfigurationSettings.AppSettings("strCon")
    Dim tBD As TratamientoBD = New TratamientoBD
    Dim conexion As SqlConnection
    Dim mplayer As MediaPlayer = New MediaPlayer
    Dim tituloCancion As String
    Dim rutaArchivoTemp As String
    Dim bytesArchivo() As Byte

    Private Sub HomePage_Initialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Initialized

    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim RithmsDataSet As ProyectoFinal.RithmsDataSet = CType(Me.FindResource("RithmsDataSet"), ProyectoFinal.RithmsDataSet)
        'Cargar datos en la tabla Canciones. Puede modificar este código según sea necesario.
        Dim RithmsDataSetCancionesTableAdapter As ProyectoFinal.RithmsDataSetTableAdapters.CancionesTableAdapter = New ProyectoFinal.RithmsDataSetTableAdapters.CancionesTableAdapter()
        RithmsDataSetCancionesTableAdapter.Fill(RithmsDataSet.Canciones)
        Dim CancionesViewSource As System.Windows.Data.CollectionViewSource = CType(Me.FindResource("CancionesViewSource"), System.Windows.Data.CollectionViewSource)
        CancionesViewSource.View.MoveCurrentToFirst()



    End Sub

    Private Sub btnAddSong_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAddSong.Click
        Dim addSongForm As New addSong
        addSongForm.ShowDialog()
    End Sub

    Private Sub CancionesDataGrid_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles CancionesDataGrid.SelectionChanged
        Dim autor As String = New String(Me.CancionesDataGrid.SelectedItem(1))
        tituloCancion = New String(Me.CancionesDataGrid.SelectedItem(2))
        Me.TextBlockDatos.Text = (tituloCancion + " de " + autor)
        mplayer.Stop()
        mplayer.Close()
        obtenerCancion()

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnRefresh.Click
        Dim homePage As New HomePage
        Me.Close()
        homePage.ShowDialog()
    End Sub

    Private Sub obtenerCancion()

        Try
            Dim ds As DataSet = New DataSet
            conexion = tBD.abrirConexion(strCon)

            rutaArchivoTemp = temp + "/currentRithm.mp3"
            ds = tBD.obtenerDatos(strCon, "SELECT archivo FROM Canciones WHERE titulo='" + tituloCancion + "'")
            bytesArchivo = ds.Tables(0).Rows(0).Item(0)

            crearArchivoTemp()

            mplayer = New MediaPlayer
            mplayer.Open(New Uri(rutaArchivoTemp, UriKind.Relative))

        Finally
            tBD.cerrarConexion(conexion)
        End Try

    End Sub

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPlay.Click

        mplayer.Play()

        Me.btnPlay.Visibility = Windows.Visibility.Hidden
        Me.btnPause.Visibility = Windows.Visibility.Visible
        Me.CancionesDataGrid.IsEnabled = False

    End Sub

    Private Sub btnPause_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPause.Click

        Me.CancionesDataGrid.IsEnabled = True
        mplayer.Pause()
        Me.btnPause.Visibility = Windows.Visibility.Hidden
        Me.btnPlay.Visibility = Windows.Visibility.Visible

    End Sub

    Private Sub crearArchivoTemp()
        System.IO.File.Delete(rutaArchivoTemp)
        System.IO.File.WriteAllBytes(rutaArchivoTemp, bytesArchivo)
    End Sub

End Class
