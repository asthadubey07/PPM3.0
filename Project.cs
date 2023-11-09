namespace PPM.Model
{
    public class Project
    {
        public string? Id { get; set; } // Unique identifier for the project
        public string? Name { get; set; } // Name of the project
        public DateTime StartDate { get; set; } // Start date of the project
        public DateTime EndDate { get; set; } // End date of the project

        public List<Employee> Employees { get; set; }
        public bool IsCompleted { get; set; }
       

        public Project()
        {
            // Initialize the Employees collection.
            Employees = new List<Employee>();
            IsCompleted = false;
        }

        public Project(string id, string name, DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }
    }
}
