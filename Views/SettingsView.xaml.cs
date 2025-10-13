using MilkBabyApp.ViewModels;

namespace MilkBabyApp.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (this.BindingContext is SettingsViewModel viewModel) { viewModel.GetData(); }
    }
}