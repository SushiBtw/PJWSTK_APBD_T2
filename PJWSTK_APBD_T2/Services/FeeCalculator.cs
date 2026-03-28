namespace PJWSTK_APBD_T2.Services
{
    public interface IFeeCalculator
    {
        decimal CalculateRentalFee(Models.Equipment equipment, DateTime rentDate, DateTime dueDate);
        decimal CalculateLateFee(Models.Equipment equipment, DateTime dueDate, DateTime returnDate);
    }

    public class FeeCalculator : IFeeCalculator
    {
        public decimal CalculateRentalFee(Models.Equipment equipment, DateTime rentDate, DateTime dueDate)
        {
            TimeSpan rentalDuration = dueDate - rentDate;
            decimal dailyRate = GetDailyRate(equipment);
            return (decimal)rentalDuration.TotalDays * dailyRate;
        }

        public decimal CalculateLateFee(Models.Equipment equipment, DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate)
                return 0;

            TimeSpan lateDuration = returnDate - dueDate;
            decimal lateDailyRate = GetLateDailyRate(equipment);
            return (decimal)lateDuration.TotalDays * lateDailyRate;
        }

        private decimal GetDailyRate(Models.Equipment equipment)
        {
            return equipment switch
            {
                Models.Laptop => 10m,
                Models.Projector => 15m,
                Models.Camera => 8m,
                _ => throw new ArgumentException("Unknown equipment type")
            };
        }

        private decimal GetLateDailyRate(Models.Equipment equipment)
        {
            return equipment switch
            {
                Models.Laptop => 20m,
                Models.Projector => 30m,
                Models.Camera => 16m,
                _ => throw new ArgumentException("Unknown equipment type")
            };
        }
    }
}