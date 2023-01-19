namespace NZWalks.API.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public double Area { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }

        //Navigation property
        public IEnumerable<Walk> Walks  { get; set; }
    }
}
