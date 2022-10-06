using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reservation.Commands.BookingTable
{
    public class UpdateBookingTableCommond : IRequest<UpdateBookingTableResponse>
    {
        [Required]
        public Guid Id { get; set; }
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
    }
    public class UpdateBookingTableResponse { }
}
