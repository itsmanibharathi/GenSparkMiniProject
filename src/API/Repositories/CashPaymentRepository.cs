using API.Context;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CashPaymentRepository : Repository<int, CashPayment>, ICashPaymentRepository
    {
        public CashPaymentRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }
        /// <summary>
        /// Check if the cash payment is a duplicate.
        /// </summary>
        /// <param name="entity">New Cash Payment entity </param>
        /// <returns>Retrun true If exist</returns>
        public override Task<bool> IsDuplicate(CashPayment entity)
        {
            return _context.CashPayments.AnyAsync(x => x.OrderId == entity.OrderId);
        }
    }
}
