using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TempoAplicativo.Prims.Models;
using TempoAplicativo.Prims.Services;
using TempoAplicativo.Prism.Views;

namespace TempoAplicativo.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _password;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Login";
            IsEnabled = true;
            _apiService = apiService;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public async void Login()
        {
            int i = 0;

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.GetListAsync<UserResponse>(url, "/api", "/userapi");


            if (!response.IsSuccess)
            {
                IsRunning = false;

                return;
            }

            List<UserResponse> users = (List<UserResponse>)response.Result;


            //if(users == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "Tou vazio", "Ok");
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", response.Result.ToString(), "Ok");
            //}

            if (string.IsNullOrEmpty(Email))
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Erro", "Verifica o Email", "Ok");
                return;

            }

            if (string.IsNullOrEmpty(Password))
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Erro", "Verifica a Palavra Passe", "Ok");
                return;

            }



            foreach (var email in users)
            {
                if (email.Email.Equals(Email) && email.Password.Equals(Password))
                {
                    IsRunning = false;
                    await App.Current.MainPage.DisplayAlert("Bem Vindo:", email.FirstName + " " + email.LastName, "Ok");
                    await NavigationService.NavigateAsync($"/NavigationPage/{nameof(TempoLocalPage)}");

                }
                else
                {
                    i++;
                }
                if (i >= users.Count)
                {
                    await App.Current.MainPage.DisplayAlert("Algo Correu Mal", "Email ou Palavra-Passe incorretas", "Ok");
                    IsRunning = false;
                    i = 0;
                    return;
                }
            }
        }

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

    }
}
