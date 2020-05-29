using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDatabase.Abstractions
{
    [Serializable]
    public abstract class BusinessEntity
    {
        internal BusinessEntity()
        {
            IsEnabled = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsEnabled { get; set; }
    }
}
