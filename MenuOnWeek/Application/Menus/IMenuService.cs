using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace MenuOnWeek.Application.Menus;

public interface IMenuService
{
    void Add(CreateMenuModel entity);

    IReadOnlyList<MenuViewModel> GetAll(int offset, int limit);

    void Update(MenuUpdateModel entity);

    void Remove(Guid entity);
}
