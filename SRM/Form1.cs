using System.Text.RegularExpressions;

namespace SRM
{
    public partial class Form1 : Form
    {
        // params
        public string imapHost;
        public int imapPort;
        public bool imapSSL;
        output fOutput;
        public Form1()
        {
            InitializeComponent();
            fOutput = new output();
            fOutput.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread checkThread = new Thread(new ThreadStart(check));
            checkThread.IsBackground = true;
            checkThread.Start();
        }

        public enum OutputType { CODE, LINK, ERROR, INFO, DEBUG}

        void check()
        {
            while (true)
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        if (server.Text != "Other")
                        {
                            host.Enabled = false;
                            ssl.Enabled = false;
                            port.Enabled = false;
                            return;
                        }
                        host.Enabled = true;
                        ssl.Enabled = true;
                        port.Enabled = true;
                    }));
                    Thread.Sleep(50);
                }
                catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ssl.Text == "true")
                    imapSSL = true;
                if (ssl.Text == "false")
                    imapSSL = false;
                if (server.Text == "rambler.ru")
                {
                    imapHost = "imap.rambler.ru";
                    imapPort = 993;
                    imapSSL = true;
                }
                if (server.Text == "gmail.com")
                {
                    imapHost = "imap.gmail.com";
                    imapPort = 993;
                    imapSSL = true;
                }
                if (server.Text == "Other")
                {
                    imapHost = host.Text;
                    imapPort = Convert.ToInt32(port.Text);
                }
                setOutput("Trying to parse code or link...", false, OutputType.INFO);
                using (var client = new MailKit.Net.Imap.ImapClient())
                {
                    client.Connect(imapHost, imapPort, imapSSL); 
                    client.Authenticate(login.Text, pass.Text); 
                    client.Inbox.Open(MailKit.FolderAccess.ReadWrite); 
                    for (int i = client.Inbox.Count - 1; i < client.Inbox.Count; i++) 
                    {
                        var message = client.Inbox.GetMessage(i);
                        List<string> services = new();
                        foreach (var service in serviceBox.Items)
                            services.Add(service.ToString());
                        Regex linkRegex = new Regex(RegexList.linkRegex, RegexOptions.IgnoreCase);
                        if (services.Contains(serviceBox.Text))
                        {
                            if (serviceBox.Text == "Steam")
                                getSteamCode(message.HtmlBody, linkRegex);
                            if (serviceBox.Text == "Epicgames")
                                getEpicGamesCode(message.HtmlBody.ToString());
                            return;
                        }
                        setOutput("Check the correctness of the settings you have set. Most likely you forgot to choose a service or an IMAP server", true, OutputType.ERROR);
                    }
                }
            }
            catch (MailKit.Net.Imap.ImapProtocolException)
            {
                setOutput("Failed to connect to the IMAP server. Make sure it is enabled in the settings or check the validity of the data from the mail", true, OutputType.ERROR);
            }
            catch (Exception ex)
            {
                setOutput(ex.ToString(), false, OutputType.ERROR);
            }

        }

        public void getEpicGamesCode(string html)
        {
            File.WriteAllText("epic.txt", Regex.Replace(html.Replace(" ", string.Empty).Replace("\t", string.Empty), "<.*?>", String.Empty)); // writing html to file
            string[] lines = File.ReadAllLines("epic.txt");
            int i = -1;
            if (html.Contains("<!-- Start code -->")) // epicgames code
            {
                foreach (var line in lines)
                {
                    i++;
                    if (int.TryParse(line, out int num))
                        setOutput(lines[i], false, OutputType.CODE);
                    else
                        continue;
                }
                File.Delete("epic.txt");
                return;
            }
            if (html.Contains("https://accounts.epicgames.com/")) // epicgames link
            {
                foreach (var line in lines)
                {
                    i++;
                    if (line.Contains("https://accounts.epicgames.com/"))
                        setOutput(lines[i], false, OutputType.LINK);
                    else
                        continue;
                }
                File.Delete("epic.txt");
                return;
            }
            setOutput("Code don't found. Try send mail again or open issue on my github", false, OutputType.ERROR);
        }

        public void getSteamCode(string html, Regex regex)
        {
            File.WriteAllText("temp.txt", Regex.Replace(html.Replace(" ", string.Empty).Replace("\t", string.Empty), "<.*?>", String.Empty));
            string[] lines = File.ReadAllLines("temp.txt");
            Match match;
            // links
            for (match = regex.Match(html); match.Success; match = match.NextMatch())
            {
                foreach (Group group in match.Groups)
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(group.ToString());
                    var anchor = doc.DocumentNode.SelectSingleNode("//a");
                    if (anchor != null)
                    {
                        string link = anchor.Attributes["href"].Value;
                        setOutput(link.Replace("&amp;", "&"), false, OutputType.LINK);
                        return;
                    }
                }
            }
            if (html.Contains("<!-- Auth Code -->"))
            {
                var result = lines.Where(x => x.Length == 5);
                foreach (var line in result)
                    setOutput(line, false, OutputType.CODE);

                File.Delete("temp.txt");
                return;
            }
            setOutput("Code don't found. Try send mail again or open issue on my github", false, OutputType.ERROR);
        }
        public void setOutput(string text, bool timestamp, OutputType type)
        {
            string msg;
            if (timestamp)
                msg = $"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} / {type}] {text}";
            else
                msg = $"[{type}] {text}";
            fOutput.outputBox.Text = msg;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fOutput = new output();
            fOutput.Show();
        }
    }
}