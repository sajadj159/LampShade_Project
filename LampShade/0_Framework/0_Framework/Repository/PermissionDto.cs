using System.Data;

namespace _0_Framework.Repository
{
    public class PermissionDto
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public PermissionDto(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}