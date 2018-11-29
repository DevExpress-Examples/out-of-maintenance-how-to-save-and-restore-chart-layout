using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Ribbon;
using SaveLayoutSample.ViewModel;
using System;
using System.Linq;
using System.Windows.Data;

namespace SaveLayoutSample {
    public partial class MainWindow : DXRibbonWindow {
        const string LayoutSavedFormatString = "The Chart Layout saved to the '{0}' file";
        const string LayoutLoadedFormatString = "The Chart Layout loaded from the '{0}' file";
        const string DefaultFileExtension = "xml";

        public MainWindow() {
            InitializeComponent();
            CreateBindings();
        }

        private void OnSaveBarItemClick(object sender, ItemClickEventArgs e) {
            DXSaveFileDialog dialog = new DXSaveFileDialog {
                DefaultExt = DefaultFileExtension
            };
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value) {
                chartControl.SaveToFile(dialog.FileName);
                statusMessageItem.Content = String.Format(LayoutSavedFormatString, dialog.FileName);
            }
        }
        private void OnLoadBarItemClick(object sender, ItemClickEventArgs e) => ShowLoadChartLayoutDialog();

        private void ShowLoadChartLayoutDialog() {
            DXOpenFileDialog dialog = new DXOpenFileDialog {
                DefaultExt = DefaultFileExtension
            };
            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value) {
                chartControl.LoadFromFile(dialog.FileName);
                CreateBindings();
                statusMessageItem.Content = String.Format(LayoutLoadedFormatString, dialog.FileName);
            }
        }
        
        public void CreateBindings() {
            Series seriesTemplate = chartControl.Diagram.SeriesTemplate;
            Legend legend = chartControl.Legends.FirstOrDefault();
            if ((seriesTemplate == null) || (legend == null)) return;

            BindingOperations.SetBinding(
                seriesDataMemberEdit,
                BarEditItem.EditValueProperty,
                new Binding("SeriesDataMember") { Source  = chartControl.Diagram });
            BindingOperations.SetBinding(
                argumentDataMemberEdit,
                BarEditItem.EditValueProperty,
                new Binding("ArgumentDataMember") { Source  = seriesTemplate });
            BindingOperations.SetBinding(
                valueDataMemberEdit,
                BarEditItem.EditValueProperty,
                new Binding("ValueDataMember") { Source  = seriesTemplate });

            BindingOperations.SetBinding(
                legendHorizontalPositionEdit,
                BarEditItem.EditValueProperty,
                new Binding("HorizontalPosition") { Source  = legend });
            BindingOperations.SetBinding(
                legendVerticalPositionEdit,
                BarEditItem.EditValueProperty,
                new Binding("VerticalPosition") { Source  = legend });
            BindingOperations.SetBinding(
                legendOrientationEdit,
                BarEditItem.EditValueProperty,
                new Binding("Orientation") { Source  = legend });
        }
    }
}
