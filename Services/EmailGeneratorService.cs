namespace TempMailX.Services
{
    public class EmailGeneratorService
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString().Substring(0, 8)
                   + "@tempmailx.com";
        }
    }
}
