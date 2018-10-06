using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DJ;

namespace DJConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var disk in WmiMapper.GetDisks())
            {
               Console.WriteLine($"Device id: {disk.DeviceId}, Volume name: {disk.VolumeName}, serial number: {disk.VolumeSerialNumber}");
            }

            Console.ReadKey();
        }
    }
}
