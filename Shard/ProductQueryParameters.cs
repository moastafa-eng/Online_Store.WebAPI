namespace Shard
{
    public class ProductQueryParameters
    {
        //brandId, typeId, sort, search, pageIndex, pageSize
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 18;
    }
}
