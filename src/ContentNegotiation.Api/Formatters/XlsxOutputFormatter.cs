using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ContentNegotiation.Api.Formatters;

public sealed class XlsxOutputFormatter : OutputFormatter
{
    public XlsxOutputFormatter()
    {
        SupportedMediaTypes.Add(
            MediaTypeHeaderValue.Parse("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        );

        //application/octet-stream
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
    {
        var data = context.Object;

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add();

        if (data is IEnumerable<object> list)
            worksheet.Cell(1, 1).InsertTable(list);

        worksheet.Columns().AdjustToContents();

        var memoryStream = new MemoryStream();
        workbook.SaveAs(memoryStream);
        
        var httpContext = context.HttpContext;

        httpContext.Response.Headers.Add(
            HeaderNames.ContentDisposition, 
            $"attachment; filename={Guid.NewGuid()}.xlsx"
        );
        await httpContext.Response.BodyWriter.WriteAsync(memoryStream.ToArray());        
    }
}
