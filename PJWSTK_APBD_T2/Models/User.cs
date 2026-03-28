namespace PJWSTK_APBD_T2.Models
{
    public enum UserType
    {
        Student,
        Employee
    }

    public abstract class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public abstract int MaxActiveRentals { get; }
        public abstract UserType UserType { get; }
    }

    public class Student : User
    {
        public string Group { get; set; }
        public override int MaxActiveRentals => 2;
        public override UserType UserType => UserType.Student;
    }

    public class Employee : User
    {
        public string Department { get; set; }
        public override int MaxActiveRentals => 5;
        public override UserType UserType => UserType.Employee;
    }
}