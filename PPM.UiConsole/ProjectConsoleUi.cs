using PPM.Domain;

namespace PPM.UIConsole
{
    public class ProjectConsoleUi
    {
        // Create an instance of ProjectUi
        readonly ProjectUi projectUi = new ProjectUi();

        // Method to manage the Project module
        public void ProjectModule()
        {
            while (true)
            {
                Console.WriteLine("Project Module:");
                Console.WriteLine("1. Add Project");
                Console.WriteLine("2. List All Project");
                Console.WriteLine("3. List Project By Id");
                Console.WriteLine("4. Delete Project");
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                
                // Switch based on user's choice
                switch (choice)
                {
                    case "1":
                        projectUi.AddProjects(); // Redirect to the method to add projects
                        break;
                    case "2":
                        projectUi.ViewProjects(); // Redirect to the method to view all projects
                        break;
                    case "3":
                        projectUi.GetProjectById(); // Redirect to the method to get project details by ID
                        break;
                    case "4":
                        projectUi.DeleteProject(); // Redirect to the method to delete a project
                        break;
                    case "5":
                        return; // Return to the main menu
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
