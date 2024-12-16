using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CultureSwitcherApp
{
    public static class CultureHelper
    {
        public static void ChangeCulture(string cultureName)
        {
            var culture = new CultureInfo(cultureName);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Apply Right-to-Left settings if necessary
            var isRtl = culture.TextInfo.IsRightToLeft;
            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                form.Invalidate();
                UpdateFormCulture(form, isRtl);
                form.Update();
                form.Validate();
                Console.WriteLine($"Form {form.Name}: RightToLeft={form.RightToLeft}, RightToLeftLayout={form.RightToLeftLayout}");
            }
            Console.WriteLine($"updated. OpenForms are {Application.OpenForms.Count}");
        }

        private static void UpdateFormCulture(Form form, bool isRtl)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(() => UpdateFormCulture(form, isRtl)));
                return;
            }

            form.RightToLeft = isRtl ? RightToLeft.Yes : RightToLeft.No;
            form.RightToLeftLayout = isRtl;

            foreach (Control control in form.Controls)
            {
                UpdateControlCulture(control, isRtl);
            }
        }

        private static void UpdateControlCulture(Control control, bool isRtl)
        {
            control.RightToLeft = isRtl ? RightToLeft.Yes : RightToLeft.No;

            if (control.Controls.Count > 0)
            {
                foreach (Control child in control.Controls)
                {
                    UpdateControlCulture(child, isRtl);
                }
            }
        }
    }
}
