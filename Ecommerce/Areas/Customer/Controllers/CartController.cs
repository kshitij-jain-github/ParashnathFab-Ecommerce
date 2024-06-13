using Ecommerce.DataAccess.Repository.IRepository;
using Ecommerce.Model;
using Ecommerce.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
    {

		private readonly IUnitOfWork _unitOfWork;
 		public ShoppingCartVM ShoppingCartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
 		}
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value,
				includeProperties: "Product"),
 			};
			foreach(var cart in ShoppingCartVM.ShoppingCartList)
			{
				double price = GetPriceBasedOnQuality(cart);
				ShoppingCartVM.OrderTotal= price*cart.Count;
			}
			return View(ShoppingCartVM);
        }

		private double GetPriceBasedOnQuality(ShoppingCart shoppingCart)

		{
			if(shoppingCart.Count<=50)
			{
				return shoppingCart.Product.Price;
			}
			else
			{
				if(shoppingCart.Count<=100)
				{
					return shoppingCart.Product.Price50;
				}
				else
				{
					return shoppingCart.Product.Price100;
				}
			}
		}
    }
}
