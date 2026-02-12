using Blackjack.Models;

namespace Blackjack
{
    public partial class MainForm : Form
    {        
        private Game game;

        // UI Controls
        private System.Media.SoundPlayer? backgroundMusic;
        private Label lblBalance = null!;
        private Label lblBet = null!;
        private Label lblPlayerCards = null!;
        private Label lblPlayerTotal = null!;
        private Label lblDealerCards = null!;
        private Label lblDealerTotal = null!;
        private Button btnDeal = null!;
        private Button btnHit = null!;
        private Button btnStand = null!;
        private TextBox txtBetAmount = null!;
        private Button btnPlaceBet = null!;
        private PictureBox picPokerChip = null!;
        private PictureBox picDeck = null!;

        public MainForm()
        {
            game = new Game();
            
            InitializeComponent();

            // Set up form properties
            this.Text = "Blackjack Game";
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Set background image
            this.BackgroundImage = Image.FromFile("background.png");
            this.BackgroundImageLayout = ImageLayout.None;

            backgroundMusic = new System.Media.SoundPlayer("Blackjack_Game_music_1.wav");
            backgroundMusic.PlayLooping();
        }

        private void InitializeComponent()
        {
            lblBalance = new Label();
            lblBalance.Text = $"Balance: ${game.GetBalance()}";
            lblBalance.Location = new Point(20, 20);
            lblBalance.Size = new Size(250, 30);
            lblBalance.Font = new Font("Terminal", 14, FontStyle.Bold);
            this.Controls.Add(lblBalance);

            lblBet = new Label();
            lblBet.Text = $"Current Bet: ${game.GetCurrentBet()}";
            lblBet.Location = new Point(20, 60);
            lblBet.Size = new Size(250, 30);
            lblBet.Font = new Font("Terminal", 14, FontStyle.Bold);
            this.Controls.Add(lblBet);

            lblDealerCards = new Label();
            lblDealerCards.Text = $"Dealer's cards: ";
            lblDealerCards.Location = new Point(400, 100);
            lblDealerCards.Size = new Size(750, 40);
            lblDealerCards.Font = new Font("Terminal", 14, FontStyle.Bold);
            this.Controls.Add(lblDealerCards);

            lblDealerTotal = new Label();
            lblDealerTotal.Text = $"Total: {game.GetDealerTotal()}";
            lblDealerTotal.Location = new Point(400, 150);
            lblDealerTotal.Size = new Size(250, 30);
            lblDealerTotal.Font = new Font("Terminal", 14,FontStyle.Bold);
            this.Controls.Add(lblDealerTotal);

            lblPlayerCards = new Label();
            lblPlayerCards.Text = $"Player's cards: ";
            lblPlayerCards.Location = new Point(400, 650);
            lblPlayerCards.Size = new Size(750, 40);
            lblPlayerCards.Font = new Font("Terminal", 14, FontStyle.Bold);
            this.Controls.Add(lblPlayerCards);

            lblPlayerTotal = new Label();
            lblPlayerTotal.Text = $"Total: {game.GetPlayerTotal()}";
            lblPlayerTotal.Location = new Point(400, 600);
            lblPlayerTotal.Size = new Size(250, 30);
            lblPlayerTotal.Font = new Font("Terminal", 14, FontStyle.Bold);
            this.Controls.Add(lblPlayerTotal);

            btnDeal = new Button();
            btnDeal.Text = "Deal";
            btnDeal.Location = new Point(400, 500);
            btnDeal.Size = new Size(120, 50);
            btnDeal.Font = new Font("Terminal", 12, FontStyle.Bold);
            btnDeal.Enabled = false;
            btnDeal.Click += BtnDeal_Click;
            this.Controls.Add(btnDeal);

            btnHit = new Button();
            btnHit.Text = "Hit";
            btnHit.Location = new Point(540, 500);
            btnHit.Size = new Size(120, 50);
            btnHit.Font = new Font("Terminal", 12, FontStyle.Bold);
            btnHit.Enabled = false;
            btnHit.Click += BtnHit_Click;
            this.Controls.Add(btnHit);

            btnStand = new Button();
            btnStand.Text = "Stand";
            btnStand.Location = new Point(680, 500);
            btnStand.Size = new Size(120, 50);
            btnStand.Font = new Font("Terminal", 12, FontStyle.Bold);
            btnStand.Enabled = false;
            btnStand.Click += BtnStand_Click;
            this.Controls.Add(btnStand);

            txtBetAmount = new TextBox();
            txtBetAmount.Text = "100";
            txtBetAmount.Location = new Point(20, 500);
            txtBetAmount.Size = new Size(150, 30);
            txtBetAmount.Font = new Font("Terminal", 12, FontStyle.Bold);
            this.Controls.Add(txtBetAmount);

            btnPlaceBet = new Button();
            btnPlaceBet.Text = "Place Bet";
            btnPlaceBet.Location = new Point(20, 550);
            btnPlaceBet.Size = new Size(150, 40);
            btnPlaceBet.Font = new Font("Terminal", 12, FontStyle.Bold);
            btnPlaceBet.Click += BtnPlaceBet_Click;
            this.Controls.Add(btnPlaceBet);

            picPokerChip = new PictureBox();
            picPokerChip.Size = new Size(50, 50);
            picPokerChip.Location = new Point(574, 390);
            picPokerChip.BackColor = Color.Transparent;
            picPokerChip.SizeMode = PictureBoxSizeMode.StretchImage;
            picPokerChip.Visible = false;
            this.Controls.Add(picPokerChip);

            picDeck = new PictureBox();
            picDeck.Image = Image.FromFile("deck_of_cards_sprite.png");
            picDeck.Size = new Size(300, 300);
            picDeck.Location = new Point(850, 150);
            picDeck.BackColor = Color.Transparent;
            picDeck.SizeMode = PictureBoxSizeMode.StretchImage;
            picDeck.Visible = true;
            this.Controls.Add(picDeck);
        }

        private void BtnPlaceBet_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtBetAmount.Text, out int betAmount))
            {
                MessageBox.Show("Please enter a valid number");
                return;
            }

            if (game.PlaceBet(betAmount))
            {
                lblBet.Text = $"Current Bet: ${game.GetCurrentBet()}";
                lblBalance.Text = $"Balance: ${game.GetBalance()}";
                picPokerChip.Image = Image.FromFile("poker chip sprites.png");
                picPokerChip.Visible = true;
                btnDeal.Enabled = true;
                btnPlaceBet.Enabled = false;
            }

            else
            {
                MessageBox.Show("Invalid bet amount or insufficient balance");
            }
        }

        private void BtnDeal_Click(object? sender, EventArgs e)
        {
            game.DealInitialCards();
            UpdateDisplay();

            if (game.GetDealerUpCard().CardRank == Rank.Ace)
            {
                var insuranceResult = MessageBox.Show($"Dealer shows an Ace! Buy insurance for ${game.GetCurrentBet() / 2} dollars?", "Insurance", MessageBoxButtons.YesNo);

                if (insuranceResult == DialogResult.Yes)
                {
                    if (game.PlaceInsurance())
                    {
                        UpdateDisplay();

                        if (game.DealerHasBlackjack())
                        {
                            MessageBox.Show($"Dealer has Blackjack! Insurance pays ${game.GetCurrentBet} dollars");
                            game.ResolveInsurance();
                            EndRound();
                            return;
                        }

                        else
                        {
                            MessageBox.Show("Dealer does not have Blackjack. Insurance bet lost.");
                            game.ResolveInsurance();
                            UpdateDisplay();
                        } 
                    }

                    else
                    {
                        MessageBox.Show("Insufficient balance for insurance");
                    }
                }
            }

            if (game.GetPlayerTotal() == 21)
            {
                MessageBox.Show("Blackjack! Dealer's turn...");
                game.DealerTurn();
                EndRound();
            }

            else
            {
                btnHit.Enabled = true;
                btnStand.Enabled = true;
                btnDeal.Enabled = false;
            }
        }

        public void BtnHit_Click(object? sender, EventArgs e)
        {
            game.PlayerHit();
            UpdateDisplay();

            if (game.IsPlayerBusted())
            {
                MessageBox.Show("Busted! You lose.");
                game.DetermineWinner();
                EndRound();
            }

            else if (game.GetPlayerTotal() == 21)
            {
                MessageBox.Show("Blackjack! Dealers turn...");
                game.DealerTurn();
                EndRound();
            }
        }

        public void BtnStand_Click(object? sender, EventArgs e)
        {
            game.DealerTurn();
            EndRound();
        }

        private void UpdateDisplay()
        {
            lblPlayerCards.Text = $"Player: {game.GetPlayerHand().GetHandDisplay()}";
            AdjustLabelFont(lblPlayerCards, 750);
            
            lblPlayerTotal.Text = $"Total: {game.GetPlayerTotal()}";
            lblDealerCards.Text = $"Dealer: {game.GetDealerUpCard()}";
            AdjustLabelFont(lblDealerCards, 750);
            
            lblDealerTotal.Text = "Total: ?";
            lblBalance.Text = $"Balance: ${game.GetBalance()}";
            lblBet.Text = $"Current Bet: ${game.GetCurrentBet()}";
        }

        private void EndRound()
        {
            lblDealerCards.Text = $"Dealer: {game.GetDealerHand().GetHandDisplay()}";
            AdjustLabelFont(lblDealerCards, 750);
            
            lblDealerTotal.Text = $"Total: {game.GetDealerTotal()}";

            string result = game.DetermineWinner();
            MessageBox.Show(result);

            game.StartNewRound();
            
            lblPlayerCards.Text = "Player: ";
            lblPlayerCards.Font = new Font("Terminal", 14, FontStyle.Bold);
            lblPlayerTotal.Text = "Total: 0";
            lblDealerCards.Text = "Dealer: ";
            lblDealerCards.Font = new Font("Terminal", 14, FontStyle.Bold);
            lblDealerTotal.Text = "Total: 0";
            lblBalance.Text = $"Balance: ${game.GetBalance()}";
            lblBet.Text = "Current Bet: $0";
            picPokerChip.Visible = false;

            btnHit.Enabled = false;
            btnStand.Enabled = false;
            
            if (game.GetBalance() <= 0)
            {
                MessageBox.Show("Game Over! You're out of money.", "Game Over");
                btnPlaceBet.Enabled = false;
                txtBetAmount.Enabled = false;
            }
            else
            {
                btnPlaceBet.Enabled = true;
            }
        }

        private void AdjustLabelFont(Label label, int maxWidth)
        {
            int fontSize = 14;
            Font testFont = new Font("Terminal", fontSize, FontStyle.Bold);
            
            // Measure the text width
            Size textSize = TextRenderer.MeasureText(label.Text, testFont);
            
            // Reduce font size until text fits
            while (textSize.Width > maxWidth && fontSize > 6)
            {
                fontSize--;
                testFont = new Font("Terminal", fontSize, FontStyle.Bold);
                textSize = TextRenderer.MeasureText(label.Text, testFont);
            }
            
            label.Font = testFont;
        }
    }
   
}