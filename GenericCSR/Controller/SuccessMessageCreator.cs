namespace GenericCSR.Controller
{
    public static class SuccessMessageCreator
    {
        public static object GetMessage()
        {
            return new
            {
                success = "Success message from abstract class"
            };
        }
    }
}