namespace MetadataRemover.WinFormsApp.Forms
{
    partial class MainForm
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
            groupBox1 = new GroupBox();
            buttonRemoveMetadata = new Button();
            textBoxMetadata = new TextBox();
            buttonReadMetadata = new Button();
            button2 = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonRemoveMetadata);
            groupBox1.Controls.Add(textBoxMetadata);
            groupBox1.Controls.Add(buttonReadMetadata);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1426, 1003);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "File Info";
            // 
            // buttonRemoveMetadata
            // 
            buttonRemoveMetadata.Location = new Point(264, 158);
            buttonRemoveMetadata.Name = "buttonRemoveMetadata";
            buttonRemoveMetadata.Size = new Size(226, 46);
            buttonRemoveMetadata.TabIndex = 6;
            buttonRemoveMetadata.Text = "Remove Metadata";
            buttonRemoveMetadata.UseVisualStyleBackColor = true;
            // 
            // textBoxMetadata
            // 
            textBoxMetadata.Location = new Point(6, 245);
            textBoxMetadata.Multiline = true;
            textBoxMetadata.Name = "textBoxMetadata";
            textBoxMetadata.ScrollBars = ScrollBars.Vertical;
            textBoxMetadata.Size = new Size(1402, 752);
            textBoxMetadata.TabIndex = 5;
            // 
            // buttonReadMetadata
            // 
            buttonReadMetadata.Location = new Point(6, 158);
            buttonReadMetadata.Name = "buttonReadMetadata";
            buttonReadMetadata.Size = new Size(226, 46);
            buttonReadMetadata.TabIndex = 4;
            buttonReadMetadata.Text = "Read Metadata";
            buttonReadMetadata.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(1258, 47);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 3;
            button2.Text = "Open";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(1161, 47);
            button1.Name = "button1";
            button1.Size = new Size(91, 46);
            button1.TabIndex = 2;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(68, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1087, 39);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 51);
            label1.Name = "label1";
            label1.Size = new Size(56, 32);
            label1.TabIndex = 0;
            label1.Text = "File:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1457, 1027);
            Controls.Add(groupBox1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainForm";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button2;
        private Button button1;
        private TextBox textBox1;
        private Label label1;
        private Button buttonRemoveMetadata;
        private TextBox textBoxMetadata;
        private Button buttonReadMetadata;
    }
}