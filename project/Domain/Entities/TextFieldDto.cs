namespace project.Domain.Entities
{
    public class TextFieldDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Father {  get; set; }
        public string View { get; set; }
    }
}
