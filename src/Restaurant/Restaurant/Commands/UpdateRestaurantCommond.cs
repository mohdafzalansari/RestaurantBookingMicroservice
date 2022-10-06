using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Commands
{
    public class UpdateRestaurantCommond : IRequest<UpdateRestaurantResponse>
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Country { get; set; }
        public int TenantId { get; set; }
    }
    public class UpdateRestaurantResponse { }
}
