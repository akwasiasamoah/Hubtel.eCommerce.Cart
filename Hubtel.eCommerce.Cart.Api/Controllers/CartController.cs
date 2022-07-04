using Hubtel.eCommerce.Cart.Application.DTO_s;
using Hubtel.eCommerce.Cart.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] FilteredCart filteredCart)
        {
            var items = await _cartService.GetAllItems(filteredCart);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = await _cartService.GetItemById(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CartModelDTO cartModelDTO)
        {
            var item = await _cartService.AddItem(cartModelDTO);

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CartModelDTO cartModelDTO)
        {
            var item = await _cartService.UpdateItem(id, cartModelDTO);

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _cartService.DeleteItem(id);

            return NoContent();
        }
    }
}
