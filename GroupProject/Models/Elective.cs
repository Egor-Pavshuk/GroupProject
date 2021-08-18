using System.Collections.Generic;

namespace GroupProject.Models
{
    public class Elective
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
    }
}