using AutoMapper;
using Entities.Dtos.MakaleDtos;
using Entities.Models;
using Entity.RequestParameters;
using Repositories;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class MakaleManager : IMakaleServices
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public MakaleManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public void CreateMakale(MakaleDtoInsertion makaleDto)
        {
            if (string.IsNullOrEmpty(makaleDto.MakaleName))
                throw new ArgumentException("Makale adı boş olamaz.");

            var makale = new Makale
            {
                MakaleName = makaleDto.MakaleName,
                MakaleSummary = makaleDto.MakaleSummary,
                MakaleContent = makaleDto.MakaleContent,
                MakaleDate = makaleDto.MakaleDate,
                MakaleIsShow = makaleDto.MakaleIsShow,
                MakaleImagesUrl = makaleDto.MakaleImagesUrl,
                KategoriId = makaleDto.KategoriId ?? 1,
                MakaleCarousel = makaleDto.MakaleCarousel
            };

            
        

        _manager.Makale.Create(makale);
            _manager.Save();
        }

        public void GetDeleteMakale(int id)
        {
            Makale makale = _manager.Makale.GetOneMakale(id, false);
            if (makale == null)
                throw new Exception("Makale Bulunamadı");

            _manager.Makale.Delete(makale);
            _manager.Save();
        }

        public void GetUpdateMakale(MakaleDtoUpdate makaleDto)
        {
            var entity = _mapper.Map<Makale>(makaleDto);
            _manager.Makale.OneUpdateMakale(entity);
            _manager.Save();
        }

        public IEnumerable<Makale> GetAllMakale(bool trackChanges)
        {
            return _manager.Makale.GetAllMakale(trackChanges);
        }

        public IEnumerable<Makale> GetAllMakaleDetails(MakaleRequestParameters m)
        {
            return _manager.Makale.GetAllMakaleDetails(m);
        }

        public Makale? GetOneMakale(int id, bool trackChanges)
        {
            var makale = _manager.Makale.GetOneMakale(id, trackChanges);
            if (makale == null)
                throw new Exception("Makale Bulunamadı");

            return makale;
        }

        public MakaleDtoUpdate GetOneMakaleUpdate(int id, bool trackChanges)
        {

            var makale = GetOneMakale(id, trackChanges);
            if (makale == null)
                throw new Exception("Makale Bulunamadı");

            // Manuel dönüşüm
            var makaleDto = new MakaleDtoUpdate
            {
                Id = makale.MakaleId,
                MakaleName = makale.MakaleName,
                MakaleSummary = makale.MakaleSummary,
                MakaleContent = makale.MakaleContent,
                MakaleDate = makale.MakaleDate,
                MakaleIsShow = makale.MakaleIsShow,
                MakaleImagesUrl = makale.MakaleImagesUrl,
                KategoriId = makale.KategoriId,
                MakaleCarousel = makale.MakaleCarousel,
                MakaleIsShowHome = makale.MakaleIsShowHome,
                Kategori = makale.Kategori // Eğer kategori bilgisi gerekiyorsa
            };

            return makaleDto;
        }

        public void OneUpdateMakale(MakaleDtoUpdate makaleDto)
        {
             var entity = new Makale
            {
                MakaleId = makaleDto.Id,
                MakaleName = makaleDto.MakaleName,
                MakaleSummary = makaleDto.MakaleSummary,
                MakaleContent = makaleDto.MakaleContent,
                MakaleDate = makaleDto.MakaleDate,
                MakaleIsShow = makaleDto.MakaleIsShow,
                MakaleImagesUrl = makaleDto.MakaleImagesUrl,
                KategoriId = makaleDto.KategoriId ?? 1,
                MakaleCarousel = makaleDto.MakaleCarousel,
                MakaleIsShowHome = makaleDto.MakaleIsShowHome
            };

            _manager.Makale.OneUpdateMakale(entity);
            _manager.Save();
        }

        public void Deletemakale(int id)
        {
            Makale makale = _manager.Makale.GetOneMakale(id, false);
            _manager.Makale.Delete(makale);
            _manager.Save();
        }
        public void UpdateMakale(Makale makale)
        {
            _manager.Makale.Update(makale);
            _manager.Save();
        }

        public IQueryable<Makale> GetMakaleCarousel(bool trackChanges)
        {
            return _manager.Makale.GetMakaleCarousel(trackChanges);
        }
        public IQueryable<Makale> GetMakaleIsShowHome(bool trackChanges)
        {
            return _manager.Makale.GetMakaleIsShowHome(trackChanges);
        }
        public IQueryable<Makale> GetMakaleIsShow(bool trackChanges)
        {
            return _manager.Makale.GetMakaleIsShow(trackChanges);
        }

        // Asenkron metotların implementasyonu:

        public async Task<IEnumerable<Makale>> GetAllMakaleAsync(bool trackChanges)
        {
            return await _manager.Makale.GetAllMakaleAsync(trackChanges);
        }

        public async Task<IEnumerable<Makale>> GetAllMakaleDetailsAsync(MakaleRequestParameters m)
        {
            return await _manager.Makale.GetAllMakaleDetailsAsync(m);
        }

        public async Task<Makale?> GetOneMakaleAsync(int id, bool trackChanges)
        {
            return await _manager.Makale.GetOneMakaleAsync(id, trackChanges);
        }

        public async Task<IEnumerable<Makale>> GetMakaleCarouselAsync(bool trackChanges)
        {
            return await _manager.Makale.GetMakaleCarouselAsync(trackChanges);
        }
        public async Task<IEnumerable<Makale>> GetMakaleIsShowHomeAsync(bool trackChanges)
        {
            return await _manager.Makale.GetMakaleIsShowHomeAsync(trackChanges);
        }
        public async Task<IEnumerable<Makale>> GetMakaleIsShowAsync(bool trackChanges)
        {
            return await _manager.Makale.GetMakaleIsShowAsync(trackChanges);
        }
    }
}
