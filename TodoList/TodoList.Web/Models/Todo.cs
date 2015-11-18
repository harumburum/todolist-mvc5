using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TodoList.Web.Models
{
    public class Todo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("done")]
        public bool IsDone { get; set; }

        public string UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}