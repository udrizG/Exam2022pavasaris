using System;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                driver = new FirefoxDriver(options);
                driver.Navigate().GoToUrl("https://www.ebay.com");

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda startējot Firefox pārlūkprogrammu: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = textBox1.Text;
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var searchBox = driver.FindElement(By.Id("gh-ac"));
                    searchBox.Clear();
                    searchBox.SendKeys(searchTerm);

                    var searchButton = driver.FindElement(By.Id("gh-btn"));
                    searchButton.Click();

                    wait.Until(d => d.FindElement(By.CssSelector(".srp-results")) != null);

                    string searchUrl = driver.Url;
                    textBox2.Text = searchUrl;

                    AddToSearchHistory(searchUrl);

                    MessageBox.Show("Meklēšanas rezultāti tika atvērti Firefox pārlūkprogrammā.", "Meklēšanas rezultāti");
                }
                else
                {
                    MessageBox.Show("Lūdzu aizpildiet meklēšanas lauku", "Kļūda");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda meklēšanas procesā: " + ex.Message);
            }
        }

        private void AddToSearchHistory(string searchUrl)
        {
            string history = richTextBox1.Text;
            richTextBox1.Text = history + searchUrl + Environment.NewLine;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.ebay.com");

                textBox1.Clear();
                textBox2.Clear();
                richTextBox1.Clear();

                MessageBox.Show("Atgriešanās eBay sākumlapā un meklēšanas vēstures dzēšana noritēja veiksmīgi", "Atpakaļ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda atpakaļ soļa procesā: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    MessageBox.Show("Pārlūkprogramma Firefox tika aizvērta.", "Pārlūkprogramma aizvērta");
                }
                else
                {
                    MessageBox.Show("Firefox pārlūkprogramma ir jau atvērta vai jau ir aizvērta.", "Informācija");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda aizverot pārlūkprogrammu Firefox: " + ex.Message);
            }
        }
    }
}