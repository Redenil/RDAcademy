using RDAcademy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RDAcademy.Formatters
{
    public class CsvTextMediaFormatter : MediaTypeFormatter
    {
        public CsvTextMediaFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable<Individual>).IsAssignableFrom(type) || typeof(Individual).IsAssignableFrom(type);
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            using (stream)
            {
                Encoding encoding = SelectCharacterEncoding(content.Headers);
                using (var writer = new StreamWriter(stream, encoding))
                {
                    var individuals = value as IEnumerable<Individual>;
                    if (individuals != null)
                    {
                        foreach (var individual in individuals)
                        {
                            await writer.WriteLineAsync(String.Format("{0,-10};{1,-10};{2,-10}", individual.Id, individual.FirstName, individual.LastName));
                        }
                        await writer.FlushAsync();
                    }
                }
            }
        }
    }
}

