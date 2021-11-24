using AutoMapper;
using GestionContact.CORE;
using GestionContact.DAL;
using GestionContactApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthRequired]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository<Contact> _repo;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository<Contact> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        // GET: api/<Contact>/userId
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                IEnumerable<Contact> entity = await _repo.GetAll(userId);
                IEnumerable<ContactApi> entityApis = _mapper.Map<IEnumerable<ContactApi>>(entity);
                if (entityApis == null)
                {
                    return NotFound();
                }

                return Ok(entityApis);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ContactController>/5/userId
        [HttpGet("{id}/{userId}")]
        public async Task<IActionResult> Get(int id, int userId)
        {
            try
            {
                Contact tmpentity = await _repo.GetOne(id, userId);
                ContactApi tmpentityApi = _mapper.Map<ContactApi>(tmpentity);

                if (tmpentityApi == null)
                {
                    return NotFound();
                }
                return Ok(tmpentityApi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/<ContactController>/ByName/Name
        [HttpGet]
        [Route("ByName/{Name}/{UserId}")]
        public async Task<IActionResult> GetByName(string name, int userId)
        {
            try
            {
                IEnumerable<Contact> entity = await _repo.GetName(name, userId);
                IEnumerable<ContactApi> entityApis = _mapper.Map<IEnumerable<ContactApi>>(entity);
                if (entityApis == null)
                {
                    return NotFound();
                }

                return Ok(entityApis);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post(ContactApi entity)
        {

            try
            {
                entity.Id = 0;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Contact tmpentity = _mapper.Map<Contact>(entity);
                Contact createdtmpentity;
                try
                {
                    createdtmpentity = await _repo.Create(tmpentity);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    return BadRequest();
                }
                ContactApi resultTPApi = _mapper.Map<ContactApi>(createdtmpentity);
                if (tmpentity == null)
                {
                    return BadRequest();
                }
                return Ok(resultTPApi);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContactApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != value.Id)
            {
                return BadRequest();
            }
            Contact entity = _mapper.Map<Contact>(value);
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repo.Update(id, entity);
                return Ok(entity);
            }
            catch (Exception)
            {
                if (!entityExists(id, entity.Email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repo.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                    throw;
            }
        }

        [HttpGet("testEmail/{id}/{email}")]
        private bool entityExists(int id, string email)
        {
            return _repo.EmailExist(id, email) != null;
        }
    }
}