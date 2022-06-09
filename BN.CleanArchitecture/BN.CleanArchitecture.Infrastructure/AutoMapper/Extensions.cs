using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.CleanArchitecture.Infrastructure.AutoMapper;
public static class Extensions
{
    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services, Type anchor)
    {
        services.AddAutoMapper(anchor);
        return services;
    }
}
