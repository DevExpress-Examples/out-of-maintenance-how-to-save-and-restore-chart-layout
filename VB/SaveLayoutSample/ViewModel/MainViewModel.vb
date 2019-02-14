Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Linq

Namespace SaveLayoutSample.ViewModel
	Friend Class MainViewModel
		Public ReadOnly Property ProductsByMonths() As IReadOnlyList(Of DevAVSaleItem) = New DevAVSaleItemRepository().GetProductsByMonths()

        Public ReadOnly Property SeriesDataMembers() As IReadOnlyList(Of String) = New List(Of String)() From {"Company", "Product", "Month"}
        Public ReadOnly Property ArgumentDataMembers() As IReadOnlyList(Of String) = New List(Of String)() From {"Product", "Month"}
        Public ReadOnly Property ValueDataMembers() As IReadOnlyList(Of String) = New List(Of String)() From {"Income", "Revenue"}
    End Class

	Public Class DevAVSaleItem
'INSTANT VB NOTE: The field saleItems was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ReadOnly saleItems_Renamed As New List(Of DevAVSaleItem)()

		Public Property OrderDate() As Date
		Public Property Product() As String
		Public Property Company() As String
		Public Property Month() As String
		Public Property Income() As Double
		Public Property Revenue() As Double
		Public Property Category() As String
		Public ReadOnly Property SaleItems() As List(Of DevAVSaleItem)
			Get
				Return Me.saleItems_Renamed
			End Get
		End Property
		Public ReadOnly Property TotalIncome() As Double
			Get
				Return saleItems_Renamed.Sum(Function(i) i.Income)
			End Get
		End Property
	End Class

	Public Class DevAVSaleItemRepository
		Private ReadOnly Shared companies() As String = { "DevAV North", "DevAV South", "DevAV West", "DevAV East", "DevAV Central" }
'INSTANT VB NOTE: The field categorizedProducts was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private Shared categorizedProducts_Renamed As Dictionary(Of String, List(Of String))

		Private Shared ReadOnly Property CategorizedProducts() As Dictionary(Of String, List(Of String))
			Get
				If categorizedProducts_Renamed Is Nothing Then
					categorizedProducts_Renamed = New Dictionary(Of String, List(Of String))()
					categorizedProducts_Renamed("Cell Phones") = New List(Of String)() From {"Smartphones", "Mobile Phones", "Smart Watches", "Sim Cards"}
					categorizedProducts_Renamed("Computers") = New List(Of String)() From {"PCs", "Laptops", "Tablets", "Printers"}
					categorizedProducts_Renamed("TV, Audio") = New List(Of String)() From {"TVs", "Home Audio", "Headphones", "DVD Players"}
					categorizedProducts_Renamed("Car Electronics") = New List(Of String)() From {"GPS Units", "Radars", "Car Alarms", "Car Accessories"}
					categorizedProducts_Renamed("Power Devices") = New List(Of String)() From {"Batteries", "Chargers", "Converters", "Testers", "AC/DC Adapters"}
					categorizedProducts_Renamed("Photo") = New List(Of String)() From {"Cameras", "Camcorders", "Binoculars", "Flashes", "Tripodes"}
				End If
				Return categorizedProducts_Renamed
			End Get
		End Property

		Public Function GetProductsByMonths() As List(Of DevAVSaleItem)
			Dim rnd As New Random(1)
			Dim items As New List(Of DevAVSaleItem)()
			For Each company As String In companies
				For Each product As String In CategorizedProducts("Photo")
					Dim dateTime As New Date(2017, 12, 01)
					For i As Integer = 0 To 11
						Dim income As Integer = rnd.Next(20, 100)
						Dim revenue As Integer = income + rnd.Next(20, 50)
						items.Add(New DevAVSaleItem() With {
							.Company = company,
							.Product = product,
							.Month = dateTime.AddMonths(1).ToString("MMMM", CultureInfo.InvariantCulture),
							.Income = income,
							.Revenue = revenue
						})
						dateTime = dateTime.AddMonths(1)
					Next i
				Next product
			Next company
			Return items
		End Function
	End Class
End Namespace
