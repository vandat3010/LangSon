﻿using System;
using Namek.Library.Data;

namespace Namek.Library.Entity.Emails
{
    /// <summary>
    ///     Represents NewsLetterSubscription entity
    /// </summary>
    public class NewsLetterSubscription : BaseEntity
    {
        /// <summary>
        ///     Gets or sets the newsletter subscription GUID
        /// </summary>
        public Guid NewsLetterSubscriptionGuid { get; set; }

        /// <summary>
        ///     Gets or sets the subcriber email
        /// </summary>
        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether subscription is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        ///     Gets or sets the store identifier in which a customer has subscribed to newsletter
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        ///     Gets or sets the date and time when subscription was created
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}