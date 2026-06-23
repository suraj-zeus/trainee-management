using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;



using Trainee.api.Configurations;
using Trainee.api.Dto;
using Trainee.api.Messaging;

namespace Trainee.api.Services;


public class RabbitMqService : IRabbitMqService
{

    private readonly RabbitMqConfig _rabbitMqConfig;
    private IConnection _connection;
    private ILogger<RabbitMqService> _logger;


    public RabbitMqService(
         IOptions<RabbitMqConfig> options,
         ILogger<RabbitMqService> logger
    )
    {
        _logger = logger;
        _rabbitMqConfig = options.Value;

    }



    private async Task<IConnection> GetConnectionAsync()
    {
        if (_connection is not null) return _connection;

        var connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.HostName,
            Port = _rabbitMqConfig.Port,
            VirtualHost = _rabbitMqConfig.VirtualHost,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password,
            ClientProvidedName = "trainee-api"
        };

        _connection = await connectionFactory.CreateConnectionAsync();
        return _connection;
    }


    public async Task PublishAsync(SubmissionProcessingRequest submissionProcessingRequest)
    {
        _connection = await GetConnectionAsync();

        await using var channel = await _connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _rabbitMqConfig.SubmissionQueue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );


        var body = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(submissionProcessingRequest)
        );

        var properties = new BasicProperties
        {
            Persistent = true,
            MessageId = submissionProcessingRequest.MessageId,
            CorrelationId = submissionProcessingRequest.CorrelationId,
            ContentType = "application/json"
        };

        _logger.LogInformation($"Attempting to publish message {submissionProcessingRequest.MessageId} with CorrelationId : {submissionProcessingRequest.CorrelationId} for Submission {submissionProcessingRequest.SubmissionId} to queue...");

        try
        {
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: _rabbitMqConfig.SubmissionQueue,
                mandatory: false,
                basicProperties: properties,
                body: body
            );

            _logger.LogInformation($"Successfully published submission-processing message with MessageId : {submissionProcessingRequest.MessageId}, SubmissionId : {submissionProcessingRequest.SubmissionId} and CorrelationId : {submissionProcessingRequest.CorrelationId}");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Publisher failed to publish submission message with MessageId : {submissionProcessingRequest.MessageId}, SubmissionId : {submissionProcessingRequest.SubmissionId} and CorrelationId : {submissionProcessingRequest.CorrelationId}");
        }

    }




}


