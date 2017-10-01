namespace EstudoDDD.Domain
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var otherObject = obj as Entity;

            if (ReferenceEquals(otherObject, null)) return false;
            if (ReferenceEquals(this, otherObject)) return true;
            if (GetType() != otherObject.GetType()) return false;
            if (Id == 0 || otherObject.Id == 0) return false;

            return Id == otherObject.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}