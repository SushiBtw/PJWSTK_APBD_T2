using PJWSTK_APBD_T2.Models;
using PJWSTK_APBD_T2.Services;

namespace PJWSTK_APBD_T2
{
    class Program
    {
        static void Main(string[] args)
        {
            IFeeCalculator feeCalculator = new FeeCalculator();
            RentalService rentalService = new RentalService(feeCalculator);

            Console.WriteLine("=== Scenariusz demonstracyjny ===");
            // 11. Dodanie kilku egzemplarzy sprzętu różnych typów.
            // 12. Dodanie kilku użytkowników różnych typów.
            // 13. Poprawne wypożyczenie sprzętu.
            // 14. Próbę wykonania niepoprawnej operacji, np. wypożyczenia sprzętu niedostępnego albo
            // przekroczenia limitu.
            // 15. Zwrot sprzętu w terminie.
            // 16. Zwrot opóźniony skutkujący naliczeniem kary.
            // 17. Wyświetlenie raportu końcowego o stanie systemu.

            Console.WriteLine("11. Dodanie kilku egzemplarzy sprzętu różnych typów.");

            var laptop = new Laptop
            {
                Name = "Apple MacBook Pro",
                Description = "Laptop Apple MacBook Pro with M1 chip",
                Processor = "Apple M1",
                RamSizeGb = 16,
                StorageSizeGb = 512
            };
            var projector = new Projector
            {
                Name = "Epson Projector",
                Description = "Epson Home Cinema 3800",
                Lumens = 3000,
                ResolutionX = 1920,
                ResolutionY = 1080
            };
            var camera = new Camera
            {
                Name = "Canon DSLR",
                Description = "Canon EOS Rebel T7",
                HasFlash = true,
                Megapixels = "24.1 MP"
            };
            var brokenCamera = new Camera
            {
                Name = "Sony DSLR",
                Description = "Sony Alpha A6000",
                HasFlash = true,
                Megapixels = "24.3 MP",
            };
            rentalService.AddEquipment(laptop);
            rentalService.AddEquipment(projector);
            rentalService.AddEquipment(camera);
            rentalService.AddEquipment(brokenCamera);
            rentalService.MarkEquipmentAsUnavailable(brokenCamera.Id);

            Console.WriteLine("= 11. DONE =");

            Console.WriteLine("12. Dodanie kilku użytkowników różnych typów.");

            var student1 = new Student
            {
                FirstName = "Adam",
                LastName = "Kalinowski",
                Group = "14C"
            };
            var student2 = new Student
            {
                FirstName = "Ewa",
                LastName = "Kowalska",
                Group = "14C"
            };
            var employee = new Employee
            {
                FirstName = "Jan",
                LastName = "Nowak",
                Department = "IT"
            };

            rentalService.AddUser(student1);
            rentalService.AddUser(student2);
            rentalService.AddUser(employee);

            Console.WriteLine("= 12. DONE =");

            Console.WriteLine("13. Poprawne wypożyczenie sprzętu.");

            var rental1 = rentalService.RentEquipment(student1, laptop, 7);
            var rental2 = rentalService.RentEquipment(student1, camera, 3);

            Console.WriteLine("= 13. DONE =");

            Console.WriteLine(
                "14. Próbę wykonania niepoprawnej operacji, np. wypożyczenia sprzętu niedostępnego albo przekroczenia limitu");

            try
            {
                rentalService.RentEquipment(student2, laptop, 7);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }

            Console.WriteLine("= 14. DONE =");

            Console.WriteLine("15. Zwrot sprzętu w terminie.");

            rentalService.ReturnEquipment(rental1.Id, rental1.DueDate);
            Console.WriteLine($"Rental 1 returned on time. Late Fee: {rental1.LateFee:C}");

            Console.WriteLine("= 15. DONE =");

            Console.WriteLine("16. Zwrot opóźniony skutkujący naliczeniem kary.");

            DateTime lateReturnDate = rental2.DueDate.AddDays(5);
            rentalService.ReturnEquipment(rental2.Id, lateReturnDate);
            Console.WriteLine($"Rental 2 returned late. Late Fee: {rental2.LateFee:C}");

            Console.WriteLine("= 16. DONE =");

            Console.WriteLine("17. Wyświetlenie raportu końcowego o stanie systemu.");

            string report17 = rentalService.GenerateEquipmentReport();
            Console.WriteLine(report17);

            Console.WriteLine("= 17. DONE =");
        }
    }
}