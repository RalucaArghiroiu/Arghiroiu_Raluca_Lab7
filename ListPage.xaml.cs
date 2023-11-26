using Arghiroiu_Raluca_Lab7.Models;

namespace Arghiroiu_Raluca_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		slist.Date = DateTime.UtcNow;
		await App.Database.SaveShopListAsync(slist);
		await Navigation.PopAsync();
	}

    async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		await App.Database.DeleteShopListAsync(slist);
		await Navigation.PopAsync();
	}

	async void OnChooseButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext) {
			BindingContext = new Product()
		});
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		var slist = (ShopList)BindingContext;

		listView.ItemsSource = await App.Database.GetListProductsAsync(slist.ID);
        deleteItemButton.IsVisible = listView.SelectedItem != null;
    }

	private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			deleteItemButton.IsVisible = true;
		}
		else
		{
			deleteItemButton.IsVisible = false;
		}
	}

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var sList = (ShopList)BindingContext;

		if (listView.SelectedItem != null)
		{
			Product product = listView.SelectedItem as Product;

			await App.Database.DeleteListProductAsync(sList.ID, product.ID);

            listView.ItemsSource = await App.Database.GetListProductsAsync(sList.ID);

        }
    }
}