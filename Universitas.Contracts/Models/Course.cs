namespace Universitas.Contracts.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Course(string name, int? id = null)
        {
            this.Name = name;
            if (id != null)
            {
                this.Id = id.Value;
            }
        }
    }
}
