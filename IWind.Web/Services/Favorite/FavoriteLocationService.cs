using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWind.Web.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;

namespace IWind.Web.Services.Favorite
{
    public class FavoriteLocationService : IFavoriteLocationService
    {
        private ProtectedLocalStorage ProtectedLocalStorage {get;}
        private const string FavoriteLocationsKey = "IWind.FavoriteLocations.Key";

        public FavoriteLocationService(IJSRuntime jsRuntime, IDataProtectionProvider dataProtectionProvider)
        {
            ProtectedLocalStorage = new ProtectedLocalStorage(jsRuntime,dataProtectionProvider);
        }
        
        public async void AddLocation(LocationViewModel location)
        {
            var locations = await GetFavoriteLocations();
            locations.Add(location);
            SetFavoriteLocations(locations);
        }

        public async void RemoveLocation(string favoriteLocationId)
        {
            var locations = await GetFavoriteLocations();
            var locationToRemove = locations.FirstOrDefault(x => x.Id == favoriteLocationId);
            if (locationToRemove != null) locations.Remove(locationToRemove);
            SetFavoriteLocations(locations);
        }

        public async Task<List<LocationViewModel>> GetFavoriteLocations()
        {
            var result = await ProtectedLocalStorage.GetAsync<List<LocationViewModel>>(FavoriteLocationsKey);
            return result.Success ? result.Value : new List<LocationViewModel>();
        }
        
        private async void SetFavoriteLocations(List<LocationViewModel> favoriteLocations) => 
            await ProtectedLocalStorage.SetAsync(FavoriteLocationsKey, favoriteLocations);

    }
}