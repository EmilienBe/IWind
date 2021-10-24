using System.Collections.Generic;
using System.Threading.Tasks;
using IWind.Web.Models;

namespace IWind.Web.Services.Favorite
{
    public interface IFavoriteLocationService
    {
        void AddLocation(LocationViewModel location);
        void RemoveLocation(string favoriteLocationId);
        Task<List<LocationViewModel>> GetFavoriteLocations();
    }
}