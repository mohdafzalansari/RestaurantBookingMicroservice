using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Commands
{
    public class CreateRestaurantCommond: IRequest<CreateRestaurantResponse>
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        [Required]
        public int TenantId { get; set; }
    }
    public class CreateRestaurantResponse { }
}
