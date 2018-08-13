using System;
using NATS.Client;
using System.Text;
using Newtonsoft.Json;
using nats_subscribe_user_info.Models;

namespace nats_subscribe_user_info
{
    class Program
    {
        static void Main(string[] args)
        {            
            // Create a new connection factory to create
            // a connection.
            ConnectionFactory cf = new ConnectionFactory();

            // Creates a live connection to the default
            // NATS Server running locally
            IConnection c = cf.CreateConnection();

            // Setup an event handler to process incoming messages.
            // An anonymous delegate function is used for brevity.
            EventHandler<MsgHandlerEventArgs> h = (sender, natsargs) =>
            {
                // print the message
                Console.WriteLine(Encoding.UTF8.GetString(natsargs.Message.Data));
                // User u = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(natsargs.Message.Data));

                // Here are some of the accessible properties from
                // the message:
                // args.Message.Data;
                // args.Message.Reply;
                // args.Message.Subject;
                // args.Message.ArrivalSubcription.Subject;
                // args.Message.ArrivalSubcription.QueuedMessageCount;
                // args.Message.ArrivalSubcription.Queue;

                // Unsubscribing from within the delegate function is supported.
                //natsargs.Message.ArrivalSubcription.Unsubscribe();
            };

            // The simple way to create an asynchronous subscriber
            // is to simply pass the event in.  Messages will start
            // arriving immediately.
            IAsyncSubscription s = c.SubscribeAsync("bingham.user.create", h);
        }
    }
}
