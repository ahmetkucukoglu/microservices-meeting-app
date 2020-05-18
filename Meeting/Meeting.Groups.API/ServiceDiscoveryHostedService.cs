namespace Meeting.Groups.API
{
    using Consul;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ServiceDiscoveryHostedService : IHostedService
    {
        private readonly IConsulClient _client;
        private readonly ConsulConfig _consulConfig;
        private string _registrationId;

        public ServiceDiscoveryHostedService(IConsulClient client, IOptions<ConsulConfig> consulConfig)
        {
            _client = client;
            _consulConfig = consulConfig.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_consulConfig.ServiceName}-{_consulConfig.ServiceId}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _consulConfig.ServiceName,
                Address = _consulConfig.ServiceAddress.Host,
                Port = _consulConfig.ServiceAddress.Port,
                Tags = new[] { $"urlprefix-/{_consulConfig.ServiceName} strip=/{_consulConfig.ServiceName}" },
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                    Interval = TimeSpan.FromSeconds(30),
                    HTTP = _consulConfig.ServiceAddress.OriginalString
                }
            };
            await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _client.Agent.ServiceRegister(registration, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}

