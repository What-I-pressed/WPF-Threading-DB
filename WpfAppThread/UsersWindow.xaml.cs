using Domain.Data;
using Infrastraction.Events;
using Infrastraction.Models;
using Infrastraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Security;
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
using Infrastraction.MVVM;
using Domain.Data.Entities;

namespace WpfAppThread
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        ViewModel _viewModel;

        public UsersWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();

            _viewModel.BindProgressBar(UserService_InsertUserEvent);
            _viewModel.BindDGUpdateFunc(UpdateDG);
            _viewModel.BindMessageBoxEvent(MessageBoxEvent);
            _viewModel.BindPBMaximumEvent(SetMaximumForPBEvent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateDGAsync();
        }

        private void UpdateDG()
        {
            MyDataContext myDataContext = new MyDataContext();
            var users = myDataContext.Users.ToList();
            Dispatcher.Invoke(() => dgSimple.ItemsSource = users);
        }

        void MessageBoxEvent(string text) =>
           MessageBox.Show(text);

        void SetMaximumForPBEvent(int maximumPB) => Dispatcher.Invoke(() =>
        pbUsersDownloaded.Maximum = maximumPB);

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            btnAdd.IsEnabled = false;
            _viewModel.AddUsers(int.Parse(txtUsersNumber.Text));
            btnAdd.IsEnabled = true;
        }

        private void UserService_InsertUserEvent(int count)
        {
            Dispatcher.Invoke(() => { pbUsersDownloaded.Value = count; });
        }

        private void dgSimple_GotFocus(object sender, RoutedEventArgs e)                    //зміна
        {
            DeleteOrEditWindow doew = new DeleteOrEditWindow();
            doew._userId = ((e.Source as DataGrid).CurrentItem as UserEntity).Id;
            doew.ShowDialog();
            _viewModel.InvokeDGUpdateEventAsync();
        }

    }
}
