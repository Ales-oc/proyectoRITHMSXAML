Public Class HomePage

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
End Class
