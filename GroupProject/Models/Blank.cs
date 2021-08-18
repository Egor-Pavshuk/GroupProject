namespace GroupProject.Models
{
    public class Blank
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ElectiveId { get; set; }
        public Elective Elective { get; set; }
    }
}