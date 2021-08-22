using System;
using System.Collections.Generic;
using System.Linq;
using GroupProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Group group1 = new Group()
                {
                    Name = "First group"
                };
            Group group2 = new Group()
                {
                    Name = "Second group"
                };
            List<Student> students = new List<Student>()
                {
                    new Student()
                    {
                        Name = "Rick", Surname = "Wale", Group = group1
                    },
                    new Student()
                    {
                        Name = "Alex", Surname = "Kary", Group = group2
                    },
                    new Student()
                    {
                        Name = "Frank", Surname = "Lion", Group = group1
                    },
                    new Student()
                    {
                        Name = "Ann", Surname = "Fox", Group = group2
                    }
                };
            
            group1.Students = new List<Student> {students[0], students[2]};
            group2.Students = new List<Student> {students[1], students[3]};
            
            
            Elective elective1 = new Elective()
                {
                    Students = new List<Student>(){students[1], students[2]}, Name = "Biology"
                };
            Elective elective2 = new Elective()
            {
                Students = new List<Student>(){students[0], students[3]}, Name = "Programing"
            };
            students[0].Electives = new List<Elective>() {elective2};
            students[1].Electives = new List<Elective>() {elective1};
            students[2].Electives = new List<Elective>() {elective1};
            students[3].Electives = new List<Elective>() {elective2};
            

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Students.AddRange(students);
                db.Groups.AddRange(group1, group2);
                db.Electives.AddRange(elective1, elective2);
                db.SaveChanges();
            }
            
            var studentsFromDb = GetStudents(); //todo rename
            ShowStudentsInformation(studentsFromDb);
            ChangingStudentName();
            ChangingStudentGroup();
            DeletingElective();

            studentsFromDb = GetStudents(); //todo rename
            ShowStudentsInformation(studentsFromDb);
           
            using (ApplicationContext db = new ApplicationContext())
            {
                var allElectives = from e in db.Electives select e;
                db.Electives.RemoveRange(allElectives);
                
                var allStudents = from s in db.Students select s;
                db.Students.RemoveRange(allStudents);
                
                var allGroups = from g in db.Groups select g;
                db.Groups.RemoveRange(allGroups);
                
                db.SaveChanges();
                var allElements = new [] {db.Electives.ToList().Count, db.Students.ToList().Count, db.Groups.ToList().Count};
                
                Console.WriteLine($"{allElements[0]}, {allElements[1]}, {allElements[2]}");
                Console.ReadKey();
            }
                

            Console.ReadKey();
        }

        static void AddEntityToDb<T>(T entity) //todo 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Add(entity);
                db.SaveChanges();
            }
        }

        static void ChangingStudentName()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var student = db.Students.FirstOrDefault(s => s.Name == "Rick");
                if (student != null)
                {
                    student.Name = "Moran";
                    student.Surname = "Corn";
                }

                db.SaveChanges();
            }
        }

        static void ChangingStudentGroup()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var student = db.Students
                    .Include(s => s.Group)
                    .Include(s => s.Electives)
                    .FirstOrDefault(s => s.Name == "Frank");
                var firstGroup = db.Groups
                    .Include(g => g.Students).FirstOrDefault(g => g.Id == student.GroupId);
                var secondGroup = db.Groups
                    .Include(g => g.Students).FirstOrDefault(g => g.Id != student.GroupId);
                
                firstGroup?.Students.Remove(student);
                secondGroup?.Students.Add(student);
                if (student != null)
                {
                    student.Group = secondGroup;
                    db.SaveChanges();
                }
            }
        }

        static void DeletingElective()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var elective = db.Electives.FirstOrDefault(e => e.Name == "Biology");
                if (elective != null) 
                    db.Electives.Remove(elective);
                db.SaveChanges();
            }
        }
        
        static List<Student> GetStudents()
        {
            List<Student> students;
            using (ApplicationContext db = new ApplicationContext())
            {
                students = db.Students.Include(s => s.Electives)
                    .Include(s => s.Group).ToList();
            }
            return students;
        }

        static void ShowStudentsInformation(List<Student> students)
        {
            var orderedStudents = students.OrderBy(s => s.GroupId).ToList();
            int preGroupId = orderedStudents[0].GroupId;
            Console.WriteLine();
            Console.WriteLine($"Group: {orderedStudents[0].Group.Name}");
            foreach (var student in orderedStudents)
            {
                int groupId = student.GroupId;
                if (groupId != preGroupId)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Group: {student.Group.Name}");
                    
                    preGroupId = groupId;
                }
                Console.Write($"{student.Name} {student.Surname} : ");
                foreach (var elective in student.Electives)
                {
                    Console.Write($"{elective.Name} ");
                    
                }
                Console.WriteLine();

            }
            
        }
    }
}