using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application;

public interface IMenuService
{
    void Add(Menu entity);

    IReadOnlyList<Menu> GetAll(Func<Menu, bool> predicate, int offset, int limit);

    void Update(Menu entity);

    void Remove(Menu entity);
}
