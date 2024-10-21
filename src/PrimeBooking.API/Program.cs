using EventBus.Messages.Common;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<IActionResultCreator, ActionResultCreator>();

builder.Services.AddMassTransit(configuration => {
    //configuration.AddConsumer<CheckCredentialsConsumer>();
    
    configuration.UsingRabbitMq((context, configurator) => {
        configurator.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        
        configurator.ReceiveEndpoint(Constants.HotelDataTransferQueue, receiveEndpointConfigurator => {
            //receiveEndpointConfigurator.ConfigureConsumer<CheckCredentialsConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
