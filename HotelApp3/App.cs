using HotelApp3.Controllers;
using HotelApp3.Repositories;
using HotelApp3.Services;
using HotelApp3.Utilities.Functions;
using HotelApp3.Utilities;
using HotelApp3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3
{
    public class App
    {
        public static void Run()
        {
            var dbContext = new ApplicationDbContext();

          
            dbContext.Database.EnsureCreated();

          
            var customerRepository = new CustomerRepository(dbContext);
            var roomRepository = new RoomRepository(dbContext);
            var bookingRepository = new BookingRepository(dbContext);

          
            var customerService = new CustomerService(customerRepository);
            var roomService = new RoomService(roomRepository);
            var bookingService = new BookingService(bookingRepository);

           
            var customerFunctions = new CustomerFunction(customerService);
            var roomFunctions = new RoomFunction(roomService);
            var bookingHandler = new BookingFunction(bookingService, customerService, roomService);

          
            var customerController = new CustomerController(customerFunctions);
            var roomController = new RoomController(roomFunctions);
            var bookingController = new BookingController(bookingHandler);

           
            var customerMenu = new CustomerMenu(customerController);
            var roomMenu = new RoomMenu(roomController);
            var bookingMenu = new BookingMenu(bookingController);

           
            var mainMenu = new MainMenu(customerMenu, roomMenu, bookingMenu);
            mainMenu.ShowMenu();
        }
    }
}
