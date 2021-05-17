namespace BlazorCommerce.Shared
{
    public class CategoryDto : IDataModel
    {
        public int Id {get;set;}

        public string Name {get;set;}

        public bool IsHero {get;set;}

        public bool IsSingleBanner {get;set;}

        public bool IsMidiumBanner {get;set;}

        public string FriendlyUrl {get;set;}

        public string ImageUrl {get;set;}

        public string OfferText {get;set;}
        
        public string MarketingText {get;set;}
        
        public string ActionButtonText {get;set;}

        public byte CategoryType
        {
            get
            {
                if (IsHero)
                {
                    return 1;
                }
                if (IsSingleBanner)
                {
                    return 2;
                }
                if (IsMidiumBanner)
                {
                    return 3;
                }
                return 4;
            }
        }

        public MetaCategoryMinDto MetaCategory {get;set;}
    }
}
