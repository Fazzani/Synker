using AutoMapper;
using System.Reflection;

namespace Synker.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {

        }

        private void LoadStandardMappings()
        {
            foreach (var map in MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly()))
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            foreach (var map in MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly()))
            {
                map.CreateMappings(this);
            }
        }
    }
}
