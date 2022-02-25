using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.A.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
    }
}