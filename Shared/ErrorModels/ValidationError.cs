namespace Shared.ErrorModels
{
    public class ValidationError
    {
        public string Field {  get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}