using eTickets.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
	public class MovieVM
	{
		public int Id { get; set; }

		[Display(Name = "Movie name")]
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Display(Name = "Movie description ")]
		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Display(Name = "Price in $")]
		[Required(ErrorMessage = "Price is required")]
		public double Price { get; set; }

		[Display(Name = "Movie poster url")]
		[Required(ErrorMessage = "Movie poster url is required")]
		public string ImageURL { get; set; }

		[Display(Name = "Movie start date")]
		[Required(ErrorMessage = "start date id required")]
		public DateTime StartDate { get; set; }

		[Display(Name = "Movie End date")]
		[Required(ErrorMessage = "End date is required")]
		public DateTime EndDate { get; set; }

		[Display(Name = "select a category")]
		[Required(ErrorMessage = "Movie category is required")]
		public MovieCategory MovieCategory { get; set; }

		[Display(Name = "Select actor(s)")]
		[Required(ErrorMessage = "Movie actor(s) is required")]

		public List<int> ActorIds { get; set; }

		[Display(Name = "Select a cinema")]
		[Required(ErrorMessage = "Movie cinema is required")]
		public int CinemaId { get; set; }

		[Display(Name = "Select a producer")]
		[Required(ErrorMessage = "Movie producer is trequired")]
		public int ProducerId { get; set; }
	}
}
