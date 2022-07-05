using System;
namespace TvMaze.Domain.Entities
{
	public class Actor : Entity
	{
        public Guid ShowId { get; set; }
        public string Name { get; private set; }
        public DateOnly BirthDay { get; private set; }
        //EF Rel.
        public Show Show { get; protected set; }

        public Actor(string name, DateOnly birthday, Guid showId, Guid? id = null)
		{
			Name = name;
			BirthDay = birthday;
			ShowId = showId;

			if (id is null)
				Id = Guid.NewGuid();
			else
				Id = (Guid)id;
		}
	}
}

