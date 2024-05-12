using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFBaic.Command;
using WPFBaic.Models;
using WPFBaic.Views;

namespace WPFBaic.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public List<string> Genders { get; set; }
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

        public ICommand RegisterCommand { get; private set; }
        public RegisterViewModel()
        {
            UserAccount = new Account();
            Genders = new List<string> { "Male", "Female" };
            RegisterCommand = new RelayCommand(Register, CanRegister);
        }

        private bool CanRegister(object parameter)
        {
            return !string.IsNullOrWhiteSpace(UserAccount.Username) && !string.IsNullOrWhiteSpace(UserAccount.Password)
                && !string.IsNullOrWhiteSpace(UserAccount.ConfirmPassword)
                && !string.IsNullOrWhiteSpace(UserAccount.Username)
                && !string.IsNullOrWhiteSpace(UserAccount.Email)
                && !string.IsNullOrWhiteSpace(UserAccount.Gender);
        }

        private void Register(object parameter)
        {
            string username = UserAccount.Username;
            string password = UserAccount.Password;
            string confirmPassword = UserAccount.ConfirmPassword;
            string fullName = UserAccount.FullName;
            string email = UserAccount.Email;
            string gender = UserAccount.Gender;
            DateTime birthday = UserAccount.DateOfBirth;

            if (CanRegister(parameter))
            {
                if (password != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không khớp!");
                    return;
                }
                Account user = new Account
                {
                    Username = username,
                    Password = password,
                    FullName = fullName,
                    Email = email,
                    Gender = gender,
                    DateOfBirth = birthday
                };
                string filePath = "UserInfo.txt";
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Username: {user.Username}");
                        writer.WriteLine($"Password: {user.Password}");
                        writer.WriteLine($"Full Name: {user.FullName}");
                        writer.WriteLine($"Email: {user.Email}");
                        writer.WriteLine($"Gender: {user.Gender}");
                        writer.WriteLine($"Date of Birth: {user.DateOfBirth}");
                    }
                    MessageBox.Show("Đăng ký thành công và thông tin đã được lưu vào tệp tin!");
                    Login loginView = new Login();
                    loginView.Show();
                    Register registerView = new Register();
                    registerView.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu thông tin: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin đăng ký!");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
