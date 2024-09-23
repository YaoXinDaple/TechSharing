namespace FluentValidationDemo.Models
{
    public class UpdateDataRequest
    {
        public string AsId { get; set; } = default!;
        public string AsName { get; set; } = default!;
        public DateOnly AsStartUseDate { get; set; }
    }
}
