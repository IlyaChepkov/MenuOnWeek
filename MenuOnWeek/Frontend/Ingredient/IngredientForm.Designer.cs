namespace MenuOnWeek.Frontend
{
    partial class IngredientForm
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
            UnitsTable = new DataGridView();
            Unit = new DataGridViewComboBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            PriceNumericUpDown = new NumericUpDown();
            UnitsList = new ComboBox();
            IngredientName = new TextBox();
            NameLabel = new Label();
            UnitLabel = new Label();
            PriceLabel = new Label();
            TableLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)UnitsTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PriceNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // UnitsTable
            // 
            UnitsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            UnitsTable.Columns.AddRange(new DataGridViewColumn[] { Unit, Value });
            UnitsTable.Location = new Point(258, 45);
            UnitsTable.Name = "UnitsTable";
            UnitsTable.Size = new Size(240, 209);
            UnitsTable.TabIndex = 7;
            UnitsTable.CellEndEdit += UnitsTable_CellEndEdit;
            UnitsTable.RowsRemoved += UnitsTable_RowsRemoved;
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
            // PriceNumericUpDown
            // 
            PriceNumericUpDown.Location = new Point(19, 133);
            PriceNumericUpDown.Maximum = new decimal(new int[] { 276447232, 23283, 0, 0 });
            PriceNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            PriceNumericUpDown.Name = "PriceNumericUpDown";
            PriceNumericUpDown.Size = new Size(120, 23);
            PriceNumericUpDown.TabIndex = 10;
            PriceNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // UnitsList
            // 
            UnitsList.FormattingEnabled = true;
            UnitsList.Location = new Point(19, 89);
            UnitsList.Name = "UnitsList";
            UnitsList.Size = new Size(215, 23);
            UnitsList.TabIndex = 9;
            UnitsList.SelectedIndexChanged += UnitsList_SelectedIndexChanged;
            UnitsList.TextUpdate += UnitsList_TextUpdate;
            UnitsList.KeyDown += UnitsList_KeyDown;
            // 
            // IngredientName
            // 
            IngredientName.Location = new Point(19, 45);
            IngredientName.Name = "IngredientName";
            IngredientName.Size = new Size(100, 23);
            IngredientName.TabIndex = 8;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(19, 27);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(59, 15);
            NameLabel.TabIndex = 11;
            NameLabel.Text = "Название";
            // 
            // UnitLabel
            // 
            UnitLabel.AutoSize = true;
            UnitLabel.Location = new Point(19, 71);
            UnitLabel.Name = "UnitLabel";
            UnitLabel.Size = new Size(116, 15);
            UnitLabel.TabIndex = 12;
            UnitLabel.Text = "Единица измерения";
            // 
            // PriceLabel
            // 
            PriceLabel.AutoSize = true;
            PriceLabel.Location = new Point(19, 115);
            PriceLabel.Name = "PriceLabel";
            PriceLabel.Size = new Size(35, 15);
            PriceLabel.TabIndex = 13;
            PriceLabel.Text = "Цена";
            // 
            // TableLabel
            // 
            TableLabel.AutoSize = true;
            TableLabel.Location = new Point(258, 27);
            TableLabel.Name = "TableLabel";
            TableLabel.Size = new Size(159, 15);
            TableLabel.TabIndex = 14;
            TableLabel.Text = "Таблица единиц измерения";
            // 
            // IngredientForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TableLabel);
            Controls.Add(PriceLabel);
            Controls.Add(UnitLabel);
            Controls.Add(NameLabel);
            Controls.Add(PriceNumericUpDown);
            Controls.Add(UnitsList);
            Controls.Add(IngredientName);
            Controls.Add(UnitsTable);
            Name = "IngredientForm";
            Size = new Size(508, 260);
            ((System.ComponentModel.ISupportInitialize)UnitsTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)PriceNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView UnitsTable;
        private DataGridViewComboBoxColumn Unit;
        private DataGridViewTextBoxColumn Value;
        private NumericUpDown PriceNumericUpDown;
        private ComboBox UnitsList;
        private TextBox IngredientName;
        private Label NameLabel;
        private Label UnitLabel;
        private Label PriceLabel;
        private Label TableLabel;
    }
}
