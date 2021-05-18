namespace BlazorCommerce.Shared
{
    public class CartItemMinDto
    {
        public int ProductOptionProductInstanceId {get;set;}
        
        public int Quantity {get;set;}

        public decimal TotalAmount {get;set;}

        public string CartThumbnailImageUrl {get;set;}

        public string ProductName {get;set;}
    }
}