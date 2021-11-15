using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
//using System.Net.Http;
using AssesmentTask2.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AssesmentTask2.Controllers
{
    public class BitCoinService : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;
        private ConnectionFactory factory;
        private readonly BookingDataContext _context;

        public BitCoinService(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<BitCoinService>();
          //  _context = context;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 31672 };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("BitCoin", true, false, false, null);
            // _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            // _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                // received message  
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                // handle the received message  
                HandleMessage(content);
               // _channel.BasicAck(ea.DeliveryTag, false);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("BitCoin", false, consumer);
            return Task.CompletedTask;
        }

        private async void HandleMessage(string content)
        {
            Booking rabitBitCoin = new Booking();

            var bitcoin = Newtonsoft.Json.JsonConvert.DeserializeObject<Child>(content);
           
            if(bitcoin != null && bitcoin.bpi != null && bitcoin.time != null)
            {
                rabitBitCoin.UpdatedTime = bitcoin.time.updated;
                rabitBitCoin.ChartName = bitcoin.chartname;
                if(bitcoin.bpi.usd != null)
                {
                    rabitBitCoin.USD_RATE = bitcoin.bpi.usd.rate;
                    rabitBitCoin.USD_RATE_FLOAT = bitcoin.bpi.usd.rate_float;
                }
               
            }

            // we just print this message   
            // _logger.LogInformation($"consumer received {content}");
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
