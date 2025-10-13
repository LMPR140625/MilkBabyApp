using MilkBabyApp.Models;
using MilkBabyApp.Services;
using MilkBabyApp.ViewModels;

namespace MilkBabyApp.Views;

public partial class ReportView : ContentPage
{
    public ReportView(ReportViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (this.BindingContext is ReportViewModel viewModel) { viewModel.GetData(); }
    }
}