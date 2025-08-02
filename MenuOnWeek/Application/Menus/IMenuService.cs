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
    Task Add(CreateMenuCommand entity, CancellationToken token);

    Task<IReadOnlyList<MenuViewModel>> GetAll(int offset, int limit, CancellationToken token);

    Task Update(MenuUpdateModel entity, CancellationToken token);

    Task Remove(Guid entity, CancellationToken token);

    Task<MenuViewModel?> GetByName(string name, CancellationToken token);
}
