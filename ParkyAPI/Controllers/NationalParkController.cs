using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Repository.IRepository;
using AutoMapper;
using ParkyAPI.Modals.Dtos;
using ParkyAPI.Modals;
using Microsoft.AspNetCore.Authorization;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    [ApiExplorerSettings(GroupName = "ParkyOpenAPISpecNP")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class NationalParkController : ControllerBase
    {
        private INationalParkRepository _npRepository;
        private readonly IMapper _mapper;

        public NationalParkController(INationalParkRepository npRepository, IMapper mapper)
        {
            _mapper = mapper;
            _npRepository = npRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDto>))]
        public IActionResult GetNationalParks()
        {
            var obj = _npRepository.GetNationalParks();
            var objDtoList = new List<NationalParkDto>();            
            foreach (var _Obj in obj)
            {
                //objDto.Add(_mapper.Map<NationalParkDto>(_Obj));
                var objDto = new NationalParkDto();
                objDto.Name = _Obj.Name;
                objDto.State = _Obj.State;
                objDto.Id = _Obj.Id;
                objDto.Creatred = _Obj.Creatred;
                objDto.Established = _Obj.Established;
                objDto.Picture = _Obj.Picture;
                objDtoList.Add(objDto);
            }
            return Ok(objDtoList);
        }
        [HttpGet("{NationalParkId:int}", Name = "GetNationalPark")]
        [ProducesResponseType(200, Type = typeof(NationalParkDto))]
        [ProducesResponseType(404)]
        [Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalPark(int NationalParkId)
        {
            var Obj = _npRepository.GetNationalPark(NationalParkId);
            if(Obj == null)
            {
                return NotFound();
            }
            //var objDto = _mapper.Map<NationalParkDto>(Obj);
            var objDto = new NationalParkDto()
            {
                Name = Obj.Name,
                Creatred = Obj.Creatred,
                State = Obj.State,
                Id = Obj.Id,
                Established = Obj.Established,
                Picture = Obj.Picture
            };
            return Ok(objDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NationalParkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto _nationalParkDto)
        {
            if(_nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            if(_npRepository.NationalParkExists(_nationalParkDto.Name))
            {
                ModelState.AddModelError("", "NationalPark Already Exists. !");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nationlaParkObj = _mapper.Map<NationalParks>(_nationalParkDto);
            if(!_npRepository.CreateNationalPark(nationlaParkObj))
            {
                ModelState.AddModelError("", $"Something went wroung while saving the record{nationlaParkObj.Name}");
            }
            return CreatedAtRoute("GetNationalPark", new { NationalParkId = nationlaParkObj.Id }, nationlaParkObj);
        }
        [HttpPatch("{NationalparkID:int}",Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int NationalparkID,[FromBody] NationalParkDto _nationalParkDto)
        {
            if (_nationalParkDto == null || NationalparkID != _nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }
            var nationalParkObj = _mapper.Map<NationalParks>(_nationalParkDto);
            if (!_npRepository.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wroung while saving the record{nationalParkObj.Name}");
            }
            return NoContent();
        }
        [HttpDelete("{NationalparkID:int}", Name = "DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int NationalparkID)
        {
            if (!_npRepository.NationalParkExistId(NationalparkID))
            {
                return NotFound();
            }
            var nationalParkObj = _npRepository.GetNationalPark(NationalparkID);
            if (!_npRepository.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went wroung while Deleting the record{nationalParkObj.Name}");
            }
            return NoContent();
        }
    }
}