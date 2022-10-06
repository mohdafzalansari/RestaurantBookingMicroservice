using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Commands.BookingTable
{
    public class CreateBookingTableCommond: IRequest<CreateBookingTableResponse>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime BookingDateTime { get; set; }
        [Required]
        public int NumberOfVisitors { get; set; }
        [Required]
        public Guid RestaurantTableId { get; set; }
        [Required]
        public int TenantId { get; set; }
    }
    public class CreateBookingTableResponse { }
}
