using PPM.Domain;
using PPM.Model;

namespace PPM.UIConsole
{
    public class ProjectUi
    {
        ProjectRepository projectManager = new();
        ProjectEmployeeUi projectEmployeeUi = new ProjectEmployeeUi();

        //add project
        public void AddProjects()
        {
            Console.Write("How Many Project Do You Want To Add \n Please Enter The Number");
            if (int.TryParse(Console.ReadLine(), out int projectCount) && projectCount > 0)
            {
                for (int i = 0; i < projectCount; i++)
                {
                    Project project = new Project();
                    Console.WriteLine("ADD PROJECT:");
                    while (true)
                    {
                        Console.Write("Enter Project ID: ");
                        string id = Console.ReadLine();
                        if (
                            projectManager.ValidateProjectId(id)
                            && projectManager.IsProjectIdUnique(id)
                        )
                        {
                            project.Id = id;
                            break;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        if (!projectManager.ValidateProjectId(id))
                        {
                            Console.WriteLine("Invalid Project ID. Project ID Can not Be Empty");
                        }
                        else
                        {
                            Console.WriteLine(
                                "Project ID is not unique. Please enter a unique ID."
                            );
                        }
                        Console.ResetColor();
                    }

                    while (true)
                    {
                        Console.Write("Enter Project Name: ");
                        string Name = Console.ReadLine();

                        if (projectManager.ValidateProjectName(Name))
                        {
                            project.Name = Name;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Project Name cannot be empty.");
                            Console.ResetColor();
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter Start Date (YYYY-MM-DD): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        {
                            project.StartDate = startDate;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "Invalid Start Date format. Please enter a valid date (YYYY-MM-DD)."
                            );
                            Console.ResetColor();
                        }
                    }

                    while (true)
                    {
                        Console.Write("Enter End Date (YYYY-MM-DD): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                        {
                            if (endDate <= project.StartDate)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("End Date should be after Start Date.");
                                Console.ResetColor();
                                continue; // Redirect to the End Date input
                            }

                            project.EndDate = endDate;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(
                                "Invalid End Date format. Please enter a valid date (YYYY-MM-DD)."
                            );
                            Console.ResetColor();
                        }
                    }
                    projectManager.Add(project);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Project Added sucscessfully.");
                    Console.ResetColor();

                    // Ask if the user wants to add employees to this project
                    Console.Write("Do you want to add employees to this project? (Y/N): ");
                    string addEmployeesToProjectResponse = Console.ReadLine();

                    if (addEmployeesToProjectResponse == "Y"|| addEmployeesToProjectResponse == "y")
                    {
                        projectEmployeeUi.AddEmployeeToProject(); // Redirect to the method to add employees to the project
                        break;
                    }
                    else
                    {
                        Console.WriteLine("ThankYou For Your Choise!");
                        return;
                    }
                }
            }
        }

        // view projects
        public void ViewProjects()
        {
            var projects = projectManager.ListAll();

            if (projects.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No projects found.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("List of Projects:");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("--------------------------------------------");
                Console.ResetColor();
                Console.ResetColor();

                foreach (var project in projects)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Project ID   : {project.Id}");
                    Console.WriteLine($"Project Name : {project.Name}");
                    Console.WriteLine($"Start Date   : {project.StartDate}");
                    Console.WriteLine($"End Date     : {project.EndDate}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        // Get project by Id
        public void GetProjectById()
        {
            Project project1 = new();
            while (true)
            {
                // Get Project ID from the user
                Console.Write("Enter Project ID to retrieve The Project Details: ");
                string projectId = Console.ReadLine();

                if (projectManager.ValidateProjectId(projectId))
                {
                    project1.Id = projectId;
                    Project project = projectManager.ListByID(projectId);
                    if (project != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Project ID   : {project.Id}");
                        Console.WriteLine($"Project Name : {project.Name}");
                        Console.WriteLine($"Start Date   : {project.StartDate}");
                        Console.WriteLine($"End Date     : {project.EndDate}");
                        Console.WriteLine();
                        Console.ResetColor();
                        Console.Write("Do you want to view employees of this project \n (Y/N): ");
                        string viewEmployeesResponse = Console.ReadLine();

                        if (viewEmployeesResponse == "Y" || viewEmployeesResponse == "y")
                        {
                            projectEmployeeUi.ViewProjectEmployeesByRole(); // Redirect to the method to view employees of the project
                        }

                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Project not found: project ID you Provide Is Not Exist..");
                        Console.ResetColor();
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Project ID is required.");
                    Console.ResetColor();
                }
            }
        }

        // Delete Project
        public void DeleteProject()
        {
            Project project = new Project();
            while (true)
            {
                // Get Project ID from the user and perform validation
                Console.Write("Enter Project ID to delete: ");
                string projectId = Console.ReadLine();
                if (projectManager.ValidateProjectId(projectId))
                {
                    project.Id = projectId;
                    Project projectToDelete = projectManager.ListByID(projectId);

                    if (projectToDelete != null)
                    {
                        // bool result = ProjectRepository.DeleteProject(projectId);
projectManager.Delete(projectId);
                        // if (result)
                        // {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Project deleted successfully.");
                        //     Console.ResetColor();
                        //     break;
                        // }
                        // else
                        // {
                        //     Console.ForegroundColor = ConsoleColor.Red;
                        //     Console.WriteLine("Failed to delete the Project.");
                        //     Console.ResetColor();
                        // }
                        // break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Project not found:Project ID you Provide Is Not Exist.");
                        Console.ResetColor();
                       return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Project ID is required.");
                    Console.ResetColor();
                }
            }
        }
    }
}
