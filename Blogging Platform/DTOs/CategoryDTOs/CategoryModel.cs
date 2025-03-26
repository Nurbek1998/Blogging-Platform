namespace Blogging_Platform.DTOs.CategoryDTOs
{
    public class CategoryModel
    {
        public string Name { get; set; }
    }

    public class CategoryResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
