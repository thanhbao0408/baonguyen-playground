using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace BN.CleanArchitecture.Infrastructure.Logging;

internal class TraceIdEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public const string DefaultPropertyName = "TraceId";

    private readonly string _traceIdName;

    public TraceIdEnricher(IHttpContextAccessor httpContextAccessor)
        : this(DefaultPropertyName, httpContextAccessor)
    {
    }

    public TraceIdEnricher(string traceIdName, IHttpContextAccessor httpContextAccessor)
    {
        _traceIdName = traceIdName;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        string? traceId = Activity.Current?.TraceId.ToString() ?? _httpContextAccessor?.HttpContext?.TraceIdentifier;
        LogEventProperty? versionProperty = propertyFactory.CreateProperty(_traceIdName, traceId);
        logEvent.AddPropertyIfAbsent(versionProperty);
    }
}