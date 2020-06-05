using System;
using System.Windows.Forms;
using Domain;
using UI;

static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // maak een venster aan
        Form form = new Form();

        // verbied resize van het venster
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        form.MaximizeBox = false;
        form.MinimizeBox = false;

        // stel de initiele grootte van dit venster in
        form.ClientSize = new System.Drawing.Size(900,900);

        // stel de titel van het venster in
        form.Text = "Boomshine";

        // maak een view
        BoomshineView view = new BoomshineView();

        // maak een control (= ui onderdeel) die de zal tonen
        Control control = new BoomshineControl(view);

        // voeg de view toe aan de control
        form.Controls.Add(control);

        // zorg ervoor dat drawing control ganse venster inneemt
        control.Dock = DockStyle.Fill;

        // Start de grafische user interface en toon de form
        Application.Run(form);
    }
}