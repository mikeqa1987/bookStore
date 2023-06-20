using BookStore.DB;
using BookStore.DB.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _bookContext;

    public BookRepository(BookContext bookContext)
    {
        _bookContext = bookContext;
    }


    public BookEntity GetSingle(int id)
    {
        return _bookContext
            .Books
            .Include(b => b.Authors)
            .Include(b => b.PublishingHouseEntity)
            .FirstOrDefault(b => b.Id == id);
    }

    public IEnumerable<BookEntity> GetAll()
    {
        return _bookContext.Books
            .Include(b => b.PublishingHouseEntity)
            .Include(b => b.Authors)
            .ToList();
    }

    public void Add(BookEntity item)
    {
        if (CheckDuplicatesAdd(item))
        {
            return;
        }

        _bookContext.Books.Add(item);
    }

    public BookEntity Update(BookEntity item)
    {
        CheckDuplicatesUpdate(item);
        _bookContext.Books.Update(item);
        
        return item;
    }

    public void Remove(BookEntity item)
    {
        _bookContext.Books.Remove(item);
    }

    private bool CheckDuplicatesAdd(BookEntity item)
    {
        var duplicate = _bookContext.Books.FirstOrDefault(b =>
            b.Naming == item.Naming && b.YearOfIssue == item.YearOfIssue);
        if (duplicate != null)
            return true;

        var finalAuthorsList = new List<AuthorEntity>();
        foreach (var newAuthor in item.Authors)
        {
            var authorsDuplicate = _bookContext.Authors
                .FirstOrDefault(b => b.Name == newAuthor.Name);
            if (authorsDuplicate != null)
                finalAuthorsList.Add(authorsDuplicate);
            else
                finalAuthorsList.Add(newAuthor);
        }

        item.Authors = finalAuthorsList;


        var pHouseDuplicate = _bookContext
            .PublishHouses
            .FirstOrDefault(b => b.Naming == item.PublishingHouseEntity.Naming && b.Location == item.PublishingHouseEntity.Location);

        if (pHouseDuplicate != null)
            item.PublishingHouseEntity = pHouseDuplicate;

        return false;
    }

    private void CheckDuplicatesUpdate(BookEntity item)
    {
        var finalAuthorsList = new List<AuthorEntity>();
        foreach (var newAuthor in item.Authors)
        {
            var authorsDuplicate = _bookContext.Authors
                .FirstOrDefault(b => b.Name == newAuthor.Name);
            if (authorsDuplicate != null)
                finalAuthorsList.Add(authorsDuplicate);
            else
                finalAuthorsList.Add(newAuthor);
        }

        item.Authors = finalAuthorsList;
        
        var pHouseDuplicate = _bookContext
            .PublishHouses
            .FirstOrDefault(b => b.Naming == item.PublishingHouseEntity.Naming && b.Location == item.PublishingHouseEntity.Location);

        if (pHouseDuplicate != null)
            item.PublishingHouseEntity = pHouseDuplicate;
    }

    public bool Save()
    {
        return (_bookContext.SaveChanges() > 0);
    }
}