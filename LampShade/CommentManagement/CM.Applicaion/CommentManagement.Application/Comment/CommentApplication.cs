using System.Collections.Generic;
using _0_Framework.Application;
using CommentManagement.Application.Contract.A.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application.Comment
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operationResult = new OperationResult();
            var comment = new Domain.CommentAgg.Comment(command.Name,command.Email,command.Website,command.Description,command.OwnerRecordId,command.Type,command.ParentId);
            _commentRepository.Create(comment);
            _commentRepository.Save();          
            return operationResult.Succeeded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }

        public OperationResult Confirm(long id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment==null)
            {
               return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Confirm();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Cancel(long id)
        {
            var operationResult = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment==null)
            {
              return  operationResult.Failed(ApplicationMessages.RecordNotFound);
            }
            comment.Cancel();
            _commentRepository.Save();
            return operationResult.Succeeded();
        }
    }
}