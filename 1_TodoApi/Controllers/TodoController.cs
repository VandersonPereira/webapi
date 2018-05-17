using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _1_TodoApi.Models;
using System.Linq;

namespace _1_TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoPeople.Count() == 0)
            {
                _context.TodoPeople.Add(new TodoPerson { Name = "Vanderson", LastName = "Pereira", IsOfAge = true });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoPerson> GetAll()
        {
            return _context.TodoPeople.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var person = _context.TodoPeople.FirstOrDefault(t => t.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return new ObjectResult(person);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoPerson person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            _context.TodoPeople.Add(person);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new {id = person.Id}, person);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoPerson person)
        {
            if (person == null || person.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoPeople.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsOfAge = person.IsOfAge;
            todo.Name = person.Name;
            todo.LastName = person.LastName;

            _context.TodoPeople.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoPeople.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoPeople.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}