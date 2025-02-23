// filepath: /D:/Data/Code/mapThis/Account.cs
namespace mapThis;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    public int Id { get; set; }
    public required string FullName { get; set; }
    public string[]? CategoryValues { get; set; }
    public List<AccountValue>? AccountValues { get; set; }
}

public class AccountValue
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Account")]
    public int AccountId { get; set; }
    public required string CategoryValue { get; set; }
    public required Account Account { get; set; }
}


