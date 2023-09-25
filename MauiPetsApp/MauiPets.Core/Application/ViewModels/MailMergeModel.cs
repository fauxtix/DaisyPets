namespace MauiPetsApp.Core.Application.ViewModels
{
    public class MailMergeModel
    {
        public int PetId { get; set; }
        public string? WordDocument { get; set; }
        public string[]? MergeFields { get; set; }
        public string[]? ValuesFields { get; set; }
        public string? DocumentHeader { get; set; }
        public bool Referral { get; set; }
        public bool SaveFile { get; set; }
    }
}