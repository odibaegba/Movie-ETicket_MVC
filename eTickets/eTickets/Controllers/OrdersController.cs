using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IMoviesService _moviesService;
		private readonly ShoppingCart _shoppingcart;
		private readonly IOrderService _orderService;

		public OrdersController(IMoviesService moviesService, ShoppingCart shoppingcart, IOrderService orderService)
		{
			_moviesService = moviesService;
			_shoppingcart = shoppingcart;
			_orderService = orderService;
		}

		public IActionResult ShoppingCart()
		{
			var items = _shoppingcart.GetShoppingCartItems();
			_shoppingcart.ShoppingCartItems = items;
			var response = new ShoppingCartVM()
			{
				ShoppingCart = _shoppingcart,
				ShoppingCartTotal = _shoppingcart.GetShoppingCartTotal(),
			};
			return View(response);
		}

		public async Task<IActionResult> AddToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingcart.AddItemToCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);
			if (item != null)
			{
				_shoppingcart.RemoveItemFromCart(item);
			}
			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingcart.GetShoppingCartItems();
			string userId = "";
			string userEmailAddress = "";

			await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
			await _shoppingcart.ClearShoppingCartAsync();
			return View("OrderCompleted");
		}
	}
}
