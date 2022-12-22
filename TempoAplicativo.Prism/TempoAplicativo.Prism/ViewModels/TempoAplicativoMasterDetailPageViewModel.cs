using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TempoAplicativo.Prism.ItemViewModel;
using TempoAplicativo.Prism.Models;
using TempoAplicativo.Prism.Views;

namespace TempoAplicativo.Prism.ViewModels
{
    public class TempoAplicativoMasterDetailPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;

        public TempoAplicativoMasterDetailPageViewModel(INavigationService navigationService) :base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "homeicon",
                    PageName = $"{nameof(TempoLocalPage)}",
                    Title = "Home"
                },
                new Menu
                {
                    Icon = "addressbookicon",
                    PageName = $"{nameof(ContactPage)}",
                    Title = "Contact Us"
                },
                new Menu
                {
                    Icon = "VeryBasicAbouticon",
                    PageName = $"{nameof(AboutPage)}",
                    Title = "About Us"
                },
                new Menu
                {
                    Icon = "Logouticon",
                    PageName = $"{nameof(LoginPage)}",
                    Title = "Logout"
                },
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    Title = m.Title,
                    PageName = m.PageName
                }).ToList());
        }
    }
}

