using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLab
{

    // this jsaon is converted automaticlly in classes an props after theese steps =>
        // 1. Delete all in 
        // 2. Edit -> Paste Special -> Paset JSON as CLASSES

    //{"employees":[
    //{"name":"Shyam", "email":"shyamjaiswal@gmail.com"},  
    //{ "name":"Bob", "email":"bob32@gmail.com"},  
    //{ "name":"Jai", "email":"jai87@gmail.com"}  
    //]}  


    public class Rootobject
    {
        public Employee[] employees { get; set; }
    }

    public class Employee
    {
        public string name { get; set; }
        public string email { get; set; }
    }

}
