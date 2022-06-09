using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivoBinario
{
    class ArchivoBinarioEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null; //flujo salida - escritura de datos
        BinaryReader br = null; //flujo entrada - lectura de datos
                                //campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;
        public void CrearArchivo(string Archivo)
        {
            //variable local método
            char resp;
            try
            {
                //creación del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo,

                FileMode.Create, FileAccess.Write));

                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: "); 
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: "); 
                    Nombre = Console.ReadLine();
                    Console.Write("Dirección del Empleado: "); 
                    Direccion = Console.ReadLine();
                    Console.Write("Teléfono del Empleado: "); 
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro Registro (s/n)?");
                    

                    resp = Char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }
            catch (IOException e)
            {
                Console.WriteLine("\nError : " + e.Message);
                Console.WriteLine("\nRuta : " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); //cierra el flujo - escritura
                Console.Write("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(Archivo))
                {
                    //creación flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo,

                    FileMode.Open, FileAccess.Read));

                    //despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();
                        //muestra los datos
                        Console.WriteLine("Numero del Empleado: " + NumEmp);
                        Console.WriteLine("Nombre del Empleado: " + Nombre);
                        Console.WriteLine("Dirección del Empleado: " + Direccion);
                        Console.WriteLine("Teléfono del Empleado: " + Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado: " + DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado:{ 0:C}",SalarioDiario);

                       Console.WriteLine("SUELDO TOTAL del Empleado : {0:C}", (DiasTrabajados*SalarioDiario));
                       Console.WriteLine("\n");

                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Archivo " + Archivo + " ¡No existe en el disco!");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione <enter> para Continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //cierra flujo
                Console.Write("\nPresione <enter> para terminar la Lectura de Datos y regresar al Menú.");
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            //declaración variables auxiliares
            string Arch = null;
            int opción;
            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();
            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opción = Int16.Parse(Console.ReadLine());
                switch (opción)
                {
                    case 1:
                        //bloque de escritura

                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el nombre del archivo a crear: "); Arch = Console.ReadLine();

                            //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\n¡El archivo existe!, ¿deseas sobreescribirlo?(s / n) ");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el nombre del archivo que deseas leer: "); Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\n¡Esa opción NO existe!, presione < enter > para continuar...");

                        
                        Console.ReadKey();
                        break;
                }
            } while (opción != 3);
        }
    }
}
