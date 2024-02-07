namespace Healthcare.Api.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        //IBrandRepository BrandRepository { get; }
        //ICategoryRepository CategoryRepository { get; }
        //IProductRepository ProductRepository { get; }
        //ISubCategoryRepository SubCategoryRepository { get; }
        //IFamilyRepository FamilyRepository { get; }
        void Save();
        Task SaveAsync();
    }
}