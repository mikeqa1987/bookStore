using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DB;
using BookStore.DB.Model;
using BookStore.Business.Model;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        private BookContext _bookDb;

        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            List<BookEntity> booksEntities = _bookRepository.GetAll().ToList();

            var finalSet = _mapper.Map<List<Book>>(booksEntities);
            

            return Ok(finalSet);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Book> GetBook(int id)
        {
            var bookEntity = _bookRepository.GetSingle(id);

            if (bookEntity == null)
                return NotFound();

            Book book = _mapper.Map<Book>(bookEntity);
        
            return Ok(book);
        }
        
        [HttpPost]
        public ActionResult<Book> AddBook([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }

            BookEntity bookEntity = _mapper.Map<BookEntity>(newBook);
            
            _bookRepository.Add(bookEntity);

            if (!_bookRepository.Save())
            {
                throw new Exception("Failed to save new book item");
            }

            Book addedBook = _mapper.Map<Book>(bookEntity);

            return Ok(addedBook);
        }

        [HttpPut]
        public ActionResult<Book> UpdateBook([FromBody] Book bookToUpdate)
        {
            if (bookToUpdate == null)
                return BadRequest();

            BookEntity bookEntity = _bookRepository.GetSingle(bookToUpdate.Id);

            if (bookEntity == null)
                return NotFound();

            _mapper.Map(bookToUpdate, bookEntity);
            BookEntity finalUpdatedEntity = _bookRepository.Update(bookEntity);

            if (!_bookRepository.Save())
            {
                throw new Exception("Failed to update a book item"); 
            }

            var finalUpdatedBook = _mapper.Map<Book>(finalUpdatedEntity);

            return Ok(finalUpdatedBook);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<int> RemoveBook(int id)
        {
            BookEntity? bookEntity = _bookRepository.GetSingle(id);

            if (bookEntity is null)
                return NotFound();
            
            _bookRepository.Remove(bookEntity);

            if (!_bookRepository.Save())
            {
                throw new Exception($"Failed to remove book with id {id}"); 
            }

            return Ok(id);
        }
    }
}