namespace productList
{
    partial class ProductList
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelPagination = new System.Windows.Forms.FlowLayoutPanel();
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.sortBox = new System.Windows.Forms.ComboBox();
            this.filterBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(12, 73);
            this.flowLayoutPanelProducts.MaximumSize = new System.Drawing.Size(1003, 640);
            this.flowLayoutPanelProducts.MinimumSize = new System.Drawing.Size(1003, 640);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(1003, 640);
            this.flowLayoutPanelProducts.TabIndex = 0;
            // 
            // flowLayoutPanelPagination
            // 
            this.flowLayoutPanelPagination.Location = new System.Drawing.Point(672, 719);
            this.flowLayoutPanelPagination.Name = "flowLayoutPanelPagination";
            this.flowLayoutPanelPagination.Size = new System.Drawing.Size(343, 41);
            this.flowLayoutPanelPagination.TabIndex = 1;
            // 
            // searchTxt
            // 
            this.searchTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.searchTxt.Location = new System.Drawing.Point(12, 19);
            this.searchTxt.MaxLength = 200;
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(466, 30);
            this.searchTxt.TabIndex = 2;
            this.searchTxt.Enter += new System.EventHandler(this.searchTxt_Enter);
            this.searchTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTxt_KeyPress);
            this.searchTxt.Leave += new System.EventHandler(this.searchTxt_Leave);
            // 
            // sortBox
            // 
            this.sortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.sortBox.FormattingEnabled = true;
            this.sortBox.ItemHeight = 18;
            this.sortBox.Items.AddRange(new object[] {
            "По умолчанию",
            "По наименованию (по возрастанию)",
            "По наименованию (по убыванию)",
            "По номеру производственного цеха (по возрастанию)",
            "По номеру производственного цеха (по убыванию)",
            "По стоимости (по возрастанию)",
            "По стоимости (по убыванию)"});
            this.sortBox.Location = new System.Drawing.Point(484, 19);
            this.sortBox.Name = "sortBox";
            this.sortBox.Size = new System.Drawing.Size(251, 26);
            this.sortBox.TabIndex = 3;
            this.sortBox.Text = "Сортировка";
            this.sortBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.sortBox_DrawItem);
            this.sortBox.SelectedIndexChanged += new System.EventHandler(this.sortBox_SelectedIndexChanged);
            // 
            // filterBox
            // 
            this.filterBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.filterBox.FormattingEnabled = true;
            this.filterBox.Location = new System.Drawing.Point(741, 19);
            this.filterBox.Name = "filterBox";
            this.filterBox.Size = new System.Drawing.Size(274, 26);
            this.filterBox.TabIndex = 4;
            this.filterBox.Text = "Фильтрация";
            this.filterBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.filterBox_DrawItem);
            this.filterBox.SelectedIndexChanged += new System.EventHandler(this.filterBox_SelectedIndexChanged_1);
            // 
            // ProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 772);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.sortBox);
            this.Controls.Add(this.searchTxt);
            this.Controls.Add(this.flowLayoutPanelPagination);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.MaximumSize = new System.Drawing.Size(1045, 819);
            this.MinimumSize = new System.Drawing.Size(1045, 819);
            this.Name = "ProductList";
            this.Text = "Список всех товаров";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPagination;
        private System.Windows.Forms.TextBox searchTxt;
        private System.Windows.Forms.ComboBox sortBox;
        private System.Windows.Forms.ComboBox filterBox;
    }
}

