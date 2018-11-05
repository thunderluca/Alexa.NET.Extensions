using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace Alexa.NET.Extensions.Globalization
{
    public class Season
    {
        public Season(string name, int startMonth, int startDay, int endMonth, int endDay)
        {
            this.Name = name;
            this.StartMonth = startMonth;
            this.StartDay = startDay;
            this.EndMonth = endMonth;
            this.EndDay = endDay;
        }

        public string Name { get; }

        public int StartMonth { get; }

        public int StartDay { get; }

        public int EndMonth { get; }

        public int EndDay { get; }

        public static Season[] Load(string twoLetterIsoLanguage)
        {
            var assembly = typeof(Season).GetTypeInfo().Assembly;

            var resourceName = assembly.GetName().Name + $".Resources.{twoLetterIsoLanguage}.json";

            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(resourceStream))
                {
                    var json = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<Season[]>(json);
                }
            }
        }
    }
}
