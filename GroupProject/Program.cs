using System;
using System.Collections.Generic;
using GroupProject.Models;

namespace GroupProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Group group = new Group()
                {
                    Name = "First"
                };
                Student student = new Student()
                {
                    Name = "Alo", Surname = "Dergoni", Group = group
                };
                group.Students = new List<Student> {student};
                Elective elective = new Elective()
                {
                    Students = new List<Student>(){student}, Name = "Elect"
                };
                student.Electives = new List<Elective>() {elective};

                db.Students.Add(student);
                db.Groups.Add(group);
                db.Electives.Add(elective);
                db.SaveChanges();
            }
        }
    }
}