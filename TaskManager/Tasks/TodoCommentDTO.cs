namespace TaskManager.Tasks
{
    public class TodoCommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public TodoComment ToTodoComment()
        {
            return new TodoComment
            {
                Text = Text
            };
        }

        public static TodoCommentDTO Create(TodoComment comment)
        {
            return new TodoCommentDTO
            {
                Id = comment.Id,
                Text = comment.Text
            
            };
        }
    }
}
