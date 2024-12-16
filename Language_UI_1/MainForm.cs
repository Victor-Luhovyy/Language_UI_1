using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CultureSwitcherApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Initialize ComboBox for languages
            comboBoxLanguages.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxLanguages.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxLanguages.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Populate supported languages
            var supportedCultures = new Dictionary<string, string>
            {
                { "en-US", "English (United States)" },
                { "ar-SA", "Arabic (Saudi Arabia)" },
                { "fr-FR", "French (France)" }
            };

            comboBoxLanguages.DataSource = new BindingSource(supportedCultures, null);
            comboBoxLanguages.DisplayMember = "Value";
            comboBoxLanguages.ValueMember = "Key";

            comboBoxLanguages.SelectedIndexChanged += ComboBoxLanguages_SelectedIndexChanged;
        }

        private void ComboBoxLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLanguages.SelectedValue is string selectedCulture)
            {
                // Change the culture at runtime
                CultureHelper.ChangeCulture(selectedCulture);

                // Update UI texts
                labelWelcome.Text = Language_UI_1.Resource1.WelcomeText_;
                labelSelectLanguage.Text = Language_UI_1.Resource1.SelectLanguageText;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            labelWelcome.Text = Language_UI_1.Resource1.WelcomeText_;
            labelSelectLanguage.Text = Language_UI_1.Resource1.SelectLanguageText;
        }
    }
}
