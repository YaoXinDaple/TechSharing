namespace MediatRInfrastructure.Models
{
    public class ToDo
    {
        public ToDo(string title, string description)
        {
            Title = title;
            IsDone = false;
            Description = description;
            CreationTime = DateTime.Now;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ComplationTime { get; set; }

        internal ToDo Complete()
        {
            IsDone = true;
            ComplationTime = DateTime.Now;
            return this;
        }
    }
}
