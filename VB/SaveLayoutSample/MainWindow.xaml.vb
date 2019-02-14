Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Charts
Imports DevExpress.Xpf.Dialogs
Imports DevExpress.Xpf.Ribbon
Imports SaveLayoutSample.ViewModel
Imports System
Imports System.Linq
Imports System.Windows.Data

Namespace SaveLayoutSample
	Partial Public Class MainWindow
		Inherits DXRibbonWindow

		Private Const LayoutSavedFormatString As String = "The Chart Layout saved to the '{0}' file"
		Private Const LayoutLoadedFormatString As String = "The Chart Layout loaded from the '{0}' file"
		Private Const DefaultFileExtension As String = "xml"

		Public Sub New()
			InitializeComponent()
			CreateBindings()
		End Sub

		Private Sub OnSaveBarItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Dim dialog As DXSaveFileDialog = New DXSaveFileDialog With {.DefaultExt = DefaultFileExtension}
			Dim result? As Boolean = dialog.ShowDialog()
			If result.HasValue AndAlso result.Value Then
				chartControl.SaveToFile(dialog.FileName)
				statusMessageItem.Content = String.Format(LayoutSavedFormatString, dialog.FileName)
			End If
		End Sub
		Private Sub OnLoadBarItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			ShowLoadChartLayoutDialog()
		End Sub

		Private Sub ShowLoadChartLayoutDialog()
			Dim dialog As DXOpenFileDialog = New DXOpenFileDialog With {.DefaultExt = DefaultFileExtension}
			Dim result? As Boolean = dialog.ShowDialog()
			If result.HasValue AndAlso result.Value Then
				chartControl.LoadFromFile(dialog.FileName)
				CreateBindings()
				statusMessageItem.Content = String.Format(LayoutLoadedFormatString, dialog.FileName)
			End If
		End Sub

		Public Sub CreateBindings()
			Dim seriesTemplate As Series = chartControl.Diagram.SeriesTemplate
			Dim legend As Legend = chartControl.Legends.FirstOrDefault()
			If (seriesTemplate Is Nothing) OrElse (legend Is Nothing) Then
				Return
			End If

			BindingOperations.SetBinding(seriesDataMemberEdit, BarEditItem.EditValueProperty, New Binding("SeriesDataMember") With {.Source = chartControl.Diagram})
			BindingOperations.SetBinding(argumentDataMemberEdit, BarEditItem.EditValueProperty, New Binding("ArgumentDataMember") With {.Source = seriesTemplate})
			BindingOperations.SetBinding(valueDataMemberEdit, BarEditItem.EditValueProperty, New Binding("ValueDataMember") With {.Source = seriesTemplate})

			BindingOperations.SetBinding(legendHorizontalPositionEdit, BarEditItem.EditValueProperty, New Binding("HorizontalPosition") With {.Source = legend})
			BindingOperations.SetBinding(legendVerticalPositionEdit, BarEditItem.EditValueProperty, New Binding("VerticalPosition") With {.Source = legend})
			BindingOperations.SetBinding(legendOrientationEdit, BarEditItem.EditValueProperty, New Binding("Orientation") With {.Source = legend})
		End Sub
	End Class
End Namespace
