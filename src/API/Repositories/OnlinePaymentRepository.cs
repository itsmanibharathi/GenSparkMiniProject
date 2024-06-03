using API.Context;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Repositories
{
    [ExcludeFromCodeCoverage]
    public class OnlinePaymentRepository : Repository<int, OnlinePayment>, IOnlinePaymentRepository
    {
        public OnlinePaymentRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }
        public override Task<bool> IsDuplicate(OnlinePayment entity)
        {
            return _context.OnlinePayments.AnyAsync(x => x.PaymentRef == entity.PaymentRef);
        }
    }
}
