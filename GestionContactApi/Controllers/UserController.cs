using AutoMapper;
using GestionContact.CORE;
using GestionContact.DAL;
using GestionContactApi.Helpers;
using GestionContactApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestionContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User> _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository<User> repo, IMapper mapper, IConfiguration config,
        ITokenService tokenService)
        {
            _repo = repo;
            _mapper = mapper;
            _config = config;
            _tokenService = tokenService;
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<User> entity = await _repo.GetAll();
                IEnumerable<UserApi> entityApis = _mapper.Map<IEnumerable<UserApi>>(entity);
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

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                User tmpentity = await _repo.GetOne(id);
                UserApi tmpentityApi = _mapper.Map<UserApi>(tmpentity);

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

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserApi entity)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                User tmpentity = _mapper.Map<User>(entity);
                User createdtmpentity;
                try
                {
                    createdtmpentity = await _repo.Create(tmpentity);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                    return BadRequest();
                }
                UserApi resultTPApi = _mapper.Map<UserApi>(createdtmpentity);
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

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != value.Id)
            {
                return BadRequest();
            }
            User entity = _mapper.Map<User>(value);
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
                if (!entityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE api/<UserController>/5
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

        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(ViewLoginApi login)
        {
            if (ModelState.IsValid)
            {
                LoginSuccessDto dto = await _repo.Login(_mapper.Map<LoginDto>(login));
                try
                {

                    if (dto is null)
                        return NotFound();
                    //Calculer le Token                
                    dto.Token = _tokenService.GenerateToken(dto);
                    dto.ExpirationDate = DateTime.Now.AddDays(1);
                    return Ok(dto);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err);
                }
            }
            return BadRequest();
        }

        private bool entityExists(int id)
        {
            return _repo.GetOne(id) != null;
        }
    }
}
