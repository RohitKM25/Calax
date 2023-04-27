using Newtonsoft.Json;

namespace Calax.UWP.Models
{
    public static class JsonReducer
    {
        public static Deducer ReduceJson(string json)
        {
            return new Deducer(JsonConvert.DeserializeObject<SlabSet>(json));
        }
    }
}
