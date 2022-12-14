using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Commands.RestaurantTable
{
    public class CreateRestaurantTableCommond: IRequest<CreateRestaurantTableResponse>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string NumberOfSits { get; set; }
        [Required]
        public int TenantId { get; set; }
    }
    public class CreateRestaurantTableResponse { }
}
