using System.ComponentModel.DataAnnotations;

namespace DetalleMaestroAPI.Models;

public class Order {
    [Key]
    public int OrderID { get; set; }
    [Required]
    public string CustomerName { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public string OrderDetails { get; set; }
}