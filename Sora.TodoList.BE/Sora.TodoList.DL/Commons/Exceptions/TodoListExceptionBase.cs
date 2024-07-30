using System;

namespace Sora.TodoList.DL.Commons.Exceptions
{
    public class TodoListExceptionBase : Exception
    {
        public string Code { get; set; }

        public TodoListExceptionBase(string code, string message) : base(message)
        {
            Code = code;
        }
    }
}