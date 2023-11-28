namespace Data.Character.Base
{
    public interface IStatusProperty<T>
    {
        public T Value { get; }
        
        public void Set(T value);
        public void Reset();
    }

    public class StatusProperty<T> : IStatusProperty<T>
    {
        public T Value { get; private set; }

        public void Set(T value)
        {
            Value = value;
        }
                
        public void Reset()
        {
            Value = default;
        }

        public static implicit operator T(StatusProperty<T> statusProperty) => statusProperty.Value;
    }
}