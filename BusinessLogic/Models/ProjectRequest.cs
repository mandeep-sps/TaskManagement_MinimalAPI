using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public  class ProjectRequest
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public List<int> UserId { get; set; }

    }
    public class ProjectUpdateRequest
    {
        public int Id { get; set; } 
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public List<int> UserId { get; set; }

    }

    public class ProjectResponse
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string ManagerName { get; set; }
        public DateTime CreatedOn { get; set; }


    }


}
