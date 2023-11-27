namespace Data.Character.Base
{
    public interface IStatusProperty<T>
    {
        public void Reset();
    }

    public class StatusProperty<T> : IStatusProperty<T>
    {
        public T Value;

        public void Reset()
        {
            Value = default;
        }

        public static implicit operator T(StatusProperty<T> statusProperty) => statusProperty.Value;
    }
}