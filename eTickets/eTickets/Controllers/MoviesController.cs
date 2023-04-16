using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class MoviesController : Controller
	{
		private readonly IMoviesService _service;
		public MoviesController(IMoviesService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var allMovies = await _service.GetAsync(n => n.Cinema);
			return View(allMovies);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Filter(string searchString)
		{
			var allMovies = await _service.GetAsync(n => n.Cinema);
			if (!string.IsNullOrEmpty(searchString))
			{
				var filterResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString));
				return View("Index", filterResult);
			}

			return View("Index", allMovies);
		}
		//Get: Movies/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var movieDetails = await _service.GetMovieByIdAsync(id);
			return View(movieDetails);
		}

		//Get: Movies/Create

		public async Task<IActionResult> Create()
		{
			var movieDropdown = await _service.GetMovieDropdownValues();
			ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(MovieVM movie)
		{
			if (!ModelState.IsValid)
			{
				var movieDropdown = await _service.GetMovieDropdownValues();

				ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

				return View(movie);
			}

			await _service.AddNewMovieAsync(movie);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await _service.GetMovieByIdAsync(id);
			if (movieDetails == null) return View("NotFound");

			var response = new MovieVM()
			{
				Id = movieDetails.Id,
				Name = movieDetails.Name,
				Description = movieDetails.Description,
				Price = movieDetails.Price,
				StartDate = movieDetails.StartDate,
				EndDate = movieDetails.EndDate,
				ImageURL = movieDetails.ImageURL,
				MovieCategory = movieDetails.MovieCategory,
				CinemaId = movieDetails.CinemaId,
				ProducerId = movieDetails.ProducerId,
				ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
			};

			var movieDropdown = await _service.GetMovieDropdownValues();

			ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
			ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

			return View(response);

		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, MovieVM movie)
		{
			if (id != movie.Id) return View("NotFound");

			if (!ModelState.IsValid)
			{
				var movieDropdown = await _service.GetMovieDropdownValues();

				ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
				ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

				return View(movie);
			}

			await _service.UpdateMovieAsync(movie);
			return RedirectToAction(nameof(Index));


		}
	}
}
