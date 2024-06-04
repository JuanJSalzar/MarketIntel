using Microsoft.Identity.Client;

namespace backend.Helpers.StringMessages
{
    public static class Messages
    {
        public static class HttpMessages
        {
            public const string BadRequestMessage = "The request could not be understood by the server due to malformed syntax.";
            public const string NotFoundMessage = "The requested resource could not be found but, you can try again.";
        }

        public static class ValidationMessages
        {
            /* Stock */
            public const string SymbolMessage = "The Symbol field is required.";
            public const string CompanyNameMessage = "The Company Name field is required.";
            public const string IndustryMessage = "The Industry field is required.";

            /* Comment */
            public const string TitleMessageMinLength = "Title should have 5 or more letters ";
            public const string TitleMessageMaxLength = "Title should have 500 or less letters ";
            public const string ContentMessageMinLength = "Content should have 5 or more letters ";
            public const string ContentMessageMaxLength = "Content should have 500 or less letters ";
        }
    }
}
