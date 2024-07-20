namespace ModernCSharp.WinFormsApp.Views
{
    partial class OrderListView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OrderDataGridView = new DataGridView();
            ImportOrdersButton = new Button();
            ((System.ComponentModel.ISupportInitialize)OrderDataGridView).BeginInit();
            SuspendLayout();
            // 
            // OrderDataGridView
            // 
            OrderDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            OrderDataGridView.Location = new Point(12, 56);
            OrderDataGridView.Name = "OrderDataGridView";
            OrderDataGridView.Size = new Size(757, 367);
            OrderDataGridView.TabIndex = 0;
            // 
            // ImportOrdersButton
            // 
            ImportOrdersButton.Location = new Point(12, 12);
            ImportOrdersButton.Name = "ImportOrdersButton";
            ImportOrdersButton.Size = new Size(75, 23);
            ImportOrdersButton.TabIndex = 1;
            ImportOrdersButton.Text = "Import";
            ImportOrdersButton.UseVisualStyleBackColor = true;
            ImportOrdersButton.Click += ImportOrdersButton_Click;
            // 
            // OrderListView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ImportOrdersButton);
            Controls.Add(OrderDataGridView);
            Name = "OrderListView";
            Text = "OrderListView";
            Load += OrderListView_Load;
            ((System.ComponentModel.ISupportInitialize)OrderDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView OrderDataGridView;
        private Button ImportOrdersButton;
    }
}