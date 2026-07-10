using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.AC.Role;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.RoleAgg.Contracts;

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

			var role = new Domain.RoleAgg.Role(command.Name, command.Permissions);
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



			role.Edit(command.Name, command.Permissions);
			_roleRepository.Save();
			return operationResult.Succeeded();
		}

		public List<RoleDto> GetRolls()
		{
			var result = _roleRepository.GetRolls();
			return result.Select(x => new RoleDto
			{
				Id = x.Id,
				Name = x.Name,
				CreationDate = x.CreationDate.ToFarsi()
			}).ToList();
		}

		public RoleDto GetDetails(long id)
		{
			var result =  _roleRepository.GetDetails(id);
			return new RoleDto
			{
				Id = result.Id,
				Name = result.Name,
				CreationDate  = result.CreationDate.ToFarsi()
			};
		}
	}
}