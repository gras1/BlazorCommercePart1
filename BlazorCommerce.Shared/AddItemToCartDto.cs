namespace BlazorCommerce.Shared
{
    public class AddItemToCartDto
    {
        public string CartGuid {get;set;}

        public int ProductOptionProductInstanceId {get;set;}

        public int Quantity {get;set;}
    }
}