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
using Utils;

namespace MenuOnWeek.Frontend.Menu
{
    public partial class MenuControl : UserControl
    {

        private IMenuService menuService;

        private MenuForm? menuForm;

        public MenuControl()
        {
            menuService = Program.ServiceProvider.GetRequiredService<IMenuService>();
            InitializeComponent();

            RefreshMenusList();
        }

        private void RefreshMenusList()
        {
            MenusList.Items.Clear();

            MenusList.Items.AddRange(menuService.GetAll(0, 100).Select(x => x.Name).ToArray());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddMenuForm addMenuForm = new AddMenuForm();
            addMenuForm.ShowDialog();

            RefreshMenusList();

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (menuForm is not null)
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

                var updateRequest = new MenuUpdateModel()
                {
                    Id = menuService.GetByName(MenusList.SelectedItem.Required().ToString().Required()).Id,
                    Name = menuDto.Name,
                    MenuType = menuDto.MenuType,
                    Recipes = menuDto.Recipes.Select(x => new MenuElementModel()
                    {
                        RecipeId = x.RecipeId,
                        Date = x.Date,
                        Meal = x.Meal,
                        ServeCount = x.ServeCount
                    }).ToList()
                };
                menuService.Update(updateRequest);
            }
        }

        private void MenusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MenusList.SelectedItem is not null)
            {
                if (menuForm is not null)
                {
                    Controls.Remove(menuForm);
                }

                menuForm = new MenuForm(menuService.GetByName(MenusList.SelectedItem.ToString().Required()));
                Controls.Add(menuForm);

                menuForm.Location = new Point(200, 5);
            }
        }

        private void MenusList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MenusList.SelectedItem is not null)
                {
                    menuService.Remove(menuService.GetByName(MenusList.SelectedItem.ToString().Required()).Id);
                    RefreshMenusList();
                    Controls.Remove(menuForm);
                    menuForm = null;
                }
            }
        }
    }
}
