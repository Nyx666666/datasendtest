using System.Net;
using System.Net.Sockets;
using System.Net.PeerToPeer;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System;




/*var hostName = Dns.GetHostName();
IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);
// This is the IP address of the local machine
IPAddress localIpAddress = localhost.AddressList[0];

Console.WriteLine(localIpAddress.ToString());


IPEndPoint ipEndPoint = new(localIpAddress, 11_000);


using Socket client = new(
    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);
try
{
    await client.ConnectAsync(ipEndPoint);
    Console.WriteLine("connected");

}
catch(Exception e)
{
    Console.WriteLine(e.ToString());
}

*/

var hostName = Dns.GetHostName();
IPAddress Ip_Address = Dns.GetHostAddresses(hostName)[0];
IPEndPoint ipEndPoint = new(Ip_Address, 52000);



IPHostEntry hostInfo = Dns.GetHostByName(hostName);
Console.WriteLine("Host name : " + hostInfo.HostName);
Console.WriteLine("IP address List : ");
for (int index = 0; index < hostInfo.AddressList.Length; index++)
{
    Console.WriteLine(hostInfo.AddressList[index]);
}

new Thread(() =>
{
    Thread.CurrentThread.IsBackground = true;
    host Host = new host(ipEndPoint);
}).Start();


Console.ReadLine();
client client = new client(ipEndPoint);
//client client = new client(hostName);

while (true) { }
class host
{

    public host(IPEndPoint ipEndPoint)
    {

        //IPAddress Ip_Address = Dns.GetHostAddresses(hostName)[0];
        //IPEndPoint ipEndPoint = new(Ip_Address, 52000);
        //Console.WriteLine(ipEndPoint.ToString + "aaaaaaaaaaaaaaaaaaaaaaaa");

        //using Socket recsoc = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        Socket recsoc = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        recsoc.Bind(ipEndPoint);
        recsoc.Listen(52000);
        

        var outlisten = recsoc.Accept();
        int i = 0;
        //while (true)
        //{
            
        //    if (recsoc.IsBound &  outlisten.Connected) { break; }
        //    else { i ++; Debug.WriteLine(i); }
        //}
        while (true)
        {
            
            var buffer = new byte[1_024];
            
            var received =  outlisten.ReceiveAsync(buffer,SocketFlags.None);
            
            var response = Encoding.UTF8.GetString(buffer, 0, received.Result);


            Console.WriteLine("response: " + response);

            outlisten.Send(Encoding.UTF8.GetBytes("<ACK>"), SocketFlags.None);
        }
        
        //TcpListener
    }
}

class client
{
    
    public client(IPEndPoint ipEndPoint)
    {
        try
        {
            // Get 'IPHostEntry' object containing information like host name, IP addresses, aliases for a host.
            //IPHostEntry hostInfo = Dns.GetHostByName(hostName);
            //Console.WriteLine("Host name : " + hostInfo.HostName);
            //Console.WriteLine("IP address List : ");
            //for (int index = 0; index < hostInfo.AddressList.Length; index++)
            //{
            //    Console.WriteLine(hostInfo.AddressList[index]);
            //}

            //IPAddress Ip_Address = Dns.GetHostAddresses(hostName)[0];
            //IPEndPoint ipEndPoint = new(Ip_Address, 52000);

            //HttpClient client = new HttpClient();
            Socket targetclient = new(
                    ipEndPoint.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);
            Console.WriteLine(targetclient.AddressFamily + targetclient.Available);
            Console.ReadLine();
            targetclient.Connect(ipEndPoint);
            while (targetclient.Connected == false) { }
            var messageBytes = Encoding.UTF8.GetBytes("test hallo");
            targetclient.Send(messageBytes,SocketFlags.None);
            while (targetclient.Connected == true) 
            {
                var buffer = new byte[1_024];

                Console.WriteLine("enter msg: ");
                messageBytes = Encoding.UTF8.GetBytes(Console.ReadLine()) ;
                targetclient.Send(messageBytes, SocketFlags.None);

                for (int i = 0; i == -100000000; i++)
                {
                    var received = targetclient.ReceiveAsync(buffer, SocketFlags.None);
                    //
                }
            }
            
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException caught!!!");
            Console.WriteLine("Source : " + e.Source);
            Console.WriteLine("Message : " + e.Message);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("ArgumentNullException caught!!!");
            Console.WriteLine("Source : " + e.Source);
            Console.WriteLine("Message : " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception caught!!!");
            Console.WriteLine("Source : " + e.Source);
            Console.WriteLine("Message : " + e.Message);
        }
    }
}



