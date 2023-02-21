namespace zws.Core.Entity
{
    internal interface IEntity<T>
    {
        T Id { get; set; }
    }
}
