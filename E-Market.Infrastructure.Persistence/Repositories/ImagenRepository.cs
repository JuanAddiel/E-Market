using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Domain.Entites;
using E_Market.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class ImagenRepository : GenericRepository<Imagen>, IImagenRepository
    {
        private readonly E_MarketContext _context;
        private readonly IImagenRepository _imagenRepository;
        public ImagenRepository(E_MarketContext context):base( context )
        {
            _context = context;
        }
        public async Task<List<Imagen>> GetAllImagenes(int IdAnuncio)
        {
            List<Imagen> imagen = await _imagenRepository.GetAll();
            return imagen.Where(i => i.idAnuncio == IdAnuncio).ToList();
        }
    }
}
