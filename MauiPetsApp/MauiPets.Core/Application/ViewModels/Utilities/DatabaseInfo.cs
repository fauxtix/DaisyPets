namespace MauiPets.Core.Application.ViewModels.Utilities
{
    public class DatabaseInfo
    {
        public string? Path { get; set; }
        public bool IsAccessible { get; set; }
        public long SizeInBytes { get; set; }
        public DateTime LastModified { get; set; }
        public Dictionary<string, long> TableCounts { get; set; } = new();
    }
}