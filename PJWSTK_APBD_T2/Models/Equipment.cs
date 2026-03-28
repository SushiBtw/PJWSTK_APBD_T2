namespace PJWSTK_APBD_T2.Models
{
    public enum EquipmentStatus
    {
        Available,
        Rented,
        Unavailable
    }

    public abstract class Equipment
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;
    }

    public class Laptop : Equipment
    {
        public int RamSizeGb { get; set; }
        public string Processor { get; set; }
        public int StorageSizeGb { get; set; }
    }

    public class Projector : Equipment
    {
        public int Lumens { get; set; }
        public int ResolutionX { get; set; }
        public int ResolutionY { get; set; }
    }

    public class Camera : Equipment
    {
        public bool HasFlash { get; set; }
        public string Megapixels { get; set; }
    }
}