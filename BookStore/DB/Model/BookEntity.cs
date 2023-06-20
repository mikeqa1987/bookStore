namespace BookStore.DB.Model;

public class BookEntity
{
    public int Id { get; set; }
    public List<AuthorEntity> Authors { get; set; }
    public string Naming { get; set; }
    public int YearOfIssue { get; set; }
    public PublishHouseEntity PublishingHouseEntity { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }

    public int PublishingHouseId { get; set; }
}