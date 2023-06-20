using BookStore.DB.Model;

namespace BookStore.Repositories;

public interface IBookRepository
{
    BookEntity GetSingle(int id);

    IEnumerable<BookEntity> GetAll();

    void Add(BookEntity item);

    BookEntity Update(BookEntity item);

    void Remove(BookEntity item);

    bool Save();
}