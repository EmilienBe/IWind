using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using IWind.Web.Models;
using Microsoft.AspNetCore.Http.Features;

namespace IWind.Web.Services.Address
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        
        private const int LimitResult = 5;

        public AddressService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }
        
        public async Task<List<LocationViewModel>> GetByFulltext(string fulltext)
        {
            List<LocationViewModel> addresses = new();
            var uri = $"search/?q={fulltext}&limit={LimitResult}";
            var response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var featureCollection = await response.Content.ReadFromJsonAsync<FeatureCollection>();
                addresses = _mapper.Map<List<LocationViewModel>>(featureCollection?.Features);
            }
            return addresses ?? new List<LocationViewModel>();
        }
        
    }
    
    internal class FeatureCollection
    {
        public List<Feature> Features { get; set; }
    }

    internal class Feature
    {
        public AdresseApi Properties { get; set; }
        public Geometry Geometry { get; set; }
    }

    internal class AdresseApi
    {
        public string Id { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string CityCode { get; set; }
        public string Name { get; set; }
    }

    internal class Geometry
    {
        public List<float> Coordinates { get; set; }
    }

}