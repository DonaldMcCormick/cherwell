using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Geometric_Layouts.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Geometric_Layouts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleController : ControllerBase
    {
        private readonly TriangleContext _context;

        public TriangleController(TriangleContext context)
        {
            _context = context;

            if (_context.Triangles.Count() == 0)
            {
                // Create a new Triangle if collection is empty,
                // which means you can't delete all Triangle.
                _context.Triangles.Add(new Triangle { TriangleLocation = "None", X1 = 0, X2 = 0, X3 = 0, Y1 = 0, Y2 = 0, Y3 = 0 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Triangle>> GetAll()
        {
            return _context.Triangles.ToList();
        }

        [HttpGet("{TriangleLocation}", Name = "GetByLocation")]
        public ActionResult<Triangle> GetByLocation(string TriangleLocation)
        {
            var item = _context.Triangles.Find(TriangleLocation);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // mine
        [HttpGet("{TriangleCoordinates}", Name = "GetByCoordinates")]
        public ActionResult<Triangle> GetByCoordinates(int X1, int X2, int X3, int Y1, int Y2, int Y3)
        {
            var item = _context.Triangles.Find(X1, X2, X3, Y1, Y2, Y3);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult CreateByCoordinates(Triangle newTriangle)
        {
            char[] letters = {'@', 'A', 'B', 'C', 'D', 'E', 'F' };

            if (newTriangle.Y1 == newTriangle.Y3)
            {
                // bottom triangle
                int answer = newTriangle.Y1 % 10;
                string myTriangleLocation = letters[answer].ToString();
                answer = newTriangle.X3 % 10;
                myTriangleLocation += answer.ToString();
            }
            else
            {
                // top triangle
                int answer = newTriangle.Y3 % 10;
                string myTriangleLocation = letters[answer].ToString();
                answer = (newTriangle.X3 % 10) * 2;
                myTriangleLocation += answer.ToString();
            }

            _context.Triangles.Add(newTriangle);
            _context.SaveChanges();

            return CreatedAtRoute("GetTriangle", new { id = newTriangle.Id }, newTriangle);
            //return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPost]
        public IActionResult CreateByTriangleLocation(Triangle newTriangle)
        {
            char[] letters = { '@', 'A', 'B', 'C', 'D', 'E', 'F' };

            string letter = newTriangle.TriangleLocation.Substring(0, 1);
            string number = newTriangle.TriangleLocation.Substring(1);

            int yMin = 0;
            int yMax = 0;

            // get the y axis
            switch (letter)
            {
                case "A":
                    yMax = 10;
                    break;
                case "B":
                    yMin = 10;
                    yMax = 20;
                    break;
                case "C":
                    yMin = 20;
                    yMax = 30;
                    break;
                case "D":
                    yMin = 30;
                    yMax = 40;
                    break;
                case "E":
                    yMin = 40;
                    yMax = 50;
                    break;
                default:
                    yMin = 50;
                    yMax = 60;
                    break;
            }

            // Get X Values
            int num = 0;
            int.TryParse(number, out num);
            int xMin = 0;
            int xMax = 0;

            if (num % 2 == 0)
            {
                // we have an even number. Top Triangle.
                xMax = num / 2 * 10;
                xMin = xMax - 10;
                newTriangle.X1 = xMin;
                newTriangle.X2 = xMax;
                newTriangle.X3 = xMax;

                newTriangle.Y1 = yMin;
                newTriangle.Y2 = yMin;
                newTriangle.Y3 = yMax;
            }
            else
            {
                // we have an odd number. Bottom Triangle.
                xMax = (num+1) / 2 * 10;
                xMin = xMax - 10;
                newTriangle.X1 = xMin;
                newTriangle.X2 = xMin;
                newTriangle.X3 = xMax;

                newTriangle.Y1 = yMin;
                newTriangle.Y2 = yMax;
                newTriangle.Y3 = yMax;
            }
            _context.Triangles.Add(newTriangle);
            _context.SaveChanges();

            return CreatedAtRoute("GetTriangle", new { id = newTriangle.Id }, newTriangle);
            //return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }


        [HttpGet("{TriangleLocation}", Name = "GetTriangleLocation")]
        public ActionResult<Triangle> GetById(string TriangleLocation)
        {
            var item = _context.Triangles.Find(TriangleLocation);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, Triangle item)
        {
            var tri = _context.Triangles.Find(id);
            if (tri == null)
            {
                return NotFound();
            }
            
            tri.X1 = item.X1;
            tri.X2 = item.X2;
            tri.X3 = item.X3;
            tri.Y1 = item.Y1;
            tri.Y2 = item.Y2;
            tri.Y3 = item.Y3;
            tri.TriangleLocation = item.TriangleLocation;

            _context.Triangles.Update(tri);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tri = _context.Triangles.Find(id);
            if (tri == null)
            {
                return NotFound();
            }

            _context.Triangles.Remove(tri);
            _context.SaveChanges();
            return NoContent();
        }





    }
}
