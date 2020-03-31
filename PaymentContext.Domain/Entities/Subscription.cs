using System;
using System.Linq;
using System.Collections.Generic;
using Flunt.Validations;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments; 
        public Subscription( DateTime? expirateDate)
        {
            CreaDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpirateDate = expirateDate;
            Active = true;
            _payments =  new List<Payment>();
        }

        public DateTime CreaDate { get; private set;}
        public DateTime LastUpdateDate { get; private set;}
        public DateTime? ExpirateDate { get; private set;}
        public bool Active {get; private set;}

        public IReadOnlyCollection<Payment> Payments {get {return _payments.ToArray();}}

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate,"Subscription.Payments","A data do pagamento deve ser futura.")
            );
            
            _payments.Add(payment);
        }

        public void Activate(){
            Active =true;
            LastUpdateDate=DateTime.Now;
        }

        public void Deactivate(){
            Active =false;
            LastUpdateDate=DateTime.Now;
        }

        public void Inactivate(){
            Active =false;
            LastUpdateDate=DateTime.Now;
        }
    }
}