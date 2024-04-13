using API_Contactos.Data;
using API_Contactos.Models;
using API_Contactos.Models.DTOs;
using API_Contactos.Models.DTOs.Contact;
using API_Contactos.Models.DTOs.Response;
using API_Contactos.Repository.IRepository.IContactRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace API_Contactos.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly ResponseDto _response;
		private IMapper _mapper;
		private readonly IContactRepository _db;
		private readonly AplicationDbContext _dbb;
		public ContactController(IContactRepository db, IMapper mapper, AplicationDbContext dbb)
        {
			_db = db;
			_response = new ResponseDto();
			_mapper = mapper;
			_dbb = dbb;
		}


		[HttpGet]
		public async Task<ActionResult<ResponseDto>> GetContacts()
		{
			try
			{
				
				IEnumerable<Contact> contactList = await _db.GetAllAsync();
				_response.Result = _mapper.Map<List<ContactDTO>>(contactList);
				
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}


		}
		[HttpGet("{id:int}", Name = "GetContact")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ResponseDto>> GetContact(int id)
		{

			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var contact = await _db.GetAsync(u => u.Id == id);
				if (contact == null)
				{
					return NotFound();
				}

				_response.Result = _mapper.Map<ContactDTO>(contact);
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}


		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<ResponseDto>> CreateContact([FromBody] ContactCreateDTO createDTO)
		{
			try
			{


				if (await _db.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
				{
					ModelState.AddModelError("CustomError", "Contact already Exists!");
					return BadRequest(ModelState);
				}
				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}

				Contact contact = _mapper.Map<Contact>(createDTO);
				
				//Logica de guardado la manejamos en el repositorio, (Patrón Repositorio)
				await _db.CreateAsync(contact);
				_response.Result = _mapper.Map<ContactDTO>(contact);
				_response.IsSuccess = true;


				return CreatedAtRoute("GetContact", new { id = contact.Id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}
		}

		[HttpDelete("{id:int}", Name = "DeleteContact")]
		[ProducesResponseType(@StatusCodes.Status204NoContent)]
		[ProducesResponseType(@StatusCodes.Status404NotFound)]
		[ProducesResponseType(@StatusCodes.Status400BadRequest)]

		public async Task<ActionResult<ResponseDto>> DeleteContact(int id)
		{
			try
			{


				if (id == 0)
				{
					return BadRequest();
				}
				var contact = await _db.GetAsync(u => u.Id == id);
				if (contact == null)
				{
					return NotFound();
				}

				_response.Result = _mapper.Map<ContactDTO>(contact);			
				_response.IsSuccess = true;
				await _db.Remove(contact);

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}
		}

		[HttpPut("{id:int}", Name = "UpdateContact")]
		[ProducesResponseType(@StatusCodes.Status204NoContent)]
		[ProducesResponseType(@StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ResponseDto>> UpdateContact(int id, [FromBody] ContactUpdateDTO updateDto)
		{
			try
			{


				if (updateDto == null || id != updateDto.Id)
				{
					return BadRequest(_response);
				}
				var contact = await _db.GetAsync(e => e.Id == id, false);


				Contact model = _mapper.Map<Contact>(updateDto);

				await _db.UpdateAsync(model);
				_response.Result = _mapper.Map<ContactDTO>(model);
				_response.IsSuccess = true;
				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };

				return Ok(_response);
			}
		}
	}
}
