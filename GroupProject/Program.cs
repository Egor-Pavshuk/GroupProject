using System;
using System.Collections.Generic;
using System.Linq;
using GroupProject.Models;

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
            students[3].Electives = new List<Elective>() {elective2};
            students[2].Electives = new List<Elective>() {elective1};

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Students.AddRange(students);
                db.Groups.AddRange(group1, group2);
                db.Electives.AddRange(elective1, elective2);
                db.SaveChanges();
            }
            
            /*using (ApplicationContext db = new ApplicationContext())
            {
                var all = from e in db.Electives select e;
                db.Electives.RemoveRange(all);
                
                var all1 = from s in db.Students select s;
                db.Students.RemoveRange(all1);
                
                var all2 = from g in db.Groups select g;
                db.Groups.RemoveRange(all2);
                
                db.SaveChanges();
                var el = new [] {db.Electives.ToList().Count, db.Students.ToList().Count, db.Groups.ToList().Count};
                
                Console.WriteLine($"{el[0]}, {el[1]}, {el[2]}");
                Console.ReadKey();
            }*/
                

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
    }
}