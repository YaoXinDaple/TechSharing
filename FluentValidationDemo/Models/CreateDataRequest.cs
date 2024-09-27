namespace FluentValidationDemo.Models
{
    public class CreateDataRequest
    {
        public required string AsId { get; set; }
        public string AsName { get; set; } = default!;
        public DateOnly AsStartUseDate { get; set; }

        public List<AsSetting> Settings { get; set; } = default!;
    }

    public class AsSetting
    {
        public int Type { get; set; }
        public required Company Company { get; set; }
        public required string Description { get; set; }

        public string? Value { get; set; }
    }

    public class Company
    {
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string LegalPersonName { get; set; }
    }
}
