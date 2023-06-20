namespace BookStore.Business.Model;

public class Book
{
    public int Id { get; set; }
    public List<string> Authors { get; set; }
    public string Naming { get; set; }
    public int YearOfIssue { get; set; }
    public string PublishingHouse { get; set; }
    public string PublishLocation { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
}