using System;

namespace GenericCSR.Controller
{
    public static class ErrorMessageCreator
    {
        public static object GetMessage(Exception e)
        {
            return new
            {
                error = "Десила се грешка:\n" +e.Message
            };
        }

        public static object GetUnauthorizedMessage()
        {
            return new
            {
                error = "Нисте ауторизовани за ову акцију!"
            };
        }
    }
}