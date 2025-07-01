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
        statusStrip1 = new StatusStrip();
        toolStripStatusLabel1 = new ToolStripStatusLabel();
        statusStrip1.SuspendLayout();
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
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
        statusStrip1.Location = new Point(0, 473);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new Size(890, 22);
        statusStrip1.TabIndex = 4;
        statusStrip1.Text = "statusStrip1";
        // 
        // toolStripStatusLabel1
        // 
        toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        toolStripStatusLabel1.Size = new Size(118, 17);
        toolStripStatusLabel1.Text = "toolStripStatusLabel1";
        // 
        // IngredientsControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(statusStrip1);
        Controls.Add(UpdateButton);
        Controls.Add(AddButton);
        Controls.Add(IngredientsList);
        Name = "IngredientsControl";
        Size = new Size(890, 495);
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ListBox IngredientsList;
    private Button AddButton;
    private Button UpdateButton;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
}
