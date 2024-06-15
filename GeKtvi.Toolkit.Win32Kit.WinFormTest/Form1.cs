using GeKtvi.Toolkit.Win32Kit.WinFormTest;
using System.Windows.Forms;

namespace GeKtvi.Toolkit.Win32Kit;

public partial class Form1 : Form
{
    private SubForm _subSubForm;
    public Form1()
    {
        InitializeComponent();

        Shown += (s, e) => ShowSubForm("ISubForm");

        Shown += (s, e) => InsertFormInAnother(this.Text, _subSubForm);
    }

    private void ShowSubForm(string name)
    {
        var subForm = new SubForm();
        subForm.Text = name;
        subForm.Name = name;
        subForm.Show(this); 

        _subSubForm = subForm;
    }

    private void InsertFormInAnother(string name, SubForm subSubForm)
    {
        subSubForm.ShowInTaskbar = false;
        subSubForm.ControlBox = false;
        subSubForm.AutoSize = false;
        subSubForm.FormBorderStyle = FormBorderStyle.None;
        subSubForm.BackColor = Color.AntiqueWhite;

        IntPtr dialog = PointerToWindowSeeker.FindWindowPointer(name);
        var rectanglListener = new WindowRectangleListener(dialog);

        new WindowAttacher(dialog, subSubForm.Handle).AttachToWindow();

        CalculateFormTransform(rectanglListener.GetRectangle(), subSubForm);
        rectanglListener.RectangleChanged += (s, e) =>
        {
            CalculateFormTransform(e, subSubForm);
        };
    }

    private static void CalculateFormTransform(Rectangle e, Form form)
    {
        int left = (int)e.Left + 16;
        int top = (int)e.Top + 59 + 20;
        int height = (int)e.Height - (59 + 55);
        int width = (int)e.Width - 16 * 2;

        form.Height = height;
        form.Width = width;
        form.Location = new System.Drawing.Point(left, top);

        form.Visible = height >= 39 && width >= 136;
    }
}
