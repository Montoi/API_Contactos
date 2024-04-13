namespace API_Contactos.Models.DTOs.Response
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
		public List<string> ErrorMessages { get; set; }

	}
}
