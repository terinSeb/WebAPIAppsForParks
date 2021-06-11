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
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpecTrails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class TrailController : ControllerBase
    {
        private ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailController(ITrailRepository trailRepository, IMapper mapper)
        {
            _mapper = mapper;
            _trailRepository = trailRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrailDto>))]
        public IActionResult GetTrail()
        {
            var obj = _trailRepository.GetTrail();
            var objDtoList = new List<TrailDto>();
            //var objDto = new TrailDto();
            foreach (var _Obj in obj)
            {
                objDtoList.Add(_mapper.Map<TrailDto>(_Obj));
                //objDto.Name = _Obj.Name;
                //objDto.State = _Obj.State;
                //objDto.Id = _Obj.Id;
                //objDto.Creatred = _Obj.Creatred;
                //objDtoList.Add(objDto);
            }
            return Ok(objDtoList);
        }
        [HttpGet("{TrailId:int}", Name = "GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles ="Admin")]
        public IActionResult GetTrail(int TrailId)
        {
            var Obj = _trailRepository.GetTrail(TrailId);
            if(Obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<TrailDto>(Obj);
            //var objDto = new TrailDto()
            //{
            //    Name = Obj.Name,
            //    Creatred = Obj.Creatred,
            //    State = Obj.State,
            //    Id = Obj.Id
            //};
            return Ok(objDto);
        }
        [HttpGet("[action]/{NationalParkId:int}")]
        [ProducesResponseType(200, Type = typeof(TrailDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailinNationalPark(int NationalParkId)
        {
            var ObjList = _trailRepository.GetTrailsNationPark(NationalParkId);
            if (ObjList == null)
            {
                return NotFound();
            }
            var ObjDto = new List<TrailDto>();
            foreach (var obj in ObjList)
            {
                ObjDto.Add(_mapper.Map<TrailDto>(obj));
            }
            //var objDto = new TrailDto()
            //{
            //    Name = Obj.Name,
            //    Creatred = Obj.Creatred,
            //    State = Obj.State,
            //    Id = Obj.Id
            //};
            return Ok(ObjDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrailDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult CreateTrail([FromBody] TraillnsertDto _TrailDto)
        {
            if(_TrailDto == null)
            {
                return BadRequest(ModelState);
            }
            if(_trailRepository.TrailExists(_TrailDto.Name))
            {
                ModelState.AddModelError("", "Trail Already Exists. !");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nationlaParkObj = _mapper.Map<Trail>(_TrailDto);
            if(!_trailRepository.CreateTrail(nationlaParkObj))
            {
                ModelState.AddModelError("", $"Something went wroung while saving the record{nationlaParkObj.Name}");
            }
            return CreatedAtRoute("GetTrail", new { TrailId = nationlaParkObj.Id }, nationlaParkObj);
        }
        [HttpPatch("{TrailID:int}",Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int TrailID,[FromBody] TrailUpdateDto _TrailDto)
        {
            if (_TrailDto == null || TrailID != _TrailDto.Id)
            {
                return BadRequest(ModelState);
            }
            var TrailObj = _mapper.Map<Trail>(_TrailDto);
            if (!_trailRepository.UpdateTrail(TrailObj))
            {
                ModelState.AddModelError("", $"Something went wroung while saving the record{TrailObj.Name}");
            }
            return NoContent();
        }
        [HttpDelete("{TrailID:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int TrailID)
        {
            if (!_trailRepository.TrailExistId(TrailID))
            {
                return NotFound();
            }
            var TrailObj = _trailRepository.GetTrail(TrailID);
            if (!_trailRepository.DeleteTrail(TrailObj))
            {
                ModelState.AddModelError("", $"Something went wroung while Deleting the record{TrailObj.Name}");
            }
            return NoContent();
        }
    }
}