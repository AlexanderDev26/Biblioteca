using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Response
{
    public class Result<T>
    {
        public T Data { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public List<string> Errors { get; private set; }

        private Result(T data, bool isSuccess, string message, List<string> errors)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
        }

        public static Result<T> Success(T data, string menssage) => new Result<T>(data, true, menssage, new List<string>());
        public static Result<T> Failure(List<string> errors, string message) => new Result<T>(default, false, message, errors);

    }
}
