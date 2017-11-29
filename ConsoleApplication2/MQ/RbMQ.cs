using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace ConsoleApplication2.MQ
{
    public class RbMQ
    {
        public IConnection Connect()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "";
            factory.Password = "";
            factory.VirtualHost = "";
            factory.HostName = "";
            IConnection con = factory.CreateConnection();
            factory.AutomaticRecoveryEnabled = true;
            factory.TopologyRecoveryEnabled = false;
            factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(100);
            con.AutoClose = true;
            
            return con;
        }

        public IModel CreateModel(IConnection con)
        {
            IModel model = con.CreateModel();
            model.ExchangeDeclare("", ExchangeType.Direct);
            model.QueueDeclare("", false, false, false, null);
            model.QueueBind("","","",null);
            return model;
        }

        public void PublishMessage(IModel model)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes("Hello World");
            IBasicProperties props = model.CreateBasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;
            props.Expiration = "3600000";
            model.BasicPublish("","",props,messageBytes );
        }

        public void PullAPI(IModel model)
        {
            bool noAck = false;
            BasicGetResult result = model.BasicGet("",noAck);
            if (result == null){
                
            }
            else {
                IBasicProperties props = result.BasicProperties;
                byte[] body = result.Body;
                model.BasicAck(result.DeliveryTag ,false);
            }
        }

        public string PushAPI(IModel model) {
            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (ch,ea) => {
                var body = ea.Body;
                model.BasicAck(ea.DeliveryTag,false);
            };
            string consumerTag = model.BasicConsume("",false ,consumer);
            return consumerTag;
        }

        public void RPCClient(IConnection con,IModel channel)
        {
            byte[] requestMessageBytes=null;
            SimpleRpcClient client = new SimpleRpcClient(channel,"");
            //SimpleRpcServer server = new SimpleRpcServer();
            client.TimedOut += Client_TimedOut;
            client.Disconnected += Client_Disconnected;
            byte[] replyMessageBytes = client.Call(requestMessageBytes);
        }

        public void MQSubscription(IModel ch)
        {
            Subscription sub = new Subscription(ch,"");
            foreach (BasicDeliverEventArgs e in sub)
            {
                sub.Ack(e);
            }
        }

        private void Client_Disconnected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Client_TimedOut(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
