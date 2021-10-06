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
    public class ShipmentDetailsController : Controller
    {
        private readonly TarsDeliveryContext _context;

        public ShipmentDetailsController(TarsDeliveryContext context)
        {
            _context = context;
        }

        // GET: ShipmentDetails
        public async Task<IActionResult> Index()
        {
            var tarsDeliveryContext = _context.ShipmentDetails.Include(s => s.Branch).Include(s => s.ChargesNavigation).Include(s => s.DeliveryAddressNavigation).Include(s => s.Tracking);
            return View(await tarsDeliveryContext.ToListAsync());
        }

        // GET: ShipmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentDetail = await _context.ShipmentDetails
                .Include(s => s.Branch)
                .Include(s => s.ChargesNavigation)
                .Include(s => s.DeliveryAddressNavigation)
                .Include(s => s.Tracking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentDetail == null)
            {
                return NotFound();
            }

            return View(shipmentDetail);
        }

        // GET: ShipmentDetails/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1");
            ViewData["Charges"] = new SelectList(_context.Shipments, "Id", "TrackingId");
            ViewData["DeliveryAddress"] = new SelectList(_context.Shipments, "Id", "TrackingId");
            ViewData["TrackingId"] = new SelectList(_context.Shipments, "Id", "TrackingId");
            return View();
        }

        // POST: ShipmentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrackingId,BranchId,DeliveryAddress,Charges")] ShipmentDetail shipmentDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipmentDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipmentDetail.BranchId);
            ViewData["Charges"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.Charges);
            ViewData["DeliveryAddress"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.DeliveryAddress);
            ViewData["TrackingId"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.TrackingId);
            return View(shipmentDetail);
        }

        // GET: ShipmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentDetail = await _context.ShipmentDetails.FindAsync(id);
            if (shipmentDetail == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipmentDetail.BranchId);
            ViewData["Charges"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.Charges);
            ViewData["DeliveryAddress"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.DeliveryAddress);
            ViewData["TrackingId"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.TrackingId);
            return View(shipmentDetail);
        }

        // POST: ShipmentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrackingId,BranchId,DeliveryAddress,Charges")] ShipmentDetail shipmentDetail)
        {
            if (id != shipmentDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentDetailExists(shipmentDetail.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Branch1", shipmentDetail.BranchId);
            ViewData["Charges"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.Charges);
            ViewData["DeliveryAddress"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.DeliveryAddress);
            ViewData["TrackingId"] = new SelectList(_context.Shipments, "Id", "TrackingId", shipmentDetail.TrackingId);
            return View(shipmentDetail);
        }

        // GET: ShipmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentDetail = await _context.ShipmentDetails
                .Include(s => s.Branch)
                .Include(s => s.ChargesNavigation)
                .Include(s => s.DeliveryAddressNavigation)
                .Include(s => s.Tracking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentDetail == null)
            {
                return NotFound();
            }

            return View(shipmentDetail);
        }

        // POST: ShipmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipmentDetail = await _context.ShipmentDetails.FindAsync(id);
            _context.ShipmentDetails.Remove(shipmentDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentDetailExists(int id)
        {
            return _context.ShipmentDetails.Any(e => e.Id == id);
        }
    }
}
