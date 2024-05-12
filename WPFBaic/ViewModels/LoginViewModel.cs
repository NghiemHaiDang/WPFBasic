using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFBaic.Command;
using WPFBaic.Models;

namespace WPFBaic.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Account _userAccount;
        public Account UserAccount
        {
            get { return _userAccount; }
            set
            {
                _userAccount = value;
                OnPropertyChanged(nameof(UserAccount));
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            UserAccount = new Account();
            LoginCommand = new RelayCommand(Login, CanLogin);

        }

        private bool CanLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(UserAccount.Username) && !string.IsNullOrWhiteSpace(UserAccount.Password);
        }

        private void Login(object parameter)
        {
            string username = UserAccount.Username;
            string password = UserAccount.Password;
            if (username == "admin" && password == "admin")
            {
                System.Windows.MessageBox.Show("Đăng nhập thành công!");
            }
            else
            {
                System.Windows.MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
