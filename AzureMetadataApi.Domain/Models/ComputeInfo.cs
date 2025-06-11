namespace AzureMetadataApi.Domain.Models
{
    public class ComputeInfo
    {
        public string Location { get; set; }
        public string Name { get; set; }
        public string Offer { get; set; }
        public string OsType { get; set; }
        public string ResourceGroupName { get; set; }
        public string SubscriptionId { get; set; }
        public string VmId { get; set; }
        public string VmSize { get; set; }
    }
}