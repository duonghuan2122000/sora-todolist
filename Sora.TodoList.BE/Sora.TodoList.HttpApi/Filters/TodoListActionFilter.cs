using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sora.TodoList.DL.Commons.Exceptions;
using Sora.TodoList.HttpApi.Dtos;

namespace Sora.TodoList.HttpApi.Filters
{
    public class TodoListActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = new ResultResDtoBase
            {
                Success = true,
            };

            if (context.Exception != null)
            {
                result.Success = false;
                result.Message = context.Exception.Message;

                if (context.Exception is TodoListExceptionBase todoListException)
                {
                    result.Code = todoListException.Code;
                }

                context.Exception = null;
            }
            else
            {
                switch (context.Result)
                {
                    case ObjectResult objectResult:
                        result.Data = objectResult.Value;
                        break;

                    case JsonResult jsonResult:
                        result.Data = jsonResult.Value;
                        break;

                    default:
                        return;
                }
            }

            context.Result = new ObjectResult(result);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}