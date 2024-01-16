using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PPM.Model;

namespace PPM.UI.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProjectController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5143/");
        }

        private bool IsUserAuthorized(string requiredRole)
        {
            var userRole = HttpContext.Session.GetString("userRole");
            return userRole == requiredRole;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync("api/Project/GetAllProjects");

                    if (response.IsSuccessStatusCode)
                    {
                        var projects = await response.Content.ReadFromJsonAsync<List<Project>>();
                        return View(projects);
                    }
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    return View();
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    // Validate model
                    if (!ModelState.IsValid)
                    {
                        return View(project);
                    }
                    var response = await _httpClient.PostAsJsonAsync(
                        "api/Project/CreateProject",
                        project
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        var createdProject = await response.Content.ReadFromJsonAsync<Project>();
                        TempData["SuccessMessage"] = "Project created successfully.";

                        // Redirect to the confirmation page with the projectId
                        return RedirectToAction(
                            "ConfirmProjectCreation",
                            new { projectId = createdProject?.ProjectId }
                        );
                        return RedirectToAction(
                            "Details",
                            new { projectId = createdProject?.ProjectId }
                        );
                    }
                    TempData["ErrorMessage"] = "Failed to Create project. Please try again.";
                    return View("Error");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync($"api/Project/GetProjectById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var projectJson = await response.Content.ReadAsStringAsync();
                        var project = JsonConvert.DeserializeObject<Project>(projectJson);

                        if (project != null)
                        {
                            return View(project);
                        }
                        else
                        {
                            return View("NotFound");
                        }
                    }
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync($"api/Project/GetProjectById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var projectJson = await response.Content.ReadAsStringAsync();
                        var project = JsonConvert.DeserializeObject<Project>(projectJson);

                        if (project != null)
                        {
                            return View(project);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("NotFound");
                    }
                    return View("Error");
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Project updateProject)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.PutAsJsonAsync(
                        $"api/Project/UpdateProject/{id}",
                        updateProject
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Project updated successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to Update project. Please try again.";
                        return View("Error");
                    }
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.GetAsync($"api/Project/GetProjectById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var projectJson = await response.Content.ReadAsStringAsync();
                        var project = JsonConvert.DeserializeObject<Project>(projectJson);

                        if (project != null)
                        {
                            return View(project);
                        }
                        else
                        {
                            return View("NotFound");
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        return View("Error");
                    }
                }

                // Redirect unauthorized users to the home page
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int projectId)
        {
            try
            {
                if (IsUserAuthorized("Manager"))
                {
                    var response = await _httpClient.DeleteAsync(
                        $"api/Project/DeleteProject/{projectId}"
                    );

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Project deleted successfully.";
                        return RedirectToAction("Index");
                    }

                    TempData["ErrorMessage"] = "Failed to Delete project. Please try again.";
                    return View("Error");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ConfirmProjectCreation(int projectId)
        {
            // Pass the projectId to the view
            ViewData["ProjectId"] = projectId;
            return View();
        }
    }
}
