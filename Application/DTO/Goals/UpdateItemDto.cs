namespace Application.DTO.Goals
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
