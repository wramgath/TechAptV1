// Copyright © 2025 Always Active Technologies PTY Ltd


// Copyright © 2025 Always Active Technologies PTY Ltd

using System.Xml;
using System.Xml.Serialization;
using TechAptV1.Client.Interfaces;
using TechAptV1.Client.Models;

namespace TechAptV1.Client.Services
{
    public class XmlService : IXmlService
    {
        public async Task ConvertToXmlAsync(List<Number> data, Stream output)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Number>), new XmlRootAttribute("Numbers"));
                await Task.Run(() => serializer.Serialize(output, data)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
