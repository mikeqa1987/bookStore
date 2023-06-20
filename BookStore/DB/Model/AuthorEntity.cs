
using System.Text.Json.Serialization;

namespace BookStore.DB.Model;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    
    public List<BookEntity> Books { get; set; }
}