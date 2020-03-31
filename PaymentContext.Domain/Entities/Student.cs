using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {   
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {   
            Name = name;
            Document = document;
            Email = email;
            _subscriptions=new List<Subscription>();

            AddNotifications(name,document,email);
        }

        public Name Name {get; private set;}
        public Document  Document {get; private set;}
        public Email Email {get; private set;}
        public IReadOnlyCollection<Subscription> Subscriptions {get {return _subscriptions.ToArray();}}
        public Address Address {get; private set;}

        public void AddSubcription(Subscription  subscription)
        {
            var hasSubscriptionActive = false;

            foreach(var sub in _subscriptions)
            {
                if(sub.Active)
                    hasSubscriptionActive=true;
            }

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive,"Sudent.Subscriptions", "Voce ja tem uma assinatrura ativa.")
                .AreEquals(0,subscription.Payments.Count,"Student.Subscription.Payments","Essa assinatura não possui pagamento.")
            );

            //Alternativa
            if(hasSubscriptionActive)
                AddNotification("Sudent.Subscriptions", "Voce ja tem uma assinatrura ativa.");
        }
    }
}