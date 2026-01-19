using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MenuOnWeek.Clients.Menus;
using MenuOnWeek.Contracts.Menus;
using MenuOnWeek.Domain.Menus;
using Microsoft.Extensions.DependencyInjection;

namespace MenuOnWeek.Frontend.Menu;

public partial class AddMenuForm : Form
{
    private MenuForm menuForm;
    private IMenuClient menuClient;

    public AddMenuForm()
    {
        menuClient = Program.ServiceProvider.GetRequiredService<IMenuClient>();

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

        var addMenuModel = new MenuCreateRequest() {

             Name = menuDto.Name,
             MenuType = menuDto.MenuType,
             MenuRecipes = menuDto.Recipes.Select(x => new MenuRecipesCreateOrUpdateRequest() {
                RecipeId = x.RecipeId,
                Serve = x.ServeCount,
                DaysOfWeek = x.Date,
                Meal = x.Meal
            }).ToList()
        };
        menuClient.Add(addMenuModel, CancellationToken.None);
        Close();
    }
}
