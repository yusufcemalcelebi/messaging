using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Messaging.Api.Models.Settings;
using Messaging.Core.Abstractions.Service;
using Messaging.Core.Dto;
using Messaging.Core.Dto.Messaging;
using Microsoft.Extensions.Options;

namespace Messaging.Service.Messaging
{
    public class SpamDetectionService : ISpamDetectionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SpamDetectionSettings _spamDetectionSettings;

        public SpamDetectionService(IHttpClientFactory httpClientFactory,
            IOptions<SpamDetectionSettings> spamDetectionSettings)
        {
            _httpClientFactory = httpClientFactory;
            _spamDetectionSettings = spamDetectionSettings.Value;
        }

        public async Task<bool> IsSpam(GetSpamProbabilityRequestDto requestDto)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var httpResponse = await httpClient.PostAsJsonAsync(_spamDetectionSettings.Url, requestDto);
            httpResponse.EnsureSuccessStatusCode();

            var spamDetectionResponseDto = await httpResponse.Content.ReadFromJsonAsync<GetSpamProbabilityResponseDto>();
            
            return spamDetectionResponseDto.SpamProbability > _spamDetectionSettings.SpamProbabilityThreshold;
        }
    }
}