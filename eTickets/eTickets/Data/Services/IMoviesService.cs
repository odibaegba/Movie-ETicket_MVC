﻿using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
	public interface IMoviesService : IEntityBaseRepository<Movie>
	{
		Task<Movie> GetMovieByIdAsync(int id);

		Task<MovieDropdownVM> GetMovieDropdownValues();
		Task AddNewMovieAsync(MovieVM data);
		Task UpdateMovieAsync(MovieVM data);
	}
}
