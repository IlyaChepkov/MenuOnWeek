namespace MenuOnWeek.Frontend;

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
        AddButton = new Button();
        UpdateButton = new Button();
        SuspendLayout();
        // 
        // IngredientsList
        // 
        IngredientsList.FormattingEnabled = true;
        IngredientsList.ItemHeight = 15;
        IngredientsList.Location = new Point(3, 34);
        IngredientsList.Name = "IngredientsList";
        IngredientsList.Size = new Size(120, 109);
        IngredientsList.TabIndex = 0;
        IngredientsList.SelectedIndexChanged += IngredientsList_SelectedIndexChanged;
        IngredientsList.KeyDown += IngredientsList_KeyDown;
        // 
        // AddButton
        // 
        AddButton.Location = new Point(3, 5);
        AddButton.Name = "AddButton";
        AddButton.Size = new Size(33, 23);
        AddButton.TabIndex = 2;
        AddButton.Text = "+";
        AddButton.UseVisualStyleBackColor = true;
        AddButton.Click += AddButton_Click;
        // 
        // UpdateButton
        // 
        UpdateButton.Location = new Point(42, 5);
        UpdateButton.Name = "UpdateButton";
        UpdateButton.Size = new Size(33, 23);
        UpdateButton.TabIndex = 3;
        UpdateButton.Text = "Update";
        UpdateButton.UseVisualStyleBackColor = true;
        UpdateButton.Click += UpdateButton_Click;
        // 
        // IngredientsControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(UpdateButton);
        Controls.Add(AddButton);
        Controls.Add(IngredientsList);
        Name = "IngredientsControl";
        Size = new Size(890, 495);
        ResumeLayout(false);
    }

    #endregion

    private ListBox IngredientsList;
    private Button AddButton;
    private Button UpdateButton;
}
