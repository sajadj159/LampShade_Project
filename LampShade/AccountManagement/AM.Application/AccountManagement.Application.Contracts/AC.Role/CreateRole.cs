using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.AC.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<int> Permissions { get; set; }

    }
}