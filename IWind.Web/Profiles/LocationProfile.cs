using System.Linq;
using AutoMapper;
using IWind.Web.Models;
using IWind.Web.Services.Address;

namespace IWind.Web.Profiles
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Feature, LocationViewModel>()
                .ForMember(x => x.Id, cfg => cfg.MapFrom(x => x.Properties.Id))
                .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Properties.Name))
                .ForMember(x => x.Longitude, cfg => cfg.MapFrom(x => x.Geometry.Coordinates.First()))
                .ForMember(x => x.Latitude, cfg => cfg.MapFrom(x => x.Geometry.Coordinates.Last()));
        }
    }
}