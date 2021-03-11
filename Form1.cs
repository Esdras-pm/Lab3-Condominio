using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Condominio
{
    public partial class Form1 : Form
    {
        List<Propietario> persona = new List<Propietario>();
        List<Casa> propiedad = new List<Casa>();
        public Form1()
        {
            InitializeComponent();
        }
        private void guardar(bool cp)
        {
            if (cp == true)
            {
                FileStream stream = new FileStream("Propietario.txt", FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);


                foreach (var p in persona)
                {
                    writer.WriteLine(p.Dpi);
                    writer.WriteLine(p.Nombre);
                    writer.WriteLine(p.Apellido);
                    writer.WriteLine(p.Cont);
                    writer.WriteLine(p.CuotaT);
                }
                writer.Close();
                string[] dpis = new string[persona.Count];
                dpi_cbx.Items.Clear();
                for (int i = 0; i < persona.Count; i++) dpis[i] = persona[i].Dpi;
                dpi_cbx.Items.AddRange(dpis);
            }
            else
            {
                FileStream stream = new FileStream("Casa.txt", FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(stream);


                foreach (var p in propiedad)
                {
                    writer.WriteLine(p.Nombre);
                    writer.WriteLine(p.Apellido);
                    writer.WriteLine(p.Nocasa);
                    writer.WriteLine(p.Cuota);
                }
                writer.Close();
            }
        }
        private void Leer(bool cp)
        {
            if (cp == true)
            {
                FileStream stream = new FileStream("Propietario.txt", FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() > -1)
                {
                    Propietario datos = new Propietario();
                    datos.Dpi = reader.ReadLine();
                    datos.Nombre = reader.ReadLine();
                    datos.Apellido = reader.ReadLine();
                    datos.Cont = int.Parse(reader.ReadLine());
                    datos.CuotaT = float.Parse(reader.ReadLine());
                    persona.Add(datos);
                }
                //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
                reader.Close();
                string[] dpis = new string[persona.Count];
                dpi_cbx.Items.Clear();
                for (int i = 0; i < persona.Count; i++) dpis[i] = persona[i].Dpi;
                dpi_cbx.Items.AddRange(dpis);
            }
            else
            {
                FileStream stream = new FileStream("Casa.txt", FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                while (reader.Peek() > -1)
                {
                    Casa datos = new Casa();
                    datos.Nombre = reader.ReadLine();
                    datos.Apellido = reader.ReadLine();
                    datos.Nocasa = reader.ReadLine();
                    datos.Cuota = float.Parse(reader.ReadLine());
                    propiedad.Add(datos);
                }
                //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
                reader.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("Propietario.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            FileStream stream2 = new FileStream("Casa.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader2 = new StreamReader(stream2);
            string leer = reader.ReadToEnd();
            string leer2 = reader2.ReadToEnd();
            reader.Close();
            reader2.Close();
            if (!leer.Equals(""))
            {
                Leer(true);
            }
            if (!leer2.Equals(""))
            {
                Leer(false);
                propiedad = propiedad.OrderBy(p => p.Cuota).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = propiedad;
                dataGridView1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Propietario agregarc = new Propietario();
            int c = 0;
            for (int i = 0; i < persona.Count; i++)
                if (persona[i].Dpi == dpi_txt.Text) c++;
            if (c == 0)
            {
                agregarc.Dpi = dpi_txt.Text;
                agregarc.Nombre = nombre_txt.Text;
                agregarc.Apellido = apellido_txt.Text;
                persona.Add(agregarc);
                guardar(true);
            }
            else
            {
                Propietario rep = persona.Find(p => p.Dpi == dpi_txt.Text);
                MessageBox.Show(rep.Nombre + " ya está agregado con el DPI: " + rep.Dpi);
            }
            dpi_txt.Text = "";
            nombre_txt.Text = "";
            apellido_txt.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Casa agregarc = new Casa();
            int c = 0;
            for (int i = 0; i < propiedad.Count; i++)
                if (propiedad[i].Nocasa == casa_txt.Text) c++;
            if (c == 0)
            {
                Propietario nom = persona.Find(p => p.Dpi == dpi_cbx.Text);
                nom.Cont += 1;
                agregarc.Nombre = nom.Nombre;
                agregarc.Apellido = nom.Apellido;
                agregarc.Nocasa = casa_txt.Text;
                agregarc.Cuota = float.Parse(cuota_txt.Text);
                nom.CuotaT += agregarc.Cuota;
                propiedad.Add(agregarc);
                guardar(true);
                guardar(false);
                propiedad = propiedad.OrderBy(p => p.Cuota).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = propiedad;
                dataGridView1.Refresh();
            }
            else
            {
                Casa rep = propiedad.Find(p => p.Nocasa == casa_txt.Text);
                MessageBox.Show("La casa número: " + rep.Nocasa + " ya está agregada.");
            }
            dpi_cbx.Text = "";
            casa_txt.Text = "";
            cuota_txt.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int mayorprom = persona.Max(al => al.Cont);
            List<Propietario> mayores = new List<Propietario>();
            mayores = persona.FindAll(al => al.Cont == mayorprom);
            for(int i=0;i<mayores.Count;i++)
                MessageBox.Show(mayores[i].Nombre + " " + mayores[i].Apellido + " tiene " + mayores[i].Cont + " propiedades.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cont = 0;
            string mayores="", menores="";
            propiedad = propiedad.OrderBy(p => p.Cuota).ToList();
            for(int i = propiedad.Count - 1; i >= (propiedad.Count / 2); i--)
            {
                if (cont >= 3) break;
                mayores += propiedad[i].Cuota.ToString() + " de la casa No." + propiedad[i].Nocasa + '\n';
                cont++; 
            } cont = 0;
            for(int i=0;i<(propiedad.Count/2);i++)
            {
                menores += propiedad[i].Cuota.ToString() + " de la casa No." + propiedad[i].Nocasa + '\n';
                cont++;
                if (cont >= 3) break;
            }
            MessageBox.Show("Las cuotas más altas son:\n" + mayores+ '\n'+"Las menores son:\n"+menores);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Propietario pagarm = persona.OrderByDescending(p => p.CuotaT).First();
            MessageBox.Show(pagarm.Nombre +" "+pagarm.Apellido+ " debe pagar en total: Q" + pagarm.CuotaT);
        }
    }
}
