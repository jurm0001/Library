using System.Windows.Forms;

using System;
using System.Collections.Generic;

using System.Text;

using System.IO;
using System.Data;

using System.Threading;


namespace Utilities.UI
{
    partial class AutomationCountsGroupbox
    {

        public void SetTotalRecords(int value)
        {
            this.textBox_Total.Text = value.ToString();
        }
        public void SetTotalRecords(string value)
        {
            this.textBox_Total.Text = value.Trim();
        }

        public void reset()
        {
            textBox_failed.Text = "0";
            textBox_fatal.Text = "0";
            textBox_processed.Text = "0";
            textBox_reviewed.Text = "0";
            textBox_skipped.Text = "0";
            textBox_Total.Text = "0";
        }
        delegate void Mydelegate(int add);
        public void UpdateReviewed(int add)
        {
            try
            {
                lock (this)
                {
                    if (textBox_reviewed.InvokeRequired)
                    {
                        textBox_reviewed.BeginInvoke(new Mydelegate(UpdateReviewed), add);
                    }
                    else
                    {
                        textBox_reviewed.Text = (int.Parse(textBox_reviewed.Text) + add).ToString();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }

        public void UpdateProcessed(int add)
        {
            try
            {
                lock (this)
                {

                    textBox_processed.Text = (int.Parse(textBox_processed.Text) + add).ToString();
                    System.Windows.Forms.Application.DoEvents();

                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }

        public void UpdateSkipped(int add)
        {
            try
            {
                lock (this)
                {
                    if (textBox_skipped.InvokeRequired)
                    {

                        textBox_skipped.BeginInvoke(new Mydelegate(UpdateSkipped), add);
                    }
                    else
                    {
                        textBox_skipped.Text = (int.Parse(textBox_skipped.Text) + add).ToString();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }
        public void UpdateFailed(int add)
        {
            try
            {
                lock (this)
                {

                    if (textBox_failed.InvokeRequired)
                    {

                        textBox_failed.BeginInvoke(new Mydelegate(UpdateFailed), add);
                    }
                    else
                    {
                        textBox_failed.Text = (int.Parse(textBox_failed.Text) + add).ToString();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }
        public void UpdateFatal(int add)
        {
            try
            {
                lock (this)
                {
                    if (textBox_fatal.InvokeRequired)
                    {

                        textBox_fatal.BeginInvoke(new Mydelegate(UpdateFatal), add);
                    }
                    else
                    {
                        textBox_fatal.Text = (int.Parse(textBox_fatal.Text) + add).ToString();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString());}
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_reviewed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_failed = new System.Windows.Forms.TextBox();
            this.textBox_skipped = new System.Windows.Forms.TextBox();
            this.textBox_processed = new System.Windows.Forms.TextBox();
            this.textBox_Total = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_fatal = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_fatal);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_reviewed);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_failed);
            this.groupBox1.Controls.Add(this.textBox_skipped);
            this.groupBox1.Controls.Add(this.textBox_processed);
            this.groupBox1.Controls.Add(this.textBox_Total);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Automation Counts";
            // 
            // textBox_reviewed
            // 
            this.textBox_reviewed.Location = new System.Drawing.Point(73, 47);
            this.textBox_reviewed.Name = "textBox_reviewed";
            this.textBox_reviewed.Size = new System.Drawing.Size(95, 20);
            this.textBox_reviewed.TabIndex = 9;
            this.textBox_reviewed.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Reviewed";
            // 
            // textBox_failed
            // 
            this.textBox_failed.Location = new System.Drawing.Point(73, 125);
            this.textBox_failed.Name = "textBox_failed";
            this.textBox_failed.Size = new System.Drawing.Size(95, 20);
            this.textBox_failed.TabIndex = 7;
            this.textBox_failed.Text = "0";
            this.textBox_failed.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox_skipped
            // 
            this.textBox_skipped.Location = new System.Drawing.Point(73, 98);
            this.textBox_skipped.Name = "textBox_skipped";
            this.textBox_skipped.Size = new System.Drawing.Size(95, 20);
            this.textBox_skipped.TabIndex = 6;
            this.textBox_skipped.Text = "0";
            // 
            // textBox_processed
            // 
            this.textBox_processed.Location = new System.Drawing.Point(73, 71);
            this.textBox_processed.Name = "textBox_processed";
            this.textBox_processed.Size = new System.Drawing.Size(95, 20);
            this.textBox_processed.TabIndex = 5;
            this.textBox_processed.Text = "0";
            // 
            // textBox_Total
            // 
            this.textBox_Total.Location = new System.Drawing.Point(74, 20);
            this.textBox_Total.Name = "textBox_Total";
            this.textBox_Total.Size = new System.Drawing.Size(95, 20);
            this.textBox_Total.TabIndex = 4;
            this.textBox_Total.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Failed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Skipped:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Processed:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Fatal:";
            // 
            // textBox_fatal
            // 
            this.textBox_fatal.Location = new System.Drawing.Point(73, 152);
            this.textBox_fatal.Name = "textBox_fatal";
            this.textBox_fatal.Size = new System.Drawing.Size(95, 20);
            this.textBox_fatal.TabIndex = 11;
            this.textBox_fatal.Text = "0";
            // 
            // AutomationCountsGroupbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "AutomationCountsGroupbox";
            this.Size = new System.Drawing.Size(181, 200);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox_failed;
        public System.Windows.Forms.TextBox textBox_skipped;
        public System.Windows.Forms.TextBox textBox_processed;
        public System.Windows.Forms.TextBox textBox_Total;
        public System.Windows.Forms.TextBox textBox_reviewed;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBox_fatal;
        private System.Windows.Forms.Label label6;
    }
}
