using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.Authority.Application.DataTranslateObject
{
    public class SqCdbDto
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("children")]
        public IList<SqCdbDto> SqCdbDtos { get; set; }
    }
}
