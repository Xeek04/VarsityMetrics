using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.UWP;
using Microsoft.Maui.Controls.Platform;

namespace VarsityMetrics.ViewModel;

public partial class SignInViewModel : ObservableObject

{
    public SignInViewModel()
    {
        Items = new ObservableCollection<string>();
    }
    [ObservableProperty]
    ObservableCollection<string> items;

    [ObservableProperty]
    string username;

    [ObservableProperty]
    string password;

    [RelayCommand]
    void Add()
    {
        if (string.IsNullOrWhiteSpace(Username))
            return;

        Items.Add(Username);
        //add our item
        Username = string.Empty;

        if (string.IsNullOrWhiteSpace(Password))
            return;

        Items.Add(Password);
        Password = string.Empty;
    }

    [RelayCommand]
    async Task Click()
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

}