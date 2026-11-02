using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pma_Lab03.Models;

namespace Pma_Lab03.Controllers
{
    public class StudentController : Controller
    {
        public List<Student> listStudent = new List<Student>();

        public StudentController()
        {
            listStudent = new List<Student>()
            {
                new Student()
                {
                    Id = 101, Name = "Hải Nam", Email = "nam@gmail.com", Password = "123456",
                    Branch = Branch.IT, Gender = Gender.Male, IsRegular = true,
                    Address = "A1-2018", DateOfBorth = new DateTime(2004, 1, 15)
                },
                new Student()
                {
                    Id = 102, Name = "Minh Tú", Email = "tu@gmail.com", Password = "123456",
                    Branch = Branch.BE, Gender = Gender.Female, IsRegular = true,
                    Address = "A1-2019", DateOfBorth = new DateTime(2004, 4, 20)
                },
                new Student()
                {
                    Id = 103, Name = "Hoàng Phong", Email = "phong@gmail.com", Password = "123456",
                    Branch = Branch.CE, Gender = Gender.Male, IsRegular = false,
                    Address = "A1-2020", DateOfBorth = new DateTime(2003, 8, 5)
                },
                new Student()
                {
                    Id = 104, Name = "Xuân Mai", Email = "mai@gmail.com", Password = "123456",
                    Branch = Branch.EE, Gender = Gender.Female, IsRegular = false,
                    Address = "A1-2021", DateOfBorth = new DateTime(2004, 12, 1)
                }
            };
        }
        [Route("Admin/Student/List", Name ="IndexStudent")]
        public IActionResult Index()
        {
            return View(listStudent);
        }


        [Route("Admin/Student/Add", Name = "CreateStudent")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem {Text = "IT", Value = "1"},
                new SelectListItem {Text = "BE", Value = "2"},
                new SelectListItem {Text = "CE", Value = "3"},
                new SelectListItem {Text = "EE", Value = "4"},
            };
            return View();
        }


        [Route("Admin/Student/Add")]
        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudent.Last<Student>().Id + 1;
                listStudent.Add(s);
                return View("Index", listStudent);
            }

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender))
                .Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" },
            };

            return View(s);
        }

    }
}
