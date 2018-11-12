using System.ComponentModel.DataAnnotations;

namespace WebApi.Todo.Models.Abstract
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public class Entity<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
