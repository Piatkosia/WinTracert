using System.Collections.Generic;
using System.Management;
namespace DJ
{
    public class WmiMapper
    {
        public List<Disk> GetDisks()
        {
            List<Disk> disks = new List<Disk>();
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_LogicalDisk");
                
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Disk disk = new Disk()
                    {
                        DeviceId = queryObj["DeviceID"],
                        VolumeName = queryObj["VolumeName"],
                        VolumeSerialNumber = queryObj["VolumeSerialNumber"],
                    };
                    disks.Add(disk);
                }
            }
            catch (ManagementException e)
            {
                //pewnie znowu w którymś momencie przestanie istnieć, ale co tam;)
            }

            return disks;
        }
    }
}
