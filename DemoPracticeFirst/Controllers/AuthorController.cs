using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoPracticeFirst.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoPracticeFirst.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private static List<Author> _authors = new List<Author>();
        private readonly IConfiguration _configuration;
        private readonly ITransientService _transientService;

        public AuthorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpGet("units")]
        //public IActionResult GetUnitsTransient() {
        //    return Ok(_transientService.GetUnits());
        //}

        //[HttpGet]
        //public IActionResult getConfigDetails()
        //{
        //    return Ok(new
        //    {
        //        defaultValue = _configuration.GetValue<string>("Logging:LogLevel:Default")
        //    }); 
        //}
        // GET: api/values
        [HttpGet("book")]
        public IActionResult SearchByBookName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return NotFound("Book was not found");
            }
            Author existingAuthor;
            //var Author = _authors.FirstOrDefault(a => a.books.Contains(a.books.FirstOrDefault(b=>b.Name == name)));
            foreach(Author author in _authors)
            {
                if(author.books.FirstOrDefault(b=>b.Name == name) != null)
                {
                    existingAuthor = author;
                    return Ok(author);
                }
            }
            return NotFound("Author not found");
        }

        // GET api/values/5
        [HttpPut("book/update/{id}")]
        public IActionResult UpdateBook(int id, Books book)
        {
            Books existingBook;
            foreach(Author author in _authors)
            {
                if(author.books.FirstOrDefault(b=>b.Id == id) != null)
                {
                    existingBook = author.books.First(b => b.Id == id);
                    //updating book
                    existingBook.Name = book.Name;
                    existingBook.Type = book.Type;
                    return Ok(book);
                }
            }
            return NotFound("Book not found");
        }

        [HttpPost("api/Author/create")]
        public IActionResult CreateAuthor(Author author)
        {
            if(author.Id == null)
            {
                return NotFound("Not valid id");
            }
            _authors.Add(author);
            return Created($"api/author/{author.Id}", author);
        }

        // PUT api/values/5
        [HttpPost("Book/create")]
        public IActionResult AddBooks(int id, [FromBody]List<Books> books)
        {
            //find author
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return NotFound("Author not found");
            author.books.AddRange(books);
            return Ok(books);
        }
    }
}

