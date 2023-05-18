namespace Jetstream.Proxmox.Sample.WebAPI.Application.Models;

public class Cluster
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Host> Hosts { get; set; }
}