namespace TvMaze.Domain.Entities;

public class Show : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    //Ctor for EFCore
    protected Show() { }

    public Show(string name, Guid? id = null)
	{
        Name = name;

        if (id is null)
            Id = Guid.NewGuid();
        else
            Id = (Guid) id;

        _cast = new List<Actor>();
	}

    private List<Actor> _cast;
    public IReadOnlyList<Actor> Cast => _cast;
}

