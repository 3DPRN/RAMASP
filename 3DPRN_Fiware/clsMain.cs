using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE;
using FIWARE.Orion.Client;
using FIWARE.Orion.Client.Models;
using _3DPRN_Fiware;
using System.Windows.Forms;
using AndreasReitberger;
using SimpleTCP;

namespace _3DPRN_Fiware
{
    internal class clsMain
    {
        public SimpleTcpClient _onBoardClient;
        Fiware_Orion_Client _orionClient;
        string _prn_ip = "192.168.0.116";
        string _prn_port = "13000";
        public string _prn_SerialNumber = "";
        public ContextResponse _prn_Attributes;
        string _orionClientConfig_BaseUrl = "http://192.168.0.33:1026/";
        string _orionClientConfig_Token = "2b4ba603987c4421957693e9bf14d1dbc846f473259a65e1d5bfaa21df6616ec";
        private int _retryperiod = 5000;
        private readonly object _syncTimer = new object();
        string fileName_PAR = Application.StartupPath + @"\3DPRN_Fiware.ini";
        public static Thread ThreadManagePrinters;

        //!!Octopi part
        public string octopiHash = "";
        public bool octoMode = true;
        OctoPrintServer OctoPiServer;

        public void MainTimer()
        {
            do
            {
                //if (STATUS.Program_Ending) break;
                try
                {

                    if (_onBoardClient.TcpClient.Connected)
                    {
                        _retryperiod = 5000;
                        _onBoardClient.Write("~:STATUS_EXPOSE_GET\r\n");  //richiesta stato
                        //_onBoardClient.Write("~:STATUS_EXPOSE_GET\r\n");  //richiesta stato
                        System.Diagnostics.Debug.Print("STATUS_EXPOSE_GET");
                        if (_prn_SerialNumber != "")
                        {
                            _orionClient.GetStatus(_prn_SerialNumber);
                            _prn_Attributes = _orionClient._attributes;
                        }
                    }
                    else
                    {
                        _retryperiod = 60000;
                        _onBoardClient.Connect(_prn_ip,int.Parse(_prn_port));
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                }

                Thread.Sleep(_retryperiod);
            }
            while (true);
        }


        public void MainTimerOctoP()
        {
            do
            {
                //if (STATUS.Program_Ending) break;
                try
                {
                    if (OctoPiServer.IsOnline)
                    {
                        _retryperiod = 5000;

                        try
                        {
                            STATUS_EXPOSE printerStatus = new STATUS_EXPOSE();
                            printerStatus.Printer.Status = OctoPiServer.State.State.ToString();
                            printerStatus.Printer.EstruderCurrentTemp = OctoPiServer.Extruders[0].Actual ?? 0;
                            printerStatus.Printer.EstruderTargetTemp = OctoPiServer.Extruders[0].Target ?? 0;
                            printerStatus.Printer.BedCurrentTemp = OctoPiServer.HeatedBeds[0].Actual ?? 0;
                            printerStatus.Printer.BedTargetTemp = OctoPiServer.HeatedBeds[0].Target ?? 0;

                            var threadOrion = new Thread(() =>
                                        _orionClient.UpdateStatus(printerStatus)
                                   );
                            threadOrion.IsBackground = true;
                            threadOrion.Start();


                            System.Diagnostics.Debug.Print($"DataArrived End Call");

                        }
                        catch (InvalidCastException exc)
                        {
                            // recover from exception
                            System.Diagnostics.Debug.Print($"DataArrived InvalidCastException");
                        }

                        if (_prn_SerialNumber != "")
                        {
                            _orionClient.GetStatus(_prn_SerialNumber);
                            _prn_Attributes = _orionClient._attributes;
                        }
                    }
                    else
                    {
                        _retryperiod = 60000;
                        _onBoardClient.Connect(_prn_ip, int.Parse(_prn_port));
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print(ex.ToString());
                }

                Thread.Sleep(_retryperiod);
            }
            while (true);
        }

        //internal static async Task Main(frmMain frmMain)
        public async void Main(frmMain frmMain)
        {
            //var mclsMain = new clsMain();
            System.Diagnostics.Debug.Print($"Started.");

            //lettura da INI dei parametri
            //_prn_ip = INI.Read("FIWARE_PRN", "IP_PRN", _prn_ip, fileName_PAR);
            //_prn_port = INI.Read("FIWARE_PRN", "Port_PRN", _prn_port, fileName_PAR);
            //_orionClientConfig_BaseUrl = INI.Read("FIWARE_PRN", "OrionClientConfig_BaseUrl", _orionClientConfig_BaseUrl, fileName_PAR);
            //_orionClientConfig_Token = INI.Read("FIWARE_PRN", "OrionClientConfig_Token", _orionClientConfig_Token, fileName_PAR);

            _prn_ip = Properties.Settings.Default.prn_ip;
            _prn_port = Properties.Settings.Default.prn_port;
            _orionClientConfig_BaseUrl = Properties.Settings.Default.fiware_address;
            _orionClientConfig_Token = Properties.Settings.Default.fiware_hash;
            octopiHash = Properties.Settings.Default.octopi_hash;


            if (octoMode)
            {
                //octoprint Rest API
                OctoPiServer = new OctoPrintServer(_prn_ip, octopiHash);
                OctoPiServer.ConnectWebSocket();

                if (OctoPiServer.IsOnline)
                {
                    //Istanzia Orion Context Broker
                    _orionClient = new Fiware_Orion_Client(_orionClientConfig_BaseUrl, _orionClientConfig_Token);

                    //Timer Lettura/scrittura

                    //Printers
                    ThreadManagePrinters = new Thread(MainTimerOctoP);
                    ThreadManagePrinters.Priority = ThreadPriority.Normal;
                    ThreadManagePrinters.IsBackground = true;
                    ThreadManagePrinters.Start();
                }
                else
                {
                    //errore connessione stampante -> chiudere app?
                    DialogResult result = MessageBox.Show("Can't connect with printer at " + _prn_ip, "CONNECTION ERROR", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                }
            }
            else
            {
                //connessione stampante            
                _onBoardClient = new SimpleTcpClient().Connect(_prn_ip, int.Parse(_prn_port));
                //_onBoardClient.DataReceived += new System.EventHandler(DataArrived);
                //_onBoardClient.LineCommand_End = "|";

                if (_onBoardClient.TcpClient.Connected)
                {
                    SimpleTCP.Message msg = _onBoardClient.WriteLineAndGetReply("~:ATI SW=99,TYPECLI=CONTROLBASE,NAMECLI=HOUSTON\r\n",new TimeSpan(0,0,20));
                    //deserializzazione
                    string data = msg.MessageString;
                    data = data.Substring(data.IndexOf(" ")).Trim();

                    //parsing data
                    string[] subs = data.Split(' ', (char)2);
                    switch (subs[0])
                    {
                        case "~:STATUS_EXPOSE":
                            //deserializzazione
                            _3DPRN_Fiware.STATUS_EXPOSE printerstatus = new _3DPRN_Fiware.STATUS_EXPOSE();
                            var serializer = new XmlSerializer(typeof(_3DPRN_Fiware.STATUS_EXPOSE), new XmlRootAttribute("STATUS_EXPOSE"));
                            data = data.Substring(data.IndexOf(" ")).Trim();

                            using (var reader = new System.IO.StringReader(data))
                            {
                                try
                                {
                                    printerstatus = (_3DPRN_Fiware.STATUS_EXPOSE)serializer.Deserialize(reader);
                                    _prn_SerialNumber = printerstatus.Printer.PRN_SerialNUmber??"";

                                    //_orionClient.UpdateStatus(printerstatus);
                                    var threadOrion = new Thread(() =>
                                                _orionClient.UpdateStatus(printerstatus)
                                           );
                                    threadOrion.IsBackground = true;
                                    threadOrion.Start();


                                    System.Diagnostics.Debug.Print($"DataArrived End Call");

                                }
                                catch (InvalidCastException exc)
                                {
                                    // recover from exception
                                    System.Diagnostics.Debug.Print($"DataArrived InvalidCastException");
                                }

                            }
                            break;

                        default:
                            System.Diagnostics.Debug.Print($"Response {subs[0]}.");
                            break;
                    }

                    //Istanzia Orion Context Broker
                    _orionClient = new Fiware_Orion_Client(_orionClientConfig_BaseUrl, _orionClientConfig_Token);


                    //Timer Lettura/scrittura

                    //Printers
                    ThreadManagePrinters = new Thread(MainTimer);
                    ThreadManagePrinters.Priority = ThreadPriority.Normal;
                    ThreadManagePrinters.IsBackground = true;
                    ThreadManagePrinters.Start();


                    //Timer generalTimer = new Timer((s) =>
                    //{
                    //    if (Monitor.TryEnter(_syncTimer))
                    //    {
                    //        try
                    //        {
                    //            _onBoardClient.Write("~:STATUS_EXPOSE_GET" + VBComp.vbCrLf);  //richiesta stato
                    //            System.Diagnostics.Debug.Print("STATUS_EXPOSE_GET");
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            System.Diagnostics.Debug.Print(ex.ToString());
                    //        }
                    //        finally
                    //        {
                    //            Monitor.Exit(_syncTimer);
                    //        }
                    //    }
                    //    else
                    //        return;
                    //}, null, 0, this._retryperiod);


                }
                else
                {
                    //errore connessione stampante -> chiudere app?
                    DialogResult result = MessageBox.Show("Can't connect with printer at " + _prn_ip, "CONNECTION ERROR", MessageBoxButtons.OK);
                    System.Environment.Exit(0);
                }
            }
        }
        //public void DataArrived(object sender, EventArgs e)
        //{
        //    string data = ((TcpClient)sender).DataRead;


        //    System.Diagnostics.Debug.Print("Evento Arrivato da Server:{0}", data);

        //    //parsing data
        //    string[] subs = data.Split(' ', (char)2);
        //    switch (subs[0])
        //    {
        //        case "~:STATUS_EXPOSE":
        //            //deserializzazione
        //            _3DPRN_Fiware.STATUS_EXPOSE printerstatus = new _3DPRN_Fiware.STATUS_EXPOSE();
        //            var serializer = new XmlSerializer(typeof(_3DPRN_Fiware.STATUS_EXPOSE), new XmlRootAttribute("STATUS_EXPOSE"));
        //            data = data.Substring(data.IndexOf(" ")).Trim();

        //            using (var reader = new System.IO.StringReader(data))
        //            {
        //                try
        //                {
        //                    printerstatus = (_3DPRN_Fiware.STATUS_EXPOSE)serializer.Deserialize(reader);
        //                    _prn_SerialNumber = printerstatus.Printer.PRN_SerialNUmber;

        //                    //_orionClient.UpdateStatus(printerstatus);
        //                    var threadOrion = new Thread(() =>
        //                                _orionClient.UpdateStatus(printerstatus)
        //                           );
        //                    threadOrion.IsBackground = true;
        //                    threadOrion.Start();


        //                    System.Diagnostics.Debug.Print($"DataArrived End Call");

        //                }
        //                catch (InvalidCastException exc)
        //                {
        //                    // recover from exception
        //                    System.Diagnostics.Debug.Print($"DataArrived InvalidCastException");
        //                }

        //            }
        //            break;

        //        default:
        //            System.Diagnostics.Debug.Print($"Response {subs[0]}.");
        //            break;
        //    }
        //}
    }
}
