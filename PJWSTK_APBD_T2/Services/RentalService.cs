using PJWSTK_APBD_T2.Models;

namespace PJWSTK_APBD_T2.Services
{
    public class RentalService
    {
        private readonly List<Rental> _rentals = new();
        private readonly List<Equipment> _equipment = new();
        private readonly List<User> _users = new();
        private readonly IFeeCalculator _feeCalculator;

        public RentalService(IFeeCalculator feeCalculator)
        {
            _feeCalculator = feeCalculator;
        }

        public IEnumerable<Rental> GetAllRentals() => _rentals;
        public IEnumerable<Equipment> GetAllEquipments() => _equipment;
        public IEnumerable<User> GetAllUsers() => _users;

        public void AddEquipment(Equipment equipment)
        {
            _equipment.Add(equipment);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public IEnumerable<Rental> GetActiveRentalsForUser(Guid userId)
        {
            return _rentals.Where(r => r.RentedBy.Id == userId && r.IsActive);
        }

        public IEnumerable<Rental> GetLateRentals()
        {
            return _rentals.Where(r => r.IsLate);
        }

        public IEnumerable<Equipment> GetAvailableEquipment()
        {
            return _equipment.Where(e => e.Status == EquipmentStatus.Available);
        }

        public Rental RentEquipment(User user, Equipment equipment, int rentalDays)
        {
            if (rentalDays <= 0) throw new ArgumentException("Rental days must be greater than zero.");

            if (equipment.Status != EquipmentStatus.Available)
                throw new InvalidOperationException($"Equipment {equipment.Name} is currently not available.");

            int activeRentals = GetAllRentals().Count(r => r.RentedBy.Id == user.Id && r.IsActive);
            if (activeRentals >= user.MaxActiveRentals)
                throw new InvalidOperationException(
                    $"User {user.FirstName} {user.LastName} has reached the maximum number of active rentals (Active Rentals: {activeRentals}, Max Allowed: {user.MaxActiveRentals}).");

            DateTime rentDate = DateTime.Now;
            DateTime dueDate = rentDate.AddDays(rentalDays);
            decimal rentalFee = _feeCalculator.CalculateRentalFee(equipment, rentDate, dueDate);

            Rental rental = new Rental
            {
                RentedBy = user,
                RentedEquipment = equipment,
                RentDate = rentDate,
                DueDate = dueDate,
                RentalFee = rentalFee
            };

            equipment.Status = EquipmentStatus.Rented;
            _rentals.Add(rental);

            return rental;
        }

        public void ReturnEquipment(Guid rentalId, DateTime returnDate)
        {
            Rental? rental = _rentals.FirstOrDefault(r => r.Id == rentalId);
            if (rental == null) throw new ArgumentException("Rental not found.");

            if (!rental.IsActive)
                throw new InvalidOperationException(
                    $"Rental for equipment {rental.RentedEquipment.Name} has already been returned.");

            rental.ReturnDate = returnDate;
            rental.LateFee =
                _feeCalculator.CalculateLateFee(rental.RentedEquipment, rental.DueDate, rental.ReturnDate.Value);
            rental.RentedEquipment.Status = EquipmentStatus.Available;
        }

        public void MarkEquipmentAsUnavailable(Guid equipmentId)
        {
            Equipment? equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);
            if (equipment == null) throw new ArgumentException("Equipment not found.");

            if (equipment.Status == EquipmentStatus.Rented)
                throw new InvalidOperationException(
                    $"Cannot mark equipment {equipment.Name} as unavailable while it is rented.");

            equipment.Status = EquipmentStatus.Unavailable;
        }

        public string GenerateEquipmentReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("Equipment Rental Report");
            report.AppendLine("======================");
            foreach (var equipment in _equipment)
            {
                var status = equipment.Status switch
                {
                    EquipmentStatus.Available => "Available",
                    EquipmentStatus.Rented => "Rented",
                    EquipmentStatus.Unavailable => "Unavailable",
                    _ => "Unknown"
                };
                report.AppendLine($"- {equipment.Name} (ID: {equipment.Id}) - Status: {status}");
            }

            report.AppendLine("======================");
            report.AppendLine($"Total Equipment: {_equipment.Count}");
            report.AppendLine($"Available: {_equipment.Count(e => e.Status == EquipmentStatus.Available)}");
            report.AppendLine($"Rented: {_equipment.Count(e => e.Status == EquipmentStatus.Rented)}");
            report.AppendLine($"Unavailable: {_equipment.Count(e => e.Status == EquipmentStatus.Unavailable)}");
            return report.ToString();
        }

        public string GenerateAvailableEquipmentReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("Available Equipment Report");
            report.AppendLine("=========================");
            var availableEquipment = GetAvailableEquipment().ToList();
            if (!availableEquipment.Any())
            {
                report.AppendLine("No equipment is currently available.");
            }
            else
            {
                foreach (var equipment in availableEquipment)
                {
                    report.AppendLine($"- {equipment.Name} (ID: {equipment.Id})");
                }
            }

            report.AppendLine("=========================");
            report.AppendLine($"Total Available Equipment: {availableEquipment.Count}");
            return report.ToString();
        }

        public string GenerateRentedEquipmentReport(Guid userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new ArgumentException("User not found.");

            var report = new System.Text.StringBuilder();
            report.AppendLine($"Current Rentals for {user.FirstName} {user.LastName}");
            report.AppendLine("==============================================");
            var activeRentals = GetActiveRentalsForUser(userId).ToList();
            if (!activeRentals.Any())
            {
                report.AppendLine("No active rentals for this user.");
            }
            else
            {
                foreach (var rental in activeRentals)
                {
                    report.AppendLine(
                        $"- {rental.RentedEquipment.Name} (Rented On: {rental.RentDate:d}, Due On: {rental.DueDate:d})");
                }
            }

            report.AppendLine("==============================================");
            report.AppendLine($"Total Active Rentals: {activeRentals.Count}");
            return report.ToString();
        }

        public string GenerateLateRentalsReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("Late Rentals Report");
            report.AppendLine("===================");
            var lateRentals = GetLateRentals().ToList();
            if (!lateRentals.Any())
            {
                report.AppendLine("No late rentals at the moment.");
            }
            else
            {
                foreach (var rental in lateRentals)
                {
                    report.AppendLine(
                        $"- {rental.RentedEquipment.Name} (Rented By: {rental.RentedBy.FirstName} {rental.RentedBy.LastName}, Due On: {rental.DueDate:d})");
                }
            }

            report.AppendLine("===================");
            report.AppendLine($"Total Late Rentals: {lateRentals.Count}");
            return report.ToString();
        }
    }
}