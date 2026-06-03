using FireEarlyWarningSystem.Infrastructure.MqttClients;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Text;
using FireEarlyWarningSystem.Infrastructure.Repositories.WarningHistories;
using FireEarlyWarningSystem.Infrastructure.Repositories.Cameras;
using FireEarlyWarningSystem.Infrastructure.Domain.Models.DataType;
namespace FireEarlyWarningSystem.BackgroundHub
{
    public class MqttService : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly MqttBuffer _buffer;
        public IServiceScopeFactory _serviceScopeFactory;

        public MqttService(ManagedMqttClient mqttClient, MqttBuffer buffer, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttClient = mqttClient;
            _buffer = buffer;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await ConnectToMqttBrokerAsync();
            Console.WriteLine("Background Service is running...");
        }
        public async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceived;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("Camera/#");
        }
        public async Task OnMqttClientMessageReceived(MqttMessage arg)
        {
            var topic = arg.Topic;
            int TopicCount = topic.Count(c => c == '/');
            var payloadMessage = arg.Payload;
            
            if (topic is null || payloadMessage is null)
            {
                return;
            }

            Console.WriteLine(topic);
            Console.WriteLine($"Topic count: {TopicCount}");

            string[] splitTopic = topic.Split('/');
            string TopicType = splitTopic[2];

            if (TopicType == "Metric")
            {
                CameraMetric cameraMetric = JsonConvert.DeserializeObject<CameraMetric>(payloadMessage);
                /*
                Console.WriteLine($"CameraId: {cameraMetric.CameraId}");
                Console.WriteLine($"AiDetection: {cameraMetric.AiDetection}");
                Console.WriteLine($"FlameSensor: {cameraMetric.FlameSensor}");
                Console.WriteLine($"SmokeValue: {cameraMetric.SmokeValue}");
                Console.WriteLine($"CameraState: {cameraMetric.CameraState}");
                Console.WriteLine($"Battery: {cameraMetric.Battery}");
                Console.WriteLine($"Timestamp: {cameraMetric.Timestamp}");
                */

                using var CameraScope = _serviceScopeFactory.CreateScope();

                var cameraRepository =
                    CameraScope.ServiceProvider.GetRequiredService<ICameraRepository>();

                await cameraRepository.SaveCameraMetricToDatabase(
                    cameraMetric.CameraId,
                    (AIDetectType)cameraMetric.AiDetection,
                    (FlameSensorType)cameraMetric.FlameSensor,
                    cameraMetric.SmokeValue,
                    (StateType)cameraMetric.CameraState,
                    cameraMetric.Battery,
                    cameraMetric.Timestamp);

                if (cameraMetric.CameraState != 0|cameraMetric.CameraState != 2)
                {
                    using var WarningHistoryScope = _serviceScopeFactory.CreateScope();
                    var warningHistoryRepository =
                        WarningHistoryScope.ServiceProvider.GetRequiredService<IWarningHistoryRepository>();
                    await warningHistoryRepository.SaveWarningHistoryToDatabase(
                        cameraMetric.CameraId,
                        (AIDetectType)cameraMetric.AiDetection,
                        (FlameSensorType)cameraMetric.FlameSensor,
                        cameraMetric.SmokeValue,
                        (StateType)cameraMetric.CameraState,
                        cameraMetric.Timestamp
                    );

                    Console.WriteLine($"Warning history saved for CameraId: {cameraMetric.CameraId} at {cameraMetric.Timestamp}");
                };
            }      
            else if (TopicType == "RealtimeCameraLink")
            {
                CameraLink cameraLink = JsonConvert.DeserializeObject<CameraLink>(payloadMessage);
                using var scope = _serviceScopeFactory.CreateScope();
                var cameraRepository =
                    scope.ServiceProvider.GetRequiredService<ICameraRepository>();
                await cameraRepository.SaveCameraLinkToDatabase(cameraLink.CameraId, cameraLink.RealtimeCameraLink);

                /*
                Console.WriteLine($"CameraId: {cameraLink.CameraId}");
                Console.WriteLine($"RealtimeCameraLink: {cameraLink.RealtimeCameraLink}");
                */
            }
            else
            {
                Console.WriteLine("Unknown topic type.");
            }
            

        }
    }
}
