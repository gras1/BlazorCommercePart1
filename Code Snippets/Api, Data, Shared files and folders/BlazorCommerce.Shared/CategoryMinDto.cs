namespace BlazorCommerce.Shared
{
    public class CategoryMinDto : IDataModel
    {
        public int Id {get;set;}

        public string Name {get;set;}

        public string FriendlyUrl {get;set;}

        public byte CategoryType {get;set;}
    }
}
