namespace MenuOnWeek.Frontend.Recipe
{
    partial class RecipeForm
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipeForm));
            IngredientsTable = new DataGridView();
            Ingredient = new DataGridViewComboBoxColumn();
            Count = new DataGridViewTextBoxColumn();
            Unit = new DataGridViewComboBoxColumn();
            Image = new PictureBox();
            RecipeName = new TextBox();
            Price = new TextBox();
            saveFileDialog = new SaveFileDialog();
            Description = new TextBox();
            ((System.ComponentModel.ISupportInitialize)IngredientsTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Image).BeginInit();
            SuspendLayout();
            // 
            // IngredientsTable
            // 
            IngredientsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            IngredientsTable.Columns.AddRange(new DataGridViewColumn[] { Ingredient, Count, Unit });
            IngredientsTable.Location = new Point(470, 14);
            IngredientsTable.Name = "IngredientsTable";
            IngredientsTable.Size = new Size(342, 288);
            IngredientsTable.TabIndex = 0;
            IngredientsTable.CellEndEdit += IngredientsTable_CellEndEdit;
            IngredientsTable.RowsRemoved += IngredientsTable_RowsRemoved;
            // 
            // Ingredient
            // 
            Ingredient.HeaderText = "Ингредиент";
            Ingredient.Name = "Ingredient";
            Ingredient.Resizable = DataGridViewTriState.True;
            Ingredient.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Count
            // 
            Count.HeaderText = "Количество";
            Count.Name = "Count";
            // 
            // Unit
            // 
            Unit.HeaderText = "Единица измерения";
            Unit.Name = "Unit";
            Unit.Resizable = DataGridViewTriState.True;
            Unit.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Image
            // 
            Image.BackgroundImage = Properties.Resources.no_photo__lg;
            Image.BorderStyle = BorderStyle.FixedSingle;
            Image.Image = Properties.Resources.no_photo__lg;
            Image.InitialImage = (Image)resources.GetObject("Image.InitialImage");
            Image.Location = new Point(244, 14);
            Image.Name = "Image";
            Image.Size = new Size(209, 195);
            Image.SizeMode = PictureBoxSizeMode.Zoom;
            Image.TabIndex = 1;
            Image.TabStop = false;
            Image.MouseClick += Image_MouseClick;
            // 
            // RecipeName
            // 
            RecipeName.Location = new Point(17, 14);
            RecipeName.Name = "RecipeName";
            RecipeName.Size = new Size(209, 23);
            RecipeName.TabIndex = 2;
            // 
            // Price
            // 
            Price.Location = new Point(244, 215);
            Price.Name = "Price";
            Price.ReadOnly = true;
            Price.Size = new Size(100, 23);
            Price.TabIndex = 3;
            // 
            // Description
            // 
            Description.Location = new Point(17, 43);
            Description.Multiline = true;
            Description.Name = "Description";
            Description.Size = new Size(211, 195);
            Description.TabIndex = 4;
            // 
            // RecipeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Description);
            Controls.Add(Price);
            Controls.Add(RecipeName);
            Controls.Add(Image);
            Controls.Add(IngredientsTable);
            Name = "RecipeForm";
            Size = new Size(819, 348);
            ((System.ComponentModel.ISupportInitialize)IngredientsTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)Image).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView IngredientsTable;
        private PictureBox Image;
        private TextBox RecipeName;
        private TextBox Price;
        private SaveFileDialog saveFileDialog;
        private TextBox Description;
        private DataGridViewComboBoxColumn Ingredient;
        private DataGridViewTextBoxColumn Count;
        private DataGridViewComboBoxColumn Unit;
    }
}
