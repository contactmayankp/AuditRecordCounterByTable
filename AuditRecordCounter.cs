using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logging;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;
using Microsoft.Xrm.Sdk.Organization;
using System.Web.Services.Description;
using Sdmsols.XTB.AuditRecordCounterByTable.Helpers;

namespace Sdmsols.XTB.AuditRecordCounterByTable
{
    public partial class AuditRecordCounterByTable : PluginControlBase,IGitHubPlugin, IPayPalPlugin, IMessageBusHost, IHelpPlugin, IStatusBarMessenger, IAboutPlugin
    {
        #region Constructor and Class Variables

        private Settings _mySettings;
        private List<EntityMetadataProxy> _entities;
        
        private EntityMetadataProxy _selectedEntity;

        private AttributeProxy _selectedAttributeMetadata;

        private int _stateCode = -1;

        public event EventHandler<MessageBusEventArgs> OnOutgoingMessage;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;


        private enum ControlSelected
        {
            Solutions=1,
            Entities=2,
            Attributes=3,
            StateCodes=4
        }

        public AuditRecordCounterByTable()
        {
            InitializeComponent();
        }

        #endregion Constructor and Class Variables

        #region XrmToolBox Plug In Methods

        private void AuditRecordCounterByTable_Load(object sender, EventArgs e)
        {
           // ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out _mySettings))
            {
                _mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (_mySettings != null && detail != null)
            {
                _mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void AuditRecordCounterByTable_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            var orgver = new Version(e.ConnectionDetail.OrganizationVersion);
            var orgok = orgver >= new Version(9, 0);

            if (orgok)
            {
                //GetAuditRecordCountByTable();
            }
            else
            {
                LogError("CRM version too old for Auto Number Manager");

                MessageBox.Show($"Auto Number feature was introduced in\nMicrosoft Dynamics 365 July 2017 (9.0)\nCurrent version is {orgver}\n\nPlease connect to a newer organization to use this cool tool.",
                    "Organization too old", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuditRecordCounterByTable_OnCloseTool(object sender, System.EventArgs e)
        {

            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), _mySettings);
        }



        #endregion XrmToolBox Plug In Methods
        
        #region Control Events

        private void btnFixAutoNumbers_Click(object sender, EventArgs e)
        {
            try
            {

                ExecuteMethod(GetAuditRecordCountByTable);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"an Error has occurred while processing.." + exception.Message);
            }
           
        }

        #endregion Control Events
        
        #region Private Helper Methods


        private void LoadEntities()
        {
            _entities = new List<EntityMetadataProxy>();
            WorkAsync(new WorkAsyncInfo("Loading entities...",
                (eventargs) => { eventargs.Result = MetadataHelper.LoadEntities(Service); })
            {
                PostWorkCallBack = (completedargs) =>
                {
                    if (completedargs.Error != null)
                    {
                        MessageBox.Show(completedargs.Error.Message);
                    }
                    else
                    {
                        if (completedargs.Result is RetrieveMetadataChangesResponse)
                        {
                            var metaresponse = ((RetrieveMetadataChangesResponse)completedargs.Result).EntityMetadata;
                            _entities.AddRange(metaresponse
                                .Where(e => e.IsCustomizable.Value == true && e.IsIntersect.Value != true)
                                .Select(m => new EntityMetadataProxy(m))
                                .OrderBy(e => e.ToString()));
                        }
                    }

                }
            });
        }

      
  
        private void LoadGridView(List<EntityInfo> results)
        {
            
            auditGridView.DataSource = results;

        }
        private List<EntityInfo> GetAuditRecordCounts()
        {

            var entityList = MetadataHelper.LoadEntities(this.Service);

            if (entityList != null)
            {
                //MessageBox.Show($"Found {result.Entities.Count} contacts");
                LogTextBoxAndProgressBar.UpdateStatusMessage(StatusText, $"Found  total tables {entityList.EntityMetadata.Count}...");
            }

            if (entityList != null)
                LogTextBoxAndProgressBar.SetProgressBar(progressBar, entityList.EntityMetadata.Count);


            var results = new List<EntityInfo>();
            foreach (var entityMetadata in entityList.EntityMetadata)
            {
                var isAuditEnabled = entityMetadata.IsAuditEnabled.Value;
                var objectTypeCode = entityMetadata.ObjectTypeCode;

                var auditFetchXML = @"<fetch aggregate='true'>"+
                           "  <entity name='audit'>"+
                           "    <attribute name='auditid' alias='count' aggregate='count' /> "+
                           "    <filter>"+
                           "      <condition attribute='objecttypecode' operator='eq' value='" + objectTypeCode + " ' /> "+
                           "    </filter>"+
                           "  </entity>"+
                           "</fetch>";

                var auditEntity = new FetchXmlHelper().FetchAll(this.Service, auditFetchXML);
                int count = (int)(auditEntity[0]["count"] as AliasedValue).Value;

                results.Add(new EntityInfo() { AuditEnabled = isAuditEnabled, EntityName=entityMetadata.LogicalName, ObjectTypeCode= entityMetadata.ObjectTypeCode.Value, Count=count });

                LogTextBoxAndProgressBar.AddProgressStep(progressBar);
            }

            return results;
        }


        private void GetAuditRecordCountByTable()
        {
           
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Audit Record Count by Each Table ..",
                Work = (worker, args) =>
                {

                    var result = GetAuditRecordCounts();
                    args.Result = result;

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as List<EntityInfo>;
                   


                    LoadGridView(result);
                }
            });
        }

        #endregion Private Helper Methods
        
        #region Logging And ProgressBar Methods
        delegate void SetStatusTextCallback(string text);

        delegate void AddProgressStepCallback();

        private void UpdateStatusMessage(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.StatusText.InvokeRequired)
            {
                SetStatusTextCallback d = new SetStatusTextCallback(UpdateStatusMessage);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                //this.StatusText.Text = text;

                StatusText.Text = StatusText.Text + text + Environment.NewLine;

                StatusText.Focus();
                StatusText.ScrollToCaret();
                ErrorLog.ReportRoutine(false, text, EventLogEntryType.Information);

                Application.DoEvents();


            }
        }
        
        private void AddProgressStep()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.progressBar.InvokeRequired)
            {
                AddProgressStepCallback d = new AddProgressStepCallback(AddProgressStep);
                this.Invoke(d);
            }
            else
            {
                progressBar.PerformStep();
                Application.DoEvents();
            }
        }

        #endregion Logging And ProgressBar Methods
        
        #region Auto Number Format Methods

        private int GuessSeed()
        {
            var result = 0;
            var lastvalue = string.Empty;
            //var entity = selectedEntity;
            var entity = _entities.Find(a => a.Metadata.LogicalName.Equals(_selectedEntity.Metadata.LogicalName));
            var selectedAttribute = _selectedAttributeMetadata.LogicalName;
            var attributename = selectedAttribute;

            var autoNumberResult = (GetNextAutoNumberValueResponse)Service.Execute(new GetNextAutoNumberValueRequest()
            {
                EntityName = entity.Metadata.LogicalName,
                AttributeName = attributename
            });


            if (autoNumberResult != null)
            {
                result = (int)autoNumberResult.NextAutoNumberValue;
                lastvalue = autoNumberResult.NextAutoNumberValue.ToString();

            }
            else
            {
                //if Dynamics API does not return latest value then find latest value using manual approach now!
                var selectedFormat = "";

                selectedFormat = _selectedAttributeMetadata.attributeMetadata.AutoNumberFormat;

                var format = selectedFormat;
                var sample = ParseNumberFormat(format, "9999999999");

                if (!format.Contains("{SEQNUM:") || !format.Contains("}"))
                {
                    throw new FormatException("Format string must contain a {SEQNUM:n} placeholder.");
                }
                var seqstart = sample.IndexOf("9999999999");
                var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                var length = 0;
                if (int.TryParse(lenghtstr, out length))
                {
                    if (length < 1)
                    {
                        throw new FormatException("Failed to parse SEQNUM length.");
                    }
                }

                var fetchxml = "<fetch top='1' ><entity name='" + entity.Metadata.LogicalName + "' >" +
                    "<attribute name='" + attributename + "' />" +
                    "<filter><condition attribute='" + attributename + "' operator='not-null' /></filter>" +
                    "<order attribute='" + selectedAttribute + "' descending='true' /></entity></fetch>";
                var lastrecord = Service.RetrieveMultiple(new FetchExpression(fetchxml)).Entities.FirstOrDefault();
                
                if (lastrecord == null)
                {
                    var seedResult = (GetAutoNumberSeedResponse)Service.Execute(new GetAutoNumberSeedRequest()
                    {
                        EntityName = entity.Metadata.LogicalName,
                        AttributeName = attributename
                    });

                    if (seedResult != null)
                    {
                        return (int)seedResult.AutoNumberSeedValue - 1;
                    }
                    else
                    {
                        //throw new Exception("No numbered data found for attribute " + attributename);
                        return 0;
                    }
                }
                lastvalue = lastrecord[attributename].ToString();
                if (lastvalue.Length >= seqstart + length)
                {
                    var lastseqstr = lastvalue.Substring(seqstart, length);
                    if (int.TryParse(lastseqstr, out int lastseq))
                    {
                        //LogUse("GuessSeed succeeded");
                        result = lastseq;
                    }
                }

            }

            if (result == 0)
            {
                //LogUse("GuessSeed failed");
                throw new Exception("That was hard. Couldn't even guess what current SEQNUM is.\n" +
                    "Numbered value for last created record is:  \n" + lastvalue);
            }
            return result;
        }

        private string ParseNumberFormat(string format, string seed)
        {
           // txtHint.Text = string.Empty;
            if (!string.IsNullOrWhiteSpace(format))
            {
                try
                {
                    format = ParseFormatSEQNUM(format, seed);
                    format = ParseFormatRANDSTRING(format);
                    format = ParseFormatDATETIMEUTC(format);
                    //txtHint.Text = "Format successfully parsed.";
                   
                }
                catch (Exception ex)
                {
                    //txtHint.Text = ex.Message;
                    format = string.Empty;
                    //btnCreateUpdate.Enabled = false;
                }
            }
            return format;
        }

        private string ParseFormatDATETIMEUTC(string format)
        {
            while (format.Contains("{DATETIMEUTC:") && format.Contains("}"))
            {
                var formatstr = format.Split(new string[] { "{DATETIMEUTC:" }, StringSplitOptions.None)[1];
                formatstr = formatstr.Split('}')[0];
                var datestr = DateTime.Now.ToString(formatstr);
                format = format.Replace("{DATETIMEUTC:" + formatstr + "}", datestr);
            }
            return format;
        }

        private string ParseFormatRANDSTRING(string format)
        {
            while (format.Contains("{RANDSTRING:") && format.Contains("}"))
            {
                var lenghtstr = format.Split(new string[] { "{RANDSTRING:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                if (int.TryParse(lenghtstr, out int length))
                {
                    if (length < 1 || length > 6)
                    {
                        throw new FormatException("RANDSTRING length must be between 1 and 6");
                    }
                    var randomstring = "X7C7D8EK3MR2L4".Substring(0, length);
                    format = format.Replace("{RANDSTRING:" + lenghtstr + "}", randomstring);
                }
                else
                {
                    throw new FormatException("Invalid RANDSTRING format. Enter as {RANDSTRING:n} where n is length of sequence.");
                }
            }
            return format;
        }

        private string ParseFormatSEQNUM(string format, string seed)
        {
            var validseqnum = false;
            try
            {
                if (!format.Contains("{SEQNUM:") || !format.Contains("}"))
                {

                    return format;

                }
                var lenghtstr = format.Split(new string[] { "{SEQNUM:" }, StringSplitOptions.None)[1];
                lenghtstr = lenghtstr.Split('}')[0];
                if (int.TryParse(lenghtstr, out int length))
                {
                    if (length < 1)
                    {
                        throw new FormatException("SEQNUM length must be 1 or higher.");
                    }
                    var seedno = string.IsNullOrEmpty(seed) ? 1 : Int64.Parse(seed);
                    var sequence = string.Format("{0:" + new string('0', length) + "}", seedno);
                    format = format.Replace("{SEQNUM:" + lenghtstr + "}", sequence);
                    validseqnum = true;
                }
                else
                {
                    throw new FormatException("Invalid SEQNUM format. Enter as {SEQNUM:n} where n is length of sequence.");
                }
                if (format.Contains("{SEQNUM:"))
                {
                    throw new FormatException("Format string must only contain one {SEQNUM:n} placeholder.");
                }
            }
            finally
            {
                //txtSeed.Enabled = validseqnum;
                //btnGuessSeed.Enabled = validseqnum && !txtLogicalName.Enabled;
            }
            return format;
        }

        public void OnIncomingMessage(MessageBusEventArgs message)
        {
            throw new NotImplementedException();
        }

        #endregion Auto Number Format Methods

        #region Interface Members

        public string RepositoryName => "AuditRecordCounterByTable";

        public string UserName => "contactmayankp";
        
        public string DonationDescription => "Auto Number Updater";
        public string EmailAccount => "mayank.pujara@gmail.com";

        public string HelpUrl => "https://mayankp.wordpress.com/2021/12/09/xrmtoolbox-AuditRecordCounterByTable-new-tool/";


        #endregion


        public void ShowAboutDialog()
        {
           // throw new NotImplementedException();
        }

        private void tslAbout_Click(object sender, EventArgs e)
        {
            Process.Start(HelpUrl);
        }

    }
}