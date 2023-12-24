using Arghiroiu_Raluca_Lab7.Models;
using Plugin.LocalNotification;

namespace Arghiroiu_Raluca_Lab7;

public partial class ShopPage : ContentPage
{
	public ShopPage()
	{
		InitializeComponent();
	}

    async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var shop = (Shop)BindingContext;
		await App.Database.SaveShopAsync(shop);

		await Navigation.PopAsync();
	}

    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var locations = await Geocoding.GetLocationsAsync(shop.Address);
        var shopLocation = locations?.FirstOrDefault();

        var userLocation = await Geolocation.GetLocationAsync();
        var distance = userLocation.CalculateDistance(shopLocation, DistanceUnits.Kilometers);

        if (distance < 4)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = shop.Address,
                Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(1)
                    }
            };

            await LocalNotificationCenter.Current.Show(request);
        }

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat"
        };
        await Map.OpenAsync(shopLocation, options);
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.DeleteShopAsync(shop);

        await Navigation.PopAsync();
    }
}