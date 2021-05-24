using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using SImpl.Storage.Repository;

namespace SImpl.Http.Storage
{
    public class Transactional : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public Transactional(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _unitOfWork.BeginTransaction();

            var executedContext = await next.Invoke();
            if (executedContext.Exception == null || executedContext.ExceptionHandled)
            {
                _unitOfWork.CommitTransaction();
            }
            else
            {
                _unitOfWork.AbortTransaction();
            }
        }
    }
}