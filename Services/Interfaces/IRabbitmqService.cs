

using Trainee.api.Messaging;

namespace Trainee.api.Services;


public interface IRabbitMqService
{
        public Task PublishAsync(SubmissionProcessingRequest submissionProcessingRequest);

}