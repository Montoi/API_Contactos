using API_Contactos.Data;
using API_Contactos.Models.DTOs.Contact;
using API_Contactos.Models.DTOs.Response;
using API_Contactos.Models;
using API_Contactos.Repository.IRepository.IContactRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Contactos.Repository.IRepository.IPhoneNumbers;
using API_Contactos.Models.DTOs.PhoneNumberss;
using Microsoft.AspNetCore.Http.HttpResults;
using API_Contactos.Models.DTOs.PhoneNumberss;

namespace API_Contactos.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhoneNumbersController : ControllerBase
	{
		private readonly ResponseDto _response;
		private IMapper _mapper;
		private readonly IPhoneNumbersRepository _db;
		
		public PhoneNumbersController(IPhoneNumbersRepository db, IMapper mapper)
		{
			_db = db;
			_response = new ResponseDto();
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<ActionResult<ResponseDto>> GetPhoneNumbers()
		{
			try
			{
				
				IEnumerable<PhoneNumbers> phoneNumbers = await _db.GetAllAsync();
				_response.Result = _mapper.Map<List<PhoneNumbersDTO>>(phoneNumbers);

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
		[HttpGet("{id:int}", Name = "GetPhoneNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ResponseDto>> GetPhoneNumber(int id)
		{

			try
			{
				if (id == 0)
				{
					return BadRequest();
				}
				var phoneNumberss = await _db.GetAsync(u => u.id == id);
				if (phoneNumberss == null)
				{
					return NotFound();
				}

				_response.Result = _mapper.Map<PhoneNumbersDTO>(phoneNumberss);
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
		public async Task<ActionResult<ResponseDto>> CreatePhoneNumber([FromBody] MainPhoneNumberCreateDTO createDTO)
		{
			try
			{


				if (await _db.GetAsync(u => u.Number ==  createDTO.Number) != null)
				{
					ModelState.AddModelError("CustomError", "PhoneNumber already Exists!");
					return BadRequest(ModelState);
				}
				if (await _db.GetAsync(u => u.ContactId != createDTO.ContactId) != null)
				{
					ModelState.AddModelError("CustomError", "Contact does not  Exists!");
					return BadRequest(ModelState);
				}
				if (createDTO == null)
				{
					return BadRequest(createDTO);
				}

				PhoneNumbers phoneNumber = _mapper.Map<PhoneNumbers>(createDTO);

				//Logica de guardado la manejamos en el repositorio, (Patrón Repositorio)
				await _db.CreateAsync(phoneNumber);
				_response.Result = _mapper.Map<PhoneNumbersDTO>(phoneNumber);
				_response.IsSuccess = true;


				return CreatedAtRoute("GetPhoneNumber", new { id = phoneNumber.id }, _response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}
		}

		[HttpDelete("{id:int}", Name = "DeletePhoneNumber")]
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
				var phoneNumber = await _db.GetAsync(u => u.id == id);
				if (phoneNumber == null)
				{
					return NotFound();
				}

				_response.Result = _mapper.Map<PhoneNumbersDTO>(phoneNumber);
				_response.IsSuccess = true;
				await _db.Remove(phoneNumber);

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return Ok(_response);
			}
		}

		[HttpPut("{id:int}", Name = "UpdatePhoneNumber")]
		[ProducesResponseType(@StatusCodes.Status204NoContent)]
		[ProducesResponseType(@StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<ResponseDto>> UpdateContact(int id, [FromBody] PhoneNumbersUpdateDTO updateDto)
		{
			try
			{


				if (updateDto == null || id != updateDto.id)
				{
					return BadRequest(_response);
				}
				if (await _db.GetAsync(u => u.Number == updateDto.Number) != null)
				{
					ModelState.AddModelError("CustomError", "PhoneNumber already Exists!");
					return BadRequest(ModelState);
				}
				//if (await _db.GetAsync(u => u.ContactId != updateDto.ContactId) != null)
				//{
				//	ModelState.AddModelError("CustomError", "Contact does not already Exists!");
				//	return BadRequest(ModelState);
				//}
				var phoneNumber = await _db.GetAsync(e => e.id == id, false);


				PhoneNumbers model = _mapper.Map<PhoneNumbers>(updateDto);

				await _db.UpdateAsync(model);
				_response.Result = _mapper.Map<PhoneNumbersDTO>(phoneNumber);
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
