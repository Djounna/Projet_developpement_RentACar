using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System.Net.Http.Json;
using System.Text;

namespace FrontEnd_MVC.Controllers
{
    public class LoueurController : Controller
    {
        //var errors = ModelState.Values.SelectMany(v => v.Errors); 
        private async Task<List<T>> GetRequest<T>(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
                }
            }
        }
        private async Task<T> GetRequestUnique<T>(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(apiResponse);

                }
            }
        }
        private async Task<IActionResult> PostRequest<T>(string chemin, T postObject)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(chemin, postObject))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
        private async Task<ActionResult> PutRequest<T>(string chemin, T putObject)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(chemin, putObject))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
                return Ok();
            }
        }
        /* // En attente
        private async Task<ActionResult> DeleteRequest(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(chemin))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return Ok();
        }
        */
        private async Task<ActionResult> DeleteRequest(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(chemin))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return Ok();
        }
        private async Task<IEnumerable<SelectListItem>> GetEnumerableList(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    IEnumerable<SelectListItem> lst = JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(apiResponse);
                    return lst;
                }
            }
        }
        public IActionResult HomeLoueur()
        {
            return View();
        }

        // EN cours Corentin, test multiples views        
        public ActionResult IndexViewBag()
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewBag.Teachers = GetTeachers();
            ViewBag.Students = GetStudents();
            return View();
        }
        private List<Teacher> GetTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            teachers.Add(new Teacher { TeacherId = 1, Code = "TT", Name = "Tejas Trivedi" });
            teachers.Add(new Teacher { TeacherId = 2, Code = "JT", Name = "Jignesh Trivedi" });
            teachers.Add(new Teacher { TeacherId = 3, Code = "RT", Name = "Rakesh Trivedi" });
            return teachers;
        }
        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student { StudentId = 1, Code = "L0001", Name = "Amit Gupta", EnrollmentNo = "201404150001" });
            students.Add(new Student { StudentId = 2, Code = "L0002", Name = "Chetan Gujjar", EnrollmentNo = "201404150002" });
            students.Add(new Student { StudentId = 3, Code = "L0003", Name = "Bhavin Patel", EnrollmentNo = "201404150003" });
            return students;
        }

        // ******************************************************** Notoriete**************************************************************************************
        [HttpGet]
        public async Task<ActionResult> AfficheNotoriete() //OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotoriete/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<ActionResult> AfficheNotorieteActive()//OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteActif/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<ActionResult> AfficheNotorieteInactive()//OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteInactif/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult CreateNotoriete()//OK Antoine
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostNotoriete([Bind("Libelle,CoefficientMultiplicateur")] Notoriete notoriete)//OK Antoine
        {
            if (ModelState.IsValid)
            {
               await PostRequest("https://localhost:7204/api/Loueur/PostNotoriete/", notoriete);                                     
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));

        }
        public async Task<IActionResult> EditNotoriete(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));
        }
        public async Task<IActionResult> UpdateNotoriete([Bind("Idnotoriete,Libelle,CoefficientMultiplicateur,Inactif")] Notoriete notoriete)
        {
            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdateNotoriete/", notoriete);              
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }

        [HttpPost, ActionName("Disable")]
        public async Task<ActionResult> DesactiverNotoriete(int id)
        {
            Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            not.Inactif = true;

            await UpdateNotoriete(not);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        [HttpPost, ActionName("Activate")]
        public async Task<ActionResult> ActiverNotoriete(int id)
        {
            Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            not.Inactif = false;

            await UpdateNotoriete(not);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }

        public async Task<IActionResult> deleteNotoriete(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }         
                return View(await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));             
        }
        // GET
        public async Task<IActionResult> Disable(int? id)
        { 
            var notoriety = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            return View(notoriety);
        }
        public async Task<IActionResult> Activate(int? id)
        {
            var notoriety = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            return View(notoriety);
        }
        /* En attente
        [HttpPost, ActionName("deleteNotoriete")]
        public async Task<ActionResult> removeNotoriete(int id)//OK Antoine
        {
                 await DeleteRequest("https://localhost:7204/api/Loueur/DeleteNotoriete/" + id);
                 return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        */
        [HttpPost, ActionName("deleteNotoriete")]  // Corentin, en cours, à voir avec ANtoine
        public async Task<ActionResult> removeNotoriete(int id)//OK Antoine
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteNotoriete/" + id);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }



        // ******************************************************** PAYS ****************************************************************************************
        [HttpGet]
        public async Task<List<Pays>> GetAllPays()//Ok Antoine
        {
            try
            {
                return await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> AffichePays()//Ok Antoine
        {
            try
            {
                List<Pays> lstPays = await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/");
                foreach( Pays p in lstPays)
                {
                    p.Ville = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetAllVilleByPays/" + p.Idpays);
                }
                return View(lstPays);
            }
           catch(Exception ex)
            {
                throw ex;
            }          
        }   
        public IActionResult CreatePays()//Ok Antoine
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostPays([Bind("Nom")] Pays pays)//Ok Antoine
        {
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostPays/", pays);
            }
            return RedirectToAction(nameof(AffichePays));
        }
        public async Task<IActionResult> EditPays(int? id)//Ok Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }     
        public async Task<IActionResult> UptadePays([Bind("Idpays,Nom")] Pays pays)//OK Antoine
        {
            {
                if (ModelState.IsValid)
                {
                    await PutRequest("https://localhost:7204/api/Loueur/UptadePays/", pays);
                }
                return RedirectToAction(nameof(AffichePays));
            }
        }       
        public async Task<IActionResult> deletePays(int? id)//Ok Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }
        [HttpPost, ActionName("deletePays")]
        public async Task<ActionResult> removePays(int id)//Ok Antoine
        {
            var response = await DeleteRequest("https://localhost:7204/api/Loueur/DeletePays/" + id);
          
             return RedirectToAction(nameof(AffichePays));
            //return View("DeletePays",await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }

        // ******************************************************** Ville ****************************************************************************************
        [HttpGet]
        public async Task<IActionResult> AfficheVille()//Ok Antoine
        {            
            List<Ville> lst = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetVille/");       
            foreach (var item in lst)
            {
                item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);               
            }
            return View(lst);
        }    
        public async Task<IActionResult> CreateVille()//OK Antoine
        {
           Ville ville = new(); 
           ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
           return View(ville);
        }
        [HttpPost]
        public async Task<IActionResult> PostVille([Bind("Idpays,Nom")] Ville ville)//OK Antoine
        {
            ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostVille/", ville);
            }
            Thread.Sleep(5000);
            return RedirectToAction(nameof(AfficheVille));     
        }
        public async Task<IActionResult> EditVille(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id));
        }
        public async Task<IActionResult> UptadeVille([Bind("Idville, Idpays, Nom")] Ville ville)//OK Antoine
        {
             ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
             ModelState.Remove("IdpaysNavigation");
             ModelState.Remove("ListPays");
             if (ModelState.IsValid)
             {
                await PutRequest("https://localhost:7204/api/Loueur/UptadeVille/", ville);
             }
            
             return RedirectToAction(nameof(AfficheVille));            
        }
        public async Task<IActionResult> deleteVille(int? id)//OK Ville
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id));
        }
        [HttpPost, ActionName("deleteVille")]
        public async Task<ActionResult> removeVille(int id)
        {           
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVille/" + id);
            return RedirectToAction(nameof(AfficheVille));
        }
    }
}
