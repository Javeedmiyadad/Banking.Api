using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingService.API.Models;

[Index("AccountId", Name = "IX_Transactions_AccountId")]
[Index("TransactionDate", Name = "IX_Transactions_Date")]
public partial class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [StringLength(50)]
    public string TransactionType { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Transactions")]
    public virtual Account Account { get; set; } = null!;
}
