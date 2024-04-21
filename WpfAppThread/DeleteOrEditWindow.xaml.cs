using Domain.Data;
using Domain.Data.Entities;
using Infrastraction.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppThread
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DeleteOrEditWindow : Window
    {
        public int _userId;
        public ViewModel _viewModel;
        public DeleteOrEditWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserEditingWindow uew = new UserEditingWindow();
            uew._userId = _userId;
            uew.ShowDialog();
            _viewModel.InvokeMessageBoxEvent("User information was successfully changed");
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (MyDataContext context = new MyDataContext())
            {
                context.Users.Remove(new UserEntity { Id = _userId});
                _viewModel.InvokeMessageBoxEvent("User was successfully removed");
                context.SaveChanges();
            }
            this.Close();
        }
    }
}
