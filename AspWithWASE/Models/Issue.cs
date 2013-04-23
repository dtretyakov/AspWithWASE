using System;
using WindowsAzure.Table.Attributes;

namespace AspWithAzureExtensions.Models
{
    /// <summary>
    ///     Issue entity.
    /// </summary>
    public sealed class Issue
    {
        /// <summary>
        ///     Gets or sets a category.
        /// </summary>
        [PartitionKey]
        public string Category { get; set; }

        /// <summary>
        ///     Gets or sets an identifier.
        /// </summary>
        [RowKey]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets a creation date.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        ///     Gets or sets a modification date.
        /// </summary>
        public DateTime Modified { get; set; }

        /// <summary>
        ///     Gets or sets a user e-mail address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets a description.
        /// </summary>
        public string Description { get; set; }
    }
}