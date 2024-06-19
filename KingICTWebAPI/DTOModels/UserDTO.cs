namespace KingICTWebAPI.DTOModels
{
    public class UserDTO
    {
        //data transfer object for choosing which information we want to show after initially getting everything from the dummy api
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
