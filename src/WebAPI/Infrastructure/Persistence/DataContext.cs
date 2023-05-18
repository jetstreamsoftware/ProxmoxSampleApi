using System.Data;
using System.Text.Json;
using Jetstream.Proxmox.Sample.WebAPI.Application.Models;

namespace Jetstream.Proxmox.Sample.WebAPI.Infrastructure.Persistence;

public class DataContext
{
    public ICollection<Cluster> Clusters { get; }

    private DataContext(ICollection<Cluster> clusters)
    {
        Clusters = clusters;
    }

    public static DataContext CreateFromJson(string filePath)
    {
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        
        var database = JsonSerializer.Deserialize<Database>(stream);
            
        return database != null
            ? new DataContext(database.Clusters)
            : throw new DataException("Provided stream does not contain correct data.");
    }
}