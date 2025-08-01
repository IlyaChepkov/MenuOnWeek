using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MenuOnWeek.Application.Menus;
using MenuOnWeek.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Menu;

public partial class AddMenuForm : Form
{
    private MenuForm menuForm;
    private IMenuService menuService;

    public AddMenuForm()
    {
        menuService = Program.ServiceProvider.GetRequiredService<IMenuService>();

        InitializeComponent();

        menuForm = new MenuForm();
        Controls.Add(menuForm);
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        var menuDto = menuForm.GetMenuDto();
        if (String.IsNullOrEmpty(menuDto.Name))
        {
            statusStrip1.Items[0].Text = "У меню нет имени";
            return;
        }
        if (menuDto.Recipes.Any(x => (menuDto.MenuType == MenuType.MenuOnWeek & x.Date == null)
            || (menuDto.MenuType != MenuType.MenuOnEvent & x.Meal == null) || (x.ServeCount == 0)))
        {
            statusStrip1.Items[0].Text = "Заполнены не все ячейки таблицы";
            return;
        }

        statusStrip1.Items[0].Text = "";

        var addMenuModel = new CreateMenuModel()
        {
            Name = menuDto.Name,
            MenuRecipes = menuDto.Recipes.Select(x => new MenuElementModel()
            {
                RecipeId = x.RecipeId,
                ServeCount = x.ServeCount,
                Date = x.Date,
                Meal = x.Meal
            }).ToList(),
            MenuType = menuDto.MenuType
        };
        menuService.Add(addMenuModel);
        Close();
    }
}
