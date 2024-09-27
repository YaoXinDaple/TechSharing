namespace FluentValidationDemo.Repository
{
    public class DataRepository
    {
        public Task<bool> CheckAsync(Guid id)
        {
            if (id == new Guid("8597CB79-8977-7886-978C-3A143E6A4194"))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> IsRegisteredCompanyName(string companyName)
        {
            return Task.FromResult(companyName.Contains("有限公司"));
        }
    }
}
