using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCake.Data;
using ProjectCake.Models;
using ProjectCake.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectCake
{
    
    public class OrderCakeController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        private OrderCakeRepository _orderCakeRepository;
        private ApplicationDbContext _context;

        public OrderCakeController(ApplicationDbContext context, IHostingEnvironment hosting)
        {
            _orderCakeRepository = new OrderCakeRepository(context);
            hostingEnvironment = hosting;
            _context = context;
        }

        public async Task<IActionResult> Index(SortStateOrderList sortOrderList = SortStateOrderList.NameAsc)
        {
            //var orderCakes = _orderCakeRepository.GetAll();

            //var orderCakeViewModels = new List<OrderCakeViewModel>();
            //foreach (var orderCake in orderCakes)
            //{
            //    var orderCakeViewModel = new OrderCakeViewModel
            //    {
            //        Id = orderCake.Id,
            //        Name = orderCake.Name,
            //        Surname = orderCake.Surname,
            //        Phone = orderCake.Phone,
            //        Email = orderCake.Email,
            //        PreparedOrderDate = orderCake.PreparedOrderDate,
            //        Comment = orderCake.Comment,
            //        Date = orderCake.Date
            //    };

            //    orderCakeViewModels.Add(orderCakeViewModel);
            //}

            IQueryable<OrderCake> orderCakeList = _context.OrderCake.Include(x => x.Id);

            switch (sortOrderList)
            {
                case SortStateOrderList.NameDesc:
                    orderCakeList = orderCakeList.OrderByDescending(s => s.Name);
                    break;
                case SortStateOrderList.DateAsc:
                    orderCakeList = orderCakeList.OrderBy(s => s.Date);
                    break;
                case SortStateOrderList.DateDesc:
                    orderCakeList = orderCakeList.OrderByDescending(s => s.Date);
                    break;
                case SortStateOrderList.PreparedOrderDateAsc:
                    orderCakeList = orderCakeList.OrderBy(s => s.PreparedOrderDate);
                    break;
                case SortStateOrderList.PreparedOrderDateDesc:
                    orderCakeList = orderCakeList.OrderByDescending(s => s.PreparedOrderDate);
                    break;
                case SortStateOrderList.SurnameAsc:
                    orderCakeList = orderCakeList.OrderBy(s => s.Surname);
                    break;
                case SortStateOrderList.SurnameDesc:
                    orderCakeList = orderCakeList.OrderByDescending(s => s.Surname);
                    break;
                default:
                    orderCakeList = orderCakeList.OrderBy(s => s.Name);
                    break;

            }

            IndexSortOrderListViewModel listViewModel = new IndexSortOrderListViewModel
            {
                OrderCake = new OrderCake(),
                SortOrderListViewModel = new SortOrderListViewModel(sortOrderList)
            };

            return View("OrderList", listViewModel);
        }

        [HttpGet]
        public IActionResult OrderCake()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderCake(OrderCakeViewModel orderCakeViewModel)
        {
            if (orderCakeViewModel.File != null)
            {
                var path = $"{Environment.CurrentDirectory}\\Images\\{orderCakeViewModel.File.FileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    orderCakeViewModel.File.CopyTo(stream);
                }
            }

            var orderCare = new OrderCake
            {
                Name = orderCakeViewModel.Name,
                Surname = orderCakeViewModel.Surname,
                Phone = orderCakeViewModel.Phone,
                Email = orderCakeViewModel.Email,
                PreparedOrderDate = orderCakeViewModel.PreparedOrderDate,
                Comment = orderCakeViewModel.Comment,
                Date = DateTime.Now
                //ImageUrl = orderCakeViewModel.File
            };

            _orderCakeRepository.Add(orderCare);

            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(orderCakeViewModel.Email);//Email which you are getting from form

                    message.To.Add("latvolpe@gmail.com");//Where mail will be sent

                    message.Body = "Name: " + orderCakeViewModel.Name + orderCakeViewModel.Surname + "\nFrom: " + orderCakeViewModel.Email + "\nPhone: " + orderCakeViewModel.Phone + "\nDate for prepared: " + orderCakeViewModel.PreparedOrderDate + "\nMessage: " + orderCakeViewModel.Comment + "\n" + orderCakeViewModel.File;

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                    ("latvolpe@gmail.com", "foxvolpe8");

                    smtp.EnableSsl = true;

                    smtp.Send(message);

                    ModelState.Clear();
                    ViewBag.Message = "Thank you for Contacting us ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            return RedirectToAction("Index");
        }  
    }
}