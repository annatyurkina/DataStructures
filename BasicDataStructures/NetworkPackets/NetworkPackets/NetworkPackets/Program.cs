using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkPackets
{
    class Program
    {
        static int[] _res;

        static void Main(string[] args)
        {
            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            Packet[] packets = new Packet[input[1]];
            for (int i = 0; i < input[1]; i++)
            {
                int[] temp = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                packets[i] = new Packet { Index = i, StartTime = temp[0], Time = temp[1]};
            }
            _res = new int[input[1]];
            GetStarTimes(input[0], packets);
            Console.WriteLine(string.Join(" ", _res));
            Console.ReadLine();
        }

        static void GetStarTimes(int s, Packet[] packets)
        {
            int finishTime = -1;
            Queue<Packet> netBuf = new Queue<Packet>();

            for(int i = 0; i < packets.Length; i++)
            {
                //bool needsQueueing = true;
                while(finishTime <= packets[i].StartTime)
                {
                    if (netBuf.Count > 0)
                    {
                        Packet firstPacket = netBuf.Peek();
                        if (firstPacket.StartTime >= finishTime)
                        {
                            _res[firstPacket.Index] = firstPacket.StartTime;
                        }
                        else
                        {
                            _res[firstPacket.Index] = finishTime;
                        }
                        finishTime = _res[firstPacket.Index] + firstPacket.Time;
                        netBuf.Dequeue();
                    }
                    else
                    {
                    //    finishTime = packets[i].StartTime + packets[i].Time;
                    //    _res[i] = packets[i].StartTime;
                    //    needsQueueing = false;
                        break;
                    }
                }
                //if(needsQueueing)
                //{
                    if (netBuf.Count < s)
                        netBuf.Enqueue(packets[i]);
                    else
                        _res[i] = -1;
                //}
            }
            while (netBuf.Count > 0)
            {
                Packet firstPacket = netBuf.Dequeue();
                _res[firstPacket.Index] = Math.Max(finishTime, firstPacket.StartTime);
                finishTime = _res[firstPacket.Index] + firstPacket.Time;
            }
        }
    }

    public class Packet
    {
        public int StartTime { get; set; }
        public int Time { get; set; }
        public int Index { get; set; }
    }
}
