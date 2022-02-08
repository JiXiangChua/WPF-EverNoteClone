using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool isShowingRegister = false;

        //Import from Model class so that we can bind to the view in this view model
        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                User = new User
                {
                    Username = username, //sets the username value
                    Password = this.Password, //retain the original password value
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                User = new User
                {
                    Username = this.Username, //retain the original username value using the Username prop of this view model
                    Password = password, //sets the password value
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = name,
                    Lastname = this.Lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Name");
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = lastname,
                    ConfirmPassword = this.ConfirmPassword
                };
                OnPropertyChanged("Lastname");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    Name = this.Name,
                    Lastname = this.Lastname,
                    ConfirmPassword = confirmPassword
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private Visibility loginVis;
        public Visibility LoginVis
        {
            get { return loginVis; }
            set
            {
                loginVis = value;
                OnPropertyChanged("LoginVis");
            }
        }

        private Visibility registerVis;
        public Visibility RegisterVis
        {
            get { return registerVis; }
            set
            {
                registerVis = value;
                OnPropertyChanged("RegisterVis");
            }
        }


        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Authenticated;

        public LoginViewModel()
        {
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            User = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister; //toggle the boolean

            if (isShowingRegister)
            {
                RegisterVis = Visibility.Visible;
                LoginVis = Visibility.Collapsed;
            } 
            else
            {
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
        }

        public async void Login()
        {
            bool result = await FirebaseAuthHelper.Login(User);

            if (result) //if successfully login
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        public async void Register()
        {
            bool result = await FirebaseAuthHelper.Register(User);

            if (result) //if successfully registered
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));       
        }

    }
}
