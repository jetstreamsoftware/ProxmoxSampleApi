namespace Jetstream.Proxmox.Sample.WebAPI.Application.Models;

public class Host
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Port { get; set; }
    public List<Machine> Machines { get; set; }
}