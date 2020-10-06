using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Entities
{

    public class ApiTagItem
    {
        [JsonPropertyName("has_synonyms")]
        public bool HasSynonyms { get; set; }

        [JsonPropertyName("is_moderator_only")]
        public bool IsModeratorOnly { get; set; }

        [JsonPropertyName("is_required")]
        public bool IsRequired { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }


        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        [JsonPropertyName("excerpt")]
        public string Excerpt { get; set; }

    }

    public class ApiTagRoot
    {
        [JsonPropertyName("items")]
        public List<ApiTagItem> Items { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        [JsonPropertyName("quota_max")]
        public int QuotaMax { get; set; }

        [JsonPropertyName("quota_remaining")]
        public int QuotaRemaining { get; set; }
    }
}
