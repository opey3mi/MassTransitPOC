using MassTransit;
using MassTransitPOC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


        var host = CreateHostBuilder(args).Build();

        var busControl = host.Services.GetRequiredService<IBusControl>();

        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
        {
            Console.WriteLine("Application started. Press any key to exit.");
        });

        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping.Register(async () =>
        {
            cancellationTokenSource.Cancel();
            await busControl.StopAsync(cancellationToken);
        });

        await host.RunAsync(cancellationToken);


        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<MyMessageConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    services.AddMassTransitHostedService();
                });
        }