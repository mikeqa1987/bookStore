using Newtonsoft.Json;

namespace BookStore.DB.Model;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    
    public List<BookEntity> Books { get; set; }
}