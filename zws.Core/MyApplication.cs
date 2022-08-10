using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zws.Core
{
    public class MyApplication : IApplication
    {

        public Animal GetData(AnimalDto dto)
        {
            return new Animal { Id = dto.Id, Name = "我也不知道这样算不算成功了", Description = "洗吧" };
        }
    }
    public interface IApplication { }
    public class Animal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class AnimalDto
    {
        public string Id { get; set; }
    }
}
