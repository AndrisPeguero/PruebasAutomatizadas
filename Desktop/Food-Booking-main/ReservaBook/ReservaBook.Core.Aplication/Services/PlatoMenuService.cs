using AutoMapper;
using Microsoft.AspNetCore.Http;
using ReservaBook.Core.Aplication.Dtos.menu;
using ReservaBook.Core.Aplication.Dtos.platoMenu;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;


namespace ReservaBook.Core.Aplication.Services
{
    public class PlatoMenuService : GenericService<SavePlatoMenuRequesDto, SavePlatoMenuRequesDto, PlatoMenuResponseDto, PlatoMenu>, IPlatoMenuServices
    {
        private readonly IPlatoMenuRepository _repo;
        private readonly IMenuRepository _menuRepository;
        private readonly IPlatoRepository platoRepository;
        private readonly IMapper _mapper;
        public PlatoMenuService(IPlatoMenuRepository _repo, IMenuRepository _menuRepository, IPlatoRepository platoRepository, IMapper _mapper) : base(_repo, _mapper)
        {

            this.platoRepository = platoRepository;
            this._repo = _repo;
            this._mapper = _mapper;
            this._menuRepository = _menuRepository;

        }



        public async Task<List<PlatoMenuResponseDto>> AddPlatoAlMenu(int menuId, List<int> idPlatos)
        {
            var response = new PlatoMenuResponseDto() { HasError = false , Errors = [] };
            var responses = new List<PlatoMenuResponseDto>();

            try
            {
                if (menuId <= 0)
                {

                    response.HasError = true;
                    response.Errors.Add("Ocurrio un error: el id del menu debe ser valido, favor verificar");
                    responses.Add(response);
                    return responses;

                }


                if (idPlatos == null || idPlatos.Count <= 0)
                {


                    response.HasError = true;
                    response.Errors.Add("Ocurrio un error: no se ha seleccionado ni un plato, favor verificar");
                    responses.Add(response);
                    return responses;

                }


                var menuExist = await _menuRepository.GetByIdAsync(menuId);
               

                if(menuExist == null)
                {

                    response.HasError = true;
                    response.Errors.Add("message: no se encontro un menu con ese id, favor verificar");
                    responses.Add(response);
                    return responses;

                }



                if (menuExist == null)
                {

                    response.HasError = true;
                    response.Errors.Add("message: no se encontro un menu con ese id, favor verificar");
                    responses.Add(response);
                    return responses;
                }


                var listEntities = new List<PlatoMenu>();

                foreach (var idPlato in idPlatos)
                {

                    var platoExist = await platoRepository.GetByIdAsync(idPlato);

                    if(platoExist == null)
                    {

                        response.HasError = true;
                        response.Errors.Add($"message: no se encontro un plato con ese id {idPlato}, favor verificar");
                        continue;
                    }


                    var platoMenu = new PlatoMenu()
                    {

                        Id = 0,
                        MenuId = menuId,
                        PlatoId = idPlato
                    };
                

                    listEntities.Add(platoMenu);
                  
                }




                if(listEntities != null || listEntities!.Count > 0)
                {

                    await _repo.AddRange(listEntities);
                    var map = _mapper.Map<List<PlatoMenuResponseDto>>(listEntities); 
                    return map;
                }


                response.HasError = true;
                response.Errors.Add("Ocurrio un error al intentar agregar el plato al menu");
                responses.Add(response);
                return responses;

            }
            catch (Exception ex)
            {

                throw new Exception($"Ocurrio un error al intentar agregar el plato al menu: {ex.Message}");
            }


        }




        public async  Task<PlatoMenuResponseDto> DeletePlatoDelMenu(int idMenu, int idPlato)
        {
            var response = new PlatoMenuResponseDto() { HasError = false, Errors = [] };

            try
            {

                if(idMenu <= 0 || idPlato <= 0)
                {
                    response.HasError = true;
                    response.Errors.Add("Ocurrio un error,Debes indicar un plato valido para eliminarlo");
                    return response;
                }


                var relacion = await _repo.GetByIdPlatoYMenuId(idPlato,idMenu);

                if(relacion == null)
                {
                    response.HasError = true;
                    response.Errors.Add("El plato seleccionado no pertenece a ese menu");
                    return response;
                   
                }

                await _repo.DeleteAsync(relacion!.Id);
                return response;
            }
            catch (Exception ex)
            {


                throw new Exception($"Ocurrio un error al intentar eliminar el plato del menu: {ex.Message}");

            }
        }
    }
}
