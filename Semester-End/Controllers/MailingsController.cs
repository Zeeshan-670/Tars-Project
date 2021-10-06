using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Semester_End.Models;

namespace Semester_End.Controllers
{
    public class MailingsController : Controller
    {
        private readonly TarsDeliveryContext _context;

        public MailingsController(TarsDeliveryContext context)
        {
            _context = context;
        }

        // GET: Mailings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mailings.ToListAsync());
        }

        // GET: Mailings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailing = await _context.Mailings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mailing == null)
            {
                return NotFound();
            }

            return View(mailing);
        }

        // GET: Mailings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mailings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,To,Subject,Text")] Mailing mailing)
        {
            if (ModelState.IsValid)
            {
                this.SendMail(mailing);
                _context.Add(mailing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mailing);
        }

        private void SendMail(Mailing mailing)
        {
            string To = mailing.To;
            string subject = mailing.Subject;
            string Text = mailing.Text;
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(To);
            mailMessage.Subject = subject;
            mailMessage.Body = mailing.Text;
            mailMessage.From = new MailAddress("tarsdelivery20@gmail.com");

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("tarsdelivery20@gmail.com", "muneerzeeshan670");
            client.Send(mailMessage);
            {
                ModelState.Clear();
            }
        }
        // GET: Mailings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailing = await _context.Mailings.FindAsync(id);
            if (mailing == null)
            {
                return NotFound();
            }
            return View(mailing);
        }

        // POST: Mailings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,To,Subject,Text")] Mailing mailing)
        {
            if (id != mailing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mailing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MailingExists(mailing.Id))
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
            return View(mailing);
        }

        // GET: Mailings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mailing = await _context.Mailings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mailing == null)
            {
                return NotFound();
            }

            return View(mailing);
        }

        // POST: Mailings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mailing = await _context.Mailings.FindAsync(id);
            _context.Mailings.Remove(mailing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MailingExists(int id)
        {
            return _context.Mailings.Any(e => e.Id == id);
        }
    }
}
