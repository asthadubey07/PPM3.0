using PPM.UIConsole;

namespace PPM.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an instance of MainConsoleUi to manage the overall application flow.
                MainConsoleUi mainConsoleUi = new();
                
                // Start the main menu loop to interact with the user.
                mainConsoleUi.AllMenu();
            }
            catch (Exception ex)
            {
                // Display a generic error message if an exception occurs.
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Display details of the inner exception if present.
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
