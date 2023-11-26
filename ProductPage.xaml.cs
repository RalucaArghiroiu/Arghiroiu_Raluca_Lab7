using Arghiroiu_Raluca_Lab7.Models;

namespace Arghiroiu_Raluca_Lab7;

public partial class ProductPage : ContentPage
{
	ShopList shopList;
	public ProductPage(ShopList sList)
	{
		InitializeComponent();
		shopList = sList;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		await App.Database.SaveProductAsync(product);
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		await App.Database.DeleteProductAsync(product);
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}

    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product product;

        if (listView.SelectedItem != null)
        {
			product = listView.SelectedItem as Product;
			var listProduct = new ListProduct()
			{
				ShopListID = shopList.ID,
				ProductID = product.ID
			};

			await App.Database.SaveListProductAsync(listProduct);
			product.ListProducts = new List<ListProduct> { listProduct };

			await Navigation.PopAsync();
        }
    }
}