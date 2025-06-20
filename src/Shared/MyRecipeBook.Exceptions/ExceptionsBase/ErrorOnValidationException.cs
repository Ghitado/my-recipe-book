﻿namespace MyRecipeBook.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : MyRecipeBookException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty) 
        { 
            ErrorMessages = errorMessages;
        }
    }
}
