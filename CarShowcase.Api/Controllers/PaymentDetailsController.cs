using CarShowcase.Entities;
using CarShowcase.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShowcase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentDetailsController(AppDbContext context) : ControllerBase
{
    // GET /api/paymentdetails
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
    {
        return await context.PaymentDetails.ToListAsync();
    }

    // GET /api/paymentdetails/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentDetails>> GetPaymentDetail(int id)
    {
        var paymentDetail = await context.PaymentDetails.FindAsync(id);
        if (paymentDetail == null)
        {
            return NotFound();
        }
        return paymentDetail;
    }

    // POST /api/paymentdetails
    [HttpPost]
    public async Task<ActionResult<PaymentDetails>> CreatePaymentDetail(PaymentDetails paymentDetail)
    {
        context.PaymentDetails.Add(paymentDetail);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPaymentDetail), new { id = paymentDetail.PaymentId }, paymentDetail);
    }

    // PUT /api/paymentdetails/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaymentDetail(int id, PaymentDetails paymentDetail)
    {
        if (id != paymentDetail.PaymentId)
        {
            return BadRequest();
        }

        context.Entry(paymentDetail).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PaymentDetailExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE /api/paymentdetails/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaymentDetail(int id)
    {
        var paymentDetail = await context.PaymentDetails.FindAsync(id);
        if (paymentDetail == null)
        {
            return NotFound();
        }

        context.PaymentDetails.Remove(paymentDetail);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool PaymentDetailExists(int id)
    {
        return context.PaymentDetails.Any(e => e.PaymentId == id);
    }
}
