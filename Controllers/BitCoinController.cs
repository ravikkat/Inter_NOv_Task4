using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssesmentTask2.Models;

namespace AssesmentTask2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitCoinController : ControllerBase
    {
        private readonly BookingDataContext _context;
        public BitCoinController(BookingDataContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async void PostBitCoin(Booking obj)
        {
            _context.TodoItems.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}
