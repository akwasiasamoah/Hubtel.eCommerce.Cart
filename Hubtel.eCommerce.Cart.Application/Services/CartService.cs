using AutoMapper;
using Hubtel.eCommerce.Cart.Application.DTO_s;
using Hubtel.eCommerce.Cart.Infrastructre.Data;
using Hubtel.eCommerce.Cart.Infrastructre.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ECommerceDbContext _eCommerceDbContext;
        private readonly IMapper _mapper;

        public CartService(ECommerceDbContext eCommerceDbContext, IMapper mapper)
        {
            this._eCommerceDbContext = eCommerceDbContext;
            this._mapper = mapper;
        }

        public async Task<CartModelDTO> AddItem(CartModelDTO cartModel)
        {
            var existingItem = await _eCommerceDbContext.CartModels.FirstOrDefaultAsync(x => x.ItemId == cartModel.ItemId);

            if(existingItem == null)
            {
                existingItem = new CartModel
                {
                    Time = DateTime.UtcNow,
                    ItemName = cartModel.ItemName,
                    PhoneNumber = cartModel.PhoneNumber,
                    Quantity = cartModel.Quantity,
                    UnitPrice = cartModel.UnitPrice
                };
                await _eCommerceDbContext.CartModels.AddAsync(existingItem);
            }
            else
            {
                existingItem.Quantity += cartModel.Quantity;
                _eCommerceDbContext.CartModels.Update(existingItem);
            }
            await _eCommerceDbContext.SaveChangesAsync();
            var itemDTO = _mapper.Map<CartModelDTO>(existingItem);
            return itemDTO;
        }

        public async Task DeleteItem(int id)
        {
            var item = await _eCommerceDbContext.CartModels.FirstOrDefaultAsync(x => x.ItemId == id);

            if (item == null)
            {
                return;
            }

            _eCommerceDbContext.CartModels.Remove(item);
            await _eCommerceDbContext.SaveChangesAsync();
        }

        public IEnumerable<CartModelDTO> GetAllItems(FilteredCart filteredCart)
        {
            var items = _eCommerceDbContext.CartModels.AsQueryable();
            if (!string.IsNullOrEmpty(filteredCart.Item))
            {
                items = items.Where(x => x.ItemName.Equals(filteredCart.Item));
            }
            if (!string.IsNullOrEmpty(filteredCart.PhoneNumber))
            {
                items = items.Where(x => x.PhoneNumber.Equals(filteredCart.PhoneNumber));
            }
            if (!string.IsNullOrEmpty(filteredCart.Time) && DateTime.TryParse(filteredCart.Time, out var time))
            {
                items = items.Where(x => x.Time <= time);
            }
            if(filteredCart.Quantity > 0)
            {
                items = items.Where(x => x.Quantity >= filteredCart.Quantity);
            }
            var filtered = items.ToList();
            var itemsDTO = filtered.Select(x => _mapper.Map<CartModelDTO>(x)).ToList();

            return itemsDTO;
        }

        public async Task<CartModelDTO> GetItemById(int id)
        {
            var item = await _eCommerceDbContext.CartModels.FirstOrDefaultAsync(x => x.ItemId == id);

            if(item == null)
            {
                return null;
            }

            var itemDTO = _mapper.Map<CartModelDTO>(item);

            return itemDTO;
        }

        public async Task<CartModelDTO> UpdateItem(int id, CartModelDTO cartModel)
        {
            var existingItem = await _eCommerceDbContext.CartModels.FirstOrDefaultAsync(x => x.ItemId == id);

            if(existingItem == null)
            {
                return null;
            }

            _mapper.Map(cartModel, existingItem);
            _eCommerceDbContext.Update(existingItem);
            await _eCommerceDbContext.SaveChangesAsync();
            return cartModel;
        }
    }
}
