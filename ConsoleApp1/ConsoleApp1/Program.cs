using System;
using System.IO.Ports;
using System.Threading;


class Program {
static SerialPort serialPort;
    static void Main(string[] args)
    {

        Console.WriteLine("Available COM Port : ");
        foreach (string s in SerialPort.GetPortNames()) {
        Console.WriteLine(" " + s);
        }

        Console.Write("Enter COM port value : ");
        string portName = Console.ReadLine();

        serialPort = new SerialPort(portName)
        {
            BaudRate = 115200,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = Parity.None
        };

        serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        try {
            serialPort.Open();
            Console.WriteLine($"Serial port {portName} opend");
        } catch (Exception e) {
            Console.WriteLine($"Failed to open serial port {portName} : ");
            return;
        }

        Console.WriteLine("Enter a message to send (type 'exit' to quit):");

        while (true)
        {
            string message = Console.ReadLine();

            /*string message = await Console.ReadLineAsync();*/

            if (message.ToLower() == "exit")
            {
                break;
            }
            try
            {
                Console.WriteLine($"Sending message: {message}");
                serialPort.Write(message + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        /*Console.WriteLine("Press any key to close the application");
        Console.ReadKey();*/

        if (serialPort.IsOpen) {
            serialPort.Close();
            Console.WriteLine("Serial port closed");
        }

    }
    private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string data = serialPort.ReadExisting();
            Console.WriteLine($"Data received: {data}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation while reading: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while reading data: {ex.Message}");
        }
    }
}
