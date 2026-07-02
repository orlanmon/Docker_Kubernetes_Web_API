namespace OAuth2Client
{
    partial class MainAppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_Invoke = new Button();
            SuspendLayout();
            // 
            // button_Invoke
            // 
            button_Invoke.Location = new Point(357, 205);
            button_Invoke.Name = "button_Invoke";
            button_Invoke.Size = new Size(75, 23);
            button_Invoke.TabIndex = 0;
            button_Invoke.TabStop = false;
            button_Invoke.Text = "Invoke";
            button_Invoke.UseVisualStyleBackColor = true;
            button_Invoke.Click += button_Invoke_Click;
            // 
            // MainAppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button_Invoke);
            Name = "MainAppForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button_Invoke;
    }
}
