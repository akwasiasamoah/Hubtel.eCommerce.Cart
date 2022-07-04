using Hubtel.eCommerce.Cart.Application.DTO_s;
using Hubtel.eCommerce.Cart.Infrastructre.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hubtel.eCommerce.Cart.Application.Services
{
    public interface ICartService
    {
        IEnumerable<CartModelDTO> GetAllItems(FilteredCart filteredCart);
        Task<CartModelDTO> GetItemById(int id);
        Task<CartModelDTO> AddItem(CartModelDTO cartModel);
        Task DeleteItem(int id);
        Task<CartModelDTO> UpdateItem(int id, CartModelDTO cartModel);

    }
}
