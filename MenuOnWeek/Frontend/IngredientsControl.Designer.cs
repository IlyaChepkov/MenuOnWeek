namespace Frontend
{
    partial class IngredientsControl
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
            IngredientsList = new ListBox();
            IngredientName = new TextBox();
            AddButton = new Button();
            UpdateButton = new Button();
            UnitsList = new ComboBox();
            PriceNumericUpDown = new NumericUpDown();
            UnitsTable = new DataGridView();
            Unit = new DataGridViewComboBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)PriceNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)UnitsTable).BeginInit();
            SuspendLayout();
            // 
            // IngredientsList
            // 
            IngredientsList.FormattingEnabled = true;
            IngredientsList.ItemHeight = 15;
            IngredientsList.Location = new Point(3, 3);
            IngredientsList.Name = "IngredientsList";
            IngredientsList.Size = new Size(120, 109);
            IngredientsList.TabIndex = 0;
            IngredientsList.SelectedIndexChanged += IngredientsList_SelectedIndexChanged;
            IngredientsList.KeyDown += IngredientsList_KeyDown;
            // 
            // IngredientName
            // 
            IngredientName.Location = new Point(129, 3);
            IngredientName.Name = "IngredientName";
            IngredientName.Size = new Size(100, 23);
            IngredientName.TabIndex = 1;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(235, 3);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(33, 23);
            AddButton.TabIndex = 2;
            AddButton.Text = "+";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(274, 3);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(33, 23);
            UpdateButton.TabIndex = 3;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // UnitsList
            // 
            UnitsList.FormattingEnabled = true;
            UnitsList.Location = new Point(129, 32);
            UnitsList.Name = "UnitsList";
            UnitsList.Size = new Size(215, 23);
            UnitsList.TabIndex = 4;
            UnitsList.SelectedIndexChanged += UnitsList_SelectedIndexChanged;
            UnitsList.TextUpdate += UnitsList_TextUpdate;
            UnitsList.KeyDown += UnitsList_KeyDown;
            // 
            // PriceNumericUpDown
            // 
            PriceNumericUpDown.Location = new Point(129, 61);
            PriceNumericUpDown.Maximum = new decimal(new int[] { 276447232, 23283, 0, 0 });
            PriceNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PriceNumericUpDown.Name = "PriceNumericUpDown";
            PriceNumericUpDown.Size = new Size(120, 23);
            PriceNumericUpDown.TabIndex = 5;
            PriceNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // UnitsTable
            // 
            UnitsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UnitsTable.Columns.AddRange(new DataGridViewColumn[] { Unit, Value });
            UnitsTable.Location = new Point(360, 3);
            UnitsTable.Name = "UnitsTable";
            UnitsTable.Size = new Size(240, 209);
            UnitsTable.TabIndex = 6;
            UnitsTable.RowsAdded += UnitsTable_RowsAdded;
            // 
            // Unit
            // 
            Unit.HeaderText = "Единица измерения";
            Unit.Name = "Unit";
            Unit.Resizable = DataGridViewTriState.True;
            Unit.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // Value
            // 
            Value.HeaderText = "Коэффицент";
            Value.Name = "Value";
            // 
            // IngredientsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(UnitsTable);
            Controls.Add(PriceNumericUpDown);
            Controls.Add(UnitsList);
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(IngredientName);
            Controls.Add(IngredientsList);
            Name = "IngredientsControl";
            Size = new Size(890, 495);
            ((System.ComponentModel.ISupportInitialize)PriceNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)UnitsTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox IngredientsList;
        private TextBox IngredientName;
        private Button AddButton;
        private Button UpdateButton;
        private ComboBox UnitsList;
        private NumericUpDown PriceNumericUpDown;
        private DataGridView UnitsTable;
        private DataGridViewComboBoxColumn Unit;
        private DataGridViewTextBoxColumn Value;
    }
}
