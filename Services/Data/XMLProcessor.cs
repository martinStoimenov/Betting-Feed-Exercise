using System;
using System.Reactive.Linq;
using System.Xml.Serialization;
using Services.Interfaces;
using UltraPlayBettingSystemExercise.ViewModels;

namespace Services.Data
{
    public class XMLProcessor : IXMLProcessor
    {
        private readonly string url = "https://sports.ultraplay.net/sportsxml?clientKey=9C5E796D-4D54-42FD-A535-D7E77906541A&sportId=2357&days=7";

        public async Task<SportViewModel> GetSportDataFeedAsync()
        {
            var root = new RootViewModel();
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(RootViewModel));

                var result = await new HttpClient().GetAsync(url);

                var strResult = await result.Content.ReadAsStringAsync();

                using TextReader reader = new StringReader(strResult);

                root = (RootViewModel)xmlSerializer.Deserialize(reader);

                root.Sport.CreatedDate = root.CreateDate;
            }
            catch (Exception ex)
            {
                throw;
            }
            return root.Sport;
        }
    }
}
