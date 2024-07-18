using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContactsMicroservice.DTOs;
using ContactsMicroservice.Entities;
using ContactsMicroservice.Extensions;
using ContactsMicroservice.Repository.Contracts;

namespace ContactsMicroservice.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   
            var result = await _repository.GetAll<ContactMinimalDto>();
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var result = await _repository.GetById<ContactDto>(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UpsertContactDto body, IMapper mapper)
        {
            var roles = User.GetApiClientRoles();

            if (roles is null) return Unauthorized();

            if (roles.Contains("user"))
            {
                var contact = mapper.Map<Contact>(body);
                _repository.Add(contact);
                if (!await _repository.SafeChangesAsync())
                    return BadRequest("Something went wrong while posting a contact");

                var contactDto = mapper.Map<ContactDto>(contact);
                return CreatedAtAction(nameof(GetOne), new { id = contact.Id }, contactDto);
            }
            return Forbid();
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var roles = User.GetApiClientRoles();

            if (roles is null) return Unauthorized();

            if (roles.Contains("user"))
            {
                var contact = await _repository.GetById(id);
                if (contact == null)
                    return NotFound();
                _repository.Delete(contact);
                if (!await _repository.SafeChangesAsync())
                    return BadRequest("Something went wrong while deleting");
                return NoContent();
            }
            return Forbid();
        }

        [HttpPatch]
        public IActionResult Test()
        {
            var response = new { message = "this is a test" };
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpsertContactMinDto body, IMapper mapper)
        {
            var roles = User.GetApiClientRoles();

            if (roles is null) return Unauthorized();

            if (roles.Contains("user"))
            {
                var contact = await _repository.GetById(id);
                if (contact == null)
                    return NotFound("Contact not found");

                mapper.Map(body, contact);
                _repository.Edit(contact);

                if (!await _repository.SafeChangesAsync())
                    return BadRequest("Something went wrong while updating the contact");

                var contactDto = mapper.Map<ContactDto>(contact);
                return Ok(contactDto);
            }
            return Forbid();
        }
    }
}
