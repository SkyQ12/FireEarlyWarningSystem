using FireEarlyWarningSystem.Infrastructure.MqttClients;
using Microsoft.AspNetCore.SignalR; 

namespace FireEarlyWarningSystem.BackgroundHub
{
    public class MqttSubcribeService : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly MqttBuffer _buffer;
        public IServiceScopeFactory _serviceScopeFactory;

        public MqttSubcribeService(ManagedMqttClient mqttClient, MqttBuffer buffer, IServiceScopeFactory serviceScopeFactory)
        {
            _mqttClient = mqttClient;
            _buffer = buffer;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await ConnectToMqttBrokerAsync();
            Console.WriteLine("ScadaHost is running...");
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
            Console.WriteLine(topic);
            var payloadMessage = arg.Payload;
            if (topic is null || payloadMessage is null)
            {
                return;
            }

            int TopicCount = topic.Count(c => c == '/');
            //Console.WriteLine($"Topic Count: {TopicCount}");


            if (TopicCount == 5)
            {

                string[] splitTopic = topic.Split('/');
                string unsplit_Id = splitTopic[3];
                string Id = unsplit_Id.Split('_')[1];
                string binId = "BIN" + Id;
                string binUnitId = "";
                string metricType = "";

                switch (unsplit_Id.Split('_')[2])
                {
                    case "Food":
                        binUnitId = binId + "OR";
                        break;
                    case "Recycle":
                        binUnitId = binId + "RI";
                        break;
                    case "Other":
                        binUnitId = binId + "NI";
                        break;
                }

                switch (splitTopic[5])
                {
                    case "Level":
                        metricType = "Level";
                        break;
                    case "Fault":
                        metricType = "Fault";
                        break;
                    case "Compress_cnt":
                        metricType = "CompressCnt";
                        break;
                    case "Full_cnt":
                        metricType = "FullCnt";
                        break;
                    case "Status":
                        metricType = "Status";
                        break;
                    case "Flame":
                        metricType = "Flame";
                        break;
                    case "Vibration":
                        metricType = "Vibration";
                        break;
                    case "Battery":
                        metricType = "Battery";
                        break;
                }
            }
        }
    }
}
