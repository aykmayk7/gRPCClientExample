using Grpc.Net.Client;
using gRPCExample;
using System;
using System.Threading.Tasks;

namespace gRPCClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var messageClient = new Message.MessageClient(channel);
            Console.WriteLine("Lütfen gönderilecek mesajı giriniz.");
            var messageResponse = messageClient.GetMessage(new MessageRequest { Message = Console.ReadLine() });

            await Task.Run(async () =>
            {
                while (await messageResponse.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                    Console.WriteLine($"Gelen mesaj : {messageResponse.ResponseStream.Current.Message}");
            });
        }
    }
}
