using AutoMapper;
using Hubtel.eCommerce.Cart.Application.DTO_s;
using Hubtel.eCommerce.Cart.Infrastructre.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hubtel.eCommerce.Cart.Application.Profiles
{
    public class CartModelProfile : Profile
    {
        public CartModelProfile()
        {
            CreateMap<CartModel, CartModelDTO>().ReverseMap();
        }
    }
}
