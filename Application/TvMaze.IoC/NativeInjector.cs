using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TvMaze.Application;
using TvMaze.Core;
using TvMaze.Core.Messages.DomainMessages;
using TvMaze.Data.Contexts;
using TvMaze.Data.Repositories;
using TvMaze.Domain.CommandHandlers;
using TvMaze.Domain.Commands;
using TvMaze.Domain.Repositories;

namespace TvMaze.IoC;

public class NativeInjector
{
	public static void InjectServicesForApi(IServiceCollection services, string ConnectionString)
	{
		//Configuration
		services.AddSingleton<IConfigurationSettingsSource, ConfigurationExtensionsSource>();
		services.AddSingleton<ISettingsReader, SettingsReader>();

		//Domain Notifications
		services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

		//Show Aggregate
		services.AddScoped<IShowAppService, ShowAppService>();
		services.AddScoped<IShowRepository, ShowRepository>();
		services.AddScoped<ShowContext>();
		services.AddDbContext<ShowContext>(options =>
					options.UseSqlServer(ConnectionString));
		services.AddScoped<IRequestHandler<AddShowCommand, bool>, AddShowCommandHandler>();
		services.AddScoped<IRequestHandler<AddActorCommand, bool>, AddActorCommandHandler>();

		
	}

	public static void InjectServicesForDaemon(IServiceCollection services, string ConnectionString)
	{
		//Configuration
		services.AddSingleton<IConfigurationSettingsSource, ConfigurationExtensionsSource>();
		services.AddSingleton<ISettingsReader, SettingsReader>();

		//Domain Notifications
		services.AddSingleton<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

		//Show Aggregate
		services.AddSingleton<IShowAppService, ShowAppService>();
		services.AddSingleton<IShowRepository, ShowRepository>();
		services.AddSingleton<ShowContext>();
		services.AddDbContext<ShowContext>(options =>
			options.UseSqlServer(ConnectionString));
		services.AddSingleton<IRequestHandler<AddShowCommand, bool>, AddShowCommandHandler>();
		services.AddSingleton<IRequestHandler<AddActorCommand, bool>, AddActorCommandHandler>();
	}
}