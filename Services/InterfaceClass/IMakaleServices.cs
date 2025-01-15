using Entities.Dtos.MakaleDtos;
using Entities.Models;
using Entity.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IMakaleServices
    {
        // Senkron metotlar
        IEnumerable<Makale> GetAllMakale(bool trackChanges);
        IEnumerable<Makale> GetAllMakaleDetails(MakaleRequestParameters m);
        Makale? GetOneMakale(int id, bool trackChanges);
        IQueryable<Makale> GetMakaleCarousel(bool trackChanges);
        IQueryable<Makale> GetMakaleIsShowHome(bool trackChanges);
        IQueryable<Makale> GetMakaleIsShow(bool trackChanges);
        MakaleDtoUpdate GetOneMakaleUpdate(int id, bool trackChanges);

        // Asenkron metotlar
        Task<IEnumerable<Makale>> GetAllMakaleAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetAllMakaleDetailsAsync(MakaleRequestParameters m);
        Task<Makale?> GetOneMakaleAsync(int id, bool trackChanges);
        Task<IEnumerable<Makale>> GetMakaleCarouselAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetMakaleIsShowHomeAsync(bool trackChanges);
        Task<IEnumerable<Makale>> GetMakaleIsShowAsync(bool trackChanges);

        void CreateMakale(MakaleDtoInsertion makaleDto);
        void OneUpdateMakale(MakaleDtoUpdate makaleDto);
        void Deletemakale(int id);
        void UpdateMakale(Makale makale);
    }
}
