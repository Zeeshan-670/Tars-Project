using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Semester_End.Models;

namespace Semester_End.Controllers
{
    public class ShipmentsController : Controller
    {
        private readonly TarsDeliveryContext _context;

        public ShipmentsController(TarsDeliveryContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var tarsDeliveryContext = _context.Shipments.Include(s => s.Branch).Include(s => s.PaymentNavigation).Include(s => s.ServicesTypeNavigation);
            return View(await tarsDeliveryContext.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Branch)
                .Include(s => s.PaymentNavigation)
                .Include(s => s.ServicesTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1");
            ViewData["Payment"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethod1");
            ViewData["ServicesType"] = new SelectList(_context.Services, "Id", "ServiceName");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackingId,User,TimeShipped,DateShipped,SenderAddress,DeliveryAddress,BranchId,OrderStatus,DeliveryStatus,ServicesType,Weight,Charges,Payment,SignatureRequired")] Shipment shipment)
        {
            Random rnd = new Random();
            shipment.TrackingId = rnd.Next(100, 999).ToString() + "-" + rnd.Next(100, 999).ToString() + "-" + rnd.Next(100, 999).ToString();
            shipment.DateShipped = DateTime.Now;
            shipment.TimeShipped = DateTime.Now;
            //var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipment.BranchId);
            ViewData["Payment"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethod1", shipment.Payment);
            ViewData["ServicesType"] = new SelectList(_context.Services, "Id", "ServiceName", shipment.ServicesType);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipment.BranchId);
            ViewData["Payment"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethod1", shipment.Payment);
            ViewData["ServicesType"] = new SelectList(_context.Services, "Id", "ServiceName", shipment.ServicesType);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackingId,User,TimeShipped,DateShipped,SenderAddress,DeliveryAddress,BranchId,OrderStatus,DeliveryStatus,ServicesType,Weight,Charges,Payment,SignatureRequired")] Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipment.BranchId);
            ViewData["Payment"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethod1", shipment.Payment);
            ViewData["ServicesType"] = new SelectList(_context.Services, "Id", "ServiceName", shipment.ServicesType);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Branch)
                .Include(s => s.PaymentNavigation)
                .Include(s => s.ServicesTypeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}
