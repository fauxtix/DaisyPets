using System.Data;

namespace MauiPetsApp.Core.Application.Interfaces.DapperContext
{
    public interface IDapperContext
	{
		public IDbConnection CreateConnection();
		public void Execute(Action<IDbConnection> @event);

	}
}
