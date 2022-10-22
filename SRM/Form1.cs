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

        public enum OutputType { CODE, LINK, ERROR, INFO}

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
                        Regex regex = new Regex(RegexList.linkRegex, RegexOptions.IgnoreCase);
                        getCode(message.HtmlBody, regex);
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

        public void getCode(string html, Regex regex)
        {
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
            File.WriteAllText("temp.txt", html);
            // codes
            if (html.Contains("Похоже, вы пытаетесь войти в аккаунт с нового устройства. Для входа вам понадобится код Steam Guard"))
            {
                setOutput(File.ReadLines("temp.txt").Skip(226).First().Replace("	", String.Empty).Replace("</td>", String.Empty), false, OutputType.LINK);
                File.Delete("temp.txt");
                return;
            }
            if (html.Contains("Код для смены пароля вашего аккаунта Steam"))
            {
                setOutput(File.ReadLines("temp.txt").Skip(237).First().Replace("	", String.Empty).Replace("</td>", String.Empty), false, OutputType.LINK);
                File.Delete("temp.txt");
                return;
            }
            File.Delete("temp.txt");
            setOutput("Code or link don't found! Send again or open issue on my github", false, OutputType.ERROR);
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