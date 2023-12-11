namespace Models
{
    public class Product
    {
        public char Code { get; set; }
        public decimal Price { get; set; }
        public int Group { get; set; }
        public bool IsMultipack { get; set; }
        public char? MultipackBaseProductCode { get; set; }
        public int MultipackQuantity { get; set; }
        public bool IsCampaignProduct { get; set; }
        public decimal CampaignDiscount { get; set; }
        public int CampaignQuantity { get; set; }
        public string CampaignDescription { get; set; }

        public Product(char code, decimal price, int group, bool isMultipack = false, char? multipackBaseProductCode = null, int multipackQuantity = 0, bool isCampaignProduct = false, decimal campaignDiscount = 0, int campaignQuantity = 0, string campaignDescription = "")
        {
            Code = code;
            Price = price;
            Group = group;
            IsMultipack = isMultipack;
            MultipackBaseProductCode = multipackBaseProductCode;
            MultipackQuantity = multipackQuantity;
            IsCampaignProduct = isCampaignProduct;
            CampaignDiscount = campaignDiscount;
            CampaignQuantity = campaignQuantity;
            CampaignDescription = campaignDescription;
        }
    }


}
