using System;
using System.Diagnostics;
using System.Windows.Forms;
using Logging;

namespace Sdmsols.XTB.AuditRecordCounterByTable
{
    internal static class LogTextBoxAndProgressBar
    {
        #region Progress Bar and Status text

        

        internal static void SetProgressBar(ProgressBar progressBar, int progBarCount)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Visible = true;
                    progressBar.Value = 0;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = progBarCount;
                    progressBar.Step = 1;
                }));
            }
            else
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.Minimum = 0;
                progressBar.Maximum = progBarCount;
                progressBar.Step = 1;
            }

            Application.DoEvents();
        }

        internal static void AddProgressStep(ProgressBar progressBar)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.PerformStep();
                }));
            }
            else
            {
                progressBar.PerformStep();
            }
            Application.DoEvents();
        }

        internal static void UpdateStatusMessage(TextBox statusText, string logMessage)
        {
            if (statusText.InvokeRequired)
            {
                statusText.Invoke(new Action(() =>
                {
                    statusText.Text = statusText.Text + logMessage + Environment.NewLine;
                    statusText.Focus();
                    statusText.ScrollToCaret();
                }));
            }
            else
            {
                statusText.Text = statusText.Text + logMessage + Environment.NewLine;
                statusText.Focus();
                statusText.ScrollToCaret();
            }

            
            ErrorLog.ReportRoutine(false, logMessage, EventLogEntryType.Information);

            Application.DoEvents();

        }

        #endregion Progress Bar and Status text
    }
}
