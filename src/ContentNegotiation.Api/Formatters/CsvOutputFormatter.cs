using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections;
using System.Globalization;
using System.Text;

namespace ContentNegotiation.Api.Formatters;

public sealed class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type) => true;

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var httpContext = context.HttpContext;
        var obj = context.Object;

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, selectedEncoding);

        using (var csvWritter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = selectedEncoding }))
        {
            if (obj is IEnumerable list)
                await csvWritter.WriteRecordsAsync(list);
            else
                await csvWritter.WriteRecordsAsync(new[] { obj });
        }

        await httpContext.Response.BodyWriter.WriteAsync(memoryStream.ToArray());
    }
}
