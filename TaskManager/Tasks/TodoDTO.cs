namespace TaskManager.Tasks
{
    public class TodoDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        internal static Todo ToTodo(TodoDTO dto)
        {
            return new Todo
            {
                Description = dto.Description,
                Title = dto.Title
            };
        }
    }
}
