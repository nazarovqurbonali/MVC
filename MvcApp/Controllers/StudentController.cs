using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers;

public class StudentController : Controller
{
    private static List<Student> _students = new List<Student>();

    public IActionResult Index()
    {
        return View(_students);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        student.Id = _students.Count + 1;
        _students.Add(student);
        return RedirectToAction("Index");
    }
    

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        var existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);
        if (existingStudent == null)
        {
            return NotFound();
        }
        existingStudent.Name = student.Name;
        existingStudent.Email = student.Email;
        existingStudent.EnrollmentDate = student.EnrollmentDate;
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var student = _students.Find(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        _students.Remove(student);
        return RedirectToAction("Index");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}