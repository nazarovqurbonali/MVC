using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;

namespace MvcApp.Controllers;

public class MentorController:Controller
{
    private static List<Mentor> _mentors = new List<Mentor>();

    public IActionResult Index()
    {
        return View(_mentors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Mentor mentor)
    {
        mentor.Id = _mentors.Count + 1;
        _mentors.Add(mentor);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(Mentor mentor)
    {
        var existingStudent = _mentors.Find(s => s.Id == mentor.Id);
        if (existingStudent == null)
        {
            return NotFound();
        }
        existingStudent.Name = mentor.Name;
        existingStudent.Email = mentor.Email;
        existingStudent.EnrollmentDate = mentor.EnrollmentDate;
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var mentor = _mentors.Find(s => s.Id == id);
        if (mentor == null)
        {
            return NotFound();
        }
        _mentors.Remove(mentor);
        return RedirectToAction("Index");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}