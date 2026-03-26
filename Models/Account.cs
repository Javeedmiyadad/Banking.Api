using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingService.API.Models;

public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    [StringLength(150)]
    public string CustomerName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Balance { get; set; }

    public DateTime CreatedDate { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
