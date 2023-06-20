using System.Text.Json.Serialization;
using MessagePack;

namespace BookStore.DB.Model;

public class PublishHouseEntity
{
    public int Id { get; set; }
    public string Naming { get; set; }
    public string Location { get; set; }
    [JsonIgnore]
    [IgnoreMember]
    public List<BookEntity> Books { get; set; }
}