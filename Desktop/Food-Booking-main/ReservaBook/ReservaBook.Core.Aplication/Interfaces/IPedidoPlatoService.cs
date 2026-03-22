using ReservaBook.Core.Aplication.Dtos.pdidoPlato;
using ReservaBook.Core.Aplication.Dtos.pedido;
using ReservaBook.Core.Aplication.Dtos.plato;
using ReservaBook.Core.Aplication.Services;
using ReservaBook.Core.Domain.Entities;


namespace ReservaBook.Core.Aplication.Interfaces
{

    public interface IPedidoPlatoService : IGenericService<CreatePedidoPlatoDto, CreatePedidoPlatoDto, PedidosPlatoResponseDto,PedidoPlato>
    {

        Task<List<PedidosPlatoResponseDto?>> AddRangeAsync(List<CreatePedidoPlatoDto> dto);
 
    }

}
