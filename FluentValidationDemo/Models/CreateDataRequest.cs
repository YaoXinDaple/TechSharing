namespace FluentValidationDemo.Models
{
    public class CreateDataRequest
    {
        public string AsId { get; set; }
        public string AsName { get; set; } = default!;
        public DateOnly AsStartUseDate { get; set; }

        public List<AsSetting> Settings { get; set; } = default!;
    }

    public class AsSetting
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string? Value { get; set; }
    }
}
