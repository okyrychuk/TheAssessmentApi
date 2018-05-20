using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAssessmentApi
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText, new JsonSerializerSettings());
        }

        public static responseModel DeserializeToModel<responseModel>(string jsonText)
        {
            return (responseModel)Deserialize<responseModel>(jsonText);
        }

        public static string Serialize<T>(T jsonText)
        {
            return JsonConvert.SerializeObject(jsonText);
        }
    }
}
