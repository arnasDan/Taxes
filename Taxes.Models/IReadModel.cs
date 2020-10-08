namespace Taxes.Models
{
    public interface IReadModel<TKey>
    {
        TKey Id { get; set; }
    }
}