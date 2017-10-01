namespace EstudoDDD.Domain
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        protected abstract bool EqualsCore(T otherObject);
        protected abstract int GetHashCodeCore();

        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        public static bool operator ==(ValueObject<T> valueObjectA, ValueObject<T> valueObjectB)
        {
            if (ReferenceEquals(valueObjectA, null) && ReferenceEquals(valueObjectB, null))
                return false;

            if (ReferenceEquals(valueObjectA, null) || ReferenceEquals(valueObjectB, null))
                return false;

            return valueObjectA.Equals(valueObjectB);
        }

        public static bool operator !=(ValueObject<T> valueObjectA, ValueObject<T> valueObjectB)
        {
            return !(valueObjectA == valueObjectB);
        }
    }
}