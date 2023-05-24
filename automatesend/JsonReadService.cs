using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automatesend
{
    internal class JsonReadService
    {
        public static void ReadJsonFile(string jsonFileIn)
        {

            dynamic jsonFile = JsonConvert.DeserializeObject(File.ReadAllText(jsonFileIn));

            Console.WriteLine($"Today's Video is: {jsonFile["today_video"]}");

            Console.WriteLine($"Today's video is about: {jsonFile["video_information"]["title"]}");

            JToken VideoTitle = jsonFile.SelectToken("video_information.creator");
            JToken VideoPurpose = jsonFile.SelectToken("video_information.purpose");

            Console.WriteLine($"Creator's Name: {VideoTitle}");
            Console.WriteLine($"Purpose of this video: {VideoPurpose}");


        }
    }
}
