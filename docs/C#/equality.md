# Equality

```c#
public sealed class RelationshipStateDto : IEquatable<RelationshipStateDto>
{
    public RelationshipStateDto(Guid id, DateTime published, List<RelationshipDto> relationships)
    {
        Id = id;
        Published = published;
        Relationships = relationships ?? new List<RelationshipDto>();
    }

    public Guid Id { get; }
    public DateTime Published { get; }
    public List<RelationshipDto> Relationships { get; }

    #region Equality

    public bool Equals(RelationshipStateDto other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id) && Published.Equals(other.Published) &&
               Relationships.SequenceEqual(other.Relationships);
    }

    public override bool Equals(object obj) =>
        ReferenceEquals(this, obj) || obj is RelationshipStateDto other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Id, Published, Relationships);

    public static bool operator ==(RelationshipStateDto left, RelationshipStateDto right) => Equals(left, right);

    public static bool operator !=(RelationshipStateDto left, RelationshipStateDto right) => !Equals(left, right);

    #endregion
}
```
