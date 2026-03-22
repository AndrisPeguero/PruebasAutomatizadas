

using ReservaBook.Core.Aplication.Dtos.restaurante;
using ReservaBook.Core.Aplication.Dtos.User;
using ReservaBook.Core.Domain.Entities;

namespace ReservaBook.Core.Aplication.Interfaces
{
    public interface IRestauranteServices : IGenericService<CreateRestauranteRequestDto, CreateRestauranteRequestDto, RestauranteResponseDto,Restaurante> 
    {


        Task<DeleteRestauranteResponseDto?> DeleteRestauranteAsync(int id);

    }
}
