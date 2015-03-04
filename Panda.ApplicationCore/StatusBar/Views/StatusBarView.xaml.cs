using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Panda.ApplicationCore.StatusBar.ViewModels;

namespace Panda.ApplicationCore.StatusBar.Views
{
    public partial class StatusBarView
    {
        public StatusBarView()
        {
            InitializeComponent();
        }

        private void OnItemsPanelLoaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid == null)
                return;

            var view_model = DataContext as StatusBarViewModel;
            if (view_model == null)
                return;
            
            grid.ColumnDefinitions.Clear();
            view_model.Apply(i => grid.ColumnDefinitions.Add(new ColumnDefinition { Width = i.Width }));
        }
    }
}
