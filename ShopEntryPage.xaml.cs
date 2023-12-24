using Arghiroiu_Raluca_Lab7.Models;

namespace Arghiroiu_Raluca_Lab7;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetShopsAsync();
    }

    async void OnAddShopClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShopPage
        {
            BindingContext = new Shop()
        });
    }

    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new ShopPage
            {
                BindingContext = e.SelectedItem as Shop
            });
        }
    }
}