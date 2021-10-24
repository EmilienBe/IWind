using System.Collections.Generic;
using System.Threading.Tasks;
using IWind.Web.Models;

namespace IWind.Web.Services.Address
{
    public interface IAddressService
    {
        Task<List<LocationViewModel>> GetByFulltext(string fulltext);
    }
}