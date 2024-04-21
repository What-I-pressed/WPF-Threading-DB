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
    /// Interaction logic for UserEditingWindow.xaml
    /// </summary>
    public partial class UserEditingWindow : Window
    {
        public int _userId;
        ViewModel _viewModel;
        public UserEditingWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs ev)
        {
            UserEntity e = new UserEntity { Id = _userId,
                LastName = txtLastName.Text,
                FistName = txtFirstName.Text,
                PhoneNumber = txtPhoneNumber.Text,
                Email = txtEmail.Text,
                Image = txtImage.Text,
            };
            _viewModel.EditUser(e);
            this.Close();
        }
    }
}
