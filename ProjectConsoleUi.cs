using PPM.Domain;

namespace PPM.UIConsole
{
    public class ProjectConsoleUi
    {
        ProjectUi projectUi = new ProjectUi();

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
                switch (choice)
                {
                    case "1":
                        projectUi.AddProjects();
                        break;
                    case "2":
                        projectUi.ViewProjects();
                        break;
                    case "3":
                        projectUi.GetProjectById();
                        break;
                    case "4":
                        projectUi.DeleteProject();
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
