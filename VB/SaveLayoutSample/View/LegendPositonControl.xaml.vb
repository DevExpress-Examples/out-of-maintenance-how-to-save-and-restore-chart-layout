Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace SaveLayoutSample.View

    Partial Public Class LegendPositonControl
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class StringToImageSourceConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim path As String = Nothing
            If Not CsImpl.__Assign(path, TryCast(value, String)) IsNot Nothing Then Return Nothing
            Dim result = New BitmapImage(New Uri(path))
            Return result
        End Function
        Private Class CsImpl
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
