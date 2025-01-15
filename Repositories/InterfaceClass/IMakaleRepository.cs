using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entity.RequestParameters;
namespace Repositories.InterfaceClass
{
    public interface IMakaleRepository : IRepositoryBase<Makale>
    {
        IQueryable<Makale> GetAllMakale(bool trackChanges);
        IQueryable<Makale> GetAllMakaleDetails(MakaleRequestParameters m);
        IQueryable<Makale> GetMakaleCarousel(bool trackChanges);
        IQueryable<Makale> GetMakaleIsShowHome(bool trackChanges);
        IQueryable<Makale> GetMakaleIsShow(bool trackChanges);
        Makale? GetOneMakale(int id,bool trackChanges);

        Task<IEnumerable<Makale>> GetAllMakaleAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetAllMakaleDetailsAsync(MakaleRequestParameters m);
        Task<IEnumerable<Makale>> GetMakaleCarouselAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetMakaleIsShowHomeAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetMakaleIsShowAsync(bool trackChanges);
        Task<Makale?> GetOneMakaleAsync(int id, bool trackChanges);

        void CreateMakale(Makale makale);
        void OneUpdateMakale(Makale makale);
        void Deletemakale(Makale makale);

    }
}
