using AutoMapper;
using System;
using System.Collections.Generic;

namespace Infraestructure.Tools
{
    public static class Mappers
    {
        public static TReturn Map<TEntry, TReturn>(TEntry item)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntry, TReturn>().IgnoreNonExistingMembers();
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var dest = mapper.Map<TEntry, TReturn>(item);

            return dest;
        }
        public static IEnumerable<TReturn> MapEnumerable<TEntry, TReturn>(IEnumerable<TEntry> item)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntry, TReturn>().IgnoreNonExistingMembers();
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            var dest = mapper.Map<IEnumerable<TEntry>, IEnumerable<TReturn>>(item);

            return dest;
        }
        public static IMappingExpression<TSource, TDestination> IgnoreNonExistingMembers<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expr)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            foreach (var property in destinationType.GetProperties())
            {
                if (sourceType.GetProperty(property.Name) != null)
                    continue;
                expr.ForMember(property.Name, opt => opt.Ignore());
            }

            return expr;
        }
    }
}
