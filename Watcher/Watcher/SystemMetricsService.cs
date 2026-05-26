using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class SystemMetricsService
{
    public int GetCpuCores()
    {
        return Environment.ProcessorCount;
    }

    public float GetRamUsedMB()
    {
        return GC.GetTotalMemory(false) / 1024f / 1024f;
    }

    public float GetDiskUsage()
    {
        DriveInfo drive = new DriveInfo("C");

        double used = drive.TotalSize - drive.TotalFreeSpace;

        return (float)(used * 100 / drive.TotalSize);
    }

    public string GetOS()
    {
        return Environment.OSVersion.ToString();
    }

    public string GetMachineName()
    {
        return Environment.MachineName;
    }
}