using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR.Hubs
{
    public class RealTimeHub : Hub
    {
        public async Task SubscribeToRealTimeUpdates()
        {
            // Send initial data to the client
            await Clients.Caller.SendAsync("SendRealTimeData", GetRealTimeData());

            // Continuously send updated data at intervals
            while (true)
            {
                await Clients.Caller.SendAsync("SendRealTimeData", GetRealTimeData());
                await Task.Delay(1000); // Adjust the delay interval as needed
            }
        }

        private Object GetRealTimeData()
        {
            List<LeftTab> left = new List<LeftTab>();
            List<RightTab> right = new List<RightTab>();
            for (int i = 0; i < 10; i++)
            {
                left.Add(new LeftTab());
            }
            string[] baseTokens = { "AVA", "TEL","MTV", "TT", "BTC", "ETH", "BNB" };
            foreach(var token in baseTokens) 
            {
                right.Add(new RightTab(
                    )
                {
                    Market = token
                });
            }

            return
                new
                {
                    Left = left.OrderBy(item => item.Price).ToList(),
                    Right = right
                };
        }
    }
    public class RealTimeData
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class LeftTab
    {
        public double Price { get; set; } = new Random().NextDouble() * 26;
        public double Amount { get; set; }= new Random().NextDouble() * 1;
        public double Total { get; set; }= new Random().NextDouble() * 20000;
    }
    public class RightTab
    {
        public string Market { get; set; }
        public double Change { get; set; } = -10.0 + (new Random().NextDouble() * 20.0);

        public double Price { get; set; } =  new Random().NextDouble() * 26;
    }
}
