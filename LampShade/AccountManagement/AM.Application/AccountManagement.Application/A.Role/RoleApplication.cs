using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application.A.Role
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operationResult = new OperationResult();
            if (_roleRepository.Exist(x => x.Name == command.Name))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }

            var role = new Domain.RoleAgg.Role(command.Name);
            _roleRepository.Create(role);
            _roleRepository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operationResult = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null)
            {
                return operationResult.Failed(ApplicationMessages.RecordNotFound);
            }

            if (_roleRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);
            }
            role.Edit(command.Name);
            _roleRepository.Save();
            return operationResult.Succeeded();
        }

        public List<RoleViewModel> GetRolls()
        {
            return _roleRepository.GetRolls();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }
    }
}