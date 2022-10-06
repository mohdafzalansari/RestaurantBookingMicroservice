using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Commands.RestaurantTable
{
    public class UpdateRestaurantgTableCommond : IRequest<UpdateRestaurantTableResponse>
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string NumberOfSits { get; set; }
    }
    public class UpdateRestaurantTableResponse { }
}
