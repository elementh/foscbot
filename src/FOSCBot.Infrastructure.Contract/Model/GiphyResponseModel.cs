using System.Text.Json.Serialization;

namespace FOSCBot.Infrastructure.Contract.Model
{
    public class GiphyResponseModel
    {
        [JsonPropertyName("data")]
        public DataModel? Data { get; set; }
        
        public class DataModel
        {
            [JsonPropertyName("images")]
            public ImagesModel? Images { get; set; }
            
            public class ImagesModel
            {
                [JsonPropertyName("original")]
                public OriginalModel? Original { get; set; }
                
                public class OriginalModel
                {
                    [JsonPropertyName("mp4")]
                    public string? Mp4 { get; set; }
                }
            }
        }
    }
}