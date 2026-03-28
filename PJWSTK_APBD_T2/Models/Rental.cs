namespace PJWSTK_APBD_T2.Models
{
    public class Rental
    {
        public Guid Id { get; } = Guid.NewGuid();
        public User RentedBy { get; set; }
        public Equipment RentedEquipment { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal RentalFee { get; set; } = 0;
        public decimal LateFee { get; set; } = 0;

        public bool IsActive => ReturnDate == null;
        public bool IsLate => IsActive && DateTime.Now > DueDate;
    }
}