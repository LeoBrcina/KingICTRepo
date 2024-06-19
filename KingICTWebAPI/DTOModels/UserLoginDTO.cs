namespace KingICTWebAPI.DTOModels
{
    public class UserLoginDto
    {
        //data transfer object for choosing which information we want to show after initially getting everything from the dummy api

        public string username { get; set; }
        public string password { get; set; }
    }
}
