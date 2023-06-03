using System.Data;

namespace DaisyPets.Core.Application.Interfaces.DapperContext
{
    public interface IDapperContext
	{
		public IDbConnection CreateConnection();
		public void Execute(Action<IDbConnection> @event);

	}
}
